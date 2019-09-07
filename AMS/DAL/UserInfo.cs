using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
    public class UserInfo
    {
       
        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static DataTable GetUserInfo(string where)
        {
            string sql = "select * from UserInfo left join Department on UserInfo.DeptID=Department.DeptID where 1=1" + where;

            DataTable dt = DBHelper.ExecuteSelect(sql);
            return dt;
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="UserID">被验证的用户名</param>
        /// <param name="Password">被验证的密码</param>
        /// <returns></returns>
        public static Model.UserInfo UserLogin(string UserID, string Password)
        {
            string sql = "select * from UserInfo  where UserID=@UserID and Password=@Password";
            SqlParameter[] para ={
                                    new SqlParameter("UserID",UserID),
                                    new SqlParameter("Password",Password)
                                };
            DataTable dt = DBHelper.ExecuteSelect(sql, para);
            Model.UserInfo u;
            if (dt.Rows.Count > 0)
            {
                u = new Model.UserInfo();   //表示用户名和密码正确
                DataRow dr = dt.Rows[0];
                u = new Model.UserInfo();
                u.Cellphone = (string)dr["Cellphone"];
                if (dr["DeptID"] != DBNull.Value)
                {
                    u.DeptID = (int)dr["DeptID"];
                }
                u.Password = (string)dr["Password"];
                u.UserID = (string)dr["UserID"];
                u.UserName = (string)dr["UserName"];
                u.UserType = (byte)dr["UserType"];
            }
            else
            {
                u = null;
            }
            return u;
        }   
        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddUserInfo(Model.UserInfo model)
        {
            
            string sql = "insert into UserInfo values('" + model.UserID + "','" + model.UserName + "'," + model.DeptID + ",'" + model.Password + "','" + model.Cellphone + "'," + model.UserType + ")";
            return DBHelper.ExecuteNonQuery(sql, null);
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateUserInfo(Model.UserInfo model)
        {
            string sql = "update UserInfo set UserName='"+model.UserName+"',DeptID='"+model.DeptID+"',CellPhone='"+model.Cellphone+"',UserType='"+model.UserType +"' where UserID ='" + model.UserID + "'";

            return DBHelper.ExecuteNonQuery(sql, null); ;
        }
        /// <summary>
        /// 修改部分用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateUserInfo1(Model.UserInfo model)
        {
            string sql = "update UserInfo set Password='" + model.Password + "', CellPhone='" + model.Cellphone + "' where UserID ='" + model.UserID + "'";

            return DBHelper.ExecuteNonQuery(sql, null); ;
        }
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool DelUserInfo(Model.UserInfo model)
        {
            string sql = " delete UserInfo where UserID='" + model.UserID + "'";
            return DBHelper.ExecuteNonQuery(sql, null);
        }
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool DelUserInfo1(Model.UserInfo model)
        {
            string sql = " delete UserInfo where UserID in(" + model.UserID + ")";   
            return DBHelper.ExecuteNonQuery(sql, null);
        }
    }
}
