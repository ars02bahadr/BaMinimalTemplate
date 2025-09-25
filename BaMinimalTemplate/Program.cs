// Program.cs - Final Version
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using BaMinimalTemplate.Data;
using BaMinimalTemplate.Endpoints;
using BaMinimalTemplate.Extensions;
using BaMinimalTemplate.Models;

var builder = WebApplication.CreateBuilder(args);

#if (UseLogging)
// Serilog Configuration
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
#endif

// Database Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer") ?? "DEFAULT_CONNECTION_STRING"));

// Identity Configuration
builder.Services
    .AddIdentityCore<User>(options =>
    {
        // örnek ayarlar
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddRoles<Microsoft.AspNetCore.Identity.IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()   
    .AddSignInManager()                                  
    .AddDefaultTokenProviders(); 

// JWT Configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"] ?? "your-very-long-secret-key-here-minimum-32-characters-for-security";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"] ?? "MinimalApiIssuer",
        ValidAudience = jwtSettings["Audience"] ?? "MinimalApiAudience",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddSingleton(provider => new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddMaps(typeof(Program).Assembly);
}).CreateMapper());

builder.Services.AddAuthorization(options =>
{
    // User silme yetkisi policy'si
    options.AddPolicy("CanDeleteUsers", policy =>
        policy.RequireClaim("permissions", "user:delete"));
    
    // User güncelleme yetkisi policy'si
    options.AddPolicy("CanUpdateUsers", policy =>
        policy.RequireClaim("permissions", "user:update"));
    
    // Admin yetkisi policy'si
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireClaim("permissions", "admin:full"));
});
builder.Services.AddApplicationServices();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Minimal API Template", 
        Version = "v1",
        Description = "A comprehensive .NET Minimal API template with JWT authentication, user management, and role-based permissions"
    });
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Sadece token'ı yazın, 'Bearer ' prefix'i otomatik eklenecek.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});




var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API Template v1");
    c.RoutePrefix = string.Empty; // kökten açılsın
    c.DocumentTitle = "BaMinimalTemplate API";
    c.DefaultModelsExpandDepth(-1); // Models section'ı gizle
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
    c.EnableDeepLinking();
    c.EnableFilter();
    c.ShowExtensions();
    c.EnableValidator();
});

app.UseHttpsRedirection();
app.UseCors("DefaultPolicy");
app.UseAuthentication();
app.UseAuthorization();


app.MapAuthEndpoints();
app.MapUserTypeEndpoints();
app.MapCategoryEndpoints();
app.MapUserEndpoints();
app.MapUserCategoryEndpoints();

app.Run();
