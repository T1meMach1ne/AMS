using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class OfficeEmailApply
    {
        /// <summary>
        /// 新增InsertOfficeEmailApply
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool InsertOfficeEmailApply(Model.OfficeEmailApply off)
        {
            //参数化的SQL语句
            string sql = "insert into OfficeEmailApplies values (@ApplyID, @Zone, @OfficePlace, @UserName,@FullName)";
            SqlParameter[] para = { 
                                   new SqlParameter("ApplyID",off.ApplyID),
                                   new SqlParameter("Zone",off.Zone),
                                   new SqlParameter("OfficePlace",off.UserName),
                                   new SqlParameter("UserName",off.UserName),
                                   new SqlParameter("FullName",off.FullName),
                                };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
        /// <summary>
        /// 删除OfficeEmailApply
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool DeleteOfficeEmailApply(Model.OfficeEmailApply off)
        {
            string sql = " delete from OfficeEmailApplies where ApplyID=@ApplyID";
            SqlParameter[] para ={
                                 new SqlParameter("ApplyID",off.ApplyID),
                                 };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
        /// <summary>
        /// 获取单个OfficeEmailApply对象
        /// </summary>
        /// <param name="ApplyID"></param>
        /// <returns></returns>
        public static Model.OfficeEmailApply GetSingleOfficeEmailApply(int ApplyID)
        {

            string sql = "select * from OfficeEmailApplies where ApplyID=@ApplyID";
            SqlParameter[] para = { 
                                    new SqlParameter("ApplyID",ApplyID)
                                  };
            DataTable dt = DBHelper.ExecuteSelect(sql, para);
            DataRow dr = dt.Rows[0];//得到DataTable里面的第一行
            //以下代码的功能：就是把DataRow里面的数据取出来装到Model.UserInfo里面去
            Model.OfficeEmailApply off = new Model.OfficeEmailApply();
            off.ApplyID = (int)dr["ApplyID"];
            off.FullName = (string)dr["FullName"];
            off.OfficePlace = (string)dr["OfficePlace"];
            off.UserName = (string)dr["UserName"];
            off.Zone = (int)dr["Zone"];
            return off;
        }
    }
}
