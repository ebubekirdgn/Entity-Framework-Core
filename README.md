# Querying
### En Temel Basit Bir Sorgulama Nasıl Yapılır?
  1. Method Syntax
> var urunler = await context.Urunler.ToListAsync();
  2. Query Syntax
>   var urunler2 = await (from urun in context.Urunler
  select urun).ToListAsync();


### Sorguyu Execute Etmek İçin Ne Yapmamız Gerekmektedir?
###  - ToListAsync 

 1.  Method Syntax
>  var urunler = await context.Urunler.ToListAsync();
 
2. Query Syntax
>  var urunler = await (from urun in context.Urunler
 select urun).ToListAsync();
 
        int urunId = 5;
        string urunAdi = "2";
    	
        var urunler = from urun in context.Urunler
                    where urun.Id > urunId && urun.UrunAdi.Contains(urunAdi)
                   select urun;
        
        urunId = 200;
        urunAdi = "4";
        
        foreach (Urun urun in urunler)
        {
            Console.WriteLine(urun.UrunAdi);
        }
        await urunler.ToListAsync();
  
### Deferred Execution(Ertelenmiş Çalışma)
IQueryable çalışmalarında ilgili kod yazıldığı noktada tetiklenmez/çalıştırılmaz yani ilgili kod yazıldığı noktada sorguyu generate etmez! Nerede eder? Çalıştırıldığı/execute edildiği noktada tetiklenir! İşte bu durumada ertelenmiş çalışma denir!

### IQueryable ve IEnumerable Nedir? Basit Olarak!

>  var urunler = await (from urun in context.Urunler
                      select urun).ToListAsync();

### IQueryable
 Sorguya karşılık gelir.
 Ef core üzerinden yapılmış olan sorgunun execute edilmemiş halini ifade eder.

### IEnumerable
 Sorgunun çalıştırılıp/execute edilip verilerin in memorye yüklenmiş halini ifade eder.

### 1. Çoğul Veri Getiren Sorgulama Fonksiyonları
### a.ToListAsync
//Üretilen sorguyu execute ettirmemizi sağlayan fonksiyondur.

### Method Syntax
 > var urunler = context.Urunler.ToListAsync();

### Query Syntax

     var urunler = (from urun in context.Urunler
                   select urun).ToListAsync();
    var urunler = from urun in context.Urunler
                   select urun;
    var datas = await urunler.ToListAsync();
 

### b.Where
 Oluşturulan sorguya where şartı eklememizi sağlayan bir fonksiyondur.

### Method Syntax

    var urunler = await context.Urunler.Where(u => u.Id > 500).ToListAsync();
    var urunler = await context.Urunler.Where(u =>u.UrunAdi.StartsWith("a")).ToListAsync();
    Console.WriteLine();
 
### Query Syntax

    var urunler = from urun in context.Urunler
                  where urun.Id > 500 && urun.UrunAdi.EndsWith("7")
                  select urun;
    var data = await urunler.ToListAsync();
    Console.WriteLine();
 
### c.OrderBy
//Sorgu üzerinde sıralama yapmamızı sağlayan bir fonksiyondur. (Ascending)

### Method Syntax

    var urunler = context.Urunler.Where(u => u.Id > 500 || u.UrunAdi.EndsWith("2")).OrderBy(u => u.UrunAdi);
 
### Query Syntax

    var urunler2 = from urun in context.Urunler
                      where urun.Id > 500 || urun.UrunAdi.StartsWith("2")
                       orderby urun.UrunAdi
    select urun;
     
    await urunler.ToListAsync();
    await urunler2.ToListAsync();
 

### d.ThenBy
OrderBy üzerinde yapılan sıralama işlemini farklı kolonlarada uygulamamızı sağlayan bir fonksiyondur. (Ascending) 

    var urunler = context.Urunler.Where(u => u.Id > 500 || u.UrunAdi.EndsWith("2")).OrderBy(u => u.UrunAdi).ThenBy(u => u.Fiyat).ThenBy(u => u.Id);
    await urunler.ToListAsync();

