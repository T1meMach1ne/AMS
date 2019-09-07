using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace DAL
{
    public class DBHelper
    {
        //1、获取数据库连接字符串
        public static string connString = ConfigurationManager.ConnectionStrings["AttendanceConnectionString"].ConnectionString;
        //2、专门用来执行增、删、改的方法
        /// <summary>
        /// 此方法专门用来执行增、删、改的sql语句;如果执行成功，则返回true;如果执行失败，则返回false;
        /// </summary>
        /// <param name="sql">参数化的sql语句</param>
        /// <param name="para">SqlParameter数组型的参数:如果此sql语句没有参数则para为null;否则在调用方传一个SqlParameter[]数组</param>
        /// <returns></returns>
        public static bool ExecuteNonQuery(string sql, params SqlParameter[] para)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    if (para != null)
                    {
                        cmd.Parameters.AddRange(para);
                    }
                    //打开连接
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    int i = cmd.ExecuteNonQuery();
                    return i > 0 ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //3、专门用来执行查询的方法
        /// <summary>
        /// 此方法专门用来执行sql语句，并且返回一个DataTable对象
        /// </summary>
        /// <param name="sql">参数化的sql语句(一般为含有select关键字的sql语句)</param>
        /// <param name="para">SqlParameter数组型的参数:如果此sql语句没有参数则para为null;否则在调用方传一个SqlParameter[]数组</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteSelect(string sql, params SqlParameter[] para)
        {
            //SQL注入式攻击
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, connString);
                if (para != null)
                {
                    da.SelectCommand.Parameters.AddRange(para);
                }
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DataTable ExecuteQuery(string userID, int year, int month)
        {

            DataTable dt = new DataTable();
            SqlParameter[] sp = new SqlParameter[]{
           new SqlParameter("@userID",SqlDbType.Char),
           new SqlParameter("@year",SqlDbType.Int),
           new SqlParameter("@month",SqlDbType.Int),
           };
            sp[0].Value = userID;
            sp[1].Value = year;
            sp[2].Value = month;
            SqlConnection conn = new SqlConnection(connString);
            if (conn.State != ConnectionState.Open)
            {

                conn.Open();
            }
            SqlCommand comm = new SqlCommand("dbo.p_GetAttendanceList", conn);
            comm.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter spt in sp)
            {

                comm.Parameters.Add(spt);
            }
            using (SqlDataAdapter sda = new SqlDataAdapter(comm))
            {

                sda.Fill(dt);
            }
            return dt;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="PageSiza">每页显示多少条数据</param>
        /// <param name="CurrentPage"> 当前第几页</param>
        /// <param name="Sort">排序字段</param>
        /// <param name="RecordCount">输出参数（共多少条数据）</param>
        /// <returns></returns>
        public static DataTable ExecuteQuery(string sql, int PageSize, int CurrentPage, string Sort, out int RecordCount)
        {

            DataTable dt = new DataTable();
            SqlParameter[] sp = new SqlParameter[]{
           new SqlParameter("@Source",SqlDbType.NVarChar),
           new SqlParameter("@PageSize",SqlDbType.Int),
           new SqlParameter("@CurrentPage",SqlDbType.Int),
           new SqlParameter("@FieldList ",SqlDbType.NVarChar),
           new SqlParameter("@Sort",SqlDbType.NVarChar),
           new SqlParameter("@RecordCount",SqlDbType.Int),
           new SqlParameter("@FdName",SqlDbType.NVarChar)
           };
            sp[0].Value = sql;
            sp[1].Value = PageSize;
            sp[2].Value = CurrentPage;
            sp[3].Value = DBNull.Value;
            sp[4].Value = Sort;
            sp[5].Value = DBNull.Value;
            sp[5].Direction = ParameterDirection.Output;
            sp[6].Value = DBNull.Value;
            SqlConnection conn = new SqlConnection(connString);
            if (conn.State != ConnectionState.Open)
            {

                conn.Open();
            }
            SqlCommand comm = new SqlCommand("pro_sys_GetRecordByPage2005", conn);
            comm.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter spt in sp)
            {

                comm.Parameters.Add(spt);
            }
            using (SqlDataAdapter sda = new SqlDataAdapter(comm))
            {

                sda.Fill(dt);
            }
            RecordCount = (int)sp[5].Value;
            return dt;
        }

        /// <summary>
        /// 执行排序的方法
        /// </summary>
        /// <param name="viewS"></param>
        /// <returns></returns>
        public static DataTable SortReturnValue(string viewS)
        {
            string sql = string.Format("select * from UsersInfo order by {0}", viewS);
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }
    }
}
