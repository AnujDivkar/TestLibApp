using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestApp.Models;
using System.Runtime.InteropServices;
using System.Text;
namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        [DllImport("MathLib.dll")]
        public static extern void fibonacci_init(int a, int b);
        [DllImport("MathLib.dll")]
        public static extern int fibonacci_index();
        [DllImport("MathLib.dll")]
        public static extern int fibonacci_current();
        [DllImport("MathLib.dll")]
        public static extern bool fibonacci_next();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int places = 5)
        {
            StringBuilder sb = new StringBuilder("Fibonacci Numbers upto " + places + " places:");
            sb.AppendLine();
            fibonacci_init(0,1);

            // Write out the sequence values until overflow.
            do
            {
                sb.Append(fibonacci_current()+ " ");
                fibonacci_next();

            } while (fibonacci_index() < places);

            Console.WriteLine(sb);
            ViewBag.Message = sb.ToString();

            return View(new FiboModel { Places = places });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}