var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DAL.CongTy.ICongTyRepository, DAL.CongTy.CongTyRepository>();
builder.Services.AddScoped<BLL.CongTy.ICongTyService, BLL.CongTy.CongTyService>();
builder.Services.AddScoped<DAL.ViecLam.IViecLamRepository, DAL.ViecLam.ViecLamRepository>();
builder.Services.AddScoped<BLL.ViecLam.IViecLamService, BLL.ViecLam.ViecLamService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
