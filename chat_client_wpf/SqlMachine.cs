using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace chat_client_wpf
{
    public class SqlMachine
    {
        string connstring = String.Format("Server=165.22.240.183;Port=5432;" +
                     "User Id=postgres;Password=passwordforchat;Database=postgres;sslmode=Require;Trust Server Certificate=true;");
        //string connstring = String.Format("Server=ec2-54-225-110-156.compute-1.amazonaws.com;Port=5432;" +
                     //"User Id=kegmwqmmzhiety;Password=e87334ea834578f6b7fc43f1fb501fc3e70e2f6e0bcafa797750844d15706a09;Database=d5eem7ifccga82;sslmode=Require;Trust Server Certificate=true;");
        public SqlMachine() { }
        public DataSet SqlQuerySelect(string query)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();
            string sql = query;

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);

            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
            {
                conn.Close();
                return null;
            }

            conn.Close();
            return ds;


            /*foreach (DataTable dt in ds.Tables)
            {
                Console.WriteLine(dt.TableName);

                foreach (DataColumn column in dt.Columns)
                    Console.Write("\t{column.ColumnName}");
                Console.WriteLine();

                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;
                    foreach (object cell in cells)
                        Console.Write("\t{0}", cell);
                    Console.WriteLine();
                }
            }*/


        }
        public void SqlQueryInsert(string query, params string[] par)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();
            string sql = query;

            var da = new NpgsqlCommand();

            da.Connection = conn;
            da.CommandText = query;
            da.Parameters.AddWithValue("p", par[0]);
            da.Parameters.AddWithValue("p1", par[1]);
            da.ExecuteNonQuery();

            conn.Close();
        }
        public void SqlQueryInsert1(string query, string a, int b, int c)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();
            string sql = query;

            var da = new NpgsqlCommand();

            da.Connection = conn;
            da.CommandText = query;
            da.Parameters.AddWithValue("p", a);
            da.Parameters.AddWithValue("p1", b);
            da.Parameters.AddWithValue("p2", c);

            da.ExecuteNonQuery();

            conn.Close();
        }
    }
}
