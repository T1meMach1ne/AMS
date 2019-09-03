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
        /// 查询部门信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static DataTable SelectDept(string where)
        {
            return DAL.Department.SelectDept(where);
        }
        /// <summary>
        /// 绑定部门信息表
        /// </summary>
        /// <returns></returns>
        public static DataTable BindDept(string where,string DeptName)
        {
            return DAL.Department.BindDept(where,DeptName);
        }
        /// <summary>
        /// 获取单个部门对象
        /// </summary>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public static Model.Department GetSingleDept(int DeptID)
        {
            return DAL.Department.GetSingleDept(DeptID);
        }
        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool InsertDepartment(Model.Department d)
        {
            return DAL.Department.InsertDepartment(d);
        }
        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool UpdateDepartment(Model.Department d)
        {
            return DAL.Department.UpdateDepartment(d);
        }
        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool DeleteDepartment(Model.Department d)
        {
            return DAL.Department.DeleteDepartment(d);
        }
    }
}
