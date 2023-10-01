using Microsoft.AspNetCore.Mvc; 

namespace ProposalRequestLogging.Web.Controllers
{
    public class HomeController : Controller
    {       
        public IActionResult Index()
        {
            return View();
        } 
    }
}
