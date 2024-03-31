using Microsoft.AspNetCore.Mvc;
using tugas2praktikum.Models;

namespace tugas2praktikum.Controllers
{
    public class LoginController : Controller
    {
        private string __constr;
        private IConfiguration __config;

        public LoginController(IConfiguration configuration)
        {
            __config = configuration;
            __constr = configuration.GetConnectionString("WebApiDatabase");
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("api/login")]

        public IEnumerable<Login> LoginUser(string namaUser, string password)
        {
            LoginContext context = new LoginContext(this.__constr);
            List<Login> listLogin = context.Authentifikasi(namaUser, password, __config);
            return (listLogin).ToArray();
        }
    }
}
