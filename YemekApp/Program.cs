using Business.Services;
using Business.Services.Bases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMalzemeService, MalzemeService>();
builder.Services.AddScoped<IYemekTarifiService, YemekTarifiService>();
builder.Services.AddScoped<ISehirService, SehirService>();
builder.Services.AddScoped<IUlkeService, UlkeService>();
builder.Services.AddScoped<IHesapService, HesapService>();
builder.Services.AddScoped<IKullaniciService, KullaniciService>();
builder.Services.AddScoped<IRolService, RolService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
