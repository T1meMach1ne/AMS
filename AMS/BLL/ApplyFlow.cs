using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class ApplyFlow
    {
        /// <summary>
        /// 查询申请单类型
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectApplyFlow(string where)
        {
            return DAL.ApplyFlows.SelectApplyFlow(where);
        }
          /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public static Model.ApplyFlow GetSingleApplyFlow(int ApplyTypeID)
        {
            return DAL.ApplyFlows.GetSingleApplyFlow(ApplyTypeID);
        }
         /// <summary>
        /// 修改方法
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool UpdatApplyFlow(Model.ApplyFlow af)
        {
            return DAL.ApplyFlows.UpdatApplyFlow(af);
        }
    }
}
