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
        /// 登录验证
        /// </summary>
        /// <param name="UserID">被验证的用户名</param>
        /// <param name="Password">被验证的密码</param>
        /// <returns></returns>
        public static Model.UserInfo UserLogin(string UserID, string Password)
        {
            string sql = "select * from UserInfos where UserID=@UserID and Password=@Password";
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
                u.UserID = (string)dr["UserID"];
                u.Name = (string)dr["Name"];
                u.Password = (string)dr["Password"];
                u.Email = (string)dr["Email"];
                u.Telephone = (string)dr["Telephone"];
                if (dr["DeptID"] != DBNull.Value)
                {
                    u.DeptID = (int)dr["DeptID"];
                }
                if (dr["Type"] != DBNull.Value)
                {
                    u.Type = (int)dr["Type"];
                }            
            }
            else
            {
                u = null;
            }
            return u;
        }
        /// <summary>
        /// 判断当前登录人是不是部门管理人
        /// </summary>
        /// <returns></returns>
        public static DataTable IsManager(string UserID)
        {
            string sql = " select * from UserInfos a left join Departments b on a.DeptID=b.DeptID where UserID=@UserID";
            SqlParameter[] para = { 
                                    new SqlParameter("UserID",UserID)
                                  };
            DataTable dt = DBHelper.ExecuteSelect(sql, para);
            return dt;
        }
        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Name"></param>
        /// <param name="Telephone"></param>
        /// <param name="Email"></param>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public static bool UpdatePersonalInfo(string UserID,string Name, string Telephone, string Email, int DeptID)
        {
            string sql = " update UserInfos set Name=@Name,Telephone=@Telephone,Email=@Email,DeptID=@DeptID where UserID=@UserID";
            SqlParameter[] para = { 
                                    new SqlParameter("Name",Name),
                                    new SqlParameter("Telephone",Telephone),
                                    new SqlParameter("Email",Email),
                                    new SqlParameter("DeptID",DeptID),
                                    new SqlParameter("UserID",UserID)
                                  };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public static bool UpdatePersonalPwd(string UserID, string Password)
        {
            string sql = " update UserInfos set Password=@Password where UserID=@UserID";
            SqlParameter[] para = { 
                                    new SqlParameter("UserID",UserID),
                                    new SqlParameter("Password",Password)
                                  };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
        /// <summary>
        /// 绑定分配处理人下拉框
        /// </summary>
        /// <returns></returns>
        public static DataTable BindDealer()
        {
            string sql = " select * from UserInfos where DeptID=2";
            DataTable dt = DBHelper.ExecuteSelect(sql);
            return dt;
        }
        /// <summary>
        /// 查询员工信息
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectUserInfo(string where)
        {
            string sql = " select * from UserInfos" + where;
            DataTable dt = DBHelper.ExecuteSelect(sql);
            return dt;
        }
        /// <summary>
        /// 绑定用户信息表
        /// </summary>
        /// <returns></returns>
        public static DataTable BindUserInfo(string where,string UserID="",string Name="",int DeptID=0)
        {
            string sql = " select * from UserInfos a left join Departments b on a.DeptID=b.DeptID where a.UserID like '%" + UserID + "%' and a.Name like '%" + Name + "%' " + where;
            if (DeptID != 0)
            {
                //进行字符串的追加
                sql += " and a.DeptID=" + DeptID + "";
            }
            return DBHelper.ExecuteSelect(sql); ;
        }
        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool InsertUserInfo(Model.UserInfo u)
        {
            //参数化的SQL语句
            string sql = "insert into UserInfos values (@UserID,@Name, @Password,@Email,@Telephone, @Type,@DeptID)";
            SqlParameter[] para = { 
                                   new SqlParameter("UserID",u.UserID),
                                   new SqlParameter("Name",u.Name),
                                   new SqlParameter("Password",u.Password),   
                                   new SqlParameter("Email",u.Email),
                                   new SqlParameter("Telephone",u.Telephone),
                                   new SqlParameter("Type",u.Type),
                                   new SqlParameter("DeptID",u.DeptID)
                                };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
        /// <summary>
        /// 获取单个用户对象
        /// </summary>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public static Model.UserInfo GetSingleUserInfo(string UserID)
        {
            string sql = " select * from UserInfos where UserID=@UserID";
            SqlParameter[] para = { 
                                    new SqlParameter("UserID",UserID)
                                  };
            DataTable dt = DBHelper.ExecuteSelect(sql, para);
            DataRow dr = dt.Rows[0];
            Model.UserInfo u = new Model.UserInfo();
            u.UserID = (string)dr["UserID"];
            u.Type = (int)dr["Type"];
            u.Telephone = (string)dr["Telephone"];
            u.Password = (string)dr["Password"];
            u.Name = (string)dr["Name"];
            u.Email = (string)dr["Email"];
            u.DeptID = (int)dr["DeptID"];
            return u;
        }
        /// <summary>
        /// 修改方法
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool UpdatUserInfo(Model.UserInfo u)
        {
            //参数化的SQL语句
            string sql = " update UserInfos set Name=@Name,Telephone=@Telephone,DeptID=@DeptID,Email=@Email,Password=@Password where UserID=@UserID";
            SqlParameter[] para = { 
                                   new SqlParameter("Name",u.Name),
                                   new SqlParameter("Telephone",u.Telephone),
                                   new SqlParameter("DeptID",u.DeptID),
                                   new SqlParameter("Email",u.Email),   
                                   new SqlParameter("Password",u.Password), 
                                   new SqlParameter("UserID",u.UserID), 
                                };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool DeleteUserInfo(Model.UserInfo u)
        {
            string sql = " delete from UserInfos where UserID=@UserID";
            SqlParameter[] para ={
                                 new SqlParameter("UserID",u.UserID),
                                 };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
    }
}
