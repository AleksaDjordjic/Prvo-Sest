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

        public static List<Test> GetTestsFromTimeStamp(ulong timeStamp, DatabaseConnection dbc = null)
        {
            if (dbc == null)
                dbc = new DatabaseConnection();

            if (dbc.CheckConnection())
            {
                string query = "SELECT * FROM Test WHERE TimeStamp >= @ts";
                MySqlCommand cmd = new MySqlCommand(query, dbc.connection);
                cmd.Parameters.AddWithValue("@ts", timeStamp);

                var r = ReadTests(cmd.ExecuteReader());
                dbc.CloseConnection();
                return r;
            }
            else return null;
        }

        public static ulong AddTest(Test test, DatabaseConnection dbc = null)
        {
            if (dbc == null)
                dbc = new DatabaseConnection();

            if (dbc.CheckConnection())
            {
                string query = "INSERT INTO Test (TimeStamp, Subject, Type, Comment) " +
                    "VALUES(@time, @sub, @type, @comm); SELECT LAST_INSERT_ID();";
                MySqlCommand cmd = new MySqlCommand(query, dbc.connection);
                cmd.Parameters.AddWithValue("@time", test.timeStamp);
                cmd.Parameters.AddWithValue("@sub", test.subjectID);
                cmd.Parameters.AddWithValue("@type", test.type);
                cmd.Parameters.AddWithValue("@comm", test.comment);

                var r = (ulong)cmd.ExecuteScalar();
                dbc.CloseConnection();
                return r;
            }
            else return 0;
        }

        public static bool RemoveTestByID(ulong id, DatabaseConnection dbc = null)
        {
            if (dbc == null)
                dbc = new DatabaseConnection();

            if (dbc.CheckConnection())
            {
                string query = "DELETE FROM Test WHERE ID = @id";
                MySqlCommand cmd = new MySqlCommand(query, dbc.connection);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                dbc.CloseConnection();
                return true;
            }
            else return false;
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
