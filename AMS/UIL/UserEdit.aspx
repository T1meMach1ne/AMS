<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="UIL.UserEdit" %>

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
    .btn {
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
        $("#txtUserID").change(function () {
            if ($("#txtUserID").val() != "") {
                $("#lblUserID").text("");
            }
        })
        $("#txtUserName").change(function () {
            if ($("#txtUserName").val() != "") {
                $("#lblUserName").text("");
            }
        })
        $("#ddlUserType").change(function () {
            if ($("#ddlUserType").find("option:selected").val() != "") {
                $("#lblUserType").text("");
            }
        })
        $("#ddlDeptName").change(function () {
            if ($("#ddlDeptName").find("option:selected").val() != "") {
                $("#lblDeptName").text("");
            }
        })
        $("#txtCellphone").change(function () {
            if ($("#txtCellphone").val() != "") {
                $("#lblCellphone").text("");
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
                    <span class="required">*</span>用 户 ID：</td>
                <td class="tdRight">
                        <asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
                       <br />
                        <span class="required"><asp:Label ID="lblUserID" runat="server" Text=""></asp:Label></span>
                </td>
            </tr>
            <tr>
                <td class="tdLeft">
                    <span class="required">*</span>用户姓名：</td>
                <td class="tdRight">
                        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox> 
                         <br />  
                    <span class="required"><asp:Label ID="lblUserName" runat="server" Text=""></asp:Label></span>
                    </td>
           </tr>
            <tr>
                <td class="tdLeft">
                    <span class="required">*</span>用户类型：</td>
                <td class="tdRight">
                    <asp:DropDownList ID="ddlUserType" runat="server"></asp:DropDownList>
                     <br />
                      <span class="required"><asp:Label ID="lblUserType" runat="server" Text=""></asp:Label></span>
                </td>
            </tr>
            <tr>
                <td class="tdLeft">
                     <span class="required">*</span>所属部门：</td>
                <td class="tdRight">
                     <asp:DropDownList ID="ddlDeptName" runat="server"></asp:DropDownList>
                     <br />
                    <span class="required"><asp:Label ID="lblDeptName" runat="server" Text=""></asp:Label></span>
                </td>            
            </tr>
            <tr>
                <td class="tdLeft">
                    手&nbsp;&nbsp;机：</td>
                <td class="tdRight">
                    <asp:TextBox ID="txtCellphone" runat="server"></asp:TextBox>
                     <br />
                      <span class="required"><asp:Label ID="lblCellphone" runat="server" Text=""></asp:Label></span>
                </td>                       
            </tr>
        </table>
        <div class="centerDiv">
                <asp:Button ID="btnInsert" runat="server" Text="保存" CssClass="btn" onclick="btnInsert_Click1"/>
        </div>
    </div>
    </form>
</body>
</html>
