using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;
namespace Apply.Handlers
{
    /// <summary>
    /// ApplyFlowHandler 的摘要说明
    /// </summary>
    public class ApplyFlowHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
           string method = context.Request["Method"];
           switch (method)
           {
               case "BindApplyType":
                   BindApplyType(context);
                   break;
               case "BindAssigner":
                   BindAssigner(context);
                   break;
               case "GetSingleApplyFlow":
                   GetSingleApplyFlow(context);
                   break;
               case "UpdateApplyFlow":
                   UpdateApplyFlow(context);
                   break;
           }
        }

        /// <summary>
        /// 绑定申请单类型
        /// </summary>
        /// <param name="context"></param>
        public static void BindApplyType(HttpContext context)
        {
            DataTable dt = BLL.ApplyFlow.SelectApplyFlow("");
            List<Model.ApplyFlow> list = new List<Model.ApplyFlow>();
            foreach (DataRow dr in dt.Rows)
            {
                Model.ApplyFlow af = dr.ToModel<Model.ApplyFlow>();
                list.Add(af);
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(list);
            context.Response.Write(json);
        }
        /// <summary>
        /// 绑定分配人下拉框
        /// </summary>
        /// <param name="context"></param>
        public static void BindAssigner(HttpContext context)
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
        ///获取单个对象 
        /// </summary>
        /// <param name="context"></param>
        public static void GetSingleApplyFlow(HttpContext context)
        {
            int ApplyTypeID = Convert.ToInt32(context.Request["ID"]);
            Model.ApplyFlow a = BLL.ApplyFlow.GetSingleApplyFlow(ApplyTypeID);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(a);
            context.Response.Write(json);
        }
        public static void UpdateApplyFlow(HttpContext context)
        {
            string i = string.Empty;
            int ApplyTypeID = Convert.ToInt32(context.Request["ID"]);
            string Assigner = context.Request["Assigner"];
            Model.ApplyFlow af=new Model.ApplyFlow();
            af.ApplyTypeID=ApplyTypeID;
            af.Assigner=Assigner;
            if (BLL.ApplyFlow.UpdatApplyFlow(af))
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