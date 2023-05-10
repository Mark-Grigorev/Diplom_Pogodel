using Diplom_Pogodel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Diplom_Pogodel.Controllers
{
	public class HomeController : Controller
	{
        public IActionResult Index() {
           if (User.Identity.IsAuthenticated) {            //В случае если пользователь авторизовался, его редиректит на главную
                return RedirectToAction("RequestFormPage", "Weather");  //Рабочую страницу
            }
            return View();                                  //В любом другом случае, можно переходить только по статическим страницам
        }

        public IActionResult AboutUsPage()
        {            //Функции, для перехода на другие страницы(Статические страницы)
            return PartialView();
        }
        public IActionResult ContactsPage()
        {
            return PartialView();
        }
       /* public IActionResult AuthorizationPage()
        {
            return PartialView();
        }*/

        

        
	}
}