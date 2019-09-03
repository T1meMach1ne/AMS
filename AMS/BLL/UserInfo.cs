using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class UserInfo
    {
        /// <summary>
        /// 判断用户名和密码
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static Model.UserInfo UserLogin(string UserID, string Password)
        {
            return DAL.UserInfo.UserLogin(UserID, Password);
        }
        /// <summary>
        /// 判断当前登录人是不是部门管理人
        /// </summary>
        /// <returns></returns>
        public static DataTable IsManager(string UserID)
        {
            return DAL.UserInfo.IsManager(UserID);
        }
         /// <summary>
        /// 绑定分配处理人下拉框
        /// </summary>
        /// <returns></returns>
        public static DataTable BindDealer()
        {
            return DAL.UserInfo.BindDealer();
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
        public static bool UpdatePersonalInfo(string UserID, string Name, string Telephone, string Email, int DeptID)
        {
            return DAL.UserInfo.UpdatePersonalInfo(UserID, Name, Telephone, Email, DeptID);
        }
         /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public static bool UpdatePersonalPwd(string UserID, string Password)
        {
            return DAL.UserInfo.UpdatePersonalPwd(UserID, Password);
        }
         /// <summary>
        /// 查询员工信息
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectUserInfo(string where)
        {
            return DAL.UserInfo.SelectUserInfo(where);
        }
        /// <summary>
        /// 绑定员工信息表
        /// </summary>
        /// <returns></returns>
        public static DataTable BindUserInfo(string where, string UserID, string Name, int DeptID)
        {
            return DAL.UserInfo.BindUserInfo(where,UserID,Name,DeptID);
        }
        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool InsertUserInfo(Model.UserInfo u)
        {
            return DAL.UserInfo.InsertUserInfo(u);
        }
         /// <summary>
        /// 获取单个用户对象
        /// </summary>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public static Model.UserInfo GetSingleUserInfo(string UserID)
        {
            return DAL.UserInfo.GetSingleUserInfo(UserID);
        }
         /// <summary>
        /// 修改方法
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool UpdatUserInfo(Model.UserInfo u)
        {
            return DAL.UserInfo.UpdatUserInfo(u);
        }
        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool DeleteUserInfo(Model.UserInfo u)
        {
            return DAL.UserInfo.DeleteUserInfo(u);
        }
    }
}
