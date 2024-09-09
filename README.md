# StudentCourseManagement

**StudentCourseManagement** uygulaması, .NET 8 üzerine geliştirilmiş, öğrenci ve kurs yönetimini modern ve ölçeklenebilir bir şekilde sunulmuştur. Bu proje, gelişmiş teknolojiler ve tasarım prensipleri kullanılarak geliştirilmiş olup, Docker ve Docker Compose ile konteynerleştirilmiş ve Redis ile optimize edilmiştir. Ayrıca, PostgreSQL veritabanı ve ASP.NET Identity kullanılarak kimlik doğrulama ve yetkilendirme işlemleri yapılmıştır.

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
  - Kursların oluşturulması, güncellenmesi, silinmesi ve görüntülenmesi (CRUD işlemleri).
  - Kurs bilgileri ve tarih aralıkları düzenlenebilir.

- **Öğrenci Yönetimi**:
  - Öğrencilerin oluşturulması, güncellenmesi, silinmesi ve görüntülenmesi (CRUD işlemleri).
  - Öğrencilere ders atama işlemleri yapılabilir.

- **Rol Yönetimi**:
  - Roller oluşturma ve yönetme.
  - Kullanıcılara roller atama.
  - Roller bazında panel içeriği ve erişim yönetimi.

  ![Ekran Resmi 2024-09-09 06 25 53](https://github.com/user-attachments/assets/77f3c175-7b9a-4589-84be-e04b0801f1a3)

## Teknolojiler ve Araçlar

- **.NET 8**: Modern ve güçlü uygulama geliştirme platformu.
- **PostgreSQL**: İlişkisel veritabanı yönetim sistemi, veri depolama ve yönetimi için kullanılır.
- **Redis**: Hızlı veri erişimi ve önbellekleme için kullanılan bellek içi veri yapıları sunucusu.
- **Docker & Docker Compose**: Konteynerleştirilmiş uygulama geliştirme ve dağıtım araçları.
- **ASP.NET Identity**: Kimlik doğrulama ve yetkilendirme işlemleri için kullanılır.
- **Tailwind CSS**: Modern ve duyarlı web tasarımı için kullanılan CSS framework'ü.

## Proje Yapısı

- **StudentCourseManagement.Application**: Uygulama iş mantığını ve servisleri içerir.
- **StudentCourseManagement.Domain**: İş nesneleri ve iş kurallarını içerir.
- **StudentCourseManagement.Infrastructure**: Veri erişimi ve diğer altyapı bileşenlerini içerir.
- **StudentCourseManagement.Presentation**: Kullanıcı arayüzü ve sunum katmanını içerir.

## Kurulum ve Çalıştırma

1. **Gereksinimler**:
   - [.NET 8 SDK](https://dotnet.microsoft.com/download)
   - [Docker ve Docker Compose](https://docs.docker.com/get-docker/)

2. **Proje Dosyalarını Klonlayın**:
   ```bash
   git clone https://github.com/enesscigdem/StudentCourseManagement.git
   cd StudentCourseManagement
   ```

3. **Docker ile Çalıştırma**:
   - Docker ve Docker Compose kullanarak uygulamayı çalıştırın:
     ```bash
     docker-compose up --build
     ```

4. **Veritabanı Bağlantısını Yapılandırma**:
   - PostgreSQL bağlantı bilgilerini `appsettings.json` dosyasında güncelleyin.

5. **Seed Data**:
   - Proje başlatıldığında, veritabanında varsayılan kullanıcı oluşturulur:
     - **Email**: admin@itb.com
     - **Password**: ITB2024!!

## Kullanım

- **Admin Paneli**: Admin tüm işlevlere erişim sağlar ve kurslar, öğrenciler, roller üzerinde CRUD işlemleri yapabilir.
- **Öğrenci Paneli**: Öğrenciler yalnızca kendi derslerini görüntüleyebilir.
