using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;

namespace Apply.Handlers
{
    /// <summary>
    /// OfficeEmailApplyHandler 的摘要说明
    /// </summary>
    public class OfficeEmailApplyHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string method = context.Request["Method"];
            switch (method)
            {
                case "GetSingleOfficeEmailApply":
                    GetSingleOfficeEmailApply(context);
                    break;
            }
        }
        /// <summary>
        /// 获取单个OfficeEmailApply对象
        /// </summary>
        /// <param name="context"></param>
        private static void GetSingleOfficeEmailApply(HttpContext context)
        {
            int ApplyID = Convert.ToInt32(context.Request["ID"]);
            Model.OfficeEmailApply off = BLL.OfficeEmailApply.GetSingleOfficeEmailApply(ApplyID);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(off);
            context.Response.Write(json);
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