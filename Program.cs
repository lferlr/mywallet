using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyWallet;
using MyWallet.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddAppConfiguration(builder.Configuration);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7097/") });
builder.Services.AddAntDesign();

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth", options.ProviderOptions);
    options.ProviderOptions.DefaultScopes.Add("email");
});

await builder.Build().RunAsync();