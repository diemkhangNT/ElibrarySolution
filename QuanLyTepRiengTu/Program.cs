using Microsoft.EntityFrameworkCore;
using QuanLyTepRiengTu.Data;
using QuanLyTepRiengTu.Interface;
using QuanLyTepRiengTu.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//cho phép mọi thiết bị kết nối đến API
builder.Services.AddCors(option => option.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

//tạo lk database
builder.Services.AddDbContext<FilePrivateDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Elibrary5"));
});
// Đăng ký interface I... và thực hiện các chức năng của nó trong file ...
builder.Services.AddScoped<IFileCrudService, FileCrudService>();
//Automapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);
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
