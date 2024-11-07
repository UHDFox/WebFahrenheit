using System.Reflection;
using Application.Infrastructure;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Repository.Infrastructure;
using Web;
using Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBusinessServices();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddRepositories();
builder.Services.AddDbContext<FahrenheitContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Psql")));

builder.Services.ConfigureCORSPolicy();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.ConfigureStaticFilesUpload();
app.UseCors("SomePolicy");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();



