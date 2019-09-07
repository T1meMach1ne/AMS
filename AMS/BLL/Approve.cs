using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class Approve
    {
        /// <summary>
        /// 查询申请信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static DataTable GetApproveInfo(string where)
        {
            return DAL.Approve.GetApproveInfo(where);
        }
        /// <summary>
        /// 新增申请信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static bool AddApproveInfo(Model.Approve model)
        {
            return DAL.Approve.AddApproveInfo(model);
        }
        /// <summary>
        /// 修改申请信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateApproveInfo(Model.Approve model)
        {
            return DAL.Approve.UpdateApproveInfo(model);
        }
        public static bool UpdateApproveInfo1(Model.Approve model)
        {
            return DAL.Approve.UpdateApproveInfo1(model);
        }
        /// <summary>
        /// 删除申请信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static bool DelApproveInfo(Model.Approve model)
        {
            return DAL.Approve.DelApproveInfo(model);
        }

        /// <summary>
        /// 判断请假状态（修改和删除）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool SelectApproveState(int id)
        {
            return DAL.Approve.SelectApproveState(id);
        }
    }
}
