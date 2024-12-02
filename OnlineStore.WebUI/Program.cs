using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Business.AbstractServices;
using OnlineStore.Business.ConcreteServices;
using OnlineStore.DAL.Context;
using OnlineStore.DAL.Repositories;
using Microsoft.EntityFrameworkCore.SqlServer.Migrations.Internal;
using Microsoft.Win32;  
using OnlineStore.Business.Mappings;
using OnlineStore.WebUI.Mappings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OnlineStoreContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
builder.Services.AddScoped(typeof(ICommentRepository), typeof(CommentRepository));
builder.Services.AddScoped(typeof(ILikeRepository), typeof(LikeRepository));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShoppingBasketService, ShoppingBasketService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ILikeService, LikeService>();

builder.Services.AddScoped<IOrderService, OrderService>();



builder.Services.AddSession();
builder.Services.AddAutoMapper(typeof(MapperProfile), typeof(AutoMapperProfile));
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    // giriþ - çýkýþ - eriþim engeli durumlandýrýnda yönlendirilecek olan adresler.
    options.LoginPath = new PathString("/");//Anasayfaya yönlendirilecek.
    options.LogoutPath = new PathString("/");
    options.AccessDeniedPath = new PathString("/Errors/Error403");
    
});

var contentRootPath = builder.Environment.ContentRootPath;
var keysDirectory = new DirectoryInfo(Path.Combine(contentRootPath, "App_Data", "Keys"));

builder.Services.AddDataProtection()
    .SetApplicationName("OnlineStore")
    .SetDefaultKeyLifetime(new TimeSpan(999999, 0, 0, 0))
    .PersistKeysToFileSystem(keysDirectory);

// App_Data -> Keys -> içerisindeki xml dosyasýna sahip
// her proje ayný þifreleme/þifre açma yöntemi kullanacaðýndan, birbirlerinin þifrelerini açabilirler.

var app = builder.Build();
app.UseStaticFiles(); // wwwroot için
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseStatusCodePagesWithRedirects("/Errors/Error{0}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{Controller=Dashboard}/{Action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "Default",
    pattern:"{Controller=Home}/{Action=Index}/{id?}"
    );

app.Run();
