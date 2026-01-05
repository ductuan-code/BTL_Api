var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DbHelper>();
builder.Services.AddSingleton<NguoiDungDAL>();
builder.Services.AddSingleton<NguoiDungBLL>();
builder.Services.AddScoped<CongTyDAL>();
builder.Services.AddScoped<CongTyBLL>();
builder.Services.AddScoped<ViecLamDAL>();
builder.Services.AddScoped<ViecLamBLL>();
builder.Services.AddScoped<DonUngTuyenDAL>();
builder.Services.AddScoped<DonUngTuyenBLL>();





builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
