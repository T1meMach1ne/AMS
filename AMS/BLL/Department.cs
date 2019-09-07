using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class Department
    {
        /// <summary>
        /// 获取主管信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllManager()
        {
            return DAL.Department.GetAllManager();
        }
       /// <summary>
       /// 查询部门对应员工信息
       /// </summary>
       /// <param name="where"></param>
       /// <returns></returns>
        public static DataTable GetDepartmentInfo(string where)
        {
            return DAL.Department.GetDepartmentInfo(where);
        }
        /// <summary>
        /// 判断部门下有无员工信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static bool HaveUserInfo(int deptID)
        {
            return DAL.Department.HaveUserInfo(deptID);
        }
        /// <summary>
        /// 新增部门信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddDepartmentInfo(Model.Department model)
        {
            return DAL.Department.AddDepartmentInfo(model);
        }
        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateDepartmentInfo(Model.Department model)
        {
            return DAL.Department.UpdateDepartmentInfo(model);
        }
        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool DelDepartmentInfo(Model.Department model)
        {
            return DAL.Department.DelDepartmentInfo(model);
        }
    }
}
