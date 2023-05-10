using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Diplom_Pogodel.Models;


var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// Add configuration providers
builder.Configuration.AddJsonFile("appsettings.json", optional: false);
builder.Configuration.AddEnvironmentVariables();

// Add services to the container
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/Home/ErrorPage";
		options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Home/ErrorPage");
		options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // задаем время жизни авторизации
	});
builder.Services.AddDbContext<UserContext>(options =>options.UseSqlServer(connection)); //Добавляем строку подключения в UserContext
builder.Services.AddControllersWithViews();

// Set up middleware pipeline
var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseExceptionHandler("/Home/Error");
app.UseHsts();
app.UseStaticFiles();
app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();// аутентификация
app.UseAuthorization();// авторизация


app.MapControllers();
app.UseEndpoints(endpoints =>
{//Шаблон для работы-(базовое значение)
    endpoints.MapControllerRoute(
       name: "default",
       pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();