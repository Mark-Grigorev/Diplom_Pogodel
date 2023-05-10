using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using Diplom_Pogodel.Models;// пространство имен UserContext и класса User
using Diplom_Pogodel.Helpers;
using Diplom_Pogodel.ViewModels;// пространство имен моделей RegisterModel и LoginModel
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Reflection.Metadata.Ecma335;

namespace Diplom_Pogodel.Controllers
{
    public class AccountController : Controller
    {


        //private string connectionString = "Server=DESKTOP-7NHBT9K\\MARK; Database=Pogoda;Trusted_Connection=True;";
        //Строка подключения к Базе данных, мб пригодится


        private UserContext db;

        public AccountController(UserContext context)
        {
            db = context;
        }

        //-------------------------------------------Авторизация------------------------------



        public IActionResult AuthorizationPage()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Authorization(string Login, string Password)
        {       //Ошибка 500, что-то не так с бэком
            //ошибка 404 - нет файла(или чего либо)
            //Ошибка 400 - трабл 
            bool find = false;
            string hashPassword = HashPasswordHelper.HashPassword(Password);
            //Производим проверку, есть ли данный логин, и захэшированный пароль
            User user = await db.User.FirstOrDefaultAsync(u => u.Login == Login && u.Password == hashPassword);
            if (user != null)
            {
                find = true;
                await AuthenticateToUser(Login); //Аутентификация
                //return RedirectToAction("RequestFormPage", "Weather");

            }

            return Json(find);
        }



        public IActionResult RegistrationPage()
        {
            return PartialView();
        }

		//-------------------------------------------Регистрация/Аутентификация/Выход------------------------------
		[HttpPost]
        public async Task<IActionResult> Register(string Login, string Password, string Name, string Phone)
        {
            
            User user = await db.User.FirstOrDefaultAsync(u => u.Login == Login);
              if (user == null)
            {  
             string hashPassword_reg = HashPasswordHelper.HashPassword(Password);
            //добавляем пользователя в бд, и хэшируем пароль пользователя
             db.User.Add(new User { Login = Login, Password = hashPassword_reg, Name = Name, Phone = Phone });
              await db.SaveChangesAsync();
			return PartialView("Index", "Home"); ;
            }
            return View("ErrorPage","Home");


        }





        private async Task AuthenticateToUser(string login)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        public async Task<string> GetCityKey(string cityName)
        {
            string apiKey = "8xCYbx31vzStcHLGY0WQqbacjPgkv8wm";
            string url = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={apiKey}&q={cityName}";

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var cities = JsonConvert.DeserializeObject<List<CityInfo>>(json);

                    var cityKey = cities.FirstOrDefault()?.Key;
                    if (cityKey != null)
                    {
                        return cityKey;
                    }
                    else
                    {
                        throw new Exception("City not found.");
                    }
                }
                catch (Exception ex)
                {
                    // Обработка исключения
                    Console.WriteLine($"Error: {ex.Message}");
                    return null;
                }
            }
        }
    }

}

