<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentEdit.aspx.cs" Inherits="UIL.DepartmentEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/easyui/jquery-1.11.3.js" type="text/javascript"></script>
      <style type="text/css">
    .tdLeft 
    {
	    text-align: right;
	    height: 32px;
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
         margin-left: 0px;
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
	     background:url('http://localhost:1453/WebSite/image/common/btn.gif') repeat-x 0px -96px;
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
        $("#txtDepartmentName").change(function () {
            if ($("#txtDepartmentName").val() != "") {
                $("#lblDeptName").text("");
            }
        })
        $("#ddlManager").change(function () {
            if ($("#ddlManager").find("option:selected").val() != "") {
                $("#lblManager").text("");
            }
        })
    });
  </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
          <table align="center" width="80%">
           
            <tr>
                <td class="tdLeft">
                    <span class="required">*</span>部门名称：
                 </td>
                <td class="tdRight">
                        <asp:TextBox ID="txtDepartmentName" runat="server"></asp:TextBox>
                        <br />
                        <span class="required"><asp:Label ID="lblDeptName" runat="server"></asp:Label></span>
                </td>
            </tr>
            <tr>
                <td class="tdLeft">
                    <span class="required">*</span>主&nbsp;&nbsp;管：</td>
                <td class="tdRight">
                    <asp:DropDownList ID="ddlManager" runat="server"></asp:DropDownList>
                     <br />
                        <span class="required"><asp:Label ID="lblManager" runat="server"></asp:Label></span>
                </td>
            </tr>
            <tr>
                <td class="tdLeft">部门说明：</td>
                <td class="tdRight">
                    <asp:TextBox ID="txtDepartmentInfo" runat="server" TextMode="MultiLine" Width="180px" ></asp:TextBox>
                </td>
            </tr>
        </table>
        <br /> <br />
        <div class="centerDiv">
                <asp:Button ID="btnInsert" runat="server" Text="保存" CssClass="btn" onclick="btnInsert_Click"/>
        </div>
    </div>
    </form>
</body>
</html>
