using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class AttendanceSetting
    {
        /// <summary>
        /// 查询考勤设置信息z
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static DataTable GetAttendanceSettingInfo(string where)
        {
            return DAL.AttendanceSetting.GetAttendanceSettingInfo(where);
        }
        /// <summary>
        /// 获取考勤设置信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public static DataTable GetAttendanceSetting(string UserID, int Year, int Month)
        {
            return DAL.AttendanceSetting.GetAttendanceSetting(UserID, Year, Month);
        }
        /// <summary>
        /// 新增考勤设置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddAttendanceSettingInfo(Model.AttendanceSetting model)
        {
            return DAL.AttendanceSetting.AddAttendanceSettingInfo(model);
        }
        /// <summary>
        /// 修改考勤设置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateAttendanceSettingInfo(Model.AttendanceSetting model)
        {
            return DAL.AttendanceSetting.UpdateAttendanceSettingInfo(model);
        }
          /// <summary>
        /// 根据日期得到单个考勤设置对象
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static Model.AttendanceSetting GetAttendanceSettingByDate(DateTime date)
        {
            return DAL.AttendanceSetting.GetAttendanceSettingByDate(date);
        }

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool InsertDataTable(DataTable dt)
        {
            return DAL.AttendanceSetting.InsertDataTable(dt);
        }
        /// <summary>
        /// 批量删除设置的数据
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static bool DeleteAttendanceSetting(int year, int month)
        {
            return DAL.AttendanceSetting.DeleteAttendanceSetting(year, month);
        }
    }
}
