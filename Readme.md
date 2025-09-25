# 🚀 BaMinimalTemplate - .NET 9 Minimal API Template

[![.NET](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/9.0)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)
[![NuGet](https://img.shields.io/nuget/v/BaMinimalTemplate.svg)](https://www.nuget.org/packages/BaMinimalTemplate/)

**BaMinimalTemplate**, modern .NET 9 Minimal API projeleri için hazırlanmış, production-ready bir template'dir. JWT kimlik doğrulama, kullanıcı yönetimi, Entity Framework Core ve temiz mimari desenleri ile birlikte gelir.

## ✨ Özellikler

### 🔐 Kimlik Doğrulama ve Yetkilendirme
- **JWT Bearer Token** kimlik doğrulama
- **Refresh Token** mekanizması
- **Role-based authorization** (rol tabanlı yetkilendirme)
- **Claims-based policies** (talep tabanlı politikalar)
- **Password hashing** ve güvenli şifre yönetimi
- **Forgot password** ve şifre sıfırlama işlevleri

### 🏗️ Mimari ve Desenler
- **Clean Architecture** prensipleri
- **Generic Repository Pattern** ile veri erişim katmanı
- **Generic Service Pattern** ile iş mantığı katmanı
- **Dependency Injection** ile gevşek bağlılık
- **AutoMapper** ile nesne dönüşümleri
- **FluentValidation** ile model doğrulama

### 📊 Veri Yönetimi
- **Entity Framework Core 9.0** ile veritabanı işlemleri
- **SQL Server** desteği (diğer veritabanları için kolayca değiştirilebilir)
- **Code First Migrations** ile veritabanı yönetimi
- **Identity Framework** entegrasyonu
- **Soft Delete** ve audit özellikleri

### 🔧 API Özellikleri
- **Minimal API** yaklaşımı
- **Swagger/OpenAPI** dokümantasyonu
- **CORS** yapılandırması
- **Health Checks** desteği
- **RESTful** endpoint tasarımı
- **Pagination** desteği

### 🛡️ Güvenlik
- **JWT token** güvenliği
- **HTTPS** zorunluluğu
- **Input validation** ve sanitization
- **SQL injection** koruması
- **CORS** politikaları

## 🎯 Bu Template Neden Kullanılmalı?

### ⚡ Hızlı Geliştirme
- **Sıfırdan başlamanıza gerek yok** - Tüm temel özellikler hazır
- **Production-ready** kod yapısı
- **Best practices** ile yazılmış
- **Minimal boilerplate** kodu

### 🏢 Kurumsal Standartlar
- **Scalable** mimari yapı
- **Maintainable** kod organizasyonu
- **Testable** servis katmanları
- **Documentation** ready

### 🎓 Öğrenme Kaynağı
- **Modern .NET** özelliklerini öğrenme
- **Clean Architecture** uygulaması
- **Security best practices** örnekleri
- **API design patterns** gösterimi

### 💰 Maliyet Tasarrufu
- **Development time** %70 azalma
- **Code review** süreçleri kısaltma
- **Bug riski** minimuma indirme
- **Maintenance** kolaylığı

## 🚀 Kurulum

### NuGet Package Manager ile
```bash
Install-Package BaMinimalTemplate
```

### .NET CLI ile
```bash
dotnet add package BaMinimalTemplate
```

### PackageReference ile
```xml
<PackageReference Include="BaMinimalTemplate" Version="1.0.0" />
```

## 📋 Kurulum Adımları

### 1. Proje Oluşturma
```bash
# Yeni bir .NET 9 projesi oluşturun
dotnet new web -n MyApi
cd MyApi

# Template'i yükleyin
dotnet add package BaMinimalTemplate
```

### 2. Veritabanı Yapılandırması
```json
// appsettings.json
{
  "ConnectionStrings": {
    "SqlServer": "Data Source=localhost,1433;Initial Catalog=MyApiDb;User ID=sa;Password=YourPassword;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;"
  },
  "JwtSettings": {
    "Issuer": "MyApi",
    "Audience": "MyApiUsers",
    "SecretKey": "your-very-long-secret-key-for-jwt-authentication-minimum-32-characters",
    "ExpiryHours": "24"
  }
}
```

### 3. Migration ve Veritabanı Oluşturma
```bash
# Migration oluşturun
dotnet ef migrations add InitialCreate

# Veritabanını oluşturun
dotnet ef database update
```

### 4. Uygulamayı Çalıştırma
```bash
dotnet run
```

Uygulama `https://localhost:5001` adresinde çalışacaktır.

## 📖 Kullanım Kılavuzu

### 🔐 Kimlik Doğrulama Endpoints

#### Kullanıcı Kaydı
```http
POST /api/auth/register
Content-Type: application/json

{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john@example.com",
  "password": "Password123!",
  "confirmPassword": "Password123!"
}
```

#### Kullanıcı Girişi
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "john@example.com",
  "password": "Password123!"
}
```

#### Token Yenileme
```http
POST /api/auth/refresh
Content-Type: application/json

{
  "refreshToken": "your-refresh-token-here"
}
```

#### Kullanıcı Çıkışı
```http
POST /api/auth/logout
Authorization: Bearer your-jwt-token-here
```

### 👤 Kullanıcı Yönetimi

#### Kullanıcıları Listeleme
```http
GET /api/users
Authorization: Bearer your-jwt-token-here
```

#### Kullanıcı Oluşturma
```http
POST /api/users
Authorization: Bearer your-jwt-token-here
Content-Type: application/json

{
  "firstName": "Jane",
  "lastName": "Smith",
  "email": "jane@example.com",
  "password": "Password123!"
}
```

### 📂 Kategori Yönetimi

#### Kategorileri Listeleme
```http
GET /api/categories
Authorization: Bearer your-jwt-token-here
```

#### Kategori Oluşturma
```http
POST /api/categories
Authorization: Bearer your-jwt-token-here
Content-Type: application/json

{
  "name": "Technology",
  "description": "Technology related content"
}
```

## 🏗️ Proje Yapısı

```
BaMinimalTemplate/
├── Data/                          # Veritabanı bağlamı ve konfigürasyonları
│   ├── ApplicationDbContext.cs
│   └── Configurations/
├── Dtos/                          # Data Transfer Objects
│   ├── Auth/
│   ├── Categories/
│   ├── Users/
│   └── UserTypes/
├── Endpoints/                     # API endpoint'leri
│   ├── AuthEndpoints.cs
│   ├── UserEndpoint.cs
│   └── CategoryEndpoints.cs
├── Extensions/                    # Extension metodları ve yardımcı sınıflar
│   ├── GenericRepository.cs
│   ├── GenericService.cs
│   ├── JwtService.cs
│   └── ServiceCollectionExtensions.cs
├── Mapping/                       # AutoMapper profilleri
├── Models/                        # Entity modelleri
├── Repositories/                  # Veri erişim katmanı
├── Services/                      # İş mantığı katmanı
└── Program.cs                     # Ana uygulama dosyası
```

## 🔧 Yapılandırma

### JWT Ayarları
```json
{
  "JwtSettings": {
    "Issuer": "YourAppName",
    "Audience": "YourAppUsers",
    "SecretKey": "your-secret-key-minimum-32-characters",
    "ExpiryHours": "24"
  }
}
```

### Veritabanı Ayarları
```json
{
  "ConnectionStrings": {
    "SqlServer": "Server=localhost;Database=YourDb;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### CORS Ayarları
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy", policy =>
    {
        policy.WithOrigins("https://yourfrontend.com")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
```

## 🧪 Test Etme

### Swagger UI ile Test
1. Uygulamayı çalıştırın: `dotnet run`
2. Tarayıcıda `https://localhost:5001` adresine gidin
3. Swagger UI'da endpoint'leri test edin

### Postman Collection
Template ile birlikte gelen `BaMinimalTemplate.http` dosyasını Postman'de import edebilirsiniz.

Bu proje MIT lisansı altında lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasına bakın.

## 👨‍💻 Geliştirici

**Bahadır Arslan**
- GitHub: [@yourusername](https://github.com/ars02bahadr)
- LinkedIn: [Bahadır Arslan](https://linkedin.com/in/yourprofile)


---

⭐ **Bu template'i beğendiyseniz, lütfen yıldız verin!**

## 🔄 Changelog

### v1.0.0 (2024-09-24)
- ✨ İlk sürüm yayınlandı
- 🔐 JWT kimlik doğrulama sistemi
- 👤 Kullanıcı yönetimi
- 📂 Kategori yönetimi
- 🏗️ Clean Architecture yapısı
- 📊 Entity Framework Core entegrasyonu
- 🔧 Generic Repository ve Service pattern'leri
- 📖 Swagger/OpenAPI dokümantasyonu
