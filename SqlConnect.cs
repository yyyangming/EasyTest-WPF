/*
    MySql 类
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;

namespace Test
{
    public class CDbMysql
    {
        #region 字段设置
        /// <summary>
        /// 数据库连接-IP地址
        /// </summary>
        public string db_host { set; private get; }
        /// <summary>
        /// 数据库连接-用户名
        /// </summary>
        public string db_uname { set; private get; }
        /// <summary>
        /// 数据库连接-密码
        /// </summary>
        public string db_upswd { set; private get; }
        /// <summary>
        /// 数据库连接-数据库名称
        /// </summary>
        public string db_database { set; private get; }
        /// <summary>
        /// 数据库连接-端口
        /// </summary>
        public string db_prost { set; private get; }
        /// <summary>
        /// 数据库连接-数据库编码
        /// </summary>
        public string db_charset { set;  get; }
        /// <summary>
        /// 数据库连接-连接句柄
        /// </summary>
        public MySqlConnection db_header;
        /// <summary>
        /// 连接字符串
        /// </summary>
        private string dh_con_string { set; get; }

        public string DbError { private set; get; }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="host">主机IP</param>
        /// <param name="uname">用户名</param>
        /// <param name="upassword">密码</param>
        /// <param name="prost">端口</param>
        /// <param name="charset">编码-默认utf8</param>
        public CDbMysql(string host, string uname, string upassword, string dbname, string prost, string charset = "utf8")
        {
            this.db_host = host;
            this.db_uname = uname;
            this.db_upswd = upassword;
            this.db_database = dbname;
            this.db_prost = prost;
            this.db_charset = charset;
            // User Id=root;Host=localhost;Database=studb;Password=root;Port=3307
            this.dh_con_string = string.Format("User Id={0};Host={1};Database={2};Password={3};Port={4}", this.db_uname,
                                               this.db_host, this.db_database, this.db_upswd, this.db_prost
                                               );

            this.DbConnection();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="host">主机IP</param>
        /// <param name="uname">用户名</param>
        /// <param name="upassword">密码</param>
        /// <param name="prost">端口</param>
        /// <param name="charset">编码-默认utf8</param>
        public CDbMysql(string Str)
        {

            // User Id=root;Host=localhost;Database=studb;Password=root;Port=3307
            this.dh_con_string = Str;

            this.DbConnection();
        }


        /// <summary>
        /// 连接数据库
        /// </summary>
        public void DbConnection()
        {
            this.db_header = new MySqlConnection(this.dh_con_string);
        }


        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="QueryString"></param>
        /// <returns></returns>
        public int ExecuteSql(string QueryString)
        {
            try
            {
                this.db_header.Open();
                using (MySqlCommand comm = new MySqlCommand(QueryString, this.db_header))
                {
                    int result = comm.ExecuteNonQuery();
                    this.DbClose(this.db_header);
                    return result;

                }
            }
            catch (MySqlException ex)
            {
                this.DbError = ex.Message.ToString();
                return -1;
            }
            finally
            {
                this.DbClose(this.db_header);
            }
        }
        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="SqlString"></param>
        /// <param name="TablName"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string SqlString)
        {
            try
            {
                this.db_header.Open();
                MySqlDataAdapter Da = new MySqlDataAdapter(SqlString, this.db_header);
                DataTable dt = new DataTable();
                Da.Fill(dt);
                return dt;
            }
            catch (MySqlException ex)
            {
                this.DbError = ex.Message.ToString();
                return null;
            }

        }
        /// <summary>
        /// 返回DataReader对象
        /// </summary>
        /// <param name="SqlString"></param>
        /// <returns></returns>
        public MySqlDataReader GetDataReader(string SqlString)
        {
            try
            {
                this.db_header.Open();
                MySqlCommand comm = new MySqlCommand(SqlString, this.db_header);
                MySqlDataReader dread = comm.ExecuteReader(CommandBehavior.Default);
                return dread;
            }
            catch (MySqlException ex)
            {
                this.DbError = ex.Message.ToString();
                return null;
            }
        }
        /// <summary>
        /// 获取DataAdapter对象
        /// </summary>
        /// <param name="SqlString"></param>
        /// <returns></returns>
        private MySqlDataAdapter GetDataAdapter(string SqlString)
        {
            try
            {
                this.db_header.Open();
                MySqlDataAdapter dadapter = new MySqlDataAdapter(SqlString, this.db_header);
                return dadapter;
            }
            catch (MySqlException ex)
            {
                this.DbError = ex.Message.ToString();
                return null;
            }

        }
        /// <summary>
        /// 返回DataSet对象
        /// </summary>
        /// <param name="SqlString"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string SqlString, string TableName)
        {
            try
            {
                this.db_header.Open();
                MySqlDataAdapter Da = this.GetDataAdapter(SqlString);
                DataSet ds = new DataSet();
                Da.Fill(ds, TableName);
                return ds;
            }
            catch (MySqlException ex)
            {
                this.DbError = ex.Message.ToString();
                return null;
            }
        }
        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="SqlString"></param>
        /// <returns></returns>
        public string GetOne(string SqlString)
        {
            string result = null;
            try
            {
                this.db_header.Open();
                MySqlCommand comm = new MySqlCommand(SqlString, this.db_header);
                MySqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    result = dr[0].ToString();
                    dr.Close();
                }
                else
                {
                    result = null;
                    dr.Close();
                }

            }
            catch (MySqlException ex)
            {
                this.DbError = ex.Message.ToString();
            }
            return result;
        }
        /// <summary>
        /// 连接测试
        /// </summary>
        /// <returns></returns>
        public bool TestConn()
        {
            try
            {
                this.db_header.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                this.DbError = ex.Message.ToString();
                return false;
            }
        }
        /// <summary>
        /// 关闭数据库句柄
        /// </summary>
        public void DbClose(MySqlConnection DbHeader)
        {
            if (DbHeader != null)
            {
                this.db_header.Close();
                this.db_header.Dispose();
            };

            GC.Collect();
        }




    }
}
