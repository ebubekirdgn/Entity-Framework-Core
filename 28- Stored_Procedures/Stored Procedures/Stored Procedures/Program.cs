using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

ApplicationDbContext context = new();

#region Stored Procedure Nedir?

//SP, view'ler gibi kompleks sorgularımızı daha basit bir şekilde tekrar kullanılabilir  bir hale getirmemiz isağlayan veritabanı nesnesidir.
//View'ler tablo misali bir davranış sergilerken, SP'lar ise fonksiyonel bir davranış sergilerler.
//Ve türlü türlü artılarıda vardır.

#endregion Stored Procedure Nedir?

#region EF Core İle Stored Procedure Kullanımı

#region Stored Procedure Oluşturma

//1. adım : boş bir migration oluşturunuz.
//2. adım : migration'ın içerisindeki Up fonksiyonuna SP'ın Create komutlarını yazınız, Down fonk. ise Drop komutlarını yazınız.
//3. adım : migrate ediniz.

#endregion Stored Procedure Oluşturma

#region Stored Procedure'ü Kullanma

//SP'ı kullanabilmek için bir entity'e ihtiyacımız vardır. Bu entity'nin DbSet propertysi olarak context nesnesine de eklenmesi gerekmektedir.
//Bu DbSet properytysi üzerinden FromSql fonksiyonunu kullanarak 'Exec ....' komutu eşliğinde SP yapılanmasını çalıştırıp sorgu neticesini elde edebilirsiniz.

#region FromSql

var datas = await context.PersonOrders.FromSql($"EXEC sp_PersonOrders").ToListAsync();

#endregion FromSql

#endregion Stored Procedure'ü Kullanma

#region Geriye Değer Döndüren Stored Procedure'ü Kullanma

//SqlParameter countParameter = new()
//{
//    ParameterName = "count",
//    SqlDbType = System.Data.SqlDbType.Int,
//    Direction = System.Data.ParameterDirection.Output
//};
//await context.Database.ExecuteSqlRawAsync($"EXEC @count = sp_bestSellingStaff", countParameter);
//Console.WriteLine(countParameter.Value);

#endregion Geriye Değer Döndüren Stored Procedure'ü Kullanma
Console.WriteLine();
#region Parametreli Stored Procedure Kullanımı

//SqlParameter nameParameter = new()
//{
//    ParameterName = "name",
//    SqlDbType = System.Data.SqlDbType.NVarChar,
//    Direction = System.Data.ParameterDirection.Output,
//    Size = 1000
//};
//await context.Database.ExecuteSqlRawAsync($"EXECUTE sp_PersonOrders2 7, @name OUTPUT", nameParameter);
//Console.WriteLine(nameParameter.Value);

#endregion Parametreli Stored Procedure Kullanımı

#endregion EF Core İle Stored Procedure Kullanımı

Console.WriteLine();

public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }

    public ICollection<Order> Orders { get; set; }
}

public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }

    public Person Person { get; set; }
}

[NotMapped]
public class PersonOrder
{
    public string Name { get; set; }
    public int Count { get; set; }
}

internal class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PersonOrder> PersonOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<PersonOrder>()
             .HasNoKey();

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }

 

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database=ApplicationDB;" );
    }
}