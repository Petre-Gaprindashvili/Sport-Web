using Microsoft.EntityFrameworkCore;
using Sport_Web.Data;
using Sport_Web.Abstraction;
using Sport_Web.Implementation;
using Microsoft.AspNetCore.Identity;
using Sport_Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAuthenticationService,AuthenticationService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
var connectionString = builder.Configuration.GetConnectionString("SportWebConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				  .AddCookie(options =>
				  {


					  options.LoginPath = "/Account/Login"; // Path to your login page
					  options.AccessDeniedPath = "/Account/AccessDenied";
					  // Path for denied access

				  });


builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("admin", policy => policy.RequireRole("Admin"));
	options.AddPolicy("user", policy => policy.RequireRole("User"));
});



builder.Services.AddControllers();


builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapControllers();


app.Run();
