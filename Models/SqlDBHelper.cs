using Npgsql;

namespace tugas2praktikum.Models
{
    internal class SqlDBHelper
    {
        private string constr;

        public SqlDBHelper(string constr)
        {
            this.constr = constr;
        }

        internal void closeConnection()
        {
            throw new NotImplementedException();
        }

        internal NpgsqlCommand GetNpgsqlCommand(string query)
        {
            throw new NotImplementedException();
        }
    }
}