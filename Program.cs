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
    options.ProviderOptions.DefaultScopes.Add("email");
    options.UserOptions.NameClaim = "email";
    builder.Configuration.Bind("Local", options.ProviderOptions);
});

await builder.Build().RunAsync();