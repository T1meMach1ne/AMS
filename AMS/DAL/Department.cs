using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Department
    {
        /// <summary>
        /// 查询部门信息
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectDept(string where)
        {
            string sql = " select * from Departments " + where;
            return DBHelper.ExecuteSelect(sql);;
        }
        /// <summary>
        /// 绑定部门信息表
        /// </summary>
        /// <returns></returns>
        public static DataTable BindDept(string where,string DeptName="")
        {
            string sql = " select * from Departments a left join UserInfos b on a.Manager=b.UserID where a.DeptName like '%" + DeptName + "%'" + where;
            return DBHelper.ExecuteSelect(sql);
        }
        /// <summary>
        /// 获取单个部门行
        /// </summary>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public static Model.Department GetSingleDept(int DeptID)
        {
            string sql = " select * from Departments where DeptID=@DeptID";
            SqlParameter[] para = { 
                                    new SqlParameter("DeptID",DeptID)
                                  };
            DataTable dt = DBHelper.ExecuteSelect(sql, para);
            DataRow dr = dt.Rows[0];
            Model.Department d = new Model.Department();
            d.DeptID = (int)dr["DeptID"];
            d.DeptName = (string)dr["DeptName"];
            d.Manager = (string)dr["Manager"];
            d.DeptInfo = (string)dr["DeptInfo"];
            return d;
        }
        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool InsertDepartment(Model.Department d)
        {
            //参数化的SQL语句
            string sql = "insert into Departments values (@DeptName,@Manager, @DeptInfo)";
            SqlParameter[] para = { 
                                   new SqlParameter("DeptName",d.DeptName),
                                   new SqlParameter("Manager",d.Manager),
                                   new SqlParameter("DeptInfo",d.DeptInfo)                                 
                                };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
        /// <summary>
        /// 修改方法
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool UpdateDepartment(Model.Department d)
        {
            //参数化的SQL语句
            string sql = " update Departments set DeptName=@DeptName,Manager=@Manager,DeptInfo=@DeptInfo where DeptID=@DeptID";
            SqlParameter[] para = { 
                                   new SqlParameter("DeptID",d.DeptID),
                                   new SqlParameter("DeptName",d.DeptName),
                                   new SqlParameter("Manager",d.Manager),
                                   new SqlParameter("DeptInfo",d.DeptInfo)                                 
                                };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool DeleteDepartment(Model.Department d)
        {
            string sql = " delete from Departments where DeptID=@DeptID";
            SqlParameter[] para ={
                                 new SqlParameter("DeptID",d.DeptID),
                                 };
            return DBHelper.ExecuteNonQuery(sql, para);
        }
    }
}
