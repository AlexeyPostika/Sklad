using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace Sklad_v1_001.SQL
{
    public class SQLCommanSelect
    {
        String _connectionString;
        SqlDataAdapter adapter;
        SqlConnection connection = null;
        DataTable table;
        // 
        public DataTable SQLCommandrs(String sqlProcedura)
        {
            try
            {
               // string sqlExpression = "xp_GetSelectProductTable";
                _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlProcedura, connection);
                    // указываем, что команда представляет хранимую процедуру
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();
                    table = new DataTable();
                    //table = reader.GetSchemaTable();
                    table.Load(reader);
                    reader.Close();
                    connection.Close();
                }                
                return table;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return table;
            }
            finally
            {
                if (connection != null)
                    connection.Close();

            }
        }
    }
}
