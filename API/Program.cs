using API.ServiceExtension;
using Application.Extensions;
using Application.MappingConfiguration;
using AspNetCoreRateLimit;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Extension;
using Infrastructure.Persistence;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//configure caching
builder.Services.AddResponseCaching();
builder.Services.AddHttpCacheHeaders(
    expirationOpt=>
    {
        expirationOpt.MaxAge = 120;
        expirationOpt.CacheLocation = CacheLocation.Public;
    },
    validationOpt=>
    {
        validationOpt.MustRevalidate = true;
    }
    );
//configure rate Limiting
builder.Services.AddMemoryCache();
builder.Services.ConfigureRateLimiting();
builder.Services.AddHttpContextAccessor();
////////
///////
builder.Services.AddDatabaseService(builder.Configuration);
builder.Services.AddMediatRServices();
builder.Services.AddValidatorConfiguration();
builder.Services.AddApplicationMappingServices();
builder.Services.AddRepositoryService();
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Add caching in controllers
builder.Services.AddControllers(config =>
{
    config.CacheProfiles.Add("240SecondsCaching", new CacheProfile
    {
        Duration=240,
        
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookReview.API", Version = "v1" });
});



// For Identity  
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

// Adding Authentication  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer  
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});  
        
  

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/swagger.json", "BookReview.API");
    });

}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/swagger.json", "BookReview.API");
});
app.UseIpRateLimiting();
app.UseHttpsRedirection();
app.UseResponseCaching();
app.UseHttpCacheHeaders();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
