using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.SessionState;
using System.Web.Script.Serialization;

namespace Apply.Handlers
{
    /// <summary>
    /// Apply 的摘要说明
    /// </summary>
    public class Apply : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string method = context.Request["Method"];
            switch (method)
            {
                case "GetPagedApply":
                    GetPagedApply(context);
                    break;
                case "InsertApply":
                    InsertApply(context);
                    break;
                case "DeleteApply":
                    DeleteApply(context);
                    break;
                case "GetSingleApply":
                    GetSingleApply(context);
                    break;
                case "GetPagedApproveMgr":
                    GetPagedApproveMgr(context);
                    break;
                case "ApproveApply":
                    ApproveApply(context);
                    break;
                case "AssignerApply":
                    AssignerApply(context);
                    break;
                case "GetPagedAssignerApply":
                    GetPagedAssignerApply(context);
                    break;
                case "GetPagedDealerApply":
                    GetPagedDealerApply(context);
                    break;
                case "DealerApply":
                    DealerApply(context);
                    break; 
                    
            }
        }
        /// <summary>
        /// 查询我的申请单
        /// </summary>
        /// <param name="context"></param>
        private static void GetPagedApply(HttpContext context)
        {
            Model.UserInfo u = (Model.UserInfo)context.Session["User"];
            int ApplyID;
            if (context.Request["ApplyID"] != "")
            {
                ApplyID = Convert.ToInt32(context.Request["ApplyID"]);
            }
            else
            {
                ApplyID = 0;
            }
            int ApplyStatus = Convert.ToInt32(context.Request["ApplyStatus"]);
            int ApplyType = Convert.ToInt32(context.Request["ApplyType"]);
            string ApplyTitle = context.Request["ApplyTitle"];
            string where = " and a.UserID= '" + u.UserID.ToString().Trim() + "'";
            DataTable dt = BLL.Apply.SelectApply(where, ApplyID, ApplyStatus, ApplyType, ApplyTitle);
            List<object> list = new List<object>();
            foreach (DataRow dr in dt.Rows)
            {
                Model.Apply a = dr.ToModel<Model.Apply>();
                Model.ApplyFlow af = dr.ToModel<Model.ApplyFlow>();
                list.Add(new { Apply = a, ApplyFlow = af });
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(list);
            context.Response.Write(json);
        }
       
        /// <summary>
        /// 新增申请单
        /// </summary>
        /// <param name="context"></param>
        private static void InsertApply(HttpContext context)
        {
            Model.Apply a = new Model.Apply();
            a.ApplyTypeID = int.Parse(context.Request["ApplyTypeID"]);
            a.ApplyDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            a.ApplyStatus = 1;
            a.UserID = context.Request["UserID"];
            a.Approver = DAL.DBHelper.ExecuteSelect("select * from UserInfos a left join Departments b on a.DeptID=b.DeptID where a.UserID='" + context.Request["UserID"] + "'").Rows[0]["Manager"].ToString();
            a.Assigner = "sqlsa";
            a.Dealer = "";
            a.Phone = context.Request["Telphone"];
            a.ApplyTitle = context.Request["ApplyTitle"];
            a.ApplyReason = context.Request["ApplyReason"];
            a.Enclosure = "";
            a.Remark = context.Request["Remark"];
            string i = "";
            if (BLL.Apply.InsertApply(a))
            {
                if (int.Parse(context.Request["ApplyTypeID"]) == 1)
                {
                    Model.IPAddressApply ip = new Model.IPAddressApply();
                    ip.ApplyID = BLL.Apply.GetMaxDateApply().ApplyID;
                    ip.Quantity = Convert.ToInt32(context.Request["Quantity1"]);
                    ip.Address = context.Request["Address1"];
                    ip.PortNumber = context.Request["PortNumber1"];
                    ip.TimeLimit = Convert.ToDateTime(context.Request["TimeLimit1"]);
                    if (BLL.IPAddressApply.InsertIPAddressApply(ip))
                    {
                        Model.OperateLog op = new Model.OperateLog();
                        op.ApplyID = BLL.Apply.GetMaxDateApply().ApplyID;
                        op.OperateDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                        op.OperateType = "提交申请";
                        op.Result = "操作成功";
                        op.UserID = context.Request["UserID"];
                        op.Describe = "";
                        if (BLL.OperateLog.InsertOperateLog(op))
                        {
                            i = ip.ApplyID.ToString();
                        }

                    }
                    else
                    {
                        i = "0";
                    }
                }
                if (int.Parse(context.Request["ApplyTypeID"]) == 2)
                {
                    Model.OfficeEmailApply off = new Model.OfficeEmailApply();
                    off.ApplyID = BLL.Apply.GetMaxDateApply().ApplyID;
                    off.Zone = Convert.ToInt32(context.Request["Zone1"]);
                    off.OfficePlace = context.Request["OfficePlace2"];
                    off.UserName = context.Request["UserName2"];
                    off.FullName = context.Request["FullName2"];
                    if (BLL.OfficeEmailApply.InsertOfficeEmailApply(off))
                    {
                        Model.OperateLog op = new Model.OperateLog();
                        op.ApplyID = BLL.Apply.GetMaxDateApply().ApplyID;
                        op.OperateDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                        op.OperateType = "提交申请";
                        op.Result = "操作成功";
                        op.UserID = context.Request["UserID"];
                        op.Describe = "";
                        if (BLL.OperateLog.InsertOperateLog(op))
                        {
                            i = off.ApplyID.ToString();
                        }
                    }
                    else
                    {
                        i = "0";
                    }
                }
                if (int.Parse(context.Request["ApplyTypeID"]) == 3)
                {
                    Model.StorageSpaceApply st = new Model.StorageSpaceApply();
                    st.ApplyID = BLL.Apply.GetMaxDateApply().ApplyID;
                    st.Zone = Convert.ToInt32(context.Request["Zone3"]);
                    st.InterfaceMan = context.Request["InterfaceMan3"];
                    st.Right = context.Request["Right3"];
                    st.TimeLimit = Convert.ToDateTime(context.Request["TimeLimit3"]);
                    if (BLL.StorageSpaceApply.InsertIPAddressApply(st))
                    {
                        Model.OperateLog op = new Model.OperateLog();
                        op.ApplyID = BLL.Apply.GetMaxDateApply().ApplyID;
                        op.OperateDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                        op.OperateType = "提交申请";
                        op.Result = "操作成功";
                        op.UserID = context.Request["UserID"];
                        op.Describe = "";
                        if (BLL.OperateLog.InsertOperateLog(op))
                        {
                            i = st.ApplyID.ToString();
                        }
                    }
                    else
                    {
                        i = "0";
                    }
                }
            }
            context.Response.Write(i);
        }
        ///// <summary>
        ///// 把单个对象转化为json输出
        ///// </summary>
        ///// <param name="context"></param>
        private static void GetSingleApply(HttpContext context)
        {
            int ApplyID = Convert.ToInt32(context.Request["ID"]);
            DataTable dt = BLL.Apply.GetSingleApply(ApplyID);
            List<object> list = new List<object>();
            foreach (DataRow dr in dt.Rows)
            {
                Model.Apply a = dr.ToModel<Model.Apply>();
                Model.UserInfo us = dr.ToModel<Model.UserInfo>();
                Model.Department dept = dr.ToModel<Model.Department>();
                list.Add(new { Apply = a, UserInfo = us, Department = dept });
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(list);
            context.Response.Write(json);
        }
        /// <summary>
        /// 查询审批管理
        /// </summary>
        /// <param name="context"></param>
        private static void GetPagedApproveMgr(HttpContext context)
        {
            int ApplyID;
            if (context.Request["ApplyID"] != "")
            {
                ApplyID = Convert.ToInt32(context.Request["ApplyID"]);
            }
            else
            {
                ApplyID = 0;
            }
            int ApplyStatus = Convert.ToInt32(context.Request["ApplyStatus"]);
            int ApplyType = Convert.ToInt32(context.Request["ApplyType"]);
            string ApplyTitle = context.Request["ApplyTitle"];
            Model.UserInfo u = (Model.UserInfo)context.Session["User"];
            string where = " and d.Manager='" + u.UserID.ToString().Trim() + "'";
            DataTable dt = BLL.Apply.SelectApply(where, ApplyID, ApplyStatus, ApplyType, ApplyTitle);
            List<object> list = new List<object>();
            foreach (DataRow dr in dt.Rows)
            {
                Model.Apply a = dr.ToModel<Model.Apply>();
                Model.ApplyFlow af = dr.ToModel<Model.ApplyFlow>();
                list.Add(new { Apply = a, ApplyFlow = af });
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(list);
            context.Response.Write(json);
        }
        /// <summary>
        /// 审批申请单
        /// </summary>
        /// <param name="context"></param>
        private static void ApproveApply(HttpContext context)
        {
            Model.UserInfo u = (Model.UserInfo)context.Session["User"];
            Model.Apply a = new Model.Apply();
            a.ApplyStatus = Convert.ToInt32(context.Request["Status"]);
            string i = "";
            if (a.ApplyStatus == 2)
            {
                if (BLL.Apply.ApproveApply(a))
                {
                    Model.OperateLog op = new Model.OperateLog();
                    op.ApplyID = a.ApplyID;
                    op.OperateDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    op.OperateType = "审批申请";
                    op.Result = "审批通过";
                    op.UserID = u.UserID;
                    op.Describe = context.Request["ApproveDescribe"];
                    if (BLL.OperateLog.InsertOperateLog(op))
                    {
                        i = "1";
                    }

                }
                else
                {
                    i = "0";
                }
            }
            else
            {
                if (BLL.Apply.ApproveApply(a))
                {
                    Model.OperateLog op = new Model.OperateLog();
                    op.ApplyID = a.ApplyID;
                    op.OperateDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    op.OperateType = "审批申请";
                    op.Result = "审批否决";
                    op.UserID = u.UserID;
                    op.Describe = context.Request["ApproveDescribe"];
                    if (BLL.OperateLog.InsertOperateLog(op))
                    {
                        i = "1";
                    }

                }
                else
                {
                    i = "0";
                }
            }
            context.Response.Write(i);
        }
        /// <summary>
        /// 查询分配列表
        /// </summary>
        /// <param name="context"></param>
        private static void GetPagedAssignerApply(HttpContext context)
        {
            int ApplyID;
            if (context.Request["ApplyID"] != "")
            {
                ApplyID = Convert.ToInt32(context.Request["ApplyID"]);
            }
            else
            {
                ApplyID = 0;
            }
            int ApplyStatus = Convert.ToInt32(context.Request["ApplyStatus"]);
            int ApplyType = Convert.ToInt32(context.Request["ApplyType"]);
            string ApplyTitle = context.Request["ApplyTitle"];
            Model.UserInfo u = (Model.UserInfo)context.Session["User"];
            string where = " and b.Assigner='" + u.UserID.ToString().Trim() + "'";
            DataTable dt = BLL.Apply.SelectApply(where, ApplyID, ApplyStatus, ApplyType, ApplyTitle);
            
            List<object> list = new List<object>();
            foreach (DataRow dr in dt.Rows)
            {
                Model.Apply a = dr.ToModel<Model.Apply>();
                Model.ApplyFlow af = dr.ToModel<Model.ApplyFlow>();
                list.Add(new { Apply = a, ApplyFlow = af });
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(list);
            context.Response.Write(json);
        }
        /// <summary>
        /// 分配申请单
        /// </summary>
        /// <param name="context"></param>
        private static void AssignerApply(HttpContext context)
        {
            Model.UserInfo u = (Model.UserInfo)context.Session["User"];
            Model.Apply a = new Model.Apply();
            a.Dealer = context.Request["Dealer"];
            a.ApplyStatus = 3;
            a.ApplyID = Convert.ToInt32(context.Request["ID"]);
            string i = "";
            if (BLL.Apply.AssignerApply(a))
            {
                Model.OperateLog op = new Model.OperateLog();
                op.ApplyID = a.ApplyID;
                op.OperateDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                op.OperateType = "分配申请";
                op.Result = "分配完成";
                op.UserID = u.UserID;
                op.Describe = context.Request["AssignerDescribe"];
                if (BLL.OperateLog.InsertOperateLog(op))
                {
                    i = "1";
                }

            }
            else
            {
                i = "0";
            }
            context.Response.Write(i);
        }
        /// <summary>
        /// 查询处理列表
        /// </summary>
        /// <param name="context"></param>
        private static void GetPagedDealerApply(HttpContext context)
        {
            int ApplyID;
            if (context.Request["ApplyID"] != "")
            {
                ApplyID = Convert.ToInt32(context.Request["ApplyID"]);
            }
            else
            {
                ApplyID = 0;
            }
            int ApplyStatus = Convert.ToInt32(context.Request["ApplyStatus"]);
            int ApplyType = Convert.ToInt32(context.Request["ApplyType"]);
            string ApplyTitle = context.Request["ApplyTitle"];
            Model.UserInfo u = (Model.UserInfo)context.Session["User"];
            string where = " and a.Dealer='" + u.UserID.ToString().Trim() + "'";
            DataTable dt = BLL.Apply.SelectApply(where, ApplyID, ApplyStatus, ApplyType, ApplyTitle);

            List<object> list = new List<object>();
            foreach (DataRow dr in dt.Rows)
            {
                Model.Apply a = dr.ToModel<Model.Apply>();
                Model.ApplyFlow af = dr.ToModel<Model.ApplyFlow>();
                list.Add(new { Apply = a, ApplyFlow = af });
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(list);
            context.Response.Write(json);
        }
        /// <summary>
        /// 处理申请单
        /// </summary>
        /// <param name="context"></param>
        private static void DealerApply(HttpContext context)
        {
            Model.UserInfo u = (Model.UserInfo)context.Session["User"];
            Model.Apply a = new Model.Apply();
            a.ApplyStatus = 4;
            a.ApplyID = Convert.ToInt32(context.Request["ID"]);
            string i = "";
            if (BLL.Apply.DealerApply(a))
            {
                Model.OperateLog op = new Model.OperateLog();
                op.ApplyID = a.ApplyID;
                op.OperateDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                op.OperateType = "处理申请";
                op.Result = context.Request["DealerResult"];
                op.UserID = u.UserID;
                op.Describe = context.Request["DealerDescribe"];
                if (BLL.OperateLog.InsertOperateLog(op))
                {
                    i = "1";
                }

            }
            else
            {
                i = "0";
            }
            context.Response.Write(i);
        }
        //删除方法
        private static void DeleteApply(HttpContext context)
        {
            Model.Apply a = new Model.Apply();

            int ApplyTypeID = int.Parse(context.Request["TypeID"]);
            int ApplyID = Convert.ToInt32(context.Request["ID"]);
            a.ApplyID = ApplyID;
            string s = "";
            if (ApplyTypeID == 1)
            {
                Model.IPAddressApply ip = new Model.IPAddressApply();
                ip.ApplyID = ApplyID;
                if (BLL.IPAddressApply.DeleteIPAddressApply(ip))
                {
                    if (BLL.Apply.DeleteApply(a))
                    {
                        s = "1";
                    }
                    else
                    {
                        //表示删除失败
                        s = "0";
                    }
                }
                else
                {
                    //表示删除失败
                    s = "0";
                }
            }
            if (ApplyTypeID == 2)
            {
                Model.OfficeEmailApply off = new Model.OfficeEmailApply();
                off.ApplyID = ApplyID;
                if (BLL.OfficeEmailApply.DeleteOfficeEmailApply(off))
                {
                    if (BLL.Apply.DeleteApply(a))
                    {
                        s = "1";
                    }
                    else
                    {
                        //表示删除失败
                        s = "0";
                    }
                }
                else
                {
                    //表示删除失败
                    s = "0";
                }
            }
            if (ApplyTypeID == 3)
            {
                Model.StorageSpaceApply st = new Model.StorageSpaceApply();
                st.ApplyID = ApplyID;
                if (BLL.StorageSpaceApply.DeleteStorageSpaceApply(st))
                {
                    if (BLL.Apply.DeleteApply(a))
                    {
                        s = "1";
                    }
                    else
                    {
                        //表示删除失败
                        s = "0";
                    }
                }
                else
                {
                    //表示删除失败
                    s = "0";
                }
            }
            context.Response.Write(s);
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