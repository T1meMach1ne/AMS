using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class OperateLog
    {
        /// <summary>
        /// 获取单个操作记录对象
        /// </summary>
        /// <returns></returns>
        public static DataTable GetOperateLog(int ApplyID)
        {

            string sql = "select * from OperateLogs where ApplyID=@ApplyID";
            SqlParameter[] para = { 
                                    new SqlParameter("ApplyID",ApplyID)
                                  };
            DataTable dt = DBHelper.ExecuteSelect(sql, para);
            return dt;
        }
        /// <summary>
        /// 新增操作记录
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public static bool InsertOperateLog(Model.OperateLog op)
        {
            string sql = "insert into OperateLogs values (@ApplyID, @OperateType, @OperateDate,@UserID,@Describe,@Result)";
            SqlParameter[] para = { 
                                   new SqlParameter("ApplyID",op.ApplyID),
                                   new SqlParameter("OperateType",op.OperateType),
                                   new SqlParameter("OperateDate",op.OperateDate),
                                   new SqlParameter("UserID",op.UserID),
                                   new SqlParameter("Describe",op.Describe),
                                   new SqlParameter("Result",op.Result),
                                };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
    }
}
