using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoccerHub.Api.Data;
using SoccerHub.Api.DTOs;
using SoccerHub.Api.Middlewares;
using SoccerHub.Api.Services;
using SoccerHub.Api.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

//var jwt = builder.Configuration.GetSection("Jwt");

//var key = Encoding.UTF8.GetBytes(jwt["Key"]!);
var jwtKey = builder.Configuration["Jwt:Key"]!;


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{

    options.TokenValidationParameters =
        new TokenValidationParameters
        {

            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer =
                builder.Configuration["Jwt:Issuer"],

            ValidAudience =
                builder.Configuration["Jwt:Audience"],

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtKey)
                )

        };
    

});

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<MatchService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<StandingService>();
builder.Services.AddScoped<DashboardService>();

builder.Services.AddControllers()

.ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState

            .Values

            .SelectMany(v => v.Errors)

            .Select(e => e.ErrorMessage)

            .ToList();

        return new BadRequestObjectResult(
            new ApiResponse<object>
            {
                Success = false,
                Message = "Validation failed",
                Errors = errors
            });
    };
});
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
//temporal
//builder.Services.AddSwaggerGen();



// ======================
// Swagger
// ======================

builder.Services.AddSwaggerGen(options =>
{

    options.AddSecurityDefinition(

        "Bearer",

        new Microsoft.OpenApi.OpenApiSecurityScheme
        {

            Name = "Authorization",

            Type = Microsoft.OpenApi.SecuritySchemeType.Http,

            Scheme = "Bearer",

            BearerFormat = "JWT",

            In = Microsoft.OpenApi.ParameterLocation.Header,

            Description =
                "Escribe solo el token"

        });

    options.AddSecurityRequirement(document => new Microsoft.OpenApi.OpenApiSecurityRequirement
    {
        [new Microsoft.OpenApi.OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
    });

});




var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();



app.UseHttpsRedirection();

//agregado
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
