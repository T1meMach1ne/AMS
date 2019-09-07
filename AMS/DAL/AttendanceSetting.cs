using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class AttendanceSetting
    {
        /// <summary>
        /// 查询考勤设置信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static DataTable GetAttendanceSettingInfo(string where)
        {
            string sql = " select SettingID,CONVERT(varchar(10),Date,120) as NewDate,DATENAME(WEEKDAY,Date) as WeekDays,Status from AttendanceSetting where 1=1" + where;
            return DBHelper.ExecuteSelect(sql);
        }
        /// <summary>
        /// 获取考勤设置信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public static DataTable GetAttendanceSetting(string UserID, int Year, int Month)
        {
            string sql = "exec p_GetAttendanceList '" + UserID + "'," + Year + "," + Month + "";
            return DBHelper.ExecuteQuery(UserID, Year, Month);
        }
        /// <summary>
        /// 新增考勤设置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddAttendanceSettingInfo(Model.AttendanceSetting model)
        {
            string sql = " insert into AttendanceSetting values('" + model.Date + "'," + model.Status + ")";
            return DBHelper.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 修改考勤设置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateAttendanceSettingInfo(Model.AttendanceSetting model)
        {
            string sql = " update AttendanceSetting set Date='" + model.Date + "',Status=" + model.Status + " where SettingID=" + model.SettingID + "";
            return DBHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 根据日期得到单个考勤设置对象
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static Model.AttendanceSetting GetAttendanceSettingByDate(DateTime date)
        {
            string sql = "select * from AttendanceSetting where Date=@Date";
            SqlParameter[] para = { 
                                    new SqlParameter("Date",date)
                                  };
            DataTable dt = DBHelper.ExecuteSelect(sql, para);
            //先得到第一行
            Model.AttendanceSetting a;
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                a = new Model.AttendanceSetting();
                a.Date = (DateTime)dr["Date"];
                a.SettingID = (int)dr["SettingID"];
                a.Status = (byte)dr["Status"];
            }
            else
            {
                a = null;
            }
            return a;
        }



        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool InsertDataTable(DataTable dt)
        {
            try
            {
                SqlBulkCopy bc = new SqlBulkCopy(DBHelper.connString);
                bc.DestinationTableName = "AttendanceSetting";
                bc.WriteToServer(dt);
                bc.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除设置的数据
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static bool DeleteAttendanceSetting(int year, int month)
        {
            string sql = "delete from AttendanceSetting where year(Date)=@Year and month(Date)=@Month";
            SqlParameter[] para = { 
                                    new SqlParameter("Year",year),
                                    new SqlParameter("Month",month)
                                  };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
    }
}
