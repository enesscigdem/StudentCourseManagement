# StudentCourseManagement

**StudentCourseManagement** uygulaması, .NET 8 üzerine geliştirilmiş, öğrenci ve kurs yönetimini modern ve ölçeklendirilmiştir. Bu proje, gelişmiş teknolojiler ve tasarım prensipleri kullanılarak geliştirilmiş olup, Docker ve Docker Compose ile konteynerleştirilmiş ve Redis ile optimize edilmiştir. Ayrıca, PostgreSQL veritabanı ve ASP.NET Identity kullanılarak kimlik doğrulama ve yetkilendirme işlemleri yapılmıştır.

![Ekran Resmi 2024-09-09 06 25 37](https://github.com/user-attachments/assets/305e72f5-c34d-46a1-beeb-fc217d4d082d)
![Ekran Resmi 2024-09-09 06 25 30](https://github.com/user-attachments/assets/2715966c-21f9-431c-956a-9efdcb1d65ac)

## İçindekiler

- [Özellikler](#özellikler)
- [Teknolojiler ve Araçlar](#teknolojiler-ve-araçlar)
- [Proje Yapısı](#proje-yapısı)
- [Kurulum ve Çalıştırma](#kurulum-ve-çalıştırma)
- [Kullanım](#kullanım)
- [Seed Data](#seed-data)

## Özellikler

- **Kullanıcı ve Kimlik Yönetimi**:
  - Giriş, kayıt işlemleri.
  - ASP.NET Identity kullanılarak kimlik doğrulama ve yetkilendirme.
  - Admin kullanıcı, tüm sistem işlevlerine erişim sağlar.
  - Öğrenciler yalnızca kendi derslerini görüntüleyebilir ve yönetebilir.
  
  ![Ekran Resmi 2024-09-09 06 27 11](https://github.com/user-attachments/assets/7aeaeb8d-9c07-4b49-9f26-078129bd3b0f)
  ![Ekran Resmi 2024-09-09 06 26 45](https://github.com/user-attachments/assets/c66d5576-abda-48d3-b83a-b52a5385578d)

- **Kurs Yönetimi**:
  - Kursların oluşturulması, güncellenmesi ve silinmesi (CRUD işlemleri).
  - Kurslar sayfasında kursların detaylı listelenmesi.

- **Öğrenci Yönetimi**:
  - Öğrencilerin oluşturulması, güncellenmesi ve silinmesi (CRUD işlemleri).
  - Öğrencilere ders atama işlemleri.
  - Öğrencilerin kendi kayıtlı derslerini görüntüleyebilmesi.

- **Rol Yönetimi**:
  - Roller oluşturulabilir ve yönetilebilir.
  - Kullanıcılara roller atanabilir.
  - Role göre panelin değişmesi; örneğin, öğrenciler sadece kendi derslerini görebilir.

- **Dashboard**:
  - Admin ve yetkilendirilmiş kullanıcılar için kapsamlı bir kontrol paneli.
  - Özet bilgiler, grafikler ve raporlar ile sistemin genel durumu hakkında bilgi sağlar.
  - Kurslar, öğrenciler ve kullanıcı yönetimi gibi ana işlevler üzerinden hızlı erişim imkanı sunar.

  ![Ekran Resmi 2024-09-09 06 25 53](https://github.com/user-attachments/assets/77f3c175-7b9a-4589-84be-e04b0801f1a3)
  ![Ekran Resmi 2024-09-09 06 26 17](https://github.com/user-attachments/assets/f75731a3-525e-480a-8435-24998f7d2f94)

## Teknolojiler ve Araçlar

- **.NET 8**: Proje geliştirme için kullanılan ana platform.
- **PostgreSQL**: Veritabanı olarak kullanılan güçlü ve açık kaynaklı sistem.
- **ASP.NET Identity**: Kullanıcı kimlik doğrulama ve yetkilendirme.
- **Redis**: Performans iyileştirmeleri ve önbellekleme için kullanıldı.
- **Docker ve Docker Compose**: Uygulamanın konteynerleştirilmesi ve yönetimi.
- **Tailwind CSS**: Modern ve duyarlı kullanıcı arayüzü tasarımı için kullanıldı.
- **Entity Framework Core**: Veritabanı ile etkileşim ve ORM işlemleri.
- **Design Patterns & SOLID Principles**: Kodun sürdürülebilirliğini ve genişletilebilirliğini artırmak için kullanıldı.
- **ViewModel'ler**: Veri aktarımını ve kullanıcı arayüzü ile etkileşimi yönetmek için kullanıldı.

## Proje Yapısı

- **StudentCourseManagement.Application**: İş mantığı ve servisler.
- **StudentCourseManagement.Domain**: İşleme ve veri modeli.
- **StudentCourseManagement.Infrastructure**: Veritabanı erişim ve yapılandırma.
- **StudentCourseManagement.Presentation**: Kullanıcı arayüzü ve web sayfaları.

## Kurulum ve Çalıştırma

1. **Gereksinimler**:
   - [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
   - [Docker](https://www.docker.com/products/docker-desktop)
   - [Docker Compose](https://docs.docker.com/compose/)

2. **Projeyi Klonlayın**:
   ```bash
   git clone https://github.com/enesscigdem/StudentCourseManagement.git
   cd StudentCourseManagement
   ```

3. **Docker ile Çalıştırın**:
   ```bash
   docker-compose up --build
   ```

4. **Uygulamayı Çalıştırın**:
   - Uygulamayı başlatmak için Visual Studio veya terminalde `dotnet run` komutunu kullanabilirsiniz.

## Kullanım

- **Admin Paneli**: Sisteme admin olarak giriş yaparak kursları, öğrencileri ve kullanıcıları yönetebilirsiniz.
- **Öğrenci Paneli**: Öğrenciler kendi derslerini görüntüleyebilir.
- **Dashboard**: Admin ve yetkilendirilmiş kullanıcılar için sistem genel durumu hakkında bilgi sağlar.

## Seed Data
Bu kullanıcı, tüm admin işlevlerine erişim sağlar.
