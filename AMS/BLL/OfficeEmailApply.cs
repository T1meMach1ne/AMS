using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class OfficeEmailApply
    {
        /// <summary>
        /// 新增InsertOfficeEmailApply
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool InsertOfficeEmailApply(Model.OfficeEmailApply off)
        {
            return DAL.OfficeEmailApply.InsertOfficeEmailApply(off);
        }
        /// <summary>
        /// 删除OfficeEmailApply
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool DeleteOfficeEmailApply(Model.OfficeEmailApply off)
        {
            return DAL.OfficeEmailApply.DeleteOfficeEmailApply(off);
        }
        /// <summary>
        /// 获取单个OfficeEmailApply对象
        /// </summary>
        /// <param name="ApplyID"></param>
        /// <returns></returns>
        public static Model.OfficeEmailApply GetSingleOfficeEmailApply(int ApplyID)
        {
            return DAL.OfficeEmailApply.GetSingleOfficeEmailApply(ApplyID);
        }
    }
}
