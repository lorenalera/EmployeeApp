using EmployeeApp.Data;
using EmployeeApp.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Register other services here, e.g.:
// builder.Services.AddScoped<IMyService, MyService>();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ItaskRepository, TaskRepository>();
builder.Services.AddScoped<IprojectRepository, ProjectRepository>();
builder.Services.AddEndpointsApiExplorer(); // Required for minimal APIs
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("Users", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Users API",
        Version = "v1"
    });
    options.SwaggerDoc("Projects", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Projects API",
        Version = "v1"
    });
    options.SwaggerDoc("Tasks", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Tasks API",
        Version = "v1"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,

    });
    options.SwaggerDoc("Login", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Login API",
        Version = "v1"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },

            Array.Empty<string>()
        }
    });


});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "MyIssuer",
        ValidAudience = "MyAudience",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecretKey"))
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();                      
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/Users/swagger.json", "Users API v1");
        c.SwaggerEndpoint("/swagger/Tasks/swagger.json", "Tasks API v1");
        c.SwaggerEndpoint("/swagger/Projects/swagger.json", "Projects API v1");
        c.SwaggerEndpoint("/swagger/Login/swagger.json", "Login API v1");
    }) ;
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization(); 

app.MapRazorPages();
app.MapControllers();
app.MapGet("/", () => "Application is running");
app.Run();
