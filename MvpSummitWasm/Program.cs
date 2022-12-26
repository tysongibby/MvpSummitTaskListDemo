using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using MvpSummit.Domain;
using MvpSummitWasm;
using MvpSummitWasm.Data;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddScoped<ISummitTaskService, InMemoryTaskService>();
builder.Services.AddScoped<ISummitTaskService, DbSummitTaskService>();
builder.Services.AddScoped<ISummitTaskContextFactory, DefaultSummitContextFactory>();
builder.Services.AddDbContextFactory<SummitTaskContext>(opts =>
    opts.UseSqlite("name=todosWasm", b => b.MigrationsAssembly(nameof(MvpSummitWasm))));
builder.Services.AddSynchronizingDataFactory();

await builder.Build().RunAsync();
