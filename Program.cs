using AzPasswordManager.Infrastructure;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using MudBlazor.Services;
using System.Net.Http.Headers;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//var initialScopes = builder.Configuration["DownstreamApi:Scopes"]?.Split(' ') ?? builder.Configuration["MicrosoftGraph:Scopes"]?.Split(' ');

// Add MudBlazor
builder.Services.AddMudServices();

// Get KeyVault BaseURI
string? KeyVaultBaseUri = builder.Configuration.GetSection("KeyVaultConfig").GetValue<string>("BaseUri");

// Add services to the container.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
        .EnableTokenAcquisitionToCallDownstreamApi(new string[] { $"{KeyVaultBaseUri?.Replace("itsinternalvault.", "")}.default" })
        .AddInMemoryTokenCaches();

builder.Services.AddControllersWithViews()
    .AddMicrosoftIdentityUI();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy
    options.FallbackPolicy = options.DefaultPolicy;
});

// MediatR Service
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// Azure KeyVault HTTP services
builder.Services.AddTransient<AzureKeyVaultHttpDelegatingHandler>();
builder.Services.AddHttpClient("keyVaultClient", (client) =>
{
    client.BaseAddress = new Uri(builder.Configuration["KeyVaultConfig:ApiAddress"] ?? "");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
}).AddHttpMessageHandler<AzureKeyVaultHttpDelegatingHandler>();


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddMicrosoftIdentityConsentHandler();


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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
