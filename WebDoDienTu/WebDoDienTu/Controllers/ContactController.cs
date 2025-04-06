using Microsoft.AspNetCore.Mvc;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace WebDoDienTu.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
