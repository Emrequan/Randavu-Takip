using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using RandevuTakipAPI.Data;
using RandevuTakipAPI.Models;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
// Veritabanı bağlantısı
builder.Services.AddDbContext<RandevuDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS servisini ekle
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()   // Herhangi bir origin'e izin ver (geliştirme için)
              .AllowAnyMethod()   // GET, POST, PUT, DELETE vb. izin ver
              .AllowAnyHeader();  // Header izinlerini aç
    });
});

// Swagger servisleri
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Kestrel'i tüm IP adreslerinden dinleyecek şekilde ayarla
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5237); // 5237 portunu aç
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// **CORS middleware'ini kullan**
app.UseCors();

// Diğer middleware'ler varsa (örneğin Authorization)
app.UseAuthorization();

// Model validasyonu için helper metot
bool TryValidateModel<T>(T model, out List<ValidationResult> results)
{
    var context = new ValidationContext(model);
    results = new List<ValidationResult>();
    return Validator.TryValidateObject(model, context, results, true);
}

// API Endpointler

app.MapGet("/randevular", async (RandevuDbContext db) =>
    await db.Randevular.ToListAsync());

app.MapGet("/randevular/{id}", async (int id, RandevuDbContext db) =>
    await db.Randevular.FindAsync(id) is Randevu randevu
        ? Results.Ok(randevu)
        : Results.NotFound());

app.MapPost("/randevular", async (Randevu randevu, RandevuDbContext db) =>
{
    if (!TryValidateModel(randevu, out var validationResults))
        return Results.BadRequest(validationResults);

    randevu.KayitTarihi = DateTime.Now;
    db.Randevular.Add(randevu);
    await db.SaveChangesAsync();
    return Results.Created($"/randevular/{randevu.Id}", randevu);
});

app.MapPut("/randevular/{id}", async (int id, Randevu updatedRandevu, RandevuDbContext db) =>
{
    if (!TryValidateModel(updatedRandevu, out var validationResults))
        return Results.BadRequest(validationResults);

    var randevu = await db.Randevular.FindAsync(id);
    if (randevu == null) return Results.NotFound();

    randevu.AdSoyad = updatedRandevu.AdSoyad;
    randevu.Tarih = updatedRandevu.Tarih;
    randevu.Aciklama = updatedRandevu.Aciklama;
    randevu.GuncellemeTarihi = DateTime.Now;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/randevular/{id}", async (int id, RandevuDbContext db) =>
{
    var randevu = await db.Randevular.FindAsync(id);
    if (randevu == null) return Results.NotFound();

    db.Randevular.Remove(randevu);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapPost("/giris", async (LoginModel login, RandevuDbContext db) =>
{
    if (!TryValidateModel(login, out var validationResults))
        return Results.BadRequest(validationResults);

    var kullanici = await db.Kullanicilar.FirstOrDefaultAsync(k => k.Email == login.Email && k.Sifre == login.Sifre);
    if (kullanici == null)
        return Results.Unauthorized();

    return Results.Ok(new { Message = "Giriş başarılı", KullaniciId = kullanici.Id, KullaniciAdi = kullanici.Isim });
});

app.MapPost("/kayit", async (Kullanici yeniKullanici, RandevuDbContext db) =>
{
    if (!TryValidateModel(yeniKullanici, out var validationResults))
        return Results.BadRequest(validationResults);

    yeniKullanici.KayitTarihi = DateTime.Now;

    db.Kullanicilar.Add(yeniKullanici);
    await db.SaveChangesAsync();

    return Results.Created($"/kullanicilar/{yeniKullanici.Id}", yeniKullanici);
});

app.MapPost("/kullanicilar/sifre-sifirla", async (SifreSifirlaModel model, RandevuDbContext db) =>
{
    if (!TryValidateModel(model, out var validationResults))
        return Results.BadRequest(validationResults);

    var kullanici = await db.Kullanicilar.FirstOrDefaultAsync(k => k.Email == model.Email);
    if (kullanici == null)
        return Results.NotFound("Kullanıcı bulunamadı.");

    kullanici.Sifre = model.YeniSifre;
    await db.SaveChangesAsync();

    return Results.Ok("Şifre başarıyla değiştirildi.");
});



app.MapGet("/api/kullanici/bilgiler/{id}", async (int id, RandevuDbContext db) =>
{
    var kullanici = await db.Kullanicilar.FindAsync(id);

    if (kullanici == null)
        return Results.NotFound();

    // İstersen sadece belli alanları dönebilirsin
    return Results.Ok(new
    {
        Ad = kullanici.Isim,  // Veya senin veritabanındaki alan adı neyse
        Soyad = kullanici.Soyisim,
        Email = kullanici.Email
    });
});



app.Run();