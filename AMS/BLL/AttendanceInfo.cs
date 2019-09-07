using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
   public class AttendanceInfo
    {
        /// <summary>
        /// 查询我的考勤信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
       public static DataTable GetMyAttendanceInfo(string userID, DateTime date)
        {
            return DAL.AttendanceInfo.GetMyAttendanceInfo(userID, date);
        }
       public static DataTable GetMyAttendanceInfo1(string userID)
       {
           return DAL.AttendanceInfo.GetMyAttendanceInfo1(userID);
       }

        /// <summary>
       /// 实现批量添加考勤
       /// </summary>
       /// <param name="dt"></param>
       /// <returns></returns>
       public static bool InsertDataTable(DataTable dt)
       {
           return DAL.AttendanceInfo.InsertDataTable(dt);
       }
    }
}
