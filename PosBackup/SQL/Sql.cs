using PosBackup.GlobalVariable;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PosBackup.Sql
{
    public class SqlConnectionMiuz
    {
        private string _constr = "";
        private Answer _sqlAnswer;
        private SqlCommand _fakeforparametres;
        private DataBaseData _dataBaseData;
        private string _request = "";

        public string Constr
        {
            get
            {
                return _constr;
            }

            set
            {
                _constr = value;
            }
        }

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

        public string Request
        {
            get
            {
                return _request;
            }

            set
            {
                _request = value;
            }
        }

        public SqlConnectionMiuz(DataBaseData dataBaseData)
        {
            _dataBaseData = dataBaseData;
            //_constr = string.Concat("Data Source=", dataBaseData._servername, ";Initial Catalog=", dataBaseData._database, ";User ID=", dataBaseData._serverlogin, ";Password=", dataBaseData._serverpassword, ";Integrated Security=true", ";Persist Security Info = False");
            //_constr = string.Concat("Data Source=", dataBaseData._servername, ";Initial Catalog=", dataBaseData._database, ";User ID=", dataBaseData._serverlogin, ";Password=", dataBaseData._serverpassword, ";Integrated Security=false", ";Persist Security Info = False");
            //_constr = string.Concat("Data Source=", dataBaseData._servername, ";Initial Catalog=", dataBaseData._database, ";User ID=", dataBaseData._serverlogin, ";Password=", dataBaseData._serverpassword);
            _constr = string.Concat("Data Source=", dataBaseData._servername, ";Initial Catalog=", dataBaseData._database, ";Integrated Security=True");
            _sqlAnswer = new Answer();
            _fakeforparametres = new SqlCommand();
        }

        public SqlConnectionMiuz(DataBaseData dataBaseData, Boolean intergrated)
        {
            _dataBaseData = dataBaseData;
            //_constr = string.Concat("Data Source=", dataBaseData._servername, ";Initial Catalog=", dataBaseData._database, ";User ID=", dataBaseData._serverlogin, ";Password=", dataBaseData._serverpassword, ";Integrated Security=true", ";Persist Security Info = False");
            //_constr = string.Concat("Data Source=", dataBaseData._servername, ";Initial Catalog=", dataBaseData._database, ";User ID=", dataBaseData._serverlogin, ";Password=", dataBaseData._serverpassword, ";Integrated Security=false", ";Persist Security Info = False");
            _constr = string.Concat("Data Source=", dataBaseData._servername, ";Initial Catalog=", dataBaseData._database, ";User ID=", dataBaseData._serverlogin, ";Password=", dataBaseData._serverpassword);
            //_constr = string.Concat("Data Source=", dataBaseData._servername, ";Initial Catalog=", dataBaseData._database, ";Integrated Security=True");
            _sqlAnswer = new Answer();
            _fakeforparametres = new SqlCommand();
        }

        public SqlConnectionMiuz(string _dataSource, string _initialCatalog)
        {
            //_constr = string.Concat("Data Source=", _dataSource, ";Initial Catalog=", _initialCatalog, ";Integrated Security=SSPI");
            _constr = string.Concat("Data Source=", _dataSource, ";Initial Catalog=", _initialCatalog, ";Integrated Security=True");
            _sqlAnswer = new Answer();
            _fakeforparametres = new SqlCommand();
        }

        public SqlConnectionMiuz(string _dataSource, string _initialCatalog, string _userID, string _Password)
        {
            _constr = string.Concat("Data Source=", _dataSource, ";Initial Catalog=", _initialCatalog, ";PersistSecurityInfo=True;User ID=", _userID, ";=", _Password);
            _sqlAnswer = new Answer();
            _fakeforparametres = new SqlCommand();
        }

        public Boolean CheckConnection()
        {
            //MessageBox.Show("Error connection to MSSQL Server with parameters: " + _constr);
            using (SqlConnection con_msql = new SqlConnection(_constr))
            {
                try
                {
                    con_msql.Open();
                    con_msql.Close();
                    return true;
                }
                catch (Exception)
                {
                    //MessageBox.Show("Error connection to MSSQL Server with parameters: "+ _constr);
                    return false;
                }
            }
        }

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

        public void AddParametr(string _parametr, SqlDbType _type = SqlDbType.NChar, Int32 _size = 0)
        {
            if (!_fakeforparametres.Parameters.Contains(_parametr))
            {
                if (_size == 0)
                    _fakeforparametres.Parameters.Add(_parametr, _type);
                else
                    _fakeforparametres.Parameters.Add(_parametr, _type, _size);
            }
        }

        public void SetParametrValue(string _parametr, object value)
        {
            _fakeforparametres.Parameters[_parametr].Value = value;
        }

        public DataTable Select()
        {
            using (SqlConnection con_msql = new SqlConnection(_constr))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_request, con_msql);
                    command.CommandType = CommandType.Text;
                    con_msql.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable data = new DataTable();
                    data.Load(reader);
                    reader.Close();
                    con_msql.Close();
                    return data;
                }
                catch
                {
                }
            }
            return null;
        }

        public void ComplexRequest(string sq, CommandType type = CommandType.Text, SqlParameterCollection _parametres = null, Int32? sqlCommandTimeOut = null)
        {
            if (type == CommandType.Text)
            {
                using (SqlConnection con_msql = new SqlConnection(_constr))
                {
                    try
                    {
                        SqlCommand sqlCmd = new SqlCommand(sq, con_msql);
                        sqlCmd.CommandType = type;
                        con_msql.Open();
                        sqlCmd.CommandText = sq;

                        if (sqlCommandTimeOut != null)
                        {
                            sqlCmd.CommandTimeout = (int)sqlCommandTimeOut;
                        }

                        SqlDataReader requestanswer = sqlCmd.ExecuteReader();
                        if (requestanswer != null)
                        {
                            _sqlAnswer.datatable.Clear();
                            _sqlAnswer.datatable.Load(requestanswer);
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
                using (SqlConnection con_msql = new SqlConnection(Constr))
                {
                    try
                    {
                        SqlCommand sqlCmd = new SqlCommand(sq, con_msql);
                        Int32 _connectTimeout = 30;
                        Int32.TryParse(_dataBaseData._connectTimeout, out _connectTimeout);
                        sqlCmd.CommandTimeout = _connectTimeout;
                        sqlCmd.CommandType = type;
                        foreach (SqlParameter parametr in _fakeforparametres.Parameters)
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
                using (SqlConnection con_msql = new SqlConnection(_constr))
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
                using (SqlConnection con_msql = new SqlConnection(Constr))
                {
                    try
                    {
                        SqlCommand sqlCmd = new SqlCommand(sq, con_msql);
                        Int32 _connectTimeout = 30;
                        Int32.TryParse(_dataBaseData._connectTimeout, out _connectTimeout);
                        sqlCmd.CommandTimeout = _connectTimeout;
                        sqlCmd.CommandType = type;
                        foreach (SqlParameter parametr in _fakeforparametres.Parameters)
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
                using (SqlConnection con_msql = new SqlConnection(_constr))
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
                    }
                }
            }

            else if (type == CommandType.StoredProcedure)
            {
                using (SqlConnection con_msql = new SqlConnection(Constr))
                {
                    try
                    {
                        SqlCommand sqlCmd = new SqlCommand(sq, con_msql);
                        Int32 _connectTimeout = 30;
                        Int32.TryParse(_dataBaseData._connectTimeout, out _connectTimeout);
                        sqlCmd.CommandTimeout = _connectTimeout;
                        sqlCmd.CommandType = type;
                        foreach (SqlParameter parametr in _fakeforparametres.Parameters)
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
                    }
                }
            }
        }
    }
}
