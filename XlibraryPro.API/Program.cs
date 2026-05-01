using Serilog;
using XlibraryPro.API.Middleware;
using XlibraryPro.Application;
using XlibraryPro.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.UseSerilog((ctx, config) =>
    config.ReadFrom.Configuration(ctx.Configuration));

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();


// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("LMSPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
////builder.Services.AddCors(options =>
////{
////    options.AddPolicy("LMSPolicy", policy =>
////    {
////        policy.WithOrigins(
////                builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()
////                    ?? new[] { "http://localhost:3000" })
////              .AllowAnyHeader()
////              .AllowAnyMethod();
////    });
////});

Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("LMSPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
