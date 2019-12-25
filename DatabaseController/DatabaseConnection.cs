using MySql.Data.MySqlClient;

namespace DatabaseController
{
    public class DatabaseConnection
    {
        public MySqlConnection connection;

        public DatabaseConnection()
        {
            Initialize();
        }

        private void Initialize()
        {
#if DEBUG
            string server = "192.168.1.234";
#else
            string server = "localhost";
#endif
            string uid = "prvo_sest";
            string password = "tCewWKmYJwQDAWcfm3GH42usHHsUxraVjfjSaahPnvz2Nt5U4d2YTjK7SK3YxEMNbcgvfdRAP8cYc9JXqEECbqNM3C2XhALQgxS97ybjzh3amRffMYDu8QEvMPQb36UX";
            string port = "3306";
            string database = "prvo_sest";

            string connectionString =
                "SERVER=" + server + ";" +
                "PORT=" + port + ";" +
                "DATABASE=" + database + ";" +
                "UID=" + uid + ";" +
                "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                return false;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                return false;
            }
        }

        public bool CheckConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                return true;

            return OpenConnection();
        }

    }
}