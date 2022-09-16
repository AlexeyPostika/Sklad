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
            public List<DataTable> listDatatable;
            public List<DataTable> listTypeDatatable;

            public Answer()
            {
                datatable = new DataTable();
                datatablerow = new DataTable();
                IsError = false;
                AnswerText = "";
                result = 0;
                listDatatable = new List<DataTable>();
                listTypeDatatable = new List<DataTable>();
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
            _sqlgcommands = new SqlCommand();
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

        public void ComplexMultipleRequest(string sq, CommandType type = CommandType.Text, SqlParameterCollection _parametres = null)
        {
            if (type == CommandType.Text)
            {
                using (SqlConnection con_msql = new SqlConnection(_connectionString))
                {
                    try
                    {
                        SqlCommand sqlCmd = new SqlCommand(sq, con_msql);
                        sqlCmd.CommandType = type;
                        con_msql.Open();
                        sqlCmd.CommandText = sq;
                        SqlDataReader requestanswer = sqlCmd.ExecuteReader();
                        if (requestanswer != null)
                        {
                            _sqlAnswer.listDatatable.Clear();
                            do
                            {
                                DataTable dataTable = new DataTable();
                                dataTable.Load(requestanswer);
                                _sqlAnswer.listDatatable.Add(dataTable);
                            }
                            while (!requestanswer.IsClosed);
                        }
                        _sqlAnswer.IsError = false;
                        con_msql.Close();
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
                using (SqlConnection con_msql = new SqlConnection(_connectionString))
                {
                    try
                    {
                        SqlCommand sqlCmd = new SqlCommand(sq, con_msql);
                        Int32 _connectTimeout = 30;
                        Int32.TryParse(_dataBaseData._connectTimeout, out _connectTimeout);
                        sqlCmd.CommandTimeout = _connectTimeout;
                        sqlCmd.CommandType = type;
                        foreach (SqlParameter parametr in _sqlgcommands.Parameters)
                        {
                            if (!sqlCmd.Parameters.Contains(parametr))
                            {
                                sqlCmd.Parameters.Add(parametr.ParameterName, parametr.SqlDbType);
                                sqlCmd.Parameters[parametr.ParameterName].Value = parametr.Value;
                            }
                        }
                        if (_parametres != null)
                        {
                            foreach (SqlParameter parametr in _parametres)
                            {
                                if (!sqlCmd.Parameters.Contains(parametr))
                                {
                                    sqlCmd.Parameters.Add(parametr.ParameterName, parametr.SqlDbType);
                                    sqlCmd.Parameters[parametr.ParameterName].Value = parametr.Value;
                                }
                            }
                        }
                        con_msql.Open();
                        SqlDataReader requestanswer = sqlCmd.ExecuteReader();
                        if (requestanswer != null)
                        {
                            _sqlAnswer.listDatatable.Clear();
                            do
                            {
                                DataTable dataTable = new DataTable();
                                dataTable.Load(requestanswer);
                                _sqlAnswer.listDatatable.Add(dataTable);
                            }
                            while (!requestanswer.IsClosed);

                            Int64 result;
                            if (_sqlAnswer.datatable != null && _sqlAnswer.datatable.Rows.Count > 0)
                            {
                                Int64.TryParse(_sqlAnswer.datatable.Rows[0][0].ToString(), out result);
                            }
                            else
                            {
                                result = 0;
                            }

                            _sqlAnswer.result = result;
                        }
                        _sqlAnswer.IsError = false;
                        con_msql.Close();
                    }
                    catch (Exception e)
                    {
                        _sqlAnswer.AnswerText = e.ToString();
                        _sqlAnswer.IsError = true;
                    }
                }
            }
        }

        public void ComplexMultipleRequestTypeData(string sq, CommandType type = CommandType.Text, SqlParameterCollection _parametres = null)
        {
            if (type == CommandType.Text)
            {
                using (SqlConnection con_msql = new SqlConnection(_connectionString))
                {
                    try
                    {
                        SqlCommand sqlCmd = new SqlCommand(sq, con_msql);
                        sqlCmd.CommandType = type;
                        con_msql.Open();
                        sqlCmd.CommandText = sq;
                        SqlDataReader requestanswer = sqlCmd.ExecuteReader();
                        if (requestanswer != null)
                        {
                            _sqlAnswer.listDatatable.Clear();
                            do
                            {
                                DataTable dataTable = new DataTable();
                                dataTable.Load(requestanswer);
                                _sqlAnswer.listDatatable.Add(dataTable);
                            }
                            while (!requestanswer.IsClosed);
                        }
                        _sqlAnswer.IsError = false;
                        con_msql.Close();
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
                using (SqlConnection con_msql = new SqlConnection(_connectionString))
                {
                    try
                    {
                        SqlCommand sqlCmd = new SqlCommand(sq, con_msql);
                        Int32 _connectTimeout = 30;
                        Int32.TryParse(_dataBaseData._connectTimeout, out _connectTimeout);
                        sqlCmd.CommandTimeout = _connectTimeout;
                        sqlCmd.CommandType = type;
                        foreach (SqlParameter parametr in _sqlgcommands.Parameters)
                        {
                            if (!sqlCmd.Parameters.Contains(parametr))
                            {
                                sqlCmd.Parameters.Add(parametr.ParameterName, parametr.SqlDbType);
                                sqlCmd.Parameters[parametr.ParameterName].Value = parametr.Value;
                            }
                        }
                        if (_parametres != null)
                        {
                            foreach (SqlParameter parametr in _parametres)
                            {
                                if (!sqlCmd.Parameters.Contains(parametr))
                                {
                                    sqlCmd.Parameters.Add(parametr.ParameterName, parametr.SqlDbType);
                                    sqlCmd.Parameters[parametr.ParameterName].Value = parametr.Value;
                                }
                            }
                        }
                        con_msql.Open();

                        SqlDataReader requestanswer = sqlCmd.ExecuteReader();
                        if (requestanswer != null)
                        {
                            _sqlAnswer.listTypeDatatable.Clear();
                            do
                            {
                                DataTable dataTable = new DataTable();
                                dataTable.Load(requestanswer);

                                if (dataTable.Rows.Count > 0)
                                {
                                    dataTable.TableName = dataTable.Rows[0]["TableName"].ToString();
                                    dataTable.Columns.Remove("TableName");
                                }
                                _sqlAnswer.listTypeDatatable.Add(dataTable);
                            }
                            while (!requestanswer.IsClosed);

                            Int64 result;
                            if (_sqlAnswer.datatable != null && _sqlAnswer.datatable.Rows.Count > 0)
                            {
                                Int64.TryParse(_sqlAnswer.datatable.Rows[0][0].ToString(), out result);
                            }
                            else
                            {
                                result = 0;
                            }

                            _sqlAnswer.result = result;
                        }
                        _sqlAnswer.IsError = false;
                        con_msql.Close();
                    }
                    catch (Exception e)
                    {
                        _sqlAnswer.AnswerText = e.ToString();
                        _sqlAnswer.IsError = true;
                    }
                }
            }
        }

        public void ComplexMultipleRequestType(string sq, CommandType type = CommandType.Text, SqlParameterCollection _parametres = null)
        {
            if (type == CommandType.StoredProcedure)
            {
                using (SqlConnection con_msql = new SqlConnection(_connectionString))
                {
                    try
                    {
                        SqlCommand sqlCmd = new SqlCommand(sq, con_msql);
                        Int32 _connectTimeout = 30;
                        Int32.TryParse(_dataBaseData._connectTimeout, out _connectTimeout);
                        sqlCmd.CommandTimeout = _connectTimeout;
                        sqlCmd.CommandType = type;
                        foreach (SqlParameter parametr in _sqlgcommands.Parameters)
                        {
                            if (!sqlCmd.Parameters.Contains(parametr))
                            {
                                sqlCmd.Parameters.Add(parametr.ParameterName, parametr.SqlDbType);
                                sqlCmd.Parameters[parametr.ParameterName].Value = parametr.Value;
                            }
                        }
                        if (_parametres != null)
                        {
                            foreach (SqlParameter parametr in _parametres)
                            {
                                if (!sqlCmd.Parameters.Contains(parametr))
                                {
                                    sqlCmd.Parameters.Add(parametr.ParameterName, parametr.SqlDbType);
                                    sqlCmd.Parameters[parametr.ParameterName].Value = parametr.Value;
                                }
                            }
                        }
                        con_msql.Open();
                        DataTable dataType = con_msql.GetSchema("StructuredTypeMembers");
                        dataType.DefaultView.Sort = "ORDINAL_POSITION ASC";
                        dataType = dataType.DefaultView.ToTable();
                        DataTable dataTableReal = null;

                        foreach (DataRow row in dataType.Rows)
                        {
                            String TYPE_NAME = row["TYPE_NAME"].ToString();
                            String DATA_TYPE_SQL = row["DATA_TYPE"].ToString();
                            String DATA_TYPE_C = null;
                            String MEMBER_NAME = row["MEMBER_NAME"].ToString();

                            dataTableReal = _sqlAnswer.listTypeDatatable.Find(x => x.TableName == TYPE_NAME);
                            if (dataTableReal == null)
                            {
                                dataTableReal = new DataTable();
                                dataTableReal.TableName = TYPE_NAME;
                                foreach (DataRow column in dataType.Rows)
                                {
                                    if (column["TYPE_NAME"].ToString() == TYPE_NAME)
                                    {
                                        MEMBER_NAME = column["MEMBER_NAME"].ToString();
                                        DATA_TYPE_C = ConvertSQLToCDataType(column["DATA_TYPE"].ToString());

                                        dataTableReal.Columns.Add(new DataColumn { DataType = System.Type.GetType($"System.{DATA_TYPE_C}"), ColumnName = MEMBER_NAME });
                                    }
                                }
                                _sqlAnswer.listTypeDatatable.Add(dataTableReal);
                            }
                        }
                        _sqlAnswer.IsError = false;
                        con_msql.Close();
                    }
                    catch (Exception e)
                    {
                        _sqlAnswer.AnswerText = e.ToString();
                        _sqlAnswer.IsError = true;
                    }
                }
            }
        }

        public void ComplexRequestRow(string sq, CommandType type = CommandType.Text, SqlParameterCollection _parametres = null)
        {
            if (type == CommandType.Text)
            {
                using (SqlConnection con_msql = new SqlConnection(_connectionString))
                {
                    try
                    {
                        SqlCommand sqlCmd = new SqlCommand(sq, con_msql);
                        sqlCmd.CommandType = type;
                        con_msql.Open();
                        sqlCmd.CommandText = sq;
                        SqlDataReader requestanswer = sqlCmd.ExecuteReader();
                        if (requestanswer != null)
                        {
                            _sqlAnswer.datatablerow.Clear();
                            _sqlAnswer.datatablerow.Load(requestanswer);
                        }
                        _sqlAnswer.IsError = false;
                        con_msql.Close();
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
                using (SqlConnection con_msql = new SqlConnection(_connectionString))
                {
                    try
                    {
                        SqlCommand sqlCmd = new SqlCommand(sq, con_msql);
                        Int32 _connectTimeout = 30;
                        Int32.TryParse(_dataBaseData._connectTimeout, out _connectTimeout);
                        sqlCmd.CommandTimeout = _connectTimeout;
                        sqlCmd.CommandType = type;
                        foreach (SqlParameter parametr in _sqlgcommands.Parameters)
                        {
                            if (!sqlCmd.Parameters.Contains(parametr))
                            {
                                sqlCmd.Parameters.Add(parametr.ParameterName, parametr.SqlDbType);
                                sqlCmd.Parameters[parametr.ParameterName].Value = parametr.Value;
                            }
                        }
                        if (_parametres != null)
                        {
                            foreach (SqlParameter parametr in _parametres)
                            {
                                if (!sqlCmd.Parameters.Contains(parametr))
                                {
                                    sqlCmd.Parameters.Add(parametr.ParameterName, parametr.SqlDbType);
                                    sqlCmd.Parameters[parametr.ParameterName].Value = parametr.Value;
                                }
                            }
                        }
                        con_msql.Open();
                        SqlDataReader requestanswer = sqlCmd.ExecuteReader();
                        if (requestanswer != null)
                        {
                            _sqlAnswer.datatablerow.Clear();
                            _sqlAnswer.datatablerow.Load(requestanswer);
                            Int32 result;
                            if (_sqlAnswer.datatablerow != null && _sqlAnswer.datatablerow.Rows.Count > 0)
                                Int32.TryParse(_sqlAnswer.datatablerow.Rows[0][0].ToString(), out result);
                            else
                                result = 0;
                            _sqlAnswer.result = result;
                        }
                        _sqlAnswer.IsError = false;
                        con_msql.Close();
                    }
                    catch (Exception e)
                    {
                        _sqlAnswer.AnswerText = e.ToString();
                        _sqlAnswer.IsError = true;

                    }
                }
            }
        }


        public void SimpleRequest(string sq, CommandType type = CommandType.Text, SqlParameterCollection _parametres = null)
        {

            if (type == CommandType.Text)
            {
                using (SqlConnection con_msql = new SqlConnection(_connectionString))
                {
                    try
                    {
                        SqlCommand sqlCmd = new SqlCommand(sq, con_msql);
                        sqlCmd.CommandType = type;
                        con_msql.Open();
                        sqlCmd.CommandText = sq;
                        object requestanswer = sqlCmd.ExecuteScalar();
                        if (requestanswer != null)
                            _sqlAnswer.AnswerText = requestanswer.ToString();
                        _sqlAnswer.IsError = false;
                        con_msql.Close();
                    }
                    catch (Exception e)
                    {
                        _sqlAnswer.AnswerText = e.ToString();
                        _sqlAnswer.IsError = true;
                        //if (App.IsErrorDb == false && con_msql.State == ConnectionState.Closed)
                        //{
                        //    //ShowErrorDialog();
                        //}
                    }
                }
            }
            else if (type == CommandType.StoredProcedure)
            {
                using (SqlConnection con_msql = new SqlConnection(_connectionString))
                {
                    try
                    {
                        SqlCommand sqlCmd = new SqlCommand(sq, con_msql);
                        Int32 _connectTimeout = 30;
                        Int32.TryParse(_dataBaseData._connectTimeout, out _connectTimeout);
                        sqlCmd.CommandTimeout = _connectTimeout;
                        sqlCmd.CommandType = type;
                        foreach (SqlParameter parametr in _parametres)
                        {
                            if (!sqlCmd.Parameters.Contains(parametr))
                            {
                                sqlCmd.Parameters.Add(parametr.ParameterName, parametr.SqlDbType);
                                sqlCmd.Parameters[parametr.ParameterName].Value = parametr.Value;
                            }
                        }
                        if (_parametres != null)
                        {
                            foreach (SqlParameter parametr in _parametres)
                            {
                                if (!sqlCmd.Parameters.Contains(parametr))
                                {
                                    sqlCmd.Parameters.Add(parametr.ParameterName, parametr.SqlDbType);
                                    sqlCmd.Parameters[parametr.ParameterName].Value = parametr.Value;
                                }
                            }
                        }
                        con_msql.Open();
                        object requestanswer = sqlCmd.ExecuteScalar();
                        if (requestanswer != null)
                            _sqlAnswer.AnswerText = requestanswer.ToString();
                        _sqlAnswer.IsError = false;
                        con_msql.Close();
                    }
                    catch (Exception e)
                    {
                        _sqlAnswer.AnswerText = e.ToString();
                        _sqlAnswer.IsError = true;
                        //if (App.IsErrorDb == false && con_msql.State == ConnectionState.Closed)
                        //{
                        //    //ShowErrorDialog();
                        //}
                    }
                }
            }
        }

        public String ConvertSQLToCDataType(String _sqlType)
        {
            String ret;
            _sqlType = _sqlType.ToLower();


            switch (_sqlType)
            {
                case "int":
                    ret = "Int32";
                    break;

                case "decimal":
                case "numeric":
                case "money":
                    ret = "Decimal";
                    break;

                case "bigint":
                    ret = "Int64";
                    break;

                case "varchar":
                case "nvarchar":
                    ret = "String";
                    break;

                case "date":
                case "datatime":
                case "time":
                    ret = "DateTime";
                    break;

                default:
                    ret = "String";
                    break;
            }
            return ret;
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
