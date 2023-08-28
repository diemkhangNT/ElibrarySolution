using Microsoft.EntityFrameworkCore;
using QuanLyMonHoc.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region
//cho phép mọi thiết bị kết nối đến API
builder.Services.AddCors(option => option.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

//tạo lk database
builder.Services.AddDbContext<MonHocDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Elibrary"));
});

// Đăng ký interface IExistAlreadyService và thực hiện các chức năng của nó trong file ExistAlreadyService
//builder.Services.AddScoped<IExistAlreadyService, ExistAlreadyService>();

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
