using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;

namespace Apply.Handlers
{
    /// <summary>
    /// UserInfoHandler 的摘要说明
    /// </summary>
    public class UserInfoHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string method = context.Request["Method"];
            switch (method)
            {
                case "BindDealer":
                    BindDealer(context);
                    break;
                case "UpdatePersonalInfo":
                    UpdatePersonalInfo(context);
                    break;
                case "UpdatePersonalPwd":
                    UpdatePersonalPwd(context);
                    break;
                case "GetPagedUserInfo":
                    GetPagedUserInfo(context);
                    break;
                case "BindDept":
                    BindDept(context);
                    break;
                case "InsertUserInfo":
                    InsertUserInfo(context);
                    break;
                case "GetSingleUserInfo":
                    GetSingleUserInfo(context);
                    break;
                case "UpdateUserInfo":
                    UpdateUserInfo(context);
                    break;
                case "DeleteUserInfo":
                    DeleteUserInfo(context);
                    break;
            }

        }
        /// <summary>
        /// 绑定处理人下拉框
        /// </summary>
        /// <param name="context"></param>
        public static void BindDealer(HttpContext context)
        {
            DataTable dt = BLL.UserInfo.BindDealer();
            List<Model.UserInfo> list = new List<Model.UserInfo>();
            foreach (DataRow dr in dt.Rows)
            {
                Model.UserInfo us = dr.ToModel<Model.UserInfo>();
                list.Add(us);
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(list);
            context.Response.Write(json);
        }
        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="context"></param>
        public static void UpdatePersonalInfo(HttpContext context)
        {
            string UserID = context.Request["ID"];
            string Phone = context.Request["Phone"];
            string Name = context.Request["Name"];
            string EMail = context.Request["EMail"];
            int DeptID = Convert.ToInt32(context.Request["DeptID"]);
            string i;
            if (BLL.UserInfo.UpdatePersonalInfo(UserID, Name, Phone, EMail, DeptID))
            {
                i = "1";
            }
            else
            {
                i = "0";
            }
            context.Response.Write(i);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="context"></param>
        public static void UpdatePersonalPwd(HttpContext context)
        {
            string UserID = context.Request["ID"];
            string oldPwd = context.Request["oldPwd"];
            string Password = context.Request["NewPwd"];
            string i = string.Empty;
            Model.UserInfo user = BLL.UserInfo.UserLogin(UserID, oldPwd);
            if (user != null)
            {
                if (BLL.UserInfo.UpdatePersonalPwd(UserID, Password))
                {
                    i = "1";
                }
                else
                {
                    i = "0";
                }
            }
            else
            {
                i = "2";
            }
            context.Response.Write(i);
        }

        /// <summary>
        /// 绑定用户信息表
        /// </summary>
        /// <param name="context"></param>
        public static void GetPagedUserInfo(HttpContext context)
        {
            string UserID = context.Request["UserID"];
            string Name = context.Request["Name"];
            int DeptID = Convert.ToInt32(context.Request["DeptID"]);
            DataTable dt = BLL.UserInfo.BindUserInfo("", UserID, Name, DeptID);
            List<object> list = new List<object>();
            foreach (DataRow dr in dt.Rows)
            {
                Model.UserInfo user = dr.ToModel<Model.UserInfo>();
                Model.Department dept = dr.ToModel<Model.Department>();
                list.Add(new { UserInfo = user, Department = dept });
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(list);
            context.Response.Write(json);
        }
        

        /// <summary>
        /// 绑定部门下拉框
        /// </summary>
        /// <param name="context"></param>
        public static void BindDept(HttpContext context)
        {
            DataTable dt = BLL.Department.SelectDept("");
            List<Model.Department> list = new List<Model.Department>();
            foreach (DataRow dr in dt.Rows)
            {
                Model.Department d = dr.ToModel<Model.Department>();
                list.Add(d);
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(list);
            context.Response.Write(json);
        }
        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="context"></param>
        public static void InsertUserInfo(HttpContext context)
        {
            Model.UserInfo u = new Model.UserInfo();
            string i = string.Empty;
            string UserID = context.Request["UserID"];
            string Name = context.Request["Name"];
            string Pwd = context.Request["Pwd"];
            string Phone = context.Request["Phone"];
            string Mail = context.Request["Mail"];
            int DeptID = Convert.ToInt32(context.Request["DeptID"]);
            u.DeptID = DeptID;
            u.Email = Mail;
            u.Name = Name;
            u.Password = Pwd;
            u.Telephone = Phone;
            u.Type = 1;
            u.UserID = UserID;
            string where = " where UserID='" + UserID + "'";
            DataTable dt = BLL.UserInfo.SelectUserInfo(where);
            if (dt.Rows.Count == 0)
            {
                if (BLL.UserInfo.InsertUserInfo(u))
                {
                    i = "1";
                }
                else
                {
                    i = "0";
                }
            }
            else
            {
                i = "2";
            }
            context.Response.Write(i);
        }
        /// <summary>
        /// 获取单个用户信息
        /// </summary>
        /// <param name="context"></param>
        public static void GetSingleUserInfo(HttpContext context)
        {
            string UserID = context.Request["ID"];
            Model.UserInfo u = BLL.UserInfo.GetSingleUserInfo(UserID);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(u);
            context.Response.Write(json);
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="context"></param>
        public static void UpdateUserInfo(HttpContext context)
        {
            string i = string.Empty;
            string UserID = context.Request["ID"].ToString().Trim();
            string Name = context.Request["Name"];
            string Phone = context.Request["Phone"];
            string Mail = context.Request["Mail"];
            int DeptID = Convert.ToInt32(context.Request["DeptId"]);
            string NewPwd = context.Request["NewPwd"];
            Model.UserInfo u = new Model.UserInfo();
            u.DeptID = DeptID;
            u.Email = Mail;
            u.Name = Name;
            u.Telephone = Phone;
            u.UserID = UserID;
            if (NewPwd == "")
            {
                u.Password = BLL.UserInfo.SelectUserInfo(" where UserID='" + UserID + "'").Rows[0]["Password"].ToString();
                if (BLL.UserInfo.UpdatUserInfo(u))
                {
                    i = "1";
                }
                else
                {
                    i = "0";
                }

            }
            else
            {
                u.Password = NewPwd;
                if (BLL.UserInfo.UpdatUserInfo(u))
                {
                    i = "1";
                }
                else
                {
                    i = "0";
                }
            }
            context.Response.Write(i);
        }
        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="context"></param>
        public static void DeleteUserInfo(HttpContext context)
        {
            Model.UserInfo u = new Model.UserInfo();
            string UserID = context.Request["ID"];
            u.UserID = UserID;
            string i = string.Empty;
            if (BLL.UserInfo.DeleteUserInfo(u))
            {
                i = "1";
            }
            else
            {
                i = "0";
            }
            context.Response.Write(i);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}