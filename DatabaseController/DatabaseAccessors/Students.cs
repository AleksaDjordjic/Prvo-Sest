using Bindings;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseController.DatabaseAccessors
{
    public static class Students
    {
        public static List<Student> GetAllStudents(DatabaseConnection dbc = null)
        {
            if (dbc == null)
                dbc = new DatabaseConnection();

            if (dbc.CheckConnection())
            {
                string query = "SELECT * FROM Student";
                MySqlCommand cmd = new MySqlCommand(query, dbc.connection);

                var r = ReadStudents(cmd.ExecuteReader());
                dbc.CloseConnection();
                return r;
            }
            else return null;
        }

        public static Student GetStudentByID(ulong id, DatabaseConnection dbc = null)
        {
            if (dbc == null)
                dbc = new DatabaseConnection();

            if (dbc.CheckConnection())
            {
                string query = "SELECT * FROM Student WHERE ID = @id";
                MySqlCommand cmd = new MySqlCommand(query, dbc.connection);
                cmd.Parameters.AddWithValue("@id", id);

                var r = ReadStudents(cmd.ExecuteReader());
                dbc.CloseConnection();
                if (r.Count >= 1)
                    return r.First();
                else return null;
            }
            else return null;
        }

        public static Student GetStudentByDiscordID(ulong discordID, DatabaseConnection dbc = null)
        {
            if (dbc == null)
                dbc = new DatabaseConnection();

            if (dbc.CheckConnection())
            {
                string query = "SELECT * FROM Student WHERE DiscordID = @did";
                MySqlCommand cmd = new MySqlCommand(query, dbc.connection);
                cmd.Parameters.AddWithValue("@did", discordID);

                var r = ReadStudents(cmd.ExecuteReader());
                dbc.CloseConnection();
                if (r.Count >= 1)
                    return r.First();
                else return null;
            }
            else return null;
        }

        static List<Student> ReadStudents(MySqlDataReader reader)
        {
            List<Student> students = new List<Student>();

            while (reader.Read())
            {
                students.Add(new Student
                {
                    ID = (ulong)reader["ID"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    DiscordID = (ulong)reader["DiscordID"]
                });
            }
            reader.Close();

            return students;
        }
    }
}
