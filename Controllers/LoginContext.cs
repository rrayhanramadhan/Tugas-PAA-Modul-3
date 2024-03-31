using tugas2praktikum.Models;

namespace tugas2praktikum.Controllers
{
    internal class LoginContext
    {
        private string constr;

        public LoginContext(string constr)
        {
            this.constr = constr;
        }

        internal List<Login> Authentifikasi(string namaUser, string password, IConfiguration config)
        {
            throw new NotImplementedException();
        }
    }
}