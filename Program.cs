using System.Text;
using contactapi.Services.Context;
using contactapi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<ContactServices>();
builder.Services.AddScoped<UserServices>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
    policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});
var secretKey = builder.Configuration["JWT:Key"] ?? "superSecretKey@345superSecretKey@345";
var signingCredentials = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
// Add authentication services to the app
builder.Services.AddAuthentication(options =>
{
    // Set the default authentication scheme/ behaviour to JWT Bearer
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    // Set the default challenge scheme (what to use when authentication fails)
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Configure JWT Bearer authentication options
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // Check if the token's issuer is valid
        ValidateAudience = true, // Check if the token's audience is valid
        ValidateLifetime = true, // Ensure the token hasn't expired
        ValidateIssuerSigningKey = true, // Check the token's signature is valid

        // The expected issuer (the API that created the token)
        ValidIssuer = "https://contactmanagerdor-a2bfb6cehkdxg2bp.westus3-01.azurewebsites.net/",

        // The expected audience (who the token is intended for)
        ValidAudience = "https://contactmanagerdor-a2bfb6cehkdxg2bp.westus3-01.azurewebsites.net/",

        // The key used to sign the token (must match the one used to create it)
        IssuerSigningKey = signingCredentials
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
