using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Areas.Admin.DesignPattern.Strategy;
using MTKPM_QuanLyKhachSan.Common.Config;
using MTKPM_QuanLyKhachSan.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IService, MyService>();

builder.Services.AddControllersWithViews();

// Kết nối đến database
builder.Services.AddDbContext<DatabaseContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DbContext")));

// Kích hoạt Session
builder.Services.AddSession();
builder.Services.AddScoped<Under5HundredRoom>();
builder.Services.AddScoped<DoubleRooms>();
builder.Services.AddScoped<StandardRooms>();

builder.Services.AddScoped<Under5HundredRoom>();
builder.Services.AddScoped<DoubleRooms>();
builder.Services.AddScoped<StandardRooms>();
builder.Services.AddSingleton<Func<string, StrategyDatabase>>(serviceProvider => key =>
{
    return key switch
    {
        "LuxuryRooms" => serviceProvider.GetService<Under5HundredRoom>(),
        "DoubleRooms" => serviceProvider.GetService<DoubleRooms>(),
        "StandardRooms" => serviceProvider.GetService<StandardRooms>(),
        _ => throw new KeyNotFoundException()
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// sử dụng session
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=PublicHome}/{action=Index}/{id?}");*/

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{area=Admin}/{controller=AdminUser}/{action=Login}/{id?}"
    );

    /*endpoints.MapControllerRoute(
      name: "Admin",
      pattern: "{area:exists}/{controller=AdminBooking}/{action=Index}/{id?}"
    );*/
});

app.Run();