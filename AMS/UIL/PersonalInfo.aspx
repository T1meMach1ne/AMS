<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalInfo.aspx.cs" Inherits="UIL.PersonalInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/easyui/jquery-1.11.3.js" type="text/javascript"></script>
        <style type="text/css"> 
    #tb1 tr
    {
	     height:40px;
    }
    .required 
    {
	    color: Red;
	    margin-right: 2px;
    }
    .tdRight 
    {
	    text-align: center;
    }
	input[type=text] 
	{
	    width:180px;
	}
	input[type="text"],textarea,select,input[type="password"]
	{
	     border: 1px solid #2598d5;
	     font-size: 12px;
	     color: #002d71;
    }
	select
    {
		 width:180px;
	}
	.centerDiv 
	{
	    width: 160px;
	    margin: 5px auto 5px auto;
	    text-align: center;
    }
    .btn 
    {
	 border:1px solid #72a1bd;
	 color:#333333;
	 cursor:pointer;
	 line-height:26px;
	 padding:0px 11px 0px 11px;
	 height:26px;
    }
</style>
<script type="text/javascript">
    $(function () {
        $("#txtPwd").change(function () {
            if ($("#txtPwd").val() != "") {
                $("#lblPwd").text("");
            }
        })
        $("#txtPwdTwo").change(function () {
            if ($("#txtPwdTwo").val() != "") {
                $("#lblPwdTwo").text("");
            }
        })
    });
  </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <table align="center" id="tb1">
        
            <tr>
                <td>
                    <span class="required">*</span>密 &nbsp; 码：
                </td>
                <td>
                    <asp:TextBox ID="txtPwd" runat="server"></asp:TextBox>
                    <br />
                    <span class="required"><asp:Label ID="lblPwd" runat="server"></asp:Label></span>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="required">*</span>确认密码：
                </td>
                <td>
                    <asp:TextBox ID="txtPwdTwo" runat="server"></asp:TextBox>
                    <br />
                     <span class="required"><asp:Label ID="lblPwdTwo" runat="server"></asp:Label></span>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;手 &nbsp;机：
                </td>
                <td>
                    <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>
                </td>
            </tr>
            </table>
    </div>   
         <div class="centerDiv">
         <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="保存" 
                 onclick="btnSave_Click"/>
    </div>
    </form>
</body>
</html>
