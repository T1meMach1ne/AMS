using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class StorageSpaceApply
    {
        /// <summary>
        /// 新增InsertStorageSpaceApply
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool InsertStorageSpaceApply(Model.StorageSpaceApply st)
        {
            //参数化的SQL语句
            string sql = "insert into StorageSpaceApplies values (@ApplyID, @Zone, @InterfaceMan, @Right,@TimeLimit)";
            SqlParameter[] para = { 
                                   new SqlParameter("ApplyID",st.ApplyID),
                                   new SqlParameter("Zone",st.Zone),
                                   new SqlParameter("InterfaceMan",st.InterfaceMan),
                                   new SqlParameter("Right",st.Right),
                                   new SqlParameter("TimeLimit",st.TimeLimit),
                                };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
        /// <summary>
        /// 删除StorageSpaceApply
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool DeleteStorageSpaceApply(Model.StorageSpaceApply st)
        {
            string sql = " delete from StorageSpaceApplies where ApplyID=@ApplyID";
            SqlParameter[] para ={
                                 new SqlParameter("ApplyID",st.ApplyID),
                                 };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
        /// <summary>
        /// 获取单个StorageSpaceApply对象
        /// </summary>
        /// <param name="ApplyID"></param>
        /// <returns></returns>
        public static Model.StorageSpaceApply GetSingleStorageSpaceApply(int ApplyID)
        {

            string sql = "select * from StorageSpaceApplies where ApplyID=@ApplyID";
            SqlParameter[] para = { 
                                    new SqlParameter("ApplyID",ApplyID)
                                  };
            DataTable dt = DBHelper.ExecuteSelect(sql, para);
            DataRow dr = dt.Rows[0];//得到DataTable里面的第一行
            //以下代码的功能：就是把DataRow里面的数据取出来装到Model.UserInfo里面去
            Model.StorageSpaceApply st = new Model.StorageSpaceApply();
            st.ApplyID = (int)dr["ApplyID"];
            st.InterfaceMan = (string)dr["InterfaceMan"];
            st.Right = (string)dr["Right"];
            st.TimeLimit = (DateTime)dr["TimeLimit"];
            st.Zone = (int)dr["Zone"];
            return st;
        }
    }
}
