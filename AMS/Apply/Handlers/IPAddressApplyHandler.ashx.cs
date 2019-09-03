using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;

namespace Apply.Handlers
{
    /// <summary>
    /// IPAddressApplyHandler 的摘要说明
    /// </summary>
    public class IPAddressApplyHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string method = context.Request["Method"];
            switch (method)
            {
                case "GetSingleIPAddressApply":
                    GetSingleIPAddressApply(context);
                    break; 
            }
        }
        /// <summary>
        /// 获取单个IPAddressApply对象
        /// </summary>
        /// <param name="context"></param>
        private static void GetSingleIPAddressApply(HttpContext context)
        {
            int ApplyID = Convert.ToInt32(context.Request["ID"]);
            Model.IPAddressApply ip = BLL.IPAddressApply.GetSingleIPAddressApply(ApplyID);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(ip);
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