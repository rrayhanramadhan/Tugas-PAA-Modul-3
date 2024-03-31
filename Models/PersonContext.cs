using Npgsql;
using System;
using System.Collections.Generic;
using tugas2praktikum.Helpers;

namespace tugas2praktikum.Models
{
    public class PersonContext
    {
        private string __constr;
        private string __ErrorMsg;

        public PersonContext(string pConstr)
        {
            __constr = pConstr;
        }

        // Read (Select)
        public List<Student> ListStudent()
        {
            List<Student> list1 = new List<Student>();
            string query = string.Format(@"SELECT id_mahasiswa, nama, nim, alamat FROM users.student;");
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list1.Add(new Student()
                    {
                        id_mahasiswa = int.Parse(reader["id_mahasiswa"].ToString()),
                        nama = reader["nama"].ToString(),
                        nim = reader["nim"].ToString(),
                        alamat = reader["alamat"].ToString()
                    });
                }
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }
            return list1;
        }

        public bool AddStudent(Student student)
        {
            string query = string.Format(@"INSERT INTO users.student (nama, nim, alamat) VALUES (@nama, @nim, @alamat)");
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama", student.nama);
                cmd.Parameters.AddWithValue("@nim", student.nim);
                cmd.Parameters.AddWithValue("@alamat", student.alamat);
                int rowsAffected = cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
                return false;
            }
        }

        public bool UpdateStudent(int id, Student updatedStudent)
        {
            string query = string.Format(@"UPDATE users.student SET nama = @nama, nim = @nim, alamat = @alamat WHERE id_mahasiswa = @id");
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nama", updatedStudent.nama);
                cmd.Parameters.AddWithValue("@nim", updatedStudent.nim);
                cmd.Parameters.AddWithValue("@alamat", updatedStudent.alamat);
                int rowsAffected = cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
                return false;
            }
        }

        public bool DeleteStudent(int id)
        {
            string query = string.Format(@"DELETE FROM users.student WHERE id_mahasiswa = @id");
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                int rowsAffected = cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
                return false;
            }
        }

        internal List<Person> ListPerson()
        {
            throw new NotImplementedException();
        }

        internal void UpdatePerson(Person person)
        {
            throw new NotImplementedException();
        }

        internal void DeletePerson(int id)
        {
            throw new NotImplementedException();
        }

        internal void AddPerson(Person person)
        {
            throw new NotImplementedException();
        }
    }
}