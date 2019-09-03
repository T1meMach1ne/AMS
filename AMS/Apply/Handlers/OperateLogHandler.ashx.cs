using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;

namespace Apply.Handlers
{
    /// <summary>
    /// OperateLogHandler 的摘要说明
    /// </summary>
    public class OperateLogHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string method = context.Request["Method"];
            switch (method)
            {
                case "GetOperateLog":
                    GetOperateLog(context);
                    break;
            }
        }
        public static void GetOperateLog(HttpContext context)
        {
            DataTable dt = BLL.OperateLog.GetOperateLog(Convert.ToInt32(context.Request["ID"]));
            List<Model.OperateLog> list = new List<Model.OperateLog>();
            foreach (DataRow dr in dt.Rows)
            {
                Model.OperateLog op = dr.ToModel<Model.OperateLog>();
                list.Add(op);
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(list);
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