/*using expense_tracker.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DI
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();*/

//new

using expense_tracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; // 1. 記得加入這行引用

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DI (資料庫連線)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

// ==========================================
//  ↓↓↓ 2. 新增這段：註冊 Identity 服務 ↓↓↓
// ==========================================
// 必須改成您的自訂使用者類別 ApplicationUser
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
// 設定密碼強度 (為了測試方便，這裡設得很簡單)
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false; // 不需要特殊符號
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4; // 密碼只要4碼
});
// ==========================================


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ==========================================
//  ↓↓↓ 3. 新增這行：啟用認證 ↓↓↓
//  (必須放在 UseRouting 之後，UseAuthorization 之前)
// ==========================================
app.UseAuthentication();
// ==========================================

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    // 修改這裡：預設改去 Account/Login
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
