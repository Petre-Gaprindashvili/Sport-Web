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

// Dependency Injection
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

// DB Context
var connectionString = builder.Configuration.GetConnectionString("SportWebConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));

// Accessor & Auth
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/Account/Login";
		options.AccessDeniedPath = "/Account/AccessDenied";
	
            // Set cookie properties
            options.Cookie.HttpOnly = true; // Ensure the cookie is only accessible by HTTP
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Use Secure cookies (HTTPS only)
            options.Cookie.SameSite = SameSiteMode.None; // Allow cross-site cookies
            options.ExpireTimeSpan = TimeSpan.FromDays(7); // Set cookie expiration time (7 days)
            options.SlidingExpiration = true; // Sliding expiration (refresh cookie on every request)

	});

// ✅ CORRECT CORS CONFIG
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowSpecificOrigin", builder =>
	{
		builder.WithOrigins("http://localhost:53977") // Frontend Angular app
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowCredentials(); // This is crucial for withCredentials: true
	});
});

builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization();

var app = builder.Build();

// ✅ USE the CORS POLICY here (do NOT use AllowAnyOrigin!)
app.UseCors("AllowSpecificOrigin");

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
