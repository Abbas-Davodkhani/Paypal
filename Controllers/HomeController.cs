using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace Paypal.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

    

    
}
