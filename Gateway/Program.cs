using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// ===== JWT CONFIG (AN TOÀN) =====
var jwtSection = builder.Configuration.GetSection("Jwt");
if (!jwtSection.Exists())
{
    throw new Exception("❌ Jwt config NOT FOUND");
}

var key = jwtSection["Key"];
if (string.IsNullOrEmpty(key))
{
    throw new Exception("❌ Jwt:Key is missing");
}

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key))
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ⚠️ THỨ TỰ BẮT BUỘC
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