### e.OrderByDescending
Descending olarak sıralama yapmamızı sağlayan bir fonksiyondur.

#### Method Syntax
    var urunler = await context.Urunler.OrderByDescending(u => u.Fiyat).ToListAsync();
##### Query Syntax
    var urunler = await (from urun in context.Urunler
                         orderby urun.UrunAdi descending
                         select urun).ToListAsync();
 

#### f.ThenByDescending
OrderByDescending üzerinde yapılan sıralama işlemini farklı kolonlarada uygulamamızı sağlayan bir fonksiyondur. (Ascending)

    var urunler = await context.Urunler.OrderByDescending(u => u.Id).ThenByDescending(u => u.Fiyat).ThenBy(u => u.UrunAdi).ToListAsync();
 

###2. Tekil Veri Getiren Sorgulama Fonksiyonları
 Yapılan sorguda sade ve sadece tek bir verinin gelmesi amaçlanıyorsa Single ya da SingleOrDefault fonksiyonları kullanılabilir.
#### SingleAsync
Eğer ki, sorgu neticesinde birden fazla veri geliyorsa ya da hiç gelmiyorsa her iki durumda da exception fırlatır.
#### Tek Kayıt Geldiğinde
    var urun = await context.Urunler.SingleAsync(u => u.Id == 3);
    Console.WriteLine();

#### Hiç Kayıt Gelmediğinde
    var urun = await context.Urunler.SingleAsync(u => u.Id == 5555);

#### Çok Kayıt Geldiğinde
    var urun = await context.Urunler.SingleAsync(u => u.Id > 55);


#### SingleOrDefaultAsync
Eğer ki, sorgu neticesinde birden fazla veri geliyorsa exception fırlatır, hiç veri gelmiyorsa null döner.
#### Tek Kayıt Geldiğinde
    var urun = await context.Urunler.SingleOrDefaultAsync(u => u.Id == 55);
    #endregion
#### Hiç Kayıt Gelmediğinde
    var urun = await context.Urunler.SingleOrDefaultAsync(u => u.Id == 5555);
#### Çok Kayıt Geldiğinde
    var urun = await context.Urunler.SingleOrDefaultAsync(u => u.Id > 1);
Yapılan sorguda tek bir verinin gelmesi amaçlanıyorsa First ya da FirstOrDefault fonksiyonları kullanılabilir.
#### FirstAsync
 Sorgu neticesinde elde edilen verilerden ilkini getirir. Eğer ki hiç veri gelmiyorsa hata fırlatır.
#### Tek Kayıt Geldiğinde
    var urun = await context.Urunler.FirstAsync(u => u.Id == 55);
#### Hiç Kayıt Gelmediğinde
    var urun = await context.Urunler.FirstAsync(u => u.Id == 5555);
#### Çok Kayıt Geldiğinde
    var urun = await context.Urunler.FirstAsync(u => u.Id > 55);
#### FirstOrDefaultAsync
 Sorgu neticesinde elde edilen verilerden ilkini getirir. Eğer ki hiç veri gelmiyorsa null değerini döndürür.
#### Tek Kayıt Geldiğinde
     var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 55);
#### Hiç Kayıt Gelmediğinde
    var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 5555);
#### Çok Kayıt Geldiğinde
    var urun = await context.Urunler.FirstAsync(u => u.Id > 55);
#### SingleAsync, SingleOrDefaultAsync, FirstAsync, FirstOrDefaultAsync Karşılaştırması
![alt yazı][resim]

[resim]: https://github.com/ebubekirdgn/Entity-Framework-Core/blob/main/5-Querying/Querying/Querying/Single%2C%20SingleOrDefault%2C%20First%2C%20FirstOrDefault%20Fonksiyonlar%C4%B1%20Kar%C5%9F%C4%B1la%C5%9Ft%C4%B1rma.png "Resim Başlığı"

#region FindAsync
 Find fonksiyonu, primary key kolonuna özel hızlı bir şekilde sorgulama yapmamızı sağlayan bir fonksiyondur.
 
    Urun urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 55);
    Urun urun = await context.Urunler.FindAsync(55);
