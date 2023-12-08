using CIT.HelpDesk.Application.Interfaces;
using CIT.HelpDesk.Domain.Entities;
using CIT.HelpDesk.Persistence.Extensions;
using CIT.HelpDesk.Persistence.Repositories;
using CIT.HelpDesk.WebAPI.Exceptions;
using CIT.HelpDesk.WebAPI.Extensions;
using CIT.HelpDesk.WebAPI.Middlewares;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceLayer(builder.Configuration);
foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
{
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
}
builder.Services.AddTransient<GlobalExceptionMiddleWare>();
builder.Services.AddTransient<IMediator, Mediator>();
builder.Services.AddScoped(typeof(IGenericrepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(User));
builder.Services.AddJwtAuthentication();
builder.Services.AddCORSPolicy();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.UseCors("AllowAccess");

app.MapControllers();

app.ExceptionMiddleware();

app.Run();
