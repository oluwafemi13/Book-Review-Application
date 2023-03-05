using Application.DTO;
using Application.Model;
using Domain.Entities;
using Application.Static;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Application.Features.Commands.author.CreateAuthor;

namespace API.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class AuthenticationController: ControllerBase
    {
        private readonly UserManager<User> _usermanager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private CreateAuthorCommandHandler _authorHandler;
        

        public AuthenticationController(UserManager<User> usermanager, 
                                        RoleManager<IdentityRole> roleManager, 
                                        IConfiguration configuration,
                                        CreateAuthorCommandHandler authorHandler)
        {
            _usermanager = usermanager;
            _roleManager = roleManager;
            _configuration = configuration;
            _authorHandler = authorHandler;
            
        }


        #region Login
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDTO model)
        {
            //var check = _mapper.Map<User>(model);
            //var UserName = model.Email;
            var user = await _usermanager.FindByNameAsync(model.Email);
            if (user != null && await _usermanager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _usermanager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo.AddHours(5)
                });
            }
            return Unauthorized();
        }

#endregion

        #region Registration
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register([FromBody] UserRegistrationDTO model)
        {
         
            var userExists = await _usermanager.FindByEmailAsync(model.Email);
           
            if (userExists !=null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            

            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
                EmailConfirmed = false,
                LockoutEnabled= false,
                TwoFactorEnabled= false,
                PhoneNumberConfirmed= false,  

            };
            var result = await _usermanager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });


            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Author))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Author));

            if(await _roleManager.RoleExistsAsync(UserRoles.Author) && model.Roles == UserRoles.Author)
            {
                
                await _usermanager.AddToRoleAsync(user, UserRoles.Author);
                
            }
            if(await _roleManager.RoleExistsAsync(UserRoles.User) && model.Roles == UserRoles.User)
            {
                await _usermanager.AddToRoleAsync(user, UserRoles.User);
                
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin) && model.Roles == UserRoles.Admin)
            {
                await _usermanager.AddToRoleAsync(user, UserRoles.Admin);
               
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!", StatusCode = (int) StatusCodes.Status200OK});
        }

        #endregion

        /* [HttpPost]
         [Route("Register-Admin")]
         public async Task<ActionResult<Response>> RegisterAdmin([FromBody] UserRegistrationDTO model)
         {
             var userExists = await _usermanager.FindByNameAsync(model.UserName);
             if (userExists != null)
                 return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

             var user = new User()
             {
                 FirstName = model.FirstName,
                 LastName = model.LastName,
                 Email = model.Email,
                 SecurityStamp = Guid.NewGuid().ToString(),
                 UserName = model.UserName,
                 PhoneNumber = model.PhoneNumber,

             };

             var result = await _usermanager.CreateAsync(user, model.Password);
             if (!result.Succeeded)
                 return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

             if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                 await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
             if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                 await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

             if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
             {
                 await _usermanager.AddToRoleAsync(user, UserRoles.Admin);
             }

             return Ok(new Response { Status = "Success", Message = "User created successfully!" });
         }*/
    }

}

