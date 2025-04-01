using EcoTrails.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMediatR(opt => 
    opt.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddLogging(); //TODO
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddOidcAuthentication(opt =>
{
    builder.Configuration.Bind("Auth0", opt.ProviderOptions);
    opt.ProviderOptions.ResponseType = "code";
});



await builder.Build().RunAsync();
