using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class Department
    {
        /// <summary>
        /// 获取主管信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllManager()
        {
            string sql = "select * from dbo.UserInfo where UserType=1";
            return DBHelper.ExecuteSelect(sql);
        }
        /// <summary>
        /// 查询部门主管对应信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static DataTable GetDepartmentInfo(string where)
        {
            string sql = " select * from Department left join UserInfo on Department.ManagerID=UserInfo.UserID where 1=1" + where;
            return DBHelper.ExecuteSelect(sql); ;
        }
        /// <summary>
        /// 判断部门下有无员工信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static bool HaveUserInfo(int deptID)
        {
            string sql = "select * from UserInfo where DeptID='" + deptID + "'";
            DataTable dt = DBHelper.ExecuteSelect(sql);
            bool b;
            if (dt.Rows.Count > 0)
            {
                //表示部门有用户
                b = true;
            }
            else
            {
                //表示部门没有用户
                b = false;
            }
            return b;
        }
        /// <summary>
        /// 新增部门信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddDepartmentInfo(Model.Department model)
        {
            string sql = "insert into Department values ('" + model.DeptName + "','" + model.ManagerID + "','" + model.DeptInfo + "')";
            return DBHelper.ExecuteNonQuery(sql, null);
        }
        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateDepartmentInfo(Model.Department model)
        {
            string sql = "update Department set DeptName='" + model.DeptName + "',ManagerID='" + model.ManagerID + "',DeptInfo='" + model.DeptInfo + "'  where DeptID ='" + model.DeptID + "'";
            return DBHelper.ExecuteNonQuery(sql, null); ;
        }
        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool DelDepartmentInfo(Model.Department model)
        {
            string sql = " delete Department where DeptID ='" + model.DeptID + "'";
            return DBHelper.ExecuteNonQuery(sql, null);
        }
    }
}
