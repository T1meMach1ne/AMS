using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Apply
    {
        /// <summary>
        /// 查询方法
        /// </summary>
        /// <param name="B_Name"></param>
        /// <param name="B_Author"></param>
        /// <param name="B_ID"></param>
        /// <returns></returns>
        public static DataTable SelecApply(string where,int ApplyID=0,int ApplyStatus=0, int ApplyTypeID=0, string ApplyTitle="")
        {
            string sql = "select * from Applies a left join ApplyFlows b on a.ApplyTypeID=b.ApplyTypeID left join UserInfos"
            + " c on a.UserID=c.UserID left join Departments d on c.DeptID=d.DeptID where a.ApplyTitle like '%" + ApplyTitle + "%'" + where;
            if (ApplyStatus != 0)
            {
                if (ApplyStatus == 5)
                {
                    //进行字符串的追加
                    sql += " and a.ApplyStatus not in (1)";
                }
                if (ApplyStatus == 6)
                {
                    //进行字符串的追加
                    sql += " and a.ApplyStatus in (3,4)";
                }
                if (ApplyStatus != 5 && ApplyStatus != 6)
                {
                    //进行字符串的追加
                    sql += " and a.ApplyStatus=" + ApplyStatus + "";
                }
            }
            if (ApplyTypeID != 0)
            {
                //进行字符串的追加
                sql += " and a.ApplyTypeID=" + ApplyTypeID + "";
            }
            if (ApplyID != 0)
            {
                //进行字符串的追加
                sql += " and a.ApplyID=" + ApplyID + "";
            }
            return DBHelper.ExecuteSelect(sql);
        }
        /// <summary>
        /// 获取单个申请单对象
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSingleApply(int ApplyID)
        {

            string sql = "select * from Applies a left join UserInfos b on a.UserID=b.UserID left join Departments c on b.DeptID=c.DeptID where a.ApplyID=@ApplyID";
            SqlParameter[] para = { 
                                    new SqlParameter("ApplyID",ApplyID)
                                  };
            DataTable dt = DBHelper.ExecuteSelect(sql, para);           
            return dt;
        }
        /// <summary>
        /// 获取最大日期所在行
        /// </summary>
        /// <returns></returns>
        public static Model.Apply GetMaxDateApply()
        {
            
            string sql = "select top 1 * from Applies order by ApplyDate desc ";
            DataTable dt = DBHelper.ExecuteSelect(sql);
            DataRow dr = dt.Rows[0];//得到DataTable里面的第一行
            //以下代码的功能：就是把DataRow里面的数据取出来装到Model.UserInfo里面去
            Model.Apply a = new Model.Apply();
            a.ApplyID = (int)dr["ApplyID"];
            a.ApplyTypeID = (int)dr["ApplyTypeID"];
            a.ApplyDate = (DateTime)dr["ApplyDate"];
            a.ApplyStatus = (int)dr["ApplyStatus"];
            a.UserID = (string)dr["UserID"];
            a.Approver = (string)dr["Approver"];
            a.Assigner = (string)dr["Assigner"];
            a.Dealer = (string)dr["Dealer"];
            a.Phone = (string)dr["Phone"];
            a.ApplyTitle = (string)dr["ApplyTitle"];
            a.ApplyReason = (string)dr["ApplyReason"];
            a.Enclosure = (string)dr["Enclosure"];
            a.Remark = (string)dr["Remark"];
            return a;
        }
        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool InsertApply(Model.Apply a)
        {
            //参数化的SQL语句
            string sql = "insert into Applies values (@ApplyTypeID, @ApplyDate, @ApplyStatus, @UserID,@Approver,@Assigner,@Dealer,@Phone,@ApplyTitle,@ApplyReason,@Enclosure,@Remark)";
            SqlParameter[] para = { 
                                   new SqlParameter("ApplyTypeID",a.ApplyTypeID),
                                   new SqlParameter("ApplyDate",a.ApplyDate),
                                   new SqlParameter("ApplyStatus",a.ApplyStatus),
                                   new SqlParameter("UserID",a.UserID),
                                   new SqlParameter("Approver",a.Approver),
                                   new SqlParameter("Assigner",a.Assigner),
                                   new SqlParameter("Dealer",a.Dealer),
                                   new SqlParameter("Phone",a.Phone),
                                   new SqlParameter("ApplyTitle",a.ApplyTitle),
                                   new SqlParameter("ApplyReason",a.ApplyReason),
                                   new SqlParameter("Enclosure",a.Enclosure),
                                   new SqlParameter("Remark",a.Remark),
                                };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
        /// <summary>
        /// 审批申请单
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool ApproveApply(Model.Apply a)
        {
            //参数化的SQL语句
            string sql = "update Applies set ApplyStatus=@ApplyStatus where ApplyID=@ApplyID";
            SqlParameter[] para = { 
                                   new SqlParameter("ApplyID",a.ApplyID),
                                   new SqlParameter("ApplyStatus",a.ApplyStatus)
                                  };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
        /// <summary>
        /// 分配申请单
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool AssignerApply(Model.Apply a)
        {
            //参数化的SQL语句
            string sql = "update Applies set ApplyStatus=@ApplyStatus,Dealer=@Dealer where ApplyID=@ApplyID";
            SqlParameter[] para = { 
                                   new SqlParameter("ApplyID",a.ApplyID),
                                   new SqlParameter("Dealer",a.Dealer),
                                   new SqlParameter("ApplyStatus",a.ApplyStatus)
                                  };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
        /// <summary>
        /// 处理申请单
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool DealerApply(Model.Apply a)
        {
            //参数化的SQL语句
            string sql = "update Applies set ApplyStatus=@ApplyStatus where ApplyID=@ApplyID";
            SqlParameter[] para = { 
                                   new SqlParameter("ApplyID",a.ApplyID),
                                   new SqlParameter("ApplyStatus",a.ApplyStatus)
                                  };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool DeleteApply(Model.Apply a)
        {
            string sql = " delete from Applies where ApplyID=@ApplyID";
            SqlParameter[] para ={
                                 new SqlParameter("ApplyID",a.ApplyID),
                                 };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
    }
}
