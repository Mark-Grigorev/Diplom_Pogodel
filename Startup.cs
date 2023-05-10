using Diplom_Pogodel.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace Diplom_Pogodel
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));
            // установка конфигурации подключения в контекст который нам необходим
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {//CookieAuthenticationOptions
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Home/ErrorPage");
                    //Для установки аутентификации с помощью куки в вызов services.AddAuthentication передается значение
                    //CookieAuthenticationDefaults.AuthenticationScheme. Далее с помощью метода AddCookie() настраивается аутентификация.
                    //По сути в этом методе производится конфигурация объекта CookieAuthenticationOptions,
                    //который описывает параметры аутентификации с помощью кук. В частности, в данном случае использовано одно свойство
                    //CookieAuthenticationOptions - LoginPath. Это свойство устанавливает
                    //относительный путь, по которому будет перенаправляться анонимный пользователь при доступе к ресурсам, для которых нужна аутентификация.
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Home/ErrorPage");
                });
            services.AddControllersWithViews();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
         
            app.UseRouting();

            app.UseAuthentication();// аутентификация
            app.UseAuthorization();// авторизация

           

            app.UseEndpoints(endpoints =>
            {//Шаблон для работы-(базовое значение)
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            
        }
    }
}