# ProductProject

Controller Katmanı

BaseController sınıfı, requestlerin ilk geldiği katmandır. Bu sınıf, TEntity ve TEntityService türleriyle çalışır. 
TEntity türü, veri tabanındaki bir tablonun özelliklerini sağlar.Projede oluşturulan diğer Controller’lar bu controller sınıfından türetilmiştir.
BaseController sınıfı, TEntityService türünde bir Service sınıfını parametre olarak alır ve bu sınıfı service değişkenine atar. 
Bu sayede BaseController sınıfı içerisinde TEntityService türünde bir Service sınıfının metotlarına erişilebilir.
BaseController sınıfı ayrıca IBaseModel arayüzünü uygular. Bu arayüz, veri tabanındaki bir tablonun Id özelliğine sahip olmasını sağlar.
Bu sayede veri tabanındaki bir tablo için Id özelliğine sahip bir sınıf oluşturulabilir ve bu sınıf üzerinden veri tabanı işlemleri gerçekleştirilebilir.



Service Katmanı

Service sınıfı, veri tabanı işlemlerinin gerçekleştirilmesi için kurgulanmış bir katmandır. 
Bu sınıf, IRepository ve IUnitOfWork arayüzlerini kullanarak veri tabanı işlemlerini gerçekleştirir. 
Bu arayüzler, veri tabanı işlemlerini gerçekleştirirken kullanılacak metotları sağlar.
Service sınıfı, TEntity türünde bir sınıfın özelliklerine sahip bir parametre alır ve bu parametre IBase arayüzünü implemente etmiş olmalıdır. 
Bu sayede TEntity türündeki sınıflar IBase arayüzünü kullanarak veri tabanı işlemleri gerçekleştirilebilir.
Service sınıfının metotları arasında, veri tabanındaki bir kaydı çekmek için Get() metodu, veri tabanına yeni bir kayıt eklemek için Insert() metodu, 
veri tabanında bulunan bir kaydı güncellemek için Update() metodu, veri tabanındaki bir kaydı silmek için Delete() metodu ve 
veri tabanında arama yapmak için Search() metodu bulunur. Bu metotların yanı sıra, veri tabanındaki değişiklikleri kaydetmek için Commit() metodu ve 
veri tabanındaki değişiklikleri geri almak için Rollback() metodu da bulunur.
Service sınıfı ayrıca, veri tabanı işlemleri öncesi ve sonrasında çalıştırılacak metotları sağlar. 
Örneğin, Insert() metodu öncesinde PreInsert() metodu çalıştırılır ve Insert() metodu sonrasında PostInsert() metodu çalıştırılır. 
Bunun yanı sıra, veri tabanı işlemleri öncesi ve sonrasında çalışacak olan PreSave() ve PostSave() metotları da mevcuttur. 
Bu metotlar, veri tabanı işlemleri öncesi ve sonrasında çalıştırılacak olan işlemleri gerçekleştirir.

Repository Katmanı

BaseRepository sınıfı, veri tabanı işlemlerinin gerçekleştirilebileceği bir sınıftır. 
Bu sınıf, Microsoft.EntityFrameworkCore kütüphanesini kullanarak veri tabanı işlemlerini gerçekleştirir.
BaseRepository sınıfı, TEntity türünde bir sınıfın özelliklerine sahip bir parametre alır ve bu parametre IBase arayüzünü implemente etmiş olmalıdır.
Bu sayede TEntity türündeki sınıflar IBase arayüzünü kullanarak veri tabanı işlemleri gerçekleştirilebilir.
BaseRepository sınıfının metotları arasında, veri tabanına yeni bir kayıt eklemek için Add() metodu, 
veri tabanında bulunan bir kaydı güncellemek için Update() metodu ve veri tabanındaki değişiklikleri kaydetmek için SaveChanges() metodu bulunur.
Ayrıca, veri tabanında arama yapmak için Search() metodu, veri tabanındaki kayıtların listesi için Queryable() metodu ve 
veri tabanındaki bir kaydı çekmek için Load() metodu da bulunur.
BaseRepository sınıfı ayrıca, nesnelerin serbest bırakılmasını sağlamak için Dispose() metodunu ve 
bu metodun yardımcı metodu olan Dispose(bool disposing) metodunu içerir. Bu metotlar, sınıfın kullandığı kaynakların serbest bırakılmasını sağlar.

UnitOfWork Design Pattern

Projede UnitOfWork design patterni kullanmayı tercih ettim. 
Entity üzerinde yapılan her değişikliğin anlık olarak database e yansıması yerine, işlemlerin toplu halde tek bir kanaldan gerçekleşmesini sağlamak için UnitOfWork design patterni kullanmayı tercih ettim. 
İşlemler tek bir kanaldan(tek bir transaction) toplu halde yapıldığı için performansı artı yönde etkileyecektir.Ayrıca işlemleri geri alma(rollback), 
hangi tabloda ne işlem yapıldı,kaç kayıt eklendi gibi sorulara da cevap verebilir olması tercihimde etkili olmuştur.

Xunit ile Unit Test

Projede yazdığım unit testleri için Xunit kütüphanesini kullandım.
Hem daha önce kullandığım bir kütüphane olduğu için hem de testleri paralel olarak çalıştırma ve testleri etiketlendirerek gruplandırma fonksiyonları bulunduğu için Xunit kütüphanesini tercih ettim.

Özetle;

-	Öncelikle controller katmanındaki metotlara requestler gelmektedir.
-	Sonrasında belli validationlar yapıldıktan sonra requestler service katmanına aktarılmaktadır.Burada entityleri modellere çevrilebilmek için AutoMapper’i tercih ettim.
-	Service katmanında entity ile ilgili gerekli işlemler yapıldıktan sonra Repository katmanına veritabanı işlemleri yapılması için akış gerçekleşir.
-	Repository katmanında gerekli database işlemleri yapıldıktan sonra aynı şekilde service katmanına ve oradan da controller katmanına ters akış gerçekleşerek response döner.Response’lar içinde HttpStatusCode’ların dönülmesine dikkat edilmiştir.
-	Tüm katmanlar generic olacak şekilde tasarlandı ve interface kullanıldı.
-	IOC Container efektif bir şekilde kullanılmıştır.
-	SOLID prensiplerine uyulmaya çalışılmıştır.
