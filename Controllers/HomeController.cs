using attempt2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace attempt2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //--------------------------------------------------------------------------------
        //
        //
        //Wykonuje się po naciśnięciu przycisku "Calculate" w formularzu
        [HttpPost]
        public IActionResult Calculate(Calculator calc)
        {
            switch (calc.Operation)
            {
                // Dodawanie
                case "+":
                    calc.Result = calc.Number1 + calc.Number2;
                    break;

                // Odejmowanie
                case "-":
                    calc.Result = calc.Number1 - calc.Number2;
                    break;

                // Mnożenie
                case "*":
                    calc.Result = calc.Number1 * calc.Number2;
                    break;

                //Dzielenie
                case "/":
                    if (calc.Number2 != 0)
                    {
                        calc.Result = calc.Number1 / calc.Number2;
                    }
                    else
                    {
                        // Ustawienie komunikatu błędu (jeśli próbujemy dzielić przez zero)
                        ViewBag.ErrorMessage = "Nie można dzielić przez zero!";
                        return View("Index");
                    }
                    break;

                // Obsługa nieznanego działania
                default:
                    // Ustawienie komunikatu błędu gdy działanie jest nieznane
                    ViewBag.ErrorMessage = "Nieznane działanie!";
                    return View("Index");
            }

            // Przekierowanie do widoku "Result"
            return View("Result", calc);
        }



    }
}
