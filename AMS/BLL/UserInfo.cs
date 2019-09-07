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
        /// 查询用户信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static DataTable GetUserInfo(string where)
        {
            return DAL.UserInfo.GetUserInfo(where);
        }
        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddUserInfo(Model.UserInfo model)
        {

            return DAL.UserInfo.AddUserInfo(model);
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateUserInfo(Model.UserInfo model)
        {
            bool i = DAL.UserInfo.UpdateUserInfo(model);
            return i;
        }
        /// <summary>
        /// 修改部分用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateUserInfo1(Model.UserInfo model)
        {
            bool i = DAL.UserInfo.UpdateUserInfo1(model);
            return i;
        }
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool DelUserInfo(Model.UserInfo model)
        {
            bool i = DAL.UserInfo.DelUserInfo(model);
            return i;
        }
        public static bool DelUserInfo1(Model.UserInfo model)
        {
            bool i = DAL.UserInfo.DelUserInfo1(model);
            return i;
        }
        
    }
}
