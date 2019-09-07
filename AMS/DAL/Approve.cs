using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Approve
    {
        /// <summary>
        /// 查询申请信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static DataTable GetApproveInfo(string where)
        {
            string sql = "select b.UserName as ApplyName,c.UserName as ApproveName,*,(case Status when 0 then '待审批' when 1 then '归档' end ) NewStatus from Approve a "+
                         " left join UserInfo b on b.UserID = a.ApplyUser "+
                         " left join UserInfo c on c.UserID = a.ApproveUser where 1=1 " + where + "  order by  ApproveID desc  ";
            DataTable dt = DBHelper.ExecuteSelect(sql);
            return dt;
        }    
        /// <summary>
        /// 新增申请信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddApproveInfo(Model.Approve model)
        {
            string sql = "insert into Approve (ApplyUser,Title,BeginDate,EndDate,Reason,ApplyDate,Status,Result) values ('" + model.ApplyUser + "', '" + model.Title + "', '" + model.BeginDate + "', '" + model.EndDate + "', '" + model.Reason + "','" + model.ApplyDate + "', '" + model.Status + "', '" + model.Result + "')";
            return DBHelper.ExecuteNonQuery(sql, null);
        }
        /// <summary>
        /// 修改申请信息
        /// </summary> ApplyUser= '" + model.ApplyUser + "'
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateApproveInfo(Model.Approve model)
        {
            string sql = "update Approve set Title= '" + model.Title + "',BeginDate= '" + model.BeginDate + "',EndDate= '" + model.EndDate + "',Reason= '" + model.Reason + "',Result='" + model.Result + "',ApplyDate= '" + model.ApplyDate + "' where ApproveID= '" + model.ApproveID + "'";
            return DBHelper.ExecuteNonQuery(sql, null); ;
        }
        public static bool UpdateApproveInfo1(Model.Approve model)
        {
            string sql = "update Approve set ApproveUser='" + model.ApproveUser + "', ApproveDate='" + model.ApproveDate + "',Status='" + model.Status + "',Result='" + model.Result + "',Remark='" + model.Remark + "' where ApproveID= '" + model.ApproveID + "'";
            return DBHelper.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除申请信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool DelApproveInfo(Model.Approve model)
        {
            string sql = " delete Approve where ApproveID ='" + model.ApproveID + "'";
            return DBHelper.ExecuteNonQuery(sql, null);
        }
        /// <summary>
        /// 判断请假状态（修改和删除）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool SelectApproveState(int id)
        {
            string sql = " select * from Approve where ApproveID='" + id + "'";
            DataTable dt = DBHelper.ExecuteSelect(sql);
            bool b;
            if (dt.Rows[0]["Status"].ToString() == "0")
            {
                //显示
                b = true;
            }
            else
            {
                //隐藏
                b = false;
            }
            return b;
        }
    }
}
