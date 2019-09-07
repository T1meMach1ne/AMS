<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AskForLeave.aspx.cs" Inherits="UIL.AskForLeave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="shortcut icon" href="image/login/logo.ico"/>
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
    <script type="text/javascript">
        //文档就绪函数
        $(function () {       
            //通过jQuery找按钮“添加”
            $("#btnAdd").click(function () {
                $("#dlg").dialog("setTitle", "添加申请").dialog("open");
                $("#frm1").attr("src", "AskForLeaveApprovalEdit.aspx");
            });
        });
        function UpdateApproveInfo(ApproveID) {
            $("#dlg").dialog("setTitle", "修改申请").dialog("open");
            $("#frm1").attr("src", "AskForLeaveApprovalEdit.aspx?ID=" + ApproveID);
        }
        function LookApproveInfo(ApproveID) {
            $("#dlg").dialog("setTitle", "查看申请").dialog("open");
            $("#frm1").attr("src", "AskForLeaveApprovalEdit.aspx?ID=" + ApproveID);
        }
      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <p>
        请假申请</p>
    <div align="center">
        <table>
            <tr>
                <td>
                    <input id="btnAdd" class="btn" type="button" value="请假" />
                </td>
                <td>
                    标题:<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                </td>
                <td>
                    申请时间:<asp:TextBox ID="txtBeginTime" runat="server" class="easyui-datebox"></asp:TextBox> 
                             到<asp:TextBox ID="txtEndTime" runat="server" class="easyui-datebox"></asp:TextBox>
                </td>
                <td>
                    
                    请假单状态：
                </td>
                <td>
                    <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="待审批" Value="0"></asp:ListItem>
                        <asp:ListItem Text="归档" Value="1"></asp:ListItem>
                        <asp:ListItem Text="全部" Value="2" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:Button ID="btn_select" runat="server" Text="查询" CssClass="btn" OnClick="btn_select_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView ID="GvAskForLeave" runat="server" AutoGenerateColumns="false" CssClass="grid"
        OnRowDataBound="GvAskForLeave_RowDataBound" 
    OnRowCommand="GvAskForLeave_RowCommand" AllowSorting="True" 
    onsorting="GvAskForLeave_Sorting" AllowPaging="True" 
        onpageindexchanging="GvAskForLeave_PageIndexChanging" PageSize="6">
        <Columns>
            <asp:BoundField HeaderText="请假单ID" DataField="ApproveID" />
            <asp:BoundField HeaderText="申请人" DataField="UserName" SortExpression="UserName" />
            <asp:BoundField HeaderText="标题" DataField="Title" />
            <asp:BoundField HeaderText="开始时间" DataField="BeginDate" SortExpression="BeginDate" />
            <asp:BoundField HeaderText="结束时间" DataField="EndDate" SortExpression="EndDate" />
            <asp:BoundField HeaderText="申请时间" DataField="ApplyDate" SortExpression="ApplyDate" />
            <asp:BoundField HeaderText="请假单状态" DataField="NewStatus" />
            <asp:TemplateField HeaderText="编辑">
                <ItemTemplate>
                    <a href="#" id="aShow" runat="server" onclick='<%# "LookApproveInfo("+Eval("ApproveID")+")"%>'>
                        查看</a>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/image/common/edit.png" onclick='<%# "UpdateApproveInfo("+Eval("ApproveID")+")"%>' />
                    <asp:ImageButton ID="imgDelete" runat="server" ImageUrl="~/image/common/delete.png"
                        CommandName="DeleteApproveInfo" CommandArgument='<%# Eval("ApproveID") %>' OnClientClick="return confirm('是否删除?'); " />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div id="dlg" class="easyui-dialog" title="My Dialog" style="width: 700px; height: 450px;"
        data-options="iconCls:'icon-save',resizable:true,modal:true,closed:true">
        <iframe id="frm1" width='99%' height='99%' frameborder='0'></iframe>
    </div>
</asp:Content>
