using Aplikacja_na_BDwAI;
using Aplikacja_na_BDwAI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Rejestracja ApplicationDbContext z użyciem SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dodanie IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Konfiguracja autentykacji za pomocą ciasteczek
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login"; // Ścieżka do strony logowania
        options.AccessDeniedPath = "/Auth/Login"; // Ścieżka w przypadku odmowy dostępu
    });

builder.Services.AddAuthorization();

// Konfiguracja sesji
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Czas trwania sesji
    options.Cookie.HttpOnly = true; // Ciasteczka dostępne tylko po stronie serwera
    options.Cookie.IsEssential = true; // Wymóg dla ciasteczek sesyjnych
});

// Dodanie kontrolerów z widokami
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Konfiguracja middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Obsługa błędów w środowisku produkcyjnym
    app.UseHsts(); // Wymuszanie HTTPS
}

app.UseHttpsRedirection(); // Przekierowanie na HTTPS
app.UseStaticFiles(); // Obsługa plików statycznych

app.UseRouting(); // Konfiguracja routingu

// Middleware dla sesji
app.UseSession();

// Middleware dla autentykacji i autoryzacji
app.UseAuthentication();
app.UseAuthorization();

// Konfiguracja routingu
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
