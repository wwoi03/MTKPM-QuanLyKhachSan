using Microsoft.EntityFrameworkCore;
using MTKPM_QuanLyKhachSan.Daos;
using MTKPM_QuanLyKhachSan.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

// Kết nối đến database
builder.Services.AddDbContext<DatabaseContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DbContext")));

// Kích hoạt Session
builder.Services.AddSession();

//Kích hoạt dịch vụ cho ServiceDao, BookRoomDao, BookRoomDetailsDao, IRepository<ServiceDao>
builder.Services.AddScoped<ServiceDao>();
builder.Services.AddScoped<BookRoomDetailsDao>();
builder.Services.AddScoped<BookRoomDao>();
builder.Services.AddScoped<IRepository<Service>, ServiceDao>();

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
        pattern: "{area=Admin}/{controller=AdminBooking}/{action=Index}/{id?}"
    );

    /*endpoints.MapControllerRoute(
      name: "Admin",
      pattern: "{area:exists}/{controller=AdminBooking}/{action=Index}/{id?}"
    );*/
});

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "areaRoute",
		pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");
});

// Thêm các route cho chức năng quản lý menu
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "adminService",
        pattern: "{area=Admin}/{controller=AdminService}/{action=Index}/{id?}"
    );
});

app.Run();