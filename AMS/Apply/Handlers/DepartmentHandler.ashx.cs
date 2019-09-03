using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;

namespace Apply.Handlers
{
    /// <summary>
    /// DepartmentHandler 的摘要说明
    /// </summary>
    public class DepartmentHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string method = context.Request["Method"];
            switch (method)
            {
                case "GetPagedDepartment":
                    GetPagedDepartment(context);
                    break;
                case "DeleteDepartment":
                    DeleteDepartment(context);
                    break;
                case "InsertDepartment":
                    InsertDepartment(context);
                    break;
                case "BindDeptManager":
                    BindDeptManager(context);
                    break;
                case "UpdateDepartment":
                    UpdateDepartment(context);
                    break;
                case "GetSingleDept":
                    GetSingleDept(context);
                    break;
            }
        }
        /// <summary>
        /// 绑定部门信息
        /// </summary>
        /// <param name="context"></param>
        public static void GetPagedDepartment(HttpContext context)
        {
            string DeptName = context.Request["DeptName"];
            DataTable dt = BLL.Department.BindDept("", DeptName);
            List<object> list = new List<object>();
            foreach (DataRow dr in dt.Rows)
            {
                Model.Department dept = dr.ToModel<Model.Department>();
                Model.UserInfo user = dr.ToModel<Model.UserInfo>();
                list.Add(new { Department = dept, UserInfo = user });
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(list);
            context.Response.Write(json);
        }
        /// <summary>
        /// 获取单个部门信息
        /// </summary>
        /// <param name="context"></param>
        public static void GetSingleDept(HttpContext context)
        {
            int DeptID = Convert.ToInt32(context.Request["ID"]);
            Model.Department d = BLL.Department.GetSingleDept(DeptID);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(d);
            context.Response.Write(json);

        }
        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="context"></param>
        public static void InsertDepartment(HttpContext context)
        {
            Model.Department d = new Model.Department();
            string i = string.Empty;
            string DeptName = context.Request["DeptName"];
            string DeptInfo = context.Request["DeptInfo"];
            d.DeptName = DeptName;
            d.DeptInfo = DeptInfo;
            d.Manager = "";
            string where = " where DeptName='" + DeptName + "'";
            DataTable dt = BLL.Department.SelectDept(where);
            if (dt.Rows.Count == 0)
            {
                if (BLL.Department.InsertDepartment(d))
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
        /// 绑定部门负责人下拉框
        /// </summary>
        /// <param name="context"></param>
        public static void BindDeptManager(HttpContext context)
        {
            int DeptID = Convert.ToInt32(context.Request["ID"]);
            string where = " where DeptID=" + DeptID + "";
            DataTable dt = BLL.UserInfo.SelectUserInfo(where);
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
        /// 修改部门信息
        /// </summary>
        /// <param name="context"></param>
        public static void UpdateDepartment(HttpContext context)
        {
            string i = string.Empty;
            int DeptID = Convert.ToInt32(context.Request["ID"]);
            string DeptName = context.Request["DeptName"];
            string DeptManager = context.Request["DeptManager"];
            string DeptInfo = context.Request["DeptInfo"];
            Model.Department d = new Model.Department();
            d.DeptID = DeptID;
            d.DeptInfo = DeptInfo;
            d.DeptName = DeptName;
            d.Manager = DeptManager;
            if (BLL.Department.UpdateDepartment(d))
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
        /// 删除方法
        /// </summary>
        /// <param name="context"></param>
        public static void DeleteDepartment(HttpContext context)
        {
            Model.Department d = new Model.Department();
            int DeptID = Convert.ToInt32(context.Request["ID"]);
            d.DeptID = DeptID;
            string where = " where DeptID=" + DeptID + "";
            string i = string.Empty;
            DataTable dt = BLL.UserInfo.SelectUserInfo(where);
            if (dt.Rows.Count == 0)
            {
                if (BLL.Department.DeleteDepartment(d))
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
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}