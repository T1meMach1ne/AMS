using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class Apply
    {
        /// <summary>
        /// 查询方法
        /// </summary>
        /// <param name="B_Name"></param>
        /// <param name="B_Author"></param>
        /// <param name="B_ID"></param>
        /// <returns></returns>
        public static DataTable SelectApply(string where,int ApplyID, int ApplyStatus, int ApplyTypeID, string ApplyTitle)
        {
            return DAL.Apply.SelecApply(where,ApplyID, ApplyStatus, ApplyTypeID, ApplyTitle);
        }
        /// <summary>
        /// 获取单个申请单对象
        /// </summary>
        /// <param name="ApplyID"></param>
        /// <returns></returns>
        public static DataTable GetSingleApply(int ApplyID)
        {
            return DAL.Apply.GetSingleApply(ApplyID);
        }
        /// <summary>
        /// 获取最大日期
        /// </summary>
        /// <returns></returns>
        public static Model.Apply GetMaxDateApply()
        {
            return DAL.Apply.GetMaxDateApply();
        }
        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool InsertApply(Model.Apply a)
        {
            return DAL.Apply.InsertApply(a);
        }
        /// <summary>
        /// 审批申请单
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool ApproveApply(Model.Apply a)
        {
            return DAL.Apply.ApproveApply(a);
        }
        /// <summary>
        /// 分配申请单
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool AssignerApply(Model.Apply a)
        {
            return DAL.Apply.AssignerApply(a);
        }
        /// <summary>
        /// 处理申请单
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool DealerApply(Model.Apply a)
        {
            return DAL.Apply.DealerApply(a);
        }
        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool DeleteApply(Model.Apply a)
        {
            return DAL.Apply.DeleteApply(a);
        }
    }
}
