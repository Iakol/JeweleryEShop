using Jewelery.data;
using Jewelery.Infrastructure.Action;
using Jewelery.Infrastructure.Binder.ProductBinder;
using Jewelery.Infrastructure.Enums;
using Jewelery.Infrastructure.Exeption;
using Jewelery.Infrastructure.Localization;
using Jewelery.Models.Identity_model;
using Jewelery.Servise.Cart_itemServise;
using Jewelery.Servise.CartServise;
using Jewelery.Servise.CategoryServise;
using Jewelery.Servise.DetermineTheSizeService;
using Jewelery.Servise.FAQServise;
using Jewelery.Servise.FashionService;
using Jewelery.Servise.IdentityService;
using Jewelery.Servise.ImageService;
using Jewelery.Servise.NovaPostServise;
using Jewelery.Servise.OrderDetailService;
using Jewelery.Servise.OrderService;
using Jewelery.Servise.ProductServise;
using Jewelery.Servise.SessionService;
using Jewelery.Servise.SubCategoryServise;
using Jewelery.Servise.UserService;
using Jewelery.Servise.WayToPayService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ConstantCategotryDisplaySettings.json", optional: true, reloadOnChange: true);



builder.Services.AddDbContext<AppDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHttpClient();


builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDBContext>();

builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddScoped<ICategoryServise, CategoryServise>();
builder.Services.AddScoped<ISubCategoryServise, SubCategoryServise>();

builder.Services.AddScoped<IOptionServise, OptionServise>();
builder.Services.AddScoped<IAtributeServise, AtributeServise>();
builder.Services.AddScoped<IProductServise, ProductServise>();


builder.Services.AddScoped<IIdentityServise, IdentityServise>();

builder.Services.AddScoped<ICart_itemServise, Cart_itemServise>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<ICartServise, CartServise>();

builder.Services.AddSingleton<CategoryConstantDisplayService>();

builder.Services.AddScoped<INovaPostServise, NovaPostServise>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IFAQService, FAQService>();
builder.Services.AddScoped<IFashionService, FashionService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IDetermineTheSizeService, DetermineTheSizeService>();






builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(6);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});












// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(JeweleryExceptionFilterAttribute));
    options.Filters.Add(typeof(ViewFilter));
    options.ModelBinderProviders.Insert(0, new ProductCSMDTOBinderProvider());

}
);





var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRequestLocalization(new RequestLocalizationOptions()
    .SetDefaultCulture("uk")
    .AddSupportedCultures(Enum.GetNames(typeof(LanguageEnums)))
    .AddSupportedUICultures(Enum.GetNames(typeof(LanguageEnums))));

app.UseMiddleware<LocalizationMiddleware>();

app.UseHttpsRedirection();


app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.UseMiddleware<JeweleryExceptionMiddleWare>();








app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");





app.Run();

