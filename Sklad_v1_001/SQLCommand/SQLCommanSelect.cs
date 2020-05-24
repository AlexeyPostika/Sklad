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
                _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                connection = new SqlConnection(_connectionString);
                using (SqlCommand command = new SqlCommand(sqlProcedura, this.connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = command.ExecuteReader();
                    table.Load(dr);
                    dr.Close();
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
