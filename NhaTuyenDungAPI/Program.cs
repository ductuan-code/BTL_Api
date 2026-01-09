using BLL.CongTy;
using BLL.ViecLam;
using BLL.DonUngTuyen;
using BLL.PhongVan;
using BLL.Offer;

using DAL.CongTy;
using DAL.ViecLam;
using DAL.DonUngTuyen;
using DAL.PhongVan;
using DAL.Offer;


var builder = WebApplication.CreateBuilder(args);

// =====================
// ADD SERVICES
// =====================

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===== CORS (PHẢI Ở ĐÂY) =====
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .WithOrigins(
                    "http://127.0.0.1:5500",
                    "http://localhost:5500"
                )
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// ===== DEPENDENCY INJECTION =====

// Công ty
builder.Services.AddScoped<ICongTyService, CongTyService>();
builder.Services.AddScoped<ICongTyRepository, CongTyRepository>();

// Việc làm
builder.Services.AddScoped<IViecLamRepository, ViecLamRepository>();
builder.Services.AddScoped<IViecLamService, ViecLamService>();

// Đơn ứng tuyển
builder.Services.AddScoped<IDonUngTuyenRepository, DonUngTuyenRepository>();
builder.Services.AddScoped<IDonUngTuyenService, DonUngTuyenService>();

// Phỏng vấn
builder.Services.AddScoped<IPhongVanRepository, PhongVanRepository>();
builder.Services.AddScoped<IPhongVanService, PhongVanService>();

// Offer
builder.Services.AddScoped<IOfferRepository, OfferRepository>();
builder.Services.AddScoped<IOfferService, OfferService>();

// Auth (nếu có)
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// =====================
// HTTP PIPELINE
// =====================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🚨 CORS PHẢI TRƯỚC Authorization
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
