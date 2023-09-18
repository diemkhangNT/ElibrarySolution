using Microsoft.EntityFrameworkCore;
using QuanLyDeThi_KiemTra.Data;
using QuanLyDeThi_KiemTra.Interface;
using QuanLyDeThi_KiemTra.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region
//Automapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);
//cho phép mọi thiết bị kết nối đến API
builder.Services.AddCors(option => option.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

//tạo lk database
builder.Services.AddDbContext<DeThiDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Elibrary7"));
});

// Đăng ký interface và thực hiện các chức năng của nó trong file
builder.Services.AddScoped<ICrudCauHoiTracNghiem, CrudCauHoiTracNghiem>();
builder.Services.AddScoped<ICrudCauHoiTuLuan, CrudCauHoiTuLuan>();
builder.Services.AddScoped<ICrudDeThi, CrudDeThi>();
builder.Services.AddScoped<IFileExtention, FileExtention>();
builder.Services.AddScoped<ICrudTraLoiTracNghiem, CrudTraLoiTracNghiem>();
builder.Services.AddScoped<ICrudTraLoiTuLuan, CrudTraLoiTuLuan>();
builder.Services.AddScoped<ICrudTepCauHoi, CrudTepCauHoi>();

#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
