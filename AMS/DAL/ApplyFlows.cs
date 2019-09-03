using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ApplyFlows
    {
        /// <summary>
        /// 查询申请单类型
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectApplyFlow(string where)
        {
            string sql = " select * from ApplyFlows" + where;
            DataTable dt = DBHelper.ExecuteSelect(sql);
            return dt;
        }
        /// <summary>
        /// 判断当前登录人是不是部门分配人
        /// </summary>
        /// <returns></returns>
        public static DataTable IsAssigner(string Assigner)
        {
            string sql = " select * from ApplyFlows where Assigner=@Assigner";
            SqlParameter[] para = { 
                                    new SqlParameter("Assigner",Assigner)
                                  };
            DataTable dt = DBHelper.ExecuteSelect(sql, para);
            return dt;
        }
        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public static Model.ApplyFlow GetSingleApplyFlow(int ApplyTypeID)
        {
            string sql = " select * from ApplyFlows where ApplyTypeID=@ApplyTypeID";
            SqlParameter[] para = { 
                                    new SqlParameter("ApplyTypeID",ApplyTypeID)
                                  };
            DataTable dt = DBHelper.ExecuteSelect(sql, para);
            DataRow dr = dt.Rows[0];
            Model.ApplyFlow af = new Model.ApplyFlow();
            af.ApplyTypeID = (int)dr["ApplyTypeID"];
            af.ApplyTypeName = (string)dr["ApplyTypeName"];
            af.Assigner = (string)dr["Assigner"];
            return af;
        }
        /// <summary>
        /// 修改方法
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool UpdatApplyFlow(Model.ApplyFlow af)
        {
            //参数化的SQL语句
            string sql = " update ApplyFlows set Assigner=@Assigner where ApplyTypeID=@ApplyTypeID";
            SqlParameter[] para = { 
                                   new SqlParameter("Assigner",af.Assigner),
                                   new SqlParameter("ApplyTypeID",af.ApplyTypeID)
                                };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
    }
}
