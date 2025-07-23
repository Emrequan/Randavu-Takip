# Randevu Takip Sistemi

Bu proje, kullanıcıların randevularını kolayca oluşturup takip edebileceği, .NET tabanlı bir mobil uygulama (MAUI) ve bir RESTful API'den oluşmaktadır. Kullanıcılar sisteme kayıt olabilir, giriş yapabilir, randevu ekleyebilir, listeleyebilir, güncelleyebilir ve silebilirler. Ayrıca şifre sıfırlama ve kullanıcı bilgilerini görüntüleme gibi temel kullanıcı işlemleri de desteklenmektedir.

---

## Proje Yapısı

- **RandevuApp/** : Mobil uygulama (MAUI) projesi
  - Kullanıcı arayüzü (XAML sayfaları)
  - Modeller (Kullanıcı, Randevu, Şifre Sıfırlama)
  - Servisler (API ile iletişim)
- **RandevuTakipAPI/** : .NET Minimal API projesi
  - RESTful endpointler (randevu ve kullanıcı işlemleri)
  - Entity Framework Core ile SQL Server veritabanı bağlantısı
  - Modeller ve DbContext

---

## Temel Özellikler

### Mobil Uygulama (RandevuApp)

- **Kullanıcı Girişi ve Kayıt:** Kullanıcılar e-posta ve şifre ile giriş yapabilir veya yeni hesap oluşturabilir.
- **Randevu Yönetimi:** Randevu oluşturma, listeleme, güncelleme ve silme işlemleri.
- **Şifre Sıfırlama:** Kullanıcılar şifrelerini sıfırlayabilir.
- **Kullanıcı Bilgileri:** Kullanıcıya özel bilgiler ve karşılama mesajı.
- **Modern ve Kullanıcı Dostu Arayüz:** MAUI ile responsive ve sade tasarım.

### API (RandevuTakipAPI)

- **Kullanıcı İşlemleri:** Kayıt, giriş, şifre sıfırlama, kullanıcı bilgisi sorgulama.
- **Randevu İşlemleri:** Randevu ekleme, listeleme, güncelleme, silme.
- **Swagger Desteği:** API endpointlerini test etmek için Swagger arayüzü.
- **CORS Desteği:** Farklı platformlardan erişim için CORS yapılandırması.
- **Veritabanı:** SQL Server ile Entity Framework Core üzerinden veri yönetimi.

---

## Kullanılan Teknolojiler

- **.NET 9.0**
- **.NET MAUI** (Mobil uygulama)
- **Entity Framework Core** (ORM)
- **SQL Server** (Veritabanı)
- **Swagger** (API dokümantasyonu)
- **Syncfusion DataGrid** (Randevu listesi için gelişmiş tablo)

---

## Başlangıç ve Kurulum

### API için:
1. `RandevuTakipAPI/appsettings.json` dosyasındaki veritabanı bağlantı bilgisini güncelleyin.
2. Terminalde API klasörüne geçin ve aşağıdaki komutları çalıştırın:
   ```
   dotnet ef database update
   dotnet run
   ```
3. API, varsayılan olarak `http://localhost:5237` adresinde çalışacaktır.

### Mobil Uygulama için:
1. `RandevuApp/Services/AuthService.cs` içindeki API adresini kendi makinenize göre güncelleyin.
2. MAUI projesini Visual Studio ile açıp çalıştırın.

---

## Temel Dosya ve Klasörler

- **RandevuApp/View/** : Giriş, kayıt, ana sayfa, randevu oluşturma, randevu listesi, şifre değiştirme gibi tüm kullanıcı arayüzü sayfaları.
- **RandevuApp/Models/** : Kullanıcı, randevu ve şifre sıfırlama için veri modelleri.
- **RandevuApp/Services/** : API ile haberleşen servisler.
- **RandevuTakipAPI/Models/** : API tarafındaki veri modelleri.
- **RandevuTakipAPI/Data/** : Entity Framework Core DbContext ve veritabanı işlemleri.
- **RandevuTakipAPI/Program.cs** : API endpointlerinin ve servislerin tanımlandığı ana dosya.

---

## Örnek Kullanım Senaryosu

1. Kullanıcı uygulamayı açar, giriş yapar veya kayıt olur.
2. Ana sayfada mevcut randevularını görür, yeni randevu ekleyebilir.
3. Randevu oluştururken başlık, açıklama, tarih, saat, tür ve hatırlatma gibi detayları girer.
4. Randevularını listeleyebilir, güncelleyebilir veya silebilir.
5. Şifresini değiştirebilir veya unuttuysa sıfırlayabilir.

---

## Katkı ve Lisans

- Katkıda bulunmak için lütfen bir pull request gönderin.
- Lisans bilgisi için LICENSE dosyasına bakınız. 