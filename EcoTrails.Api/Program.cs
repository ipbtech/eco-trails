using System.Reflection;
using EcoTrails.Api.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<EcoTrailsDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddValidatorsFromAssembly(Assembly.Load("EcoTrails.Shared"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = new PathString("/Images")
});

app.UseRouting();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
