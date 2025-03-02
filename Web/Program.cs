using System.Reflection;
using System.Runtime.CompilerServices;
using Application.Infrastructure;
using Repository.Infrastructure;
using Serilog;
using Web.Infrastructure;
using Web.Infrastructure.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(opts =>
    opts.AddPolicy("ApiCorsPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:7045", "http://localhost:5000")
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});



builder.Services.AddSwaggerGen(options =>
{
    // Add JWT security definition
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid JWT token."
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>() // jwt doesn't use access scopes
        }
    });
});
builder.Services.AddFahrenheitDbContext();
builder.Services.AddBusinessServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddRepositories();
builder.Services.AddJwtAuthentication();
builder.Services.AddSerilog();
builder.Configuration.AddEnvironmentVariables();
builder.WebHost.UseUrls(builder.Configuration["ASPNETCORE_URLS"] ?? "http://localhost:5000");
builder.Services.ConfigureCORSPolicy();

var app = builder.Build();
app.ApplyMigrations();
app.UseCors("ApiCorsPolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<LoggingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.ConfigureStaticFilesUpload();
app.UseHttpsRedirection();
app.MapControllers();

Log.Logger.Information("Application started");

app.Run();