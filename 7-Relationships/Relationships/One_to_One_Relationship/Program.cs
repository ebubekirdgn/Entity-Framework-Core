﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

ESirketDbContext context = new();

#region Default Convention

//Her iki entity'de Navigation Property ile birbirlerini tekil olarak referans ederek fiziksel bir ilişkinin olacağı ifade edilir.
//One to One ilişki türünde, dependent entity'nin hangisi olduğunu default olarak belirleyebilmek pek kolay değildir. Bu durumda fiziksel olarak bir foreign key'e karşılık property/kolon tanımlayarak çözüm getirebiliyoruz.
//Böylece foreign key'e karşılık property tanımlayarak lüzumsuz bir kolon oluşturmuş oluyoruz.
// class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }
//    public CalisanAdresi CalisanAdresi { get; set; }
//}

// class CalisanAdresi
//{
//    public int Id { get; set; }
//    public string Adres { get; set; }
//    public int CalisanId { get; set; }
//    public Calisan Calisan { get; set; }
//}

#endregion Default Convention

#region Data Annotations

//Navigation Property'ler tanımlanmalıdır.
//Foreign koonunun ismi default convention'ın dışında bir kolon olacaksa eğer ForeignKey attribute ile bunu bildirebiliriz.
//Foreign Key kolonu oluşturulmak zorunda değildir.
//1'e 1 ilişkide ekstradan foreign key kolonuna ihtiyaç olmayacağından dolayı dependent entity'deki id kolonunun
//hem foreign key hem de primary key olarak kullanmayı tercih ediyoruz ve bu duruma özen gösterilmelidir.
//class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }

//    public CalisanAdresi CalisanAdresi { get; set; }
//}
//class CalisanAdresi
//{
//    [Key, ForeignKey(nameof(Calisan))]
//    public int Id { get; set; }
//    public string Adres { get; set; }

//    public Calisan Calisan { get; set; }
//}

#endregion Data Annotations

#region Fluent API

//Navigation Proeprtyler tanımlanmalı!
//Fleunt API yönteminde entity'ler arasındaki ilişki context sınıfı içerisinde OnModelCreating fonksiyonun override edilerek metotlar aracılığıyla tasarlanması gerekmektedir. Yani tüm sorumluluk bu fonksiyon içerisindeki çalışmalardadır.
class Calisan
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public CalisanAdresi CalisanAdresi { get; set; }
}
class CalisanAdresi
{
    public int Id { get; set; }
    public string Adres { get; set; }
    public Calisan Calisan { get; set; }
}

#endregion Fluent API

internal class ESirketDbContext : DbContext
{
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<CalisanAdresi> CalisanAdresleri { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-3DL43QU;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database=ESirketDB;");
    }

    //Model'ların(entity) veritabanında generate edilecek yapıları bu fonksiyonda içerisinde konfigüre edilir
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalisanAdresi>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Calisan>()
             .HasOne(c => c.CalisanAdresi)
             .WithOne(c => c.Calisan)
             .HasForeignKey<CalisanAdresi>(c => c.Id);
    }
}