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
using Application.Contract.Persistence.Interface;
//using System.Web.Http;

namespace API.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class AuthenticationController : ControllerBase
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly UserManager<User> _usermanager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private  IConfiguration _configuration;
        private IAuthorRepository _authorRepository;
        private IMapper _mapper;
        

        public AuthenticationController(UserManager<User> usermanager, 
                                        RoleManager<IdentityRole> roleManager, 
                                        IConfiguration configuration,
                                       IAuthorRepository authorRepository,
                                        IMapper mapper)
        {
            _usermanager = usermanager;
            _roleManager = roleManager;
            _configuration = configuration;
            _authorRepository= authorRepository;
            _mapper = mapper;
            
        }


        #region Login
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDTO model)
        {
            try
            {

            
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    Status = "Error",
                    Message = "Invalid model",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var user = await _usermanager.FindByEmailAsync(model.Email);
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
            catch (Exception)
            {

                throw;
            }
        }

#endregion

        #region Registration
        [HttpPost]
        [Route("Register")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public async Task<ActionResult<Response>> Register([FromBody] UserRegistrationDTO model, CancellationToken token)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            try
            {

            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    Status="Error",
                    Message="Invalid model",
                    StatusCode=StatusCodes.Status400BadRequest
                });
            }
            var userExists = await _usermanager.FindByEmailAsync(model.Email);
            if (userExists !=null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", 
                                                                                           Message = $"User with Email: {model.Email} already exists!", 
                                                                                           StatusCode = StatusCodes.Status500InternalServerError });

            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
                EmailConfirmed = false,
                //LockoutEnabled= false,
                TwoFactorEnabled= false,
                PhoneNumberConfirmed= false,  
            };
            var result = await _usermanager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", 
                                                                                           Message = "User creation failed! Please check user details and try again." });


            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Author))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Author));

            if(await _roleManager.RoleExistsAsync(UserRoles.Author) && model.Roles.ToLower() == UserRoles.Author.ToLower())
            {
                user.RoleName = UserRoles.Author;
                await _usermanager.AddToRoleAsync(user, UserRoles.Author);
                await _usermanager.UpdateAsync(user);
                var authorCommand = new Author
                {
                    AuthorEmail = model.Email,
                    AuthorName = model.FirstName + " " + model.LastName,
                    AuthorBio = string.Empty
                };
                await _authorRepository.AddAsync(authorCommand);
 
            }
            if(await _roleManager.RoleExistsAsync(UserRoles.User) && model.Roles.ToLower() == UserRoles.User.ToLower())
            {
                user.RoleName = UserRoles.User;
                await _usermanager.AddToRoleAsync(user, UserRoles.User);
                await _usermanager.UpdateAsync(user);

            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin) && model.Roles.ToLower() == UserRoles.Admin.ToLower())
            {
                user.RoleName = UserRoles.Admin;
                await _usermanager.AddToRoleAsync(user, UserRoles.Admin);
                await _usermanager.UpdateAsync(user);
               
            }

            return Ok(new Response { Status = "Success", 
                                    Message = "User created successfully!", 
                                    StatusCode = StatusCodes.Status200OK});

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region not needed
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
        #endregion

       /* [HttpPost]
        [Route("api/login")]
        public IHttpActionResult Login(LoginRequest request)
        {
            var user = _userRepository.GetUser(request.Username);

            if (user == null || !ValidatePassword(user.Password, request.Password))
            {
                return Unauthorized();
            }

            // Generate a JWT token for the user
            var token = GenerateJwtToken(user);

            // Return the token to the client
            return Ok(new LoginResponse { Token = token });
        }

        [HttpGet]
        [Route("api/resources/{id}")]
        [Authorize]
        public IHttpActionResult GetResource(int id)
        {
            // Retrieve the authenticated user from the token
            var user = (User)HttpContext.Current.Items["User"];

            var resource = _repository.GetResource(id);

            if (resource == null)
            {
                return NotFound();
            }

            // Check if the user has permission to access the resource
            if (!HasPermission(user, resource))
            {
                return Unauthorized();
            }

            // Return the resource
            return Ok(resource);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("secret-key");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim("UserId", user.Id.ToString())
        }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private bool ValidatePassword(string passwordHash, string password)
        {
            // Validate the password hash against the provided password
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }*/

    }

}