#### Composite Primary key Durumu
    UrunParca u = await context.UrunParca.FindAsync(2, 5);

#### FindAsync İle SingleAsync, SingleOrDefaultAsync, FirstAsync, FirstOrDefaultAsync Fonksiyonlarının Karşılaştırması
![alt yazı][resim]

[resim]: https://github.com/ebubekirdgn/Entity-Framework-Core/blob/main/5-Querying/Querying/Querying/Find%20%C4%B0le%20Single%2C%20SingleOrDefault%2C%20First%2C%20FirstOrDefault%20Fonksiyonlar%C4%B1n%C4%B1%20Kar%C5%9F%C4%B1la%C5%9Ft%C4%B1rm.png "Resim Başlığı"


#### LastAsync
Sorgu neticesinde gelen verilerden en sonuncusunu getirir. Eğer ki hiç veri gelmiyorsa hata fırlatır. OrderBy kullanılması mecburidir.

    var urun = await context.Urunler.OrderBy(u => u.Fiyat).LastAsync(u => u.Id > 55);
#### LastOrDefaultAsync
Sorgu neticesinde gelen verilerden en sonuncusunu getirir. Eğer ki hiç veri gelmiyorsa null döner. OrderBy kullanılması mecburidir.

    var urun = await context.Urunler.OrderBy(u => u.Fiyat).LastOrDefaultAsync(u => u.Id > 55);
 

##3. Diğer Sorgulama Fonksiyonları
#### CountAsync
Oluşturulan sorgunun execute edilmesi neticesinde kaç adet satırın elde edileceğini sayısal olarak(int) bizlere bildiren fonksiyondur.

    var urunler = (await context.Urunler.ToListAsync()).Count();
    var urunler = await context.Urunler.CountAsync();
 
#### LongCountAsync
Oluşturulan sorgunun execute edilmesi neticesinde kaç adet satırın elde edileceğini sayısal olarak(long) bizlere bildiren fonksiyondur.

    var urunler = await context.Urunler.LongCountAsync(u => u.Fiyat > 5000);
#### AnyAsync
Sorgu neticesinde verinin gelip gelmediğini bool türünde dönen fonksiyondur. 

    var urunler = await context.Urunler.Where(u => u.UrunAdi.Contains("1")).AnyAsync();
    var urunler = await context.Urunler.AnyAsync(u => u.UrunAdi.Contains("1"));

#### MaxAsync
Verilen kolondaki max değeri getirir.

    var fiyat = await context.Urunler.MaxAsync(u => u.Fiyat);
#### MinAsync
Verilen kolondaki min değeri getirir.

    var fiyat = await context.Urunler.MinAsync(u => u.Fiyat);
#### Distinct
Sorguda mükerrer kayıtlar varsa bunları tekilleştiren bir işleve sahip fonksiyondur.

    var urunler = await context.Urunler.Distinct().ToListAsync();    
#### AllAsync
Bir sorgu neticesinde gelen verilerin, verilen şarta uyup uymadığını kontrol etmektedir. Eğer ki tüm veriler şarta uyuyorsa true, uymuyorsa false döndürecektir.

    var m = await context.Urunler.AllAsync(u => u.Fiyat < 15000);
    var m = await context.Urunler.AllAsync(u => u.UrunAdi.Contains("a"));

#### SumAsync
Vermiş olduğumuz sayısal proeprtynin toplamını alır.

    var fiyatToplam = await context.Urunler.SumAsync(u => u.Fiyat);

#### AverageAsync
Vermiş olduğumuz sayısal proeprtynin aritmatik ortalamasını alır.

    var aritmatikOrtalama = await context.Urunler.AverageAsync(u => u.Fiyat);
#### Contains
Like '%...%' sorgusu oluşturmamızı sağlar.

    var urunler = await context.Urunler.Where(u => u.UrunAdi.Contains("7")).ToListAsync();
#### StartsWith
Like '...%' sorgusu oluşturmamızı sağlar.

    var urunler = await context.Urunler.Where(u => u.UrunAdi.StartsWith("7")).ToListAsync();
