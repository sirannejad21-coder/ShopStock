using DNTCaptcha.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ShopStock.Infra.Data.Context;
using ShopStock.Infra.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Memory Cache

builder.Services.AddMemoryCache();

#endregion


#region Session

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

#endregion


#region Captcha

builder.Services.AddDNTCaptcha(options =>
{
    options.UseSessionStorageProvider();

    options.ShowThousandsSeparators(false);

    options.WithEncryptionKey("ShopStock-Captcha-2026-Secure-Key");
});

#endregion


#region Db

builder.Services.AddDbContext<EshopDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("EshopConection")));

#endregion


#region Configure Service

builder.Services.RegisterServices();

#endregion


#region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        CookieAuthenticationDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        CookieAuthenticationDefaults.AuthenticationScheme;

    options.DefaultScheme =
        CookieAuthenticationDefaults.AuthenticationScheme;

    options.DefaultSignInScheme =
        CookieAuthenticationDefaults.AuthenticationScheme;

})
.AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});

#endregion


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


// Session باید قبل از Controllerها فعال شود
app.UseSession();

app.UseAuthentication();

app.UseAuthorization();


// Areas
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);


// Default
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);


app.Run();