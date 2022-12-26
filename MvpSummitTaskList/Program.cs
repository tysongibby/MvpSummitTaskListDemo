using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using MvpSummit.Domain;
using MvpSummitTaskList.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
//builder.Services.AddScoped<ISummitTaskService, InMemoryTaskService>();
builder.Services.AddScoped<ISummitTaskService, DbSummitTaskService>();
builder.Services.AddScoped<ISummitTaskContextFactory, DefaultSummitContextFactory>();
builder.Services.AddDbContextFactory<SummitTaskContext>(opts => 
    opts.UseSqlite("name=todos", b => b.MigrationsAssembly(nameof(MvpSummitTaskList))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
