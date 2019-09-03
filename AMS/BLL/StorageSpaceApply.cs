using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class StorageSpaceApply
    {
        /// <summary>
        /// 新增StorageSpaceApply
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool InsertIPAddressApply(Model.StorageSpaceApply st)
        {
            return DAL.StorageSpaceApply.InsertStorageSpaceApply(st);
        }
        /// <summary>
        /// 删除StorageSpaceApply
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool DeleteStorageSpaceApply(Model.StorageSpaceApply st)
        {
            return DAL.StorageSpaceApply.DeleteStorageSpaceApply(st);
        }
        /// <summary>
        /// 获取单个StorageSpaceApply对象
        /// </summary>
        /// <param name="ApplyID"></param>
        /// <returns></returns>
        public static Model.StorageSpaceApply GetSingleStorageSpaceApply(int ApplyID)
        {
            return DAL.StorageSpaceApply.GetSingleStorageSpaceApply(ApplyID);
        }
    }
}
