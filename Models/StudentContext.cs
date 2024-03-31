using Npgsql;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace tugas2praktikum.Models
{
    public class StudentContext
    {
        private string __constr;
        private string __ErrorMsg;
        private string __errormsg;

        public StudentContext(string pConstr)
        {
            __constr = pConstr;
        }
        public List<Login> Authentifikasi(string p_username, string p_password, IConfiguration p_config)
        {
            List<Login> list1 = new List<Login>();

            string query = string.Format(@"SELECT ps.id_person, ps.nama, ps.alamat, ps.email, pp.id_peran, p.nama_peran
                FROM public.person ps
                INNER JOIN public.peran_person pp ON ps.id_person=pp.id_person
                INNER JOIN public.peran p ON pp.id_peran=p.id_peran
                where ps.username='{0}' and ps.password='{1}'; ", p_username, p_password);
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    list1.Add(new Login()
                    {
                        id_person = int.Parse(reader["id_person"].ToString()),
                        nama = reader["nama"].ToString(),
                        alamat = reader["alamat"].ToString(),
                        email = reader["email"].ToString(),
                        id_peran = int.Parse(reader["id_peran"].ToString()),
                        nama_peran = reader["nama_peran"].ToString(),
                        token = GenerateJwtToken(p_username, p_password, p_config)


                    });
                }

                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __errormsg = ex.Message;
            }
            return list1;
        }

        internal List<Login> Authentifikasi(string namaUser, string password, object config)
        {
            throw new NotImplementedException();
        }

        private string GenerateJwtToken(string namaUser, string peran, IConfiguration pConfig)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(pConfig["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, namaUser),
                new Claim(ClaimTypes.Role, peran),
            };
            var token = new JwtSecurityToken(pConfig["Jwt:Issuer"],
                pConfig["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

