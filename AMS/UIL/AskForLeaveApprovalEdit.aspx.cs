using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace UIL
{
    public partial class AskForLeaveApproval : System.Web.UI.Page
    {
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                Model.UserInfo u = (Model.UserInfo)Session["User"];
                if (Request["ID"] == null)      //表示添加
                {
                    lblUser.Text = u.UserName;
                    lblNum.Text = (int.Parse(BLL.Approve.GetApproveInfo(null).Rows[0]["ApproveID"].ToString()) + 1).ToString();
                    Panel2.Visible = false;
                }
                else
                {
                    dt = BLL.Approve.GetApproveInfo(" and ApproveID='" + Request["ID"].ToString() + "'");
                    if (u.UserType == 0)           //用户点击
                    {
                        if (dt.Rows[0]["Status"].ToString() == "0")         //状态为待审批，修改
                        {
                            if (dt.Rows.Count > 0)
                            {
                                lblUser.Text = dt.Rows[0]["UserName"].ToString();
                                lblNum.Text = dt.Rows[0]["ApproveID"].ToString();
                                txtTitle.Text = dt.Rows[0]["Title"].ToString();
                                txtBeginTime.Text = dt.Rows[0]["BeginDate"].ToString();
                                txtEndTime.Text = dt.Rows[0]["EndDate"].ToString();
                                txtReason.Text = dt.Rows[0]["Reason"].ToString();
                                Panel2.Visible = false;
                            }
                        }
                        else if (dt.Rows[0]["Status"].ToString() == "1")             //状态为归档，查看
                        {
                            if (dt.Rows.Count > 0)
                            {
                                lblUser.Text = dt.Rows[0]["ApplyName"].ToString();
                                lblNum.Text = dt.Rows[0]["ApproveID"].ToString();
                                txtTitle.Text = dt.Rows[0]["Title"].ToString();
                                txtBeginTime.Text = dt.Rows[0]["BeginDate"].ToString();
                                txtEndTime.Text = dt.Rows[0]["EndDate"].ToString();
                                txtReason.Text = dt.Rows[0]["Reason"].ToString();
                                lblApplyDate.Text = dt.Rows[0]["ApplyDate"].ToString();
                                ddlResult.SelectedValue = dt.Rows[0]["Result"].ToString();
                                lblApproveUser.Text = dt.Rows[0]["ApproveName"].ToString();
                                lblApproveDate.Text = dt.Rows[0]["ApproveDate"].ToString();
                                txtRemark.Text = dt.Rows[0]["Remark"].ToString();
                                Panel1.Enabled = false;
                                Panel2.Enabled = false;                              
                                btnSave.Visible = false;                           
                            }
                        }
                    }
                    else if (u.UserType == 1)                    //主管登录
                    {
                        if (dt.Rows[0]["Status"].ToString() == "0")         //状态为待审批，请假审批
                        {
                            lblUser.Text = dt.Rows[0]["ApplyName"].ToString();
                            lblNum.Text = dt.Rows[0]["ApproveID"].ToString();
                            txtTitle.Text = dt.Rows[0]["Title"].ToString();
                            txtBeginTime.Text = dt.Rows[0]["BeginDate"].ToString();
                            txtEndTime.Text = dt.Rows[0]["EndDate"].ToString();
                            txtReason.Text = dt.Rows[0]["Reason"].ToString();
                            lblApplyDate.Text = dt.Rows[0]["ApplyDate"].ToString();
                            lblApproveUser.Text = u.UserName;
                            lblApproveDate.Text = "";
                            txtRemark.Text = "";
                            Panel1.Enabled = false;
                        }
                        else if (dt.Rows[0]["Status"].ToString() == "1")              //查看
                        {
                            lblUser.Text = dt.Rows[0]["ApplyName"].ToString();
                            lblNum.Text = dt.Rows[0]["ApproveID"].ToString();
                            txtTitle.Text = dt.Rows[0]["Title"].ToString();
                            txtBeginTime.Text = dt.Rows[0]["BeginDate"].ToString();
                            txtEndTime.Text = dt.Rows[0]["EndDate"].ToString();
                            txtReason.Text = dt.Rows[0]["Reason"].ToString();
                            lblApplyDate.Text = dt.Rows[0]["ApplyDate"].ToString();
                            ddlResult.SelectedValue = dt.Rows[0]["Result"].ToString();
                            lblApproveUser.Text = dt.Rows[0]["ApproveName"].ToString();
                            lblApproveDate.Text = dt.Rows[0]["ApproveDate"].ToString();
                            txtRemark.Text = dt.Rows[0]["Remark"].ToString();
                            Panel1.Enabled = false;
                            Panel2.Enabled = false;
                            btnSave.Visible = false;
                        }
                    }
                }
            }
        }
        public string userNum;
        public bool VaildateNull()
        {
            bool nu = true;
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                lblTitle.Text = "请填写标题！—_—";
                nu = false;

            }
            if (string.IsNullOrEmpty(txtBeginTime.Text))
            {
                lblBeginTime.Text = "请填写开始时间！—_—";
                nu = false;
            }
            if (string.IsNullOrEmpty(txtEndTime.Text))
            {
                lblEndTime.Text = "请填写结束时间！—_—";
                nu = false;
            }
            if (string.IsNullOrEmpty(txtReason.Text))
            {
                lblReason.Text = "请填写原因！—_—";
                nu = false;
            }
            return nu;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        { 
            Model.UserInfo u = (Model.UserInfo)Session["User"];
            Model.Approve a = new Model.Approve();
            if (u.UserType == 0)    //员工点击
            {
                userNum = u.UserID;
                if (Request["ID"] == null)
                {
                    if (VaildateNull()) //非空验证
                    {
                        DataTable dt = BLL.Approve.GetApproveInfo(" and b.UserID='" + u.UserID + "'");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        { 
                            DateTime begin=DateTime.Parse( txtBeginTime.Text + " " + ddlBeginTime.Text);
                            DateTime end=DateTime.Parse(txtEndTime.Text + " " + ddlEndTime.Text);
                            if (begin > DateTime.Parse(dt.Rows[i]["BeginDate"].ToString()) && begin <= DateTime.Parse(dt.Rows[i]["EndDate"].ToString()))
                            {
                                this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('在" + begin + " 至" + end + " 存在请假记录');", true);
                            }
                            if (end > DateTime.Parse(dt.Rows[i]["BeginDate"].ToString()) && begin <= DateTime.Parse(dt.Rows[i]["EndDate"].ToString()))
                            {
                                this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('在" + begin + " 至" + end + " 存在请假记录');", true);
                            }
                        }
                        a.ApplyUser = userNum;
                        a.Title = txtTitle.Text;
                        a.BeginDate = DateTime.Parse(txtBeginTime.Text + " " + ddlBeginTime.Text);
                        a.EndDate = DateTime.Parse(txtEndTime.Text + " " + ddlEndTime.Text);
                        a.ApplyDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        a.Reason = txtReason.Text;
                        a.Status = 0;
                        if (BLL.Approve.AddApproveInfo(a))
                        {
                            //表示添加成功
                            this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加成功！');window.parent.location='/AskForLeave.aspx';", true);
                        }
                        else
                        {
                            //表示添加失败
                            this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加失败！');", true);
                        }
                    }
                }
                else
                {
                    if (VaildateNull())
                    {
                        //DataTable dt = BLL.Approve.GetApproveInfo(" and b.UserID='" + u.UserID + "'");
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    DateTime begin = DateTime.Parse(txtBeginTime.Text + " " + ddlBeginTime.Text);
                        //    DateTime end = DateTime.Parse(txtEndTime.Text + " " + ddlEndTime.Text);
                        //    if (begin > DateTime.Parse(dt.Rows[i]["BeginDate"].ToString()) && begin <= DateTime.Parse(dt.Rows[i]["EndDate"].ToString()))
                        //    {
                        //        this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('在" + begin + " 至" + end + " 已经存在请假记录');", true);
                        //    }
                        //    if (end > DateTime.Parse(dt.Rows[i]["BeginDate"].ToString()) && begin <= DateTime.Parse(dt.Rows[i]["EndDate"].ToString()))
                        //    {
                        //        this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('在" + begin + " 至" + end + " 已经存在请假记录');", true);
                        //    }
                        //}
                        a.ApproveID = int.Parse(lblNum.Text);
                        a.ApplyUser = userNum;
                        a.Title = txtTitle.Text;
                        a.BeginDate = DateTime.Parse(txtBeginTime.Text + " " + ddlBeginTime.Text);
                        a.EndDate = DateTime.Parse(txtEndTime.Text + " " + ddlEndTime.Text);
                        a.ApplyDate = DateTime.Parse(DateTime.Now.ToString());
                        a.Reason = txtReason.Text;
                        if (BLL.Approve.UpdateApproveInfo(a))
                        {
                            //表示修改成功
                            this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改成功！');window.parent.location='/AskForLeave.aspx';", true);
                        }
                        else
                        {
                            //表示修改失败
                            this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改失败！');", true);
                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(ddlResult.SelectedValue))
                {
                    lblResult.Text = "请选择审批结果！";
                }
                else
                {
                    a.ApproveID = int.Parse(Request["ID"].ToString());
                    a.ApproveUser = u.UserID;
                    a.ApplyDate = DateTime.Parse(lblApplyDate.Text);
                    a.ApproveDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    a.Status = 1;
                    a.Result = byte.Parse(ddlResult.SelectedValue);
                    a.Remark = txtRemark.Text;
                    if (BLL.Approve.UpdateApproveInfo1(a))
                    {
                        //表示审批成功
                        this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('审批成功！');window.parent.location='/AskForLeaveApproval.aspx';", true);
                    }
                    else
                    {
                        //表示审批失败
                        this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('审批失败！');", true);
                    }
                }
            }  
        }
    }
}