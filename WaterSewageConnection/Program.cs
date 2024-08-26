using Grpc.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Security.Claims;
using System.Text;
using WaterSewageConnection.Middleware;
using WaterSewageConnection.Services;


// creates an object of WebApplicationBuilder with preconfigured defaults using the CreateBuilder() method.
// The CreateBuilder() method setup the internal web server which is Kestrel. It also specifies the content root and read application settings file appsettings.json.
// Using this builder object, you can configure various things for your web application, such as dependency injection, middleware, and hosting environment
var builder = WebApplication.CreateBuilder(args);


// The builder object has the Services() method which can be used to add services to the dependency injection container.
// Add services to the container.
// The AddControllersWithViews() is an extension method that register types needed for MVC application (model, view, controller) to the dependency injection. It includes all the necessary services and configurations for MVC So that your application can use MVC architecture.
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


// The builder.Build() method returns the object of WebApplication using which you can configure the request pipeline using middleware and hosting environment that manages the execution of your web application.

// register services with the dependency injection container using builder object here
var app = builder.Build();



//using this WebApplication object app, you can configure an application based on the environment it runs on e.g. development, staging or production. The following adds the middleware that will catch the exceptions, logs them, and reset and execute the request path to '"/home/error" if the application runs on the development environment.

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	// Use UseExceptionHandler to handle unexpected errors and redirect users to an error page
	//app.UseExceptionHandler("/Account/Login");
	app.UseExceptionHandler("/Account/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// configures the static files, routing, and authorization middleware respectively.
app.UseHttpsRedirection();

//The UseStaticFiles() method configures the middleware that returns the static files from the wwwroot folder only.
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

// Middleware configuration (middleware stays above authentication and authorization) [Request Pipeline]
app.UseMiddleware<TokenMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();


// The MapControllerRoute() defines the default route pattern that specifies which controller, action, and optional route parameters should be used to handle incoming requests.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=JE}/{action=ProposedApplications}/{id?}");


// runs the application,start listening the incomming request. It turns a console application into an MVC application based on the provided configuration.

// start the application
app.Run();
