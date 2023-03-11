using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using net6JWT.DataAccess.EntityframeworkCore.Abstract;
using net6JWT.DataAccess.EntityframeworkCore.Concrete;
using net6JWT.Db;
using net6JWT.Services.Abstract;
using net6JWT.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//builder.Services.AddDbContext<DatabaseContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SamplePostgresConnection")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IAuthDal, AuthDal>();


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
