using Bindings;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseController.DatabaseAccessors
{
    public static class Subjects
    {
        public static List<Subject> GetAllSubjects(DatabaseConnection dbc = null)
        {
            if (dbc == null)
                dbc = new DatabaseConnection();

            if (dbc.CheckConnection())
            {
                string query = "SELECT * FROM Subject";
                MySqlCommand cmd = new MySqlCommand(query, dbc.connection);

                var r = ReadSubjects(cmd.ExecuteReader());
                dbc.CloseConnection();
                return r;
            }
            else return null;
        }

        public static Subject GetSubjectByID(ulong id, DatabaseConnection dbc = null)
        {
            if (dbc == null)
                dbc = new DatabaseConnection();

            if (dbc.CheckConnection())
            {
                string query = "SELECT * FROM Subject WHERE ID = @id";
                MySqlCommand cmd = new MySqlCommand(query, dbc.connection);
                cmd.Parameters.AddWithValue("@id", id);

                var r = ReadSubjects(cmd.ExecuteReader());
                dbc.CloseConnection();
                if (r.Count >= 1)
                    return r.First();
                else return null;
            }
            else return null;
        }

        static List<Subject> ReadSubjects(MySqlDataReader reader)
        {
            List<Subject> subjects = new List<Subject>();

            while (reader.Read())
            {
                subjects.Add(new Subject
                {
                    ID = (ulong)reader["ID"],
                    Name = (string)reader["Name"]
                });
            }
            reader.Close();

            return subjects;
        }
    }
}
