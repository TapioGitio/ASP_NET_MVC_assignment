﻿using Microsoft.AspNetCore.Mvc;

namespace AlphaWebApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [Route("Register")]
    public IActionResult Register()
    {
        return View();
    }

}
