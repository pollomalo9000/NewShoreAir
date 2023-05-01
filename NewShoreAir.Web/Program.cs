using NewShoreAir.Domain.Helpers;
using NewShoreAir.Web.Extensions;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddApplicationServices();
builder.Services.ConfigureCors();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSwagger();


// Add configuration
var configuration = builder.Configuration;
builder.Services.AddSingleton<IConfiguration>(configuration);
HelperConfiguracion.Initialize(configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
   
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Add your logging handler
//builder.Logging.AddLog4Net();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

//app.MapFallbackToFile("index.html"); ;

app.UseCors("CorsPolicy");




app.Run();
