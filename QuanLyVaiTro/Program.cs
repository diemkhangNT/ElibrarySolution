using Microsoft.EntityFrameworkCore;
using QuanLyVaiTro.Data;
using QuanLyVaiTro.Interface;
using QuanLyVaiTro.Services;

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
builder.Services.AddDbContext<VaiTroDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Elibrary1"));
});

// Đăng ký interface I... và thực hiện các chức năng của nó trong file ...
builder.Services.AddScoped<ICrudPhanQuyenService, CrudPhanQuyen>();
builder.Services.AddScoped<ICrudVaiTroService, CrudVaiTro>();

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
