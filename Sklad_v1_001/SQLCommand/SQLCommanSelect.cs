using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Sklad_v1_001.GlobalVariable;

namespace Sklad_v1_001.SQL
{
    public class SQLCommanSelect
    {
        public class Answer
        {
            public Boolean IsError;
            public String AnswerText;
            public DataTable datatable;
            public DataTable datatablerow;
            public Int64 result;

            public Answer()
            {
                datatable = new DataTable();
                datatablerow = new DataTable();
                IsError = false;
                AnswerText = "";
                result = 0;
            }
        }

        Answer _sqlAnswer;

        String _connectionString;
        SqlDataAdapter adapter;
        SqlConnection connection = null;
        private SqlCommand _sqlgcommands;
        private DataBaseData _dataBaseData;
        DataTable table;

        public Answer SqlAnswer
        {
            get
            {
                return _sqlAnswer;
            }

            set
            {
                _sqlAnswer = value;
            }
        }

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }

            set
            {
                _connectionString = value;
            }
        }

        // 
        public SQLCommanSelect()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _sqlgcommands = new SqlCommand();
            _dataBaseData = new DataBaseData();
            _sqlAnswer = new Answer();
        }
       
        public void AddParametr (string _parametr,SqlDbType _type=SqlDbType.NVarChar, Int32 _size = 0)
        {
            if (!_sqlgcommands.Parameters.Contains(_parametr))
            {
                if (_size == 0)
                {
                    _sqlgcommands.Parameters.Add(_parametr, _type);
                }
                else
                    _sqlgcommands.Parameters.Add(_parametr, _type, _size);
            }
        }

        public void SetParametrValue(string _parametr, object value)
        {
            _sqlgcommands.Parameters[_parametr].Value = value;
        }

        public DataTable Select()
        {
            using (SqlConnection connect=new SqlConnection(_connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_connectionString, connect);
                    command.CommandType = CommandType.Text;
                    connect.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable data = new DataTable();
                    data.Load(reader);
                    reader.Close();
                    connect.Close();
                    return data;
                }
                catch
                {

                }
                return null;
            }
        }

        public void ComplexRequest (string sqlstr, CommandType type=CommandType.Text, SqlParameterCollection _parametrs = null)
        {
            if (type == CommandType.Text)
            {
                using (SqlConnection connect = new SqlConnection(_connectionString))
                {
                    try
                    {
                        SqlCommand sqlCommand = new SqlCommand(_connectionString, connect);
                        sqlCommand.CommandType = type;
                        connect.Open();
                        sqlCommand.CommandText = sqlstr;
                        SqlDataReader reader = sqlCommand.ExecuteReader();
                        if (reader != null)
                        {
                            _sqlAnswer.datatable.Clear();
                            _sqlAnswer.datatable.Load(reader);
                        }
                        _sqlAnswer.IsError = false;
                        connect.Close();
                    }
                    catch (Exception e)
                    {
                        _sqlAnswer.AnswerText = e.ToString();
                        _sqlAnswer.IsError = true;
                    }
                }
            }
            else if (type == CommandType.StoredProcedure)
            {
                using (SqlConnection connect = new SqlConnection(_connectionString))
                {
                    try
                    {
                        SqlCommand sqlCommand = new SqlCommand(sqlstr, connect);
                        Int32 _connectTimeout = 30;
                        Int32.TryParse(_dataBaseData._connectTimeout, out _connectTimeout);
                        sqlCommand.CommandTimeout = _connectTimeout;
                        sqlCommand.CommandType = type;
                        //    foreach (SqlParameter parametr in _sqlgcommands.Parameters)
                        //    {
                        //        if (!sqlCommand.Parameters.Contains(parametr))
                        //        {
                        //            sqlCommand.Parameters.Add(parametr.ParameterName, parametr.SqlDbType);
                        //            sqlCommand.Parameters[parametr.ParameterName].Value = parametr.Value;
                        //        }
                        //    }
                        //    if(_parametrs!=null)
                        //    {
                        //        foreach (SqlParameter parametr in _parametrs)
                        //        {
                        //            if (!sqlCommand.Parameters.Contains(parametr))
                        //            {
                        //                sqlCommand.Parameters.Add(parametr.ParameterName, parametr.SqlDbType);
                        //                sqlCommand.Parameters[parametr.ParameterName].Value = parametr.Value;
                        //            }
                        //        }
                        //    }
                        //    connect.Open();
                        //    SqlDataReader reader = sqlCommand.ExecuteReader();
                        //    if (reader != null)
                        //    {
                        //        _sqlAnswer.datatable.Clear();
                        //        _sqlAnswer.datatable.Load(reader);
                        //        Int64 result;
                        //        Int64.TryParse(_sqlAnswer.datatable.Rows[0][0].ToString(), out result);
                        //        _sqlAnswer.result = result;
                        //    }
                        //}
                        foreach (SqlParameter parametr in _sqlgcommands.Parameters)
                        {
                            if (!sqlCommand.Parameters.Contains(parametr))
                            {
                                sqlCommand.Parameters.Add(parametr.ParameterName, parametr.SqlDbType);
                                sqlCommand.Parameters[parametr.ParameterName].Value = parametr.Value;
                            }
                        }
                        if (_parametrs != null)
                        {
                            foreach (SqlParameter parametr in _parametrs)
                            {
                                if (!sqlCommand.Parameters.Contains(parametr))
                                {
                                    sqlCommand.Parameters.Add(parametr.ParameterName, parametr.SqlDbType);
                                    sqlCommand.Parameters[parametr.ParameterName].Value = parametr.Value;
                                }
                            }
                        }
                        connect.Open();
                        SqlDataReader requestanswer = sqlCommand.ExecuteReader();
                        if (requestanswer != null)
                        {
                            _sqlAnswer.datatable.Clear();
                            _sqlAnswer.datatable.Load(requestanswer);
                            Int64 result;
                            if (_sqlAnswer.datatable != null && _sqlAnswer.datatable.Rows.Count > 0)
                                Int64.TryParse(_sqlAnswer.datatable.Rows[0][0].ToString(), out result);
                            else
                                result = 0;
                            _sqlAnswer.result = result;
                        }
                        _sqlAnswer.IsError = false;
                        connect.Close();
                    }
                    catch (Exception e)
                    {
                        _sqlAnswer.AnswerText = e.ToString();
                        _sqlAnswer.IsError = true;
                    }
                }
            }           
        }

        public DataTable SQLCommandrs(String sqlProcedura)
        {
            try
            {
               // string sqlExpression = "xp_GetSelectProductTable";             
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    _sqlgcommands = new SqlCommand(sqlProcedura, connection);
                    // указываем, что команда представляет хранимую процедуру
                    _sqlgcommands.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = _sqlgcommands.ExecuteReader();
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
