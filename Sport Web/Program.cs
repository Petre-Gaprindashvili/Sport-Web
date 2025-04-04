//using Microsoft.EntityFrameworkCore;
//using Sport_Web.Data;
//using Sport_Web.Abstraction;
//using Sport_Web.Implementation;
//using Microsoft.AspNetCore.Identity;
//using Sport_Web.Models;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.OpenApi.Models;

//var builder = WebApplication.CreateBuilder(args);

//// ✅ Add services
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();

////builder.Services.AddSwaggerGen(options =>
////{
////	options.SwaggerDoc("v1", new OpenApiInfo { Title = "Sport Web API", Version = "v1" });
////});

//// ✅ Register dependencies
//builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
//builder.Services.AddScoped<IEmailService, EmailService>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();
//builder.Services.AddScoped<IImageUploadService, ImageUploadService>();
//builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

//// ✅ Database Connection
//var connectionString = builder.Configuration.GetConnectionString("SportWebConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//	options.UseSqlServer(connectionString));

//builder.Services.AddHttpContextAccessor();

//// ✅ Authentication & Authorization
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//	.AddCookie(options =>
//	{
//		options.LoginPath = "/Account/Login";
//		options.AccessDeniedPath = "/Account/AccessDenied";
//	});

//builder.Services.AddAuthorization();

//var app = builder.Build();

//// ✅ Enable Swagger
//app.UseSwagger();
//app.UseSwaggerUI();

//app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();
//app.MapControllers();

//app.Run();































































using Microsoft.EntityFrameworkCore;
using Sport_Web.Data;
using Sport_Web.Abstraction;
using Sport_Web.Implementation;
using Microsoft.AspNetCore.Identity;
using Sport_Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IteamsService, TeamServicecs>();
builder.Services.AddScoped<IMatchesService, MatchesService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddScoped<IImageUploadService, ImageUploadService>();
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




builder.Services.AddControllers();


builder.Services.AddAuthorization();


var app = builder.Build();


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
