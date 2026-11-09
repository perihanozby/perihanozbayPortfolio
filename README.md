<div align="center">

# Perihan Özbay · Portfolio MVC

**ASP.NET Core MVC · Entity Framework Core · SQL Server**

Veritabanı destekli kişisel portfolyo ve içerik yönetim paneli  
**M&Y Yazılım Eğitim Akademi · 12. Dönem Akşam Grubu · CASE 01**

[Portfolyo](#-portfolyo-arayüzü) · [Admin Paneli](#-yönetim-paneli) · [Teknolojiler](#-teknolojiler) · [Kurulum](#-yerel-kurulum)

<img src="docs/screenshots/hero.png" alt="Portfolyo ana sayfa ve karşılama alanı" width="860" />

</div>

## 📌 Proje Hakkında

Bu çalışma, mevcut portfolyo arayüzü ve yönetim tasarımını **ASP.NET Core MVC**, **Entity Framework Core (Code First)** ve **SQL Server** ile dinamik hale getirmeyi amaçlar. Public sayfa **ViewComponent** bileşenlerine ayrılmıştır; içerik yönetimi **Admin Area** üzerinden gerçekleştirilir.

> Akademi projesidir. Ekranlarda yer alan bazı proje kayıtları örnek/demo içeriklerdir.

## 🖥️ Portfolyo Arayüzü

| Hakkımda | Teknik yetenekler |
|:--:|:--:|
| <img src="docs/screenshots/about.png" alt="Dinamik hakkımda alanı" width="440" /> | <img src="docs/screenshots/skills.png" alt="Yüzdelik yetenek çubukları" width="440" /> |

**Öne çıkan projeler:** Proje kartları, teknoloji etiketleri ve proje açıklamaları.

<div align="center"><img src="docs/screenshots/projects.png" alt="Portfolyo proje kartları" width="780" /></div>

| Deneyim ve eğitim | İletişim formu |
|:--:|:--:|
| <img src="docs/screenshots/experience-education.png" alt="Deneyim ve eğitim kayıtları" width="440" /> | <img src="docs/screenshots/contact.png" alt="İletişim formu" width="440" /> |

Public arayüzde bölüm bağlantıları, yetenek seviye çubukları, proje kartları, iletişim formu ve **PDF CV indirme** bağlantısı bulunmaktadır.

## 🛠️ Yönetim Paneli

**Dashboard:** Veritabanı verilerine dayalı toplam/aktif proje, yetenek, deneyim, eğitim, mesaj ve okunmamış mesaj istatistikleri; son mesajlar, son projeler ve hızlı işlemler.

<div align="center"><img src="docs/screenshots/dashboard.png" alt="Admin dashboard ve istatistikler" width="860" /></div>

| Proje yönetimi | Mesaj yönetimi |
|:--:|:--:|
| <img src="docs/screenshots/admin-projects.png" alt="Admin proje listesi" width="440" /> | <img src="docs/screenshots/admin-messages.png" alt="Admin mesaj listesi ve sıralama" width="440" /> |

**İçerik modülleri:** Ana sayfa, Hakkımda, yetenekler, projeler, proje kategorileri, deneyim, eğitim, mesajlar ve ayarlar için yönetim ekranları.

- **Projeler:** Ekleme, düzenleme ve silme; kategori seçimi, aktif/pasif ve öne çıkarma alanları.
- **Mesajlar:** Gelen kutusu, gönderene/konuya göre arama, tarih sıralaması ve okundu bilgisi.
- **CV yönetimi:** PDF yükleme/güncelleme; public arayüzden dosyayı indirme.
- **Görsel yükleme:** Proje formlarında dosya seçme desteği; dosya boyutu ve türü doğrulaması için sunucu tarafı işlemler.

<details>
<summary><strong>Ek ekranlar: CV yönetimi ve sayfa altlığı</strong></summary>

<p align="center"><img src="docs/screenshots/admin-cv.png" alt="Admin PDF CV yönetimi" width="720" /></p>
<p align="center"><img src="docs/screenshots/footer.png" alt="Portfolyo altlık ve navigasyon" width="720" /></p>

</details>

## 🗂️ Veri Modeli ve MVC Yapısı

- **MVC:** Controller, Entity/Model ve Razor View ayrımı.
- **ViewComponent:** Public sayfa bölümlerinin bileşen bazında yönetimi.
- **EF Core:** Code First, migration ve LINQ sorguları.
- **İlişkili tablolar:** `ProjectCategory (1) → (N) Project` ilişkisi; foreign key ve navigation property.
- **Formlar:** `ModelState` ve model doğrulaması, kategori dropdown'ında `SelectList` / `asp-items`.
- **Dashboard sorguları:** `Count`, `OrderByDescending`, `Take` ve ilişkili veri erişimi.

## ⚙️ Teknolojiler

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-MVC-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-C%23-239120)
![EF Core](https://img.shields.io/badge/Entity_Framework-Core-512BD4)
![SQL Server](https://img.shields.io/badge/SQL_Server-Database-CC2927)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5-7952B3)

`C#` · `ASP.NET Core MVC` · `Entity Framework Core` · `SQL Server` · `Razor` · `ViewComponent` · `Bootstrap` · `HTML/CSS` · `JavaScript`

## 📋 CASE 01 | Proje Kapsamı

Çalışmanın temel amacı, ASP.NET Core MVC mimarisini kullanarak statik bir portfolyo tasarımını veritabanı destekli, dinamik ve yönetilebilir bir web uygulamasına dönüştürmektir.

Proje kapsamında;

- **Dinamik içerik yönetimi:** Portfolyo bölümlerinin Admin paneli üzerinden yönetilmesi.
- **CRUD işlemleri:** Proje, kategori, eğitim, deneyim, yetenek ve diğer içeriklerin yönetimi.
- **İlişkisel veritabanı:** Entity Framework Core Code First yaklaşımı ve SQL Server entegrasyonu.
- **ViewComponent mimarisi:** Arayüz bileşenlerinin modüler ve sürdürülebilir şekilde yapılandırılması.
- **İletişim yönetimi:** Form verilerinin veritabanına kaydedilmesi ve Admin panelinden görüntülenmesi.
- **Dashboard:** Veritabanından elde edilen istatistiklerin ve son kayıtların sunulması.
- **Dosya yönetimi:** Proje görsellerinin yüklenmesi ve PDF formatında CV yönetimi.

Bu çalışma ile MVC mimarisi, ilişkisel veri yönetimi, sunucu taraflı doğrulama ve dinamik web uygulaması geliştirme konularında edinilen bilgilerin uygulamalı olarak pekiştirilmesi hedeflenmiştir.
