using Api.Features.Office.Models.Validator;
using Core.Features.Employee.DataTransferObjects.Validator;
using Core.Features.Employee.Interfaces;
using Core.Features.Employee.Services;
using Core.Features.Office.Interfaces;
using Core.Features.Office.Services;
using Core.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// There is no reason to expose our backend framework.
builder.WebHost.UseKestrel(options => options.AddServerHeader = false);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<CreateEmployeeRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateEmployeeRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<OfficeCreateRequestValidator>();

/*
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});
*/

builder.Services.AddDbContextFactory<DBContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DBConnection"),
        builder => builder.MigrationsAssembly(typeof(DBContext).Assembly.FullName)));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IOfficeService, OfficeService>();

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
