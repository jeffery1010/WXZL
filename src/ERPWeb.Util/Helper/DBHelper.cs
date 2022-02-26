using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ERPWeb.Util.Helper
{
    public class DBHelper
    {
        private string _DBConnectionString = ConfigurationManager.ConnectionStrings["WXZL"].ToString(); 

        public DBHelper() { }
        public DBHelper(string connStr) {
            this._DBConnectionString = ConfigurationManager.ConnectionStrings[connStr].ToString();
        }

        #region ahai2019
        /// <summary>
        /// 执行T-SQL，返回表格
        /// </summary>
        /// <param name="tSql">T-SQL</param>
        /// <returns>DateTable对象</returns>
        public DataTable GetDataTable(string tSql)
        {
            try
            {
                DataTable table = new DataTable();
                using (SqlConnection conn = new SqlConnection(this._DBConnectionString))
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(tSql, conn))
                    {
                        adapter.Fill(table);
                    }
                }
                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR in Database.GetDateTable tsql  msg:" + ex.Message);
            }
        }

        /// <summary>
        /// 执行存储过程，返回表格
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="dict">输入参数</param>
        /// <returns></returns>
        public DataTable GetDataTable(string procName, Dictionary<string, object> dict)
        {
            try
            {
                DataTable table = new DataTable();
                using (SqlConnection conn = new SqlConnection(this._DBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = procName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach (string key in dict.Keys)
                        {
                            cmd.Parameters.AddWithValue(key, dict[key]);
                        }
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(table);
                        }
                    }
                }
                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR in Database.GetDateTable procedure  msg:" + ex.Message);
            }
        }
        public DataTable GetDataTable2(string procName, Dictionary<string, object> dict)
        {
            try
            {
                DataTable table = new DataTable();
                using (SqlConnection conn = new SqlConnection(this._DBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = procName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach (string key in dict.Keys)
                        {
                            cmd.Parameters.AddWithValue(key, dict[key]);
                        }
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(table);
                        }
                    }
                }
                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR in Database.GetDateTable procedure  msg:" + ex.Message);
            }
        }
        /// <summary>
        /// 执行存储过程，各道表格和输出参
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="dictIn">输入参</param>
        /// <param name="listtOut">输出参数名</param>
        /// <returns>[0]DataTable   [1]Dict输出参</returns>
        public object[] GetDataTableWithOutPut(string procName, Dictionary<string, object> dictIn, List<string> listtOut)
        {
            try
            {
                DataTable table = new DataTable();
                Dictionary<string, object> dictOut = new Dictionary<string, object>();
                using (SqlConnection conn = new SqlConnection(this._DBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = procName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach (string key in dictIn.Keys)
                        {
                            cmd.Parameters.AddWithValue(key, dictIn[key]);
                        }
                        foreach (string key in listtOut)
                        {
                            cmd.Parameters.Add(key, SqlDbType.VarChar, 128);
                            cmd.Parameters[key].Direction = ParameterDirection.Output;
                        }
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(table);
                        }

                        foreach (string key in listtOut)
                        {
                            dictOut.Add(key, cmd.Parameters[key].Value);
                        }
                    }
                }
                return new object[] { table, dictOut };
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR in Database.GetDateTable procedure  msg:" + ex.Message);
            }
        }

        /// <summary>
        /// 执行SQL得到首行首列
        /// </summary>
        /// <param name="tSql">sql语句</param>
        /// <returns></returns>
        public object ExecuteScalar(string tSql)
        {
            try
            {
                object result = "0";
                using (SqlConnection conn = new SqlConnection(this._DBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(tSql, conn))
                    {
                        result = cmd.ExecuteScalar();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR in Helper.DB.DBOperator  ExecuteScalar  msg:" + ex.Message);
            }
        }

        /// <summary>
        /// 执行增删改查，返回受影响的行数
        /// </summary>
        /// <param name="tSql">SQL语句</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string tSql)
        {
            try
            {
                int result = 0;
                using (SqlConnection conn = new SqlConnection(this._DBConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(tSql, conn))
                    {
                        result = cmd.ExecuteNonQuery();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR in Helper.DB.DBOperator  ExecuteNonQuery  msg:" + ex.Message);
            }
        }

        /// <summary>
        /// 执行存储过程，得到表格，带一个返回值
        /// </summary>
        /// <param name="parametersInstance">参数列表</param>
        /// <param name="storedProcedureName">存储过程名称</param>
        /// <param name="outKey">返回变量名</param>
        /// <param name="outValue">返回值</param>
        /// <returns>表格</returns>
        public DataTable ExecStoredProcGetTableWithResults(Dictionary<string, object> parametersInstance, string storedProcedureName, string outKey, out object outValue)
        {
            using (SqlConnection con = new SqlConnection(_DBConnectionString))
            {
                try
                {
                    //设置Sql
                    SqlCommand cmd = new SqlCommand(storedProcedureName, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parametersInstance != null)
                    {
                        foreach (KeyValuePair<string, object> item in parametersInstance)
                        {
                            SqlParameter parm = new SqlParameter(item.Key, item.Value);
                            cmd.Parameters.Add(parm);
                        }
                    }
                    SqlParameter parOutput = cmd.Parameters.Add(outKey, SqlDbType.NVarChar, 64);//定义输出参数  
                    parOutput.Direction = ParameterDirection.Output;//参数类型为Output  

                    DataTable dt = new DataTable();
                    //cmd.CommandTimeout = _ConnectTimeOut;
                    SqlDataAdapter sdap = new SqlDataAdapter(cmd);

                    sdap.Fill(dt);
                    outValue = parOutput.Value;
                    return dt;
                }
                catch (Exception er)
                {
                    throw er;
                }
            }
        }
        #endregion
    }
}
