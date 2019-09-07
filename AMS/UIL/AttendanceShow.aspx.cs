using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace UIL
{
    public partial class AttendanceShow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                int year = 2015;
                int month = 11;
                for (int i = year-3; i < year + 3; i++)
                {
                    ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddlYear.SelectedValue = year.ToString();
                for (int i = 1; i <= 12; i++)
                {
                    ddlMonth.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddlMonth.SelectedValue = month.ToString();               
            }
        }

        protected void btnSee_Click(object sender, EventArgs e)
        {
            //整合一个DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("Date", typeof(DateTime));
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            int month = Convert.ToInt32(ddlMonth.SelectedValue);
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                //每循环一次我们就去创建一个新行
                DataRow dr = dt.NewRow();
                dr["Date"] = new DateTime(year, month, i);
                dt.Rows.Add(dr);
            }
            gvAttendanceInfo.DataSource = dt;
            gvAttendanceInfo.DataBind();
        }

        protected void gvAttendanceInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //判断该行是否是数据行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //1、得到该行对应的日期
                DataRowView drv = (DataRowView)e.Row.DataItem;
                DateTime date = (DateTime)drv["Date"];
                Label lblFirstTime = (Label)e.Row.FindControl("lblFirstTime");
                Label lblLastTime = (Label)e.Row.FindControl("lblLastTime");
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                Model.UserInfo u = (Model.UserInfo)Session["User"];
                DataTable dt = BLL.AttendanceInfo.GetMyAttendanceInfo(Request["ID"].ToString(), date);
                //得到DataTable的第一行
                DataRow dr = dt.Rows[0];
                //2、得到首次打卡时间
                DateTime firstTime = DateTime.Now;
                if (dr["FirstTime"] != DBNull.Value)
                {
                    firstTime = (DateTime)dr["FirstTime"];
                    lblFirstTime.Text = firstTime.ToString();
                }
                //3、得到最后打卡时间
                DateTime lastTime = DateTime.Now;
                if (dr["LastTime"] != DBNull.Value)
                {
                    lastTime = (DateTime)dr["LastTime"];
                    lblLastTime.Text = lastTime.ToString();
                }
                //4、得到当天的考勤设置状态
                DataTable dtApprove = BLL.Approve.GetApproveInfo(" and b.UserID='" + Request["ID"].ToString() + "'");
                Model.AttendanceSetting a = BLL.AttendanceSetting.GetAttendanceSettingByDate(date);
                //5、得到当天的上班时间
                DateTime d1 = date.AddHours(8).AddMinutes(30);
                //上午下班时间
                DateTime d2 = date.AddHours(11).AddMinutes(30);
                //下午上班时间
                DateTime d3 = date.AddHours(13).AddMinutes(50);
                //6、得到当天的下班时间
                DateTime d4 = date.AddHours(17);
                if (a != null)
                {
                    //表示当天有考勤设置
                    if (a.Status == 2)
                    {
                        // a.Status == 2表示休假
                        lblStatus.Text = "<font color='#99dd33'>休假</font>";
                    }
                    else
                    {
                        //a.Status == 1表示上班
                        if (firstTime > d1)
                        {
                            lblStatus.Text = "<font color='#00FFFF'>迟到</font>";
                        }
                        if (lastTime < d4)
                        {
                            lblStatus.Text = "<font color='#00FFFF'>早退</font>";
                        }
                        if ((firstTime > d1) && (lastTime < d4))
                        {
                            lblStatus.Text = "<font color='#00FFFF'>迟到</font>and<font color='#00FFFF'>早退</font>";
                            if (lblFirstTime.Text == lblLastTime.Text)
                            {
                                lblStatus.Text = "<font color='#FFC0CB'>未打卡</font>";
                            }
                        }
                        if (firstTime < d1 && lastTime > d4)
                        {
                            lblStatus.Text = "<font color='#3CB371'>正常</font>";
                        }
                        if (string.IsNullOrEmpty(lblFirstTime.Text) && string.IsNullOrEmpty(lblLastTime.Text))
                        {
                            lblStatus.Text = "<font color='#FF0000'>缺勤</font>";
                        }
                        //a.Status == 1表示上班
                        //请假一天
                        for (int i = 0; i < dtApprove.Rows.Count; i++)
                        {
                            //{                                       begin<=   8:30    11:30<=end
                            //                    2014-02-11 08:30:00.000	2014-02-11 11:30:00.000
                            //                    2014-02-17 13:50:00.000	2014-02-17 17:00:00.000
                            //                    2014-02-19 08:30:00.000	2014-02-19 17:00:00.000
                            if (DateTime.Parse(dtApprove.Rows[i]["BeginDate"].ToString()) <= d1 && d4 <= DateTime.Parse(dtApprove.Rows[i]["EndDate"].ToString()))
                            {
                                lblStatus.Text = "<font color='#436EEE'>请假</font>";
                            }
                            else if (DateTime.Parse(dtApprove.Rows[i]["BeginDate"].ToString()) <= d1 && d2 <= DateTime.Parse(dtApprove.Rows[i]["EndDate"].ToString()))
                            {       //上午请假

                                if (firstTime > d3)
                                {
                                    lblStatus.Text = "<font color='#436EEE'>请假</font>and<font color='#00FFFF'>迟到</font>";
                                }
                                if (lastTime < d4)
                                {
                                    lblStatus.Text = "<font color='#436EEE'>请假</font>and<font color='#00FFFF'>早退</font>";
                                }
                                if (firstTime > d3 && lastTime < d4)
                                {
                                    lblStatus.Text = "<font color='#436EEE'>请假</font>and<font color='#00FFFF'>迟到</font>,<font color='#00FFFF'>早退</font>";
                                }
                                if (firstTime < d3 && lastTime > d4)
                                {
                                    lblStatus.Text = "<font color='#436EEE'>请假</font>and<font color='#3CB371'>正常</font>";
                                }

                            }
                            else if (DateTime.Parse(dtApprove.Rows[i]["BeginDate"].ToString()) <= d3 && d4 <= DateTime.Parse(dtApprove.Rows[i]["EndDate"].ToString()))
                            {       //下午请假

                                if (firstTime > d1)
                                {
                                    lblStatus.Text = "<font color='#00FFFF'>迟到</font>and<font color='#436EEE'>请假</font>";
                                }
                                if (lastTime < d2)
                                {
                                    lblStatus.Text = "<font color='#00FFFF'>早退</font>and<font color='#436EEE'>请假</font>";
                                }
                                if (firstTime > d1 && lastTime < d2)
                                {
                                    lblStatus.Text = "<font color='#00FFFF'>迟到</font>,<font color='#00FFFF'>早退</font>and<font color='#436EEE'>请假</font>";
                                }
                                if (firstTime < d1 && lastTime > d2)
                                {
                                    lblStatus.Text = "<font color='#3CB371'>正常</font>and<font color='#436EEE'>请假</font>";
                                }
                            }
                        }
                    }
                }
                else
                {
                    //表示当天木有考勤设置
                    //判断当天是否是星期六和星期天
                    if ((date.DayOfWeek == DayOfWeek.Saturday) || (date.DayOfWeek == DayOfWeek.Sunday))
                    {
                        //表示是星期六和星期天
                        lblStatus.Text = "<font color='#808080'>休假</font>";
                    }
                    else
                    {
                        if (firstTime > d1)
                        {
                            lblStatus.Text = "<font color='#00FFFF'>迟到</font>";
                        }
                        if (lastTime < d4)
                        {
                            lblStatus.Text = "<font color='#00FFFF'>早退</font>";
                        }
                        if ((firstTime > d1) && (lastTime < d4))
                        {
                            lblStatus.Text = "<font color='#00FFFF'>迟到</font>and<font color='#00FFFF'>早退</font>";
                            if (lblFirstTime.Text == lblLastTime.Text)
                            {
                                lblStatus.Text = "<font color='#FFC0CB'>未打卡</font>";
                            }
                        }
                        if (firstTime < d1 && lastTime > d4)
                        {
                            lblStatus.Text = "<font color='#3CB371'>正常</font>";
                        }
                        if (string.IsNullOrEmpty(lblFirstTime.Text) && string.IsNullOrEmpty(lblLastTime.Text))
                        {
                            lblStatus.Text = "<font color='#FF0000'>缺勤</font>";
                        }
                        //a.Status == 1表示上班
                        //请假一天
                        for (int i = 0; i < dtApprove.Rows.Count; i++)
                        {
                            //{                                       begin<=   8:30    11:30<=end
                            //                    2014-02-11 08:30:00.000	2014-02-11 11:30:00.000
                            //                    2014-02-17 13:50:00.000	2014-02-17 17:00:00.000
                            //                    2014-02-19 08:30:00.000	2014-02-19 17:00:00.000
                            if (DateTime.Parse(dtApprove.Rows[i]["BeginDate"].ToString()) <= d1 && d4 <= DateTime.Parse(dtApprove.Rows[i]["EndDate"].ToString()))
                            {
                                lblStatus.Text = "<font color='#436EEE'>请假</font>";
                            }
                            else if (DateTime.Parse(dtApprove.Rows[i]["BeginDate"].ToString()) <= d1 && d2 <= DateTime.Parse(dtApprove.Rows[i]["EndDate"].ToString()))
                            {       //上午请假

                                if (firstTime > d3)
                                {
                                    lblStatus.Text = "<font color='#436EEE'>请假</font>and<font color='#00FFFF'>迟到</font>";
                                }
                                if (lastTime < d4)
                                {
                                    lblStatus.Text = "<font color='#436EEE'>请假</font>and<font color='#00FFFF'>早退</font>";
                                }
                                if (firstTime > d3 && lastTime < d4)
                                {
                                    lblStatus.Text = "<font color='#436EEE'>请假</font>and<font color='#00FFFF'>迟到</font>,<font color='#00FFFF'>早退</font>";
                                }
                                if (firstTime < d3 && lastTime > d4)
                                {
                                    lblStatus.Text = "<font color='#436EEE'>请假</font>and<font color='#3CB371'>正常</font>";
                                }

                            }
                            else if (DateTime.Parse(dtApprove.Rows[i]["BeginDate"].ToString()) <= d3 && d4 <= DateTime.Parse(dtApprove.Rows[i]["EndDate"].ToString()))
                            {       //下午请假

                                if (firstTime > d1)
                                {
                                    lblStatus.Text = "<font color='#00FFFF'>迟到</font>and<font color='#436EEE'>请假</font>";
                                }
                                if (lastTime < d2)
                                {
                                    lblStatus.Text = "<font color='#00FFFF'>早退</font>and<font color='#436EEE'>请假</font>";
                                }
                                if (firstTime > d1 && lastTime < d2)
                                {
                                    lblStatus.Text = "<font color='#00FFFF'>迟到</font>,<font color='#00FFFF'>早退</font>and<font color='#436EEE'>请假</font>";
                                }
                                if (firstTime < d1 && lastTime > d2)
                                {
                                    lblStatus.Text = "<font color='#3CB371'>正常</font>and<font color='#436EEE'>请假</font>";
                                }
                            }
                        }
                    }
                }
            }
        }    
    }
}