using Microsoft.EntityFrameworkCore;
using QuanLyMonHoc.Data;
using QuanLyMonHoc.Interface;
using QuanLyMonHoc.Services;

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
builder.Services.AddDbContext<MonHocDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Elibrary"));
});

// Đăng ký interface và thực hiện các chức năng của nó trong file
builder.Services.AddScoped<IExtension, Extension>();
builder.Services.AddScoped<ICrudCauHoi, CrudCauHoi>();
builder.Services.AddScoped<ICrudLopHoc, CrudLopHoc>();
builder.Services.AddScoped<ICrudMonHoc, CrudMonHoc>();
builder.Services.AddScoped<ICrudNienKhoa, CrudNienKhoa>();
builder.Services.AddScoped<ICrudTraLoi, CrudTraLoi>();

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
