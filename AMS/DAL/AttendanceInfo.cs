using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
   public class AttendanceInfo
    {
      
       /// <summary>
       /// 根据UserID、当天日期来得到打卡信息
       /// </summary>
       /// <param name="userID"></param>
       /// <param name="date"></param>
       /// <returns></returns>
       public static DataTable GetMyAttendanceInfo(string userID, DateTime date)
       {
           string sql = "select min(FaceTime) FirstTime,max(FaceTime) LastTime from AttendanceInfo where UserID=@UserID and convert(varchar(10),FaceTime,112)=@Date";
           SqlParameter[] para = { 
                                    new SqlParameter("UserID",userID),
                                    new SqlParameter("Date",date)
                                  };
           return DBHelper.ExecuteSelect(sql, para);
       }
       public static DataTable GetMyAttendanceInfo1(string userID)
       {
           string sql = "select * from AttendanceInfo where UserID=@UserID ";
           SqlParameter[] para = { 
                                    new SqlParameter("UserID",userID),
                                  };
           return DBHelper.ExecuteSelect(sql, para);
       }

       /// <summary>
       /// 实现批量添加考勤
       /// </summary>
       /// <param name="dt"></param>
       /// <returns></returns>
       public static bool InsertDataTable(DataTable dt)
       {
           try
           {
               SqlBulkCopy bc = new SqlBulkCopy(DBHelper.connString);
               bc.DestinationTableName = "AttendanceInfo";
               bc.WriteToServer(dt);
               bc.Close();
               return true;
           }
           catch (Exception)
           {
               return false;
           }
       }
    }
}
