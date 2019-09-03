using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class IPAddressApply
    {
        /// <summary>
        /// 新增InsertIPAddressApply
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool InsertIPAddressApply(Model.IPAddressApply ip)
        {
            //参数化的SQL语句
            string sql = "insert into IPAddressApplies values (@ApplyID, @Quantity, @Address, @PortNumber,@TimeLimit)";
            SqlParameter[] para = { 
                                   new SqlParameter("ApplyID",ip.ApplyID),
                                   new SqlParameter("Quantity",ip.Quantity),
                                   new SqlParameter("Address",ip.Address),
                                   new SqlParameter("PortNumber",ip.PortNumber),
                                   new SqlParameter("TimeLimit",ip.TimeLimit),
                                };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
        /// <summary>
        /// 删除IPAddressApply
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool DeleteIPAddressApply(Model.IPAddressApply ip)
        {
            string sql = " delete from IPAddressApplies where ApplyID=@ApplyID";
            SqlParameter[] para ={
                                 new SqlParameter("ApplyID",ip.ApplyID),
                                 };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
        /// <summary>
        /// 获取单个IPAddressApply对象
        /// </summary>
        /// <param name="ApplyID"></param>
        /// <returns></returns>
        public static Model.IPAddressApply GetSingleIPAddressApply(int ApplyID)
        {

            string sql = "select * from IPAddressApplies where ApplyID=@ApplyID";
            SqlParameter[] para = { 
                                    new SqlParameter("ApplyID",ApplyID)
                                  };
            DataTable dt = DBHelper.ExecuteSelect(sql, para);
            DataRow dr = dt.Rows[0];//得到DataTable里面的第一行
            //以下代码的功能：就是把DataRow里面的数据取出来装到Model.UserInfo里面去
            Model.IPAddressApply ip = new Model.IPAddressApply();
            ip.ApplyID = (int)dr["ApplyID"];
            ip.Address = (string)dr["Address"];
            ip.PortNumber = (string)dr["PortNumber"];
            ip.Quantity = (int)dr["Quantity"];
            ip.TimeLimit = (DateTime)dr["TimeLimit"];
            return ip;
        }
    }
}
