using API.Middlewares;
using Application;
using Carter;
using Domain.Primitives;
using Domain.User;
using Infrastructure.DbContextConfig;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureApplicationService();
builder.Services.AddCarter();

builder.Services.AddDbContext<CrudDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});
builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
builder.Services.AddTransient<IUserRepository,UserRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionHandlingMIddleware>();
app.MapCarter();
app.Run();
