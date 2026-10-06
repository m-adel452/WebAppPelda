using System.Diagnostics;                                                                                                           
using System.Security.Cryptography.Xml;                                                                                             
using Microsoft.AspNetCore.Mvc;                                                                                                     
using WebAppPélda.Models;                                                                                                           
using WebAppPelda.Models;                                                                                                          
using WebAppPelda.Service;                                                                                                          

namespace WebAppPélda.Controllers                                                                                                   
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

        public IActionResult Privacy()                                                                                             
        {
            return View();                                                                                                          
        }

        public IActionResult Sajat()                                                                                                
        {
            List<Vasarlo> vasarlok = new VasarloService().GetAllVasarlo();                                                          
            return View(vasarlok);                                                                                                  
        }

        public IActionResult Vasarlo(int id)                                                                                        
        {
            Vasarlo kivalasztott=new VasarloService().GetById(id);                                                                   
            return View(kivalasztott);                                                                                              
        }                                                                                                                           

        //Üres vásárló létrehozás a GET kérésre, majd a POST kérés feldolgozása
        [HttpGet]                                                                                                                  
        public IActionResult UjVasarlo()                                                                                              
        {
            var model=new Vasarlo();                                                                                                    
            return View(model);                                                                                                      
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //CSRF elleni védelem
        public IActionResult UjVasarlo(Vasarlo vasarlo)                                                                                  
        {
            // Ha a szerveroldali validáció megbukik
            if (!ModelState.IsValid)
            {
                return View(vasarlo);
            }
            
            //Adatbázisba mentés logikája
            string result = new VasarloService().PostVasarlo(vasarlo);

            TempData["SuccessMessage"] = result;

            // Átirányitás a sikere jelzésre (PRG pattern)
            return RedirectToAction(nameof(UjVasarlo));
        }

        [HttpGet]
        public IActionResult VasarloModositas(int id)
        {
            var model = new VasarloService().GetById(id);
            return View(model);
        }

        //PUT de formban csak POST lehet
        [HttpPost]
        [ValidateAntiForgeryToken] //CSRF elleni védelem
        public IActionResult VasarloModositas(Vasarlo vasarlo)
        {
            // Ha a szerveroldali validáció megbukik
            if (!ModelState.IsValid)
            {
                return View(vasarlo);
            }

            //Adatbázisba mentés logikája
            string result = new VasarloService().PutVasarlo(vasarlo);
            TempData["SuccessMessage"] = result;

            // Átirányitás a sikere jelzésre (PRG pattern)
            return RedirectToAction(nameof(VasarloModositas));
        }

        [HttpGet]
        public IActionResult VasarloTorlese(int id)
        {
            var model = new VasarloService().GetById(id);
            return View(model);
        }

        //DELETE de formban csak POST lehet
        [HttpPost]
        [ValidateAntiForgeryToken] //CSRF elleni védelem
        public IActionResult VasarloTorlese(Vasarlo vasarlo)
        {
            // Ha a szerveroldali validáció megbukik
            if (!ModelState.IsValid)
            {
                return View(vasarlo);
            }

            //Adatbázisba mentés logikája
            string result = new VasarloService().DeleteVasarlo(vasarlo);
            TempData["SuccessMessage"] = result;

            // Átirányitás a sikere jelzésre (PRG pattern)
            return RedirectToAction(nameof(VasarloTorlese));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]                                       
        public IActionResult Error()                                                                                                // Hibakezelő oldal megjelenítése
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });                    // Kilépek a progragmomból, és a hibát a hibakezelő oldalra irányítom, ahol az ErrorViewModel osztályt használom a hibaüzenet megjelenítésére                    
        }
    }
}