#### EndsWith
Like '%...' sorgusu oluşturmamızı sağlar.

    var urunler = await context.Urunler.Where(u => u.UrunAdi.EndsWith("7")).ToListAsync();
##4. Sorgu Sonucu Dönüşüm Fonksiyonları
Bu fonksiyonlar ile sorgu neticesinde elde edilen verileri isteğimiz doğrultuusnda farklı türlerde projecsiyon edebiliyoruz.

#### ToDictionaryAsync
Sorgu neticesinde gelecek olan veriyi bir dictioanry olarak elde etmek/tutmak/karşılamak istiyorsak eğer kullanılır!

    var urunler = await context.Urunler.ToDictionaryAsync(u => u.UrunAdi, u => u.Fiyat);

ToList ile aynı amaca hizmet etmektedir. Yani, oluşturulan sorguyu execute edip neticesini alırlar.
**ToList :** Gelen sorgu neticesini entity türünde bir koleksiyona(List<TEntity>) dönüştürmekteyken,
**ToDictionary ise :** Gelen sorgu neticesini Dictionary türünden bir koleksiyona dönüştürecektir.

#### ToArrayAsync
Oluşturulan sorguyu dizi olarak elde eder.
ToList ile muadil amaca hizmet eder. Yani sorguyu execute eder lakin gelen sonucu entity dizisi  olarak elde eder.

    var urunler = await context.Urunler.ToArrayAsync();
#### Select
Select fonksiyonunun işlevsel olarak birden fazla davranışı söz konusudur,
1. Select fonksiyonu, generate edilecek sorgunun çekilecek kolonlarını ayarlamamızı sağlamaktadır. 


    var urunler = await context.Urunler.Select(u => new Urun
    {
        Id = u.Id,
        Fiyat = u.Fiyat
    }).ToListAsync();

2.Select fonksiyonu, gelen verileri farklı türlerde karşılamamızı sağlar. T, anonim



    var urunler = await context.Urunler.Select(u => new 
    {
        Id = u.Id,
        Fiyat = u.Fiyat
    }).ToListAsync();

<BR>

    var urunler = await context.Urunler.Select(u => new UrunDetay
    {
        Id = u.Id,
        Fiyat = u.Fiyat
    }).ToListAsync();

 
#### SelectMany
Select ile aynı amaca hizmet eder. Lakin, ilişkisel tablolar neticesinde gelen koleksiyonel verileri de tekilleştirip projeksiyon etmemizi sağlar.


    var urunler = await context.Urunler.Include(u => u.Parcalar).SelectMany(u => u.Parcalar, (u, p) => new
    {
        u.Id,
        u.Fiyat,
        p.ParcaAdi
    }).ToListAsync();


#### GroupBy Fonksiyonu
Gruplama yapmamızı sağlayan fonksiyondur.
#### Method Syntax


    var datas = await context.Urunler.GroupBy(u => u.Fiyat).Select(group => new
    {
        Count = group.Count(),
        Fiyat = group.Key
    }).ToListAsync();
 
#### Query Syntax

    var datas = await (from urun in context.Urunler
                       group urun by urun.Fiyat
                into @group
                       select new
                       {
                            Fiyat = @group.Key,
                           Count = @group.Count()
                       }).ToListAsync();
 

#### Foreach Fonksiyonu
Bir sorgulama fonksiyonu felan değildir!
Sorgulama neticesinde elde edilen koleksiyonel veriler üzerinde iterasyonel olarak dönmemizi ve teker teker verileri elde edip işlemler yapabilmemizi sağlayan bir fonksiyondur. foreach döngüsünün metot halidir!



    foreach (var item in datas)
    {
    
    }
    datas.ForEach(x =>
    {
    
    });

https://github.com/gncyyldz egitiminden alınıp read me kısmına cevrilmesi kendime aittir.


## Title

### Place 1

Hello, this is some text to fill in this, [here](#place-2), is a link to the second place.

### Place 2

Place one has the fun times of linking here, but I can also link back [here](#place-1).

### Place's 3: other example

Place one has the fun times of linking here, but I can also link back [here](#places-3-other-example).
