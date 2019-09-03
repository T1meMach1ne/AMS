using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class OperateLog
    {
        /// <summary>
        /// 获取单个申请单对象
        /// </summary>
        /// <param name="ApplyID"></param>
        /// <returns></returns>
        public static DataTable GetOperateLog(int ApplyID)
        {
            return DAL.OperateLog.GetOperateLog(ApplyID);
        }
         /// <summary>
        /// 新增操作记录
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public static bool InsertOperateLog(Model.OperateLog op)
        {
            return DAL.OperateLog.InsertOperateLog(op);
        }
    }
}
