using Grpc.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Security.Claims;
using System.Text;
using WaterSewageConnection.Middleware;
using WaterSewageConnection.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

//Jwt configuration starts here
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

//JWT Authentication
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	//options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
	{
	options.SaveToken = true;
	options.RequireHttpsMetadata = false;
	options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
	{
		ValidateIssuer = false,
		ValidateAudience = false,
		//ValidAudience = "dotnetclient",
		//ValidIssuer = "WaterConnection",
		ClockSkew = TimeSpan.Zero,// It forces tokens to expire exactly at token expiration time instead of 5 minutes later
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
		RoleClaimType = ClaimTypes.Role // ensure role claim type for authorization
	};
});

builder.Services.AddAuthentication().AddCookie("jwtToken", options =>
{
	options.Cookie.Name = "jwtToken";
	options.LoginPath = "/Account/Login";
});

//builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	// Use UseExceptionHandler to handle unexpected errors and redirect users to an error page
	//app.UseExceptionHandler("/Account/Login");
	app.UseExceptionHandler("/Account/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


// built-in Middleware for Exception Handling and Status Code Pages
app.UseStatusCodePages(async context =>
{
	var response = context.HttpContext.Response;
	var statuscode = response.StatusCode;

	if (statuscode == StatusCodes.Status401Unauthorized)
		response.Redirect("/Account/Login");
	else if (statuscode == StatusCodes.Status404NotFound)
		response.Redirect("/Account/Error?errorCode=404");

});

// Middleware configuration (middleware stays above authentication and authorization)
app.UseMiddleware<TokenMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=JE}/{action=ProposedApplications}/{id?}");

app.Run();
