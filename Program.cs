using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using IoTMonitoring;
using System.Reflection;
using IoTMonitoring.Filters;
using IoTMonitoring.Repositories;
using IoTMonitoring.Services;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");

if (string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtKey))
{
    throw new ArgumentNullException("Credenciais inválidas", "JWT_ISSUER and JWT_KEY must be set in the environment variables.");
}

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")!,
    new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<TelnetService>();

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
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Community IoT Device (CIoTD)",
        Description = "A CIoTD é uma plataforma colaborativa para compartilhamento de acesso à dados de dispositivos IoT. Através dela, colaboradores podem cadastrar seus dispositivos, permitindo que qualquer pessoa possa coletar os dados desses dispositivos e utilizar em suas aplicações.",
        Version = "1.0.0"
    });

    c.DocumentFilter<AddTagsDescriptionFilter>();

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Inserir o token recebido no campo abaixo, para garantir acesso autorizado à plataforma.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
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
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
        builder.WithOrigins("http://localhost:3000") 
               .AllowAnyHeader()
               .AllowAnyMethod());
});

var app = builder.Build();
app.UseCors("AllowSpecificOrigin");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Community IoT Device (CIoTD) v1");
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
