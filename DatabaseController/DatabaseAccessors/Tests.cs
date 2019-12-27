using Bindings;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseController.DatabaseAccessors
{
    public static class Tests
    {
        public static List<Test> GetAllTests(DatabaseConnection dbc = null)
        {
            if (dbc == null)
                dbc = new DatabaseConnection();

            if (dbc.CheckConnection())
            {
                string query = "SELECT * FROM Test";
                MySqlCommand cmd = new MySqlCommand(query, dbc.connection);

                var r = ReadTests(cmd.ExecuteReader());
                dbc.CloseConnection();
                return r;
            }
            else return null;
        }

        public static Test GetTestByID(ulong id, DatabaseConnection dbc = null)
        {
            if (dbc == null)
                dbc = new DatabaseConnection();

            if (dbc.CheckConnection())
            {
                string query = "SELECT * FROM Test WHERE ID = @id";
                MySqlCommand cmd = new MySqlCommand(query, dbc.connection);
                cmd.Parameters.AddWithValue("@id", id);

                var r = ReadTests(cmd.ExecuteReader());
                dbc.CloseConnection();
                if (r.Count >= 1)
                    return r.First();
                else return null;
            }
            else return null;
        }

        public static Test GetTestsFromTimeStamp(ulong timeStamp, DatabaseConnection dbc = null)
        {
            if (dbc == null)
                dbc = new DatabaseConnection();

            if (dbc.CheckConnection())
            {
                string query = "SELECT * FROM Test WHERE ID >= @ts";
                MySqlCommand cmd = new MySqlCommand(query, dbc.connection);
                cmd.Parameters.AddWithValue("@ts", timeStamp);

                var r = ReadTests(cmd.ExecuteReader());
                dbc.CloseConnection();
                return r.First();
            }
            else return null;
        }

        static List<Test> ReadTests(MySqlDataReader reader)
        {
            List<Test> tests = new List<Test>();

            while (reader.Read())
            {
                tests.Add(new Test
                {
                    ID = (ulong)reader["ID"],
                    timeStamp = (ulong)reader["TimeStamp"],
                    subjectID = (ulong)reader["Subject"],
                    type = (TestType)(uint)reader["Type"],
                    comment = (string)reader["Comment"]
                });
            }
            reader.Close();

            return tests;
        }
    }
}
