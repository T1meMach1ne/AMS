<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DepartmentList.aspx.cs" Inherits="UIL.DepartmentList" %>
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
                $("#dlg").dialog("setTitle", "添加部门").dialog("open");
                $("#frm1").attr("src", "DepartmentEdit.aspx");
            });
        });
        function DepartmentInfo(DeptID) {
            $("#dlg").dialog("setTitle", "修改部门").dialog("open");
            $("#frm1").attr("src", "DepartmentEdit.aspx?DeptID=" + DeptID);
        }

    </script>
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
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <p>部门管理 </p>
        <div align="center">
        <table>
            <tr>
                <td class="tdRight" style="width:200px"><input id="btnAdd" class="btn" type="button" value="添加" /> </td>
                <td class="tdLeft">部门名称:<asp:TextBox ID="txt_DepartmentName" runat="server"></asp:TextBox></td>
                <td class="tdLeft"> 主管:<asp:DropDownList ID="ddl_Manager" runat="server" AppendDataBoundItems="True"></asp:DropDownList>  </td>
                <td class="tdLeft" style="width:100px">  <asp:Button ID="btn_select" runat="server" Text="查询" CssClass="btn" onclick="btn_select_Click"/></td>
            </tr>
        </table>
        </div>
        <div align="center"  >
        <asp:GridView AutoGenerateColumns="False" ID="GvDepartmentInfo" runat="server" Width="60%" 
                AllowPaging="True" PageSize="6"  
                onpageindexchanging="GridView1_PageIndexChanging" CssClass="grid"
                onrowdatabound="GvDepartmentInfo_RowDataBound" 
                onrowcommand="GvDepartmentInfo_RowCommand" >
        <Columns>
        <asp:TemplateField HeaderText="序号" InsertVisible="False"> 
            <ItemTemplate> 
                 <asp:Label ID="lblNO" runat="server"></asp:Label>
            </ItemTemplate> 
        </asp:TemplateField>
        
       <asp:BoundField DataField="DeptName" HeaderText="部门名称" />
            <asp:BoundField DataField="UserName" HeaderText="主管" />
        <asp:TemplateField HeaderText="编辑">
            <ItemTemplate>
                    
                  <asp:Image ID="imgUpdate" ImageUrl="~/image/common/edit.png" runat="server" onclick='<%# "DepartmentInfo("+Eval("DeptID")+")"%>'/>
                  
                  <asp:ImageButton ID="imgDelete" ImageUrl="~/image/common/delete.png" runat="server" CommandName="DeleteUserInfo" CommandArgument='<%# Eval("DeptID")%>' OnClientClick="return confirm('是否删除?'); " />
                         
            </ItemTemplate>
        </asp:TemplateField>   
    </Columns>
          
    </asp:GridView>
    <div id="dlg" class="easyui-dialog" title="My Dialog" style="width:400px;height:300px;"   
        data-options="iconCls:'icon-save',resizable:true,modal:true,closed:true">   
        <iframe id="frm1" width="99%" height="99%" frameborder="0"></iframe>
    </div>
</asp:Content>
