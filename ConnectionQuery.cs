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
        string connectionString = "server=52.78.127.115;port=57000;database=nicekaffeebillingsystem;uid=root;pwd=pass;";
        public MySqlConnection conn;

        public string Connection()
        {
            return connectionString;
        }

        public void OpenConnection()
        {
            conn = new MySqlConnection(connectionString);
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
            ds.Load(cmd.ExecuteReader());
            return ds;
        }
    }
}
