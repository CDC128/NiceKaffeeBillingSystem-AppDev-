using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace NiceKaffee
{
    public class ConnectionQuery
    {
        string connectionString1 = "server=52.78.127.115;port=57000;database=nicekaffeebillingsystem;uid=root;pwd=pass;";
        string connectionString2 = "server='0.tcp.ap.ngrok.io';port=19344;database=nicekaffeebillingsystem;uid=root;pwd=shia1931;";
        string connectionString = "";
        public MySqlConnection conn;

        public string TestConn()
        {
            try {
                conn = new MySqlConnection(connectionString2);
                conn.Open();
                conn.Close();
                connectionString = connectionString2;
            }
            catch(Exception e)
            {
                conn = new MySqlConnection(connectionString1);
                conn.Open();
                conn.Close();
                connectionString = connectionString1;
            }
            
            return connectionString;
        }

        public string Connection()
        {
            return TestConn();
        }

        public void OpenConnection()
        {
            conn = new MySqlConnection(TestConn());
            conn.Open();
        }
        public void CloseConnection()
        {
            conn.Close();
        }
        public void ExecuteQueries(string Query_)
        {
            MySqlCommand cmd = new MySqlCommand(Query_, conn);
            cmd.ExecuteNonQuery();
        }
        public MySqlDataReader DataReader(string Query_)
        {
            MySqlCommand cmd = new MySqlCommand(Query_, conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }
        public DataTable ShowDataInGridView(string Query_)
        {
            MySqlCommand cmd = new MySqlCommand(Query_, conn);
            DataTable ds = new DataTable();
            MySqlDataReader reader = cmd.ExecuteReader();
            ds.Load(reader);
            reader.Close();
            return ds;
        }
    }
}
