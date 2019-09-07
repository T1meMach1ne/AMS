<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AskForLeaveApprovalEdit.aspx.cs"
    Inherits="UIL.AskForLeaveApproval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--第一步：引入Easy UI所需的样式表文件-->
    <link href="css/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <!--第二步：引入Easy UI所需的图标样式表文件-->
    <link href="css/themes/icon.css" rel="stylesheet" type="text/css" />
    <!--第三步：引入jQuery所需的js文件-->
    <script src="Scripts/easyui/jquery-1.11.3.js" type="text/javascript"></script>
    <!--第四步：引入Easy UI所需的js文件-->
    <script src="Scripts/easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <!--第五步：引入Easy UI所需的汉化包js文件-->
    <script src="Scripts/easyui/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="Scripts/easyui/jquery-1.11.3.js" type="text/javascript"></script>
    <style type="text/css">
        .div1
        {
             text-align:center; 	
        }
           
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
		input[type=text] {
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
        $("#txtTitle").change(function () {
            if ($("#txtTitle").val() != "") {
                $("#lblTitle").text("");
            }
        })
        $("#txtBeginTime").change(function () {
            if ($("#txtBeginTime").val() != "") {
                $("#lblBeginTime").text("");
            }
        })
        $("#txtEndTime").change(function () {
            if ($("#txtEndTime").val() != "") {
                $("#lblEndTime").text("");
            }
        })
        $("#txtReason").change(function () {
            if ($("#txtReason").val() != "") {
                $("#lblReason").text("");
            }
        })
        $("#ddlResult").change(function () {
            if ($("#ddlResult").find("option:selected").val() != "") {
                $("#lblEndTime").text("");
            }
        })
    });
  </script>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="Panel1" runat="server" GroupingText="请假信息">
            <table align="center">
                <tr>
                    <td class="tdLeft">
                        <span class="required">*</span>请假单号：
                    </td>
                    <td>
                        <asp:Label ID="lblNum" runat="server"></asp:Label>
                    </td>
                    <td>
                        申请人：
                    </td>
                    <td>
                        <asp:Label ID="lblUser" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdLeft">
                        <span class="required">*</span>标&nbsp;&nbsp;题：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtTitle" runat="server" Width="420px"></asp:TextBox>
                        <br />
                        <span class="required"><asp:Label ID="lblTitle" runat="server"></asp:Label></span>
                    </td>
                    
                </tr>
               <tr>
                    <td class="tdLeft">
                        <span class="required">*</span>起始时间：
                    </td>
                    <td>
                        <asp:TextBox ID="txtBeginTime" runat="server" Width="120px" class="easyui-datebox"></asp:TextBox>
                        <asp:DropDownList ID="ddlBeginTime" runat="server" Width="60px">
                            <asp:ListItem Text="8:30" Value="8:30"></asp:ListItem>
                            <asp:ListItem Text="13:50" Value="13:50"></asp:ListItem>
                        </asp:DropDownList>
                          <br />
                        <span class="required"><asp:Label ID="lblBeginTime" runat="server"></asp:Label></span>
                    </td>
                    <td>
                        <span class="required">*</span>结束时间：
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndTime" runat="server" Width="120px" class="easyui-datebox"></asp:TextBox>
                        <asp:DropDownList ID="ddlEndTime" runat="server" Width="60px">
                            <asp:ListItem Text="11:30" Value="11:30"></asp:ListItem>
                            <asp:ListItem Text="17:00" Value="17:00"></asp:ListItem>
                        </asp:DropDownList>
                          <br />
                        <span class="required"><asp:Label ID="lblEndTime" runat="server"></asp:Label></span>
                    </td>
                </tr>
                <tr>
                    <td class="tdLeft">
                        <span class="required">*</span>请假原因：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Width="430px"></asp:TextBox>
                          <br />
                        <span class="required"><asp:Label ID="lblReason" runat="server"></asp:Label></span>
                    </td>
                </tr>
                
            </table>
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" GroupingText="审批信息">
            <table align="center" style="width: 365px">
                <tr>
                    <td>
                        &nbsp;&nbsp;申请时间：
                    </td>
                    <td>
                        <asp:Label ID="lblApplyDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                          &nbsp; <span class="required">*</span>审批结果：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlResult" runat="server" Width="100px">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                            <asp:ListItem Text="不同意" Value="0"></asp:ListItem>
                            <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                        <span class="required"><asp:Label ID="lblResult" runat="server" Text=""></asp:Label></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;&nbsp;审 批 人：
                    </td>
                    <td>
                        <asp:Label ID="lblApproveUser" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;&nbsp;审批时间：
                    </td>
                    <td>
                        <asp:Label ID="lblApproveDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;&nbsp;备&nbsp;&nbsp;注：
                    </td>
                    <td>
                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Height="30px" Width="218px"></asp:TextBox>
                    </td>
                </tr>
                </table>
        </asp:Panel>
           <div class="div1"><asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn" onclick="btnSave_Click" /></div>
         
    </div>
    </form>
</body>
</html>
