using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace UIL
{
    public partial class AttendanceSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                int year = 2015;
                int month = 10;
                for (int i = year - 3; i <= year + 3; i++)
                {
                    ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddlYear.SelectedValue = year.ToString();
                for (int i = 1; i <= 12; i++)
                {
                    ddlMonth.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddlMonth.SelectedValue = month.ToString();
                btnSave.Visible = false;
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            int month = Convert.ToInt32(ddlMonth.SelectedValue);
            //整合一个DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("Date", typeof(DateTime));
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                DataRow dr = dt.NewRow();
                dr["Date"] = new DateTime(year, month, i);
                dt.Rows.Add(dr);
            }
            gvAttendanceSetting.DataSource = dt;
            gvAttendanceSetting.DataBind();
            btnSave.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //整合一个DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("SettingID", typeof(int));
            dt.Columns.Add("Date", typeof(DateTime));
            dt.Columns.Add("Status", typeof(byte));
            //循环遍历GridView里面的每一行
            foreach (GridViewRow gvr in gvAttendanceSetting.Rows)
            {
                DropDownList ddlStatus = (DropDownList)gvr.FindControl("ddlStatus");
                Label lblDate = (Label)gvr.FindControl("lblDate");
                if (ddlStatus.SelectedValue != "0")
                {
                    //就代表状态被我们改过
                    DataRow dr = dt.NewRow();
                    dr["Date"] = lblDate.Text;
                    dr["Status"] = ddlStatus.SelectedValue;
                    dt.Rows.Add(dr);
                }
            }
            //先删除原来设置的数据
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            int month = Convert.ToInt32(ddlMonth.SelectedValue);
            BLL.AttendanceSetting.DeleteAttendanceSetting(year, month);
            //后进行新设置的添加
            if (BLL.AttendanceSetting.InsertDataTable(dt))
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('恭喜您，考勤设置成功！');", true);
            }
            else
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('对不起，考勤设置失败！');", true);
            }
        }

        protected void gvAttendanceSetting_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                Label lblDate = (Label)e.Row.FindControl("lblDate");
                Model.AttendanceSetting a = BLL.AttendanceSetting.GetAttendanceSettingByDate(Convert.ToDateTime(lblDate.Text));
                if (a != null)
                {
                    //表示当前日期在数据库的表里面有设置
                    ddlStatus.SelectedValue = a.Status.ToString();
                }
            }
        }
    }
}