using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{
    public class IPAddressApply
    {

        /// <summary>
        /// 新增InsertIPAddressApply
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool InsertIPAddressApply(Model.IPAddressApply ip)
        {
            return DAL.IPAddressApply.InsertIPAddressApply(ip);
        }
        /// <summary>
        /// 删除IPAddressApply
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool DeleteIPAddressApply(Model.IPAddressApply ip)
        {
            return DAL.IPAddressApply.DeleteIPAddressApply(ip);
        }
        /// <summary>
        /// 获取单个IPAddressApply对象
        /// </summary>
        /// <param name="ApplyID"></param>
        /// <returns></returns>
        public static Model.IPAddressApply GetSingleIPAddressApply(int ApplyID)
        {
            return DAL.IPAddressApply.GetSingleIPAddressApply(ApplyID);
        }
    }
}
