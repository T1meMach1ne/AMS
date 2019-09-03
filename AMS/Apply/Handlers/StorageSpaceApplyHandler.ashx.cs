using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;

namespace Apply.Handlers
{
    /// <summary>
    /// StorageSpaceApplyHandler 的摘要说明
    /// </summary>
    public class StorageSpaceApplyHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string method = context.Request["Method"];
            switch (method)
            {
                case "GetSingleStorageSpaceApply":
                    GetSingleStorageSpaceApply(context);
                    break;
            }
        }
        /// <summary>
        /// 获取单个OfficeEmailApply对象
        /// </summary>
        /// <param name="context"></param>
        private static void GetSingleStorageSpaceApply(HttpContext context)
        {
            int ApplyID = Convert.ToInt32(context.Request["ID"]);
            Model.StorageSpaceApply st = BLL.StorageSpaceApply.GetSingleStorageSpaceApply(ApplyID);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(st);
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