using EcoTrails.Client;
using EcoTrails.Client.Features.Auth;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMediatR(opt => 
    opt.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddHttpClient(
        "SecureAPIClient", 
        client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddOidcAuthentication(opt =>
{
    opt.ProviderOptions.Authority = builder.Configuration["Auth0:Authority"] ?? "";
    opt.ProviderOptions.ClientId = builder.Configuration["Auth0:ClientId"] ?? "";
    opt.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:ApiIdentifier"] ?? "");
    opt.ProviderOptions.ResponseType = "code";
}).AddAccountClaimsPrincipalFactory<CustomUserFactory<RemoteUserAccount>>();

await builder.Build().RunAsync();