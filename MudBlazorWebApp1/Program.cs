using MudBlazor.Services;
using MudBlazorWebApp1.Client.Pages;
using MudBlazorWebApp1.Components;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCors(options =>
options.AddPolicy("AllowAllOrigins",
builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

var app = builder.Build();

app.UseCors("AllowAllOrigins");
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Frame-Options", "ALLOWALL");
    await next();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MudBlazorWebApp1.Client._Imports).Assembly);

app.Run();
