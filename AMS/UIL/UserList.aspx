<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="UIL.UserList" %>
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
                $("#dlg3").dialog("setTitle", "添加用户").dialog("open");
                $("#frm3").attr("src", "UserEdit.aspx");
            });
        });
        function UpdateUserInfo(UserID) {
            $("#dlg3").dialog("setTitle", "修改用户").dialog("open");
            $("#frm3").attr("src", "UserEdit.aspx?UserID=" + UserID);
        }
        function SelectAll(checkAll) {
            var items = document.getElementsByTagName("input");
            for (var i = 0; i < items.length; i++) {
                items[i].checked = checkAll.checked;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <p> 用户管理 </p>
        <div  align="center">
        <input id="btnAdd" class="btn" type="button" value="添加"  />
        <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="btn" 
                onclick="btnDel_Click" OnClientClick="return confirm('是否删除?'); "/>  
        &nbsp;&nbsp; &nbsp;&nbsp;
         用户ID:<asp:TextBox ID="txtID"  runat="server"></asp:TextBox>
         &nbsp;&nbsp;
         用户名:<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
         部门：<asp:DropDownList ID="ddlDepartment" runat="server"  AppendDataBoundItems="True">                   
               </asp:DropDownList>
             <asp:Button ID="btn_select" runat="server" Text="查询" CssClass="btn" 
                onclick="btn_select_Click" />
                </div>       
        <div align="center">
        <asp:GridView ID="GvUserInfo" AutoGenerateColumns="False" runat="server" Width="60%" 
                CssClass="grid" onrowcommand="GvUserInfo_RowCommand" AllowSorting="True" 
                onrowdatabound="GvUserInfo_RowDataBound"
                onsorting="GvUserInfo_Sorting" AllowPaging="True" 
                onpageindexchanging="GvUserInfo_PageIndexChanging1" PageSize="5" >
        <Columns>
       <asp:TemplateField HeaderText="序号" InsertVisible="False"> 
            <ItemTemplate> 
                 <asp:Label ID="lblNO" runat="server"></asp:Label>
            </ItemTemplate> 
        </asp:TemplateField>
        <asp:TemplateField HeaderText="全选">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chAll" onclick="SelectAll(this)" runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                     <asp:CheckBox ID="ck1" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>
        <asp:BoundField DataField="UserID" SortExpression="UserID" HeaderText="用户ID"/>
        <asp:BoundField DataField="UserName" SortExpression="UserName" HeaderText="用户姓名"/>
        <asp:BoundField DataField="DeptName" SortExpression="DeptName" HeaderText="所属部门"/>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/image/common/edit.png" onclick='<%# "UpdateUserInfo("+Eval("UserID")+")"%>'/>
                    <asp:ImageButton  ID="imgDelete" runat="server" ImageUrl="~/image/common/delete.png" CommandName="DeleteUserInfo" CommandArgument='<%# Eval("UserID") %>' OnClientClick="return confirm('是否删除?'); "/>
                </ItemTemplate>
            </asp:TemplateField>
    </Columns>
    </asp:GridView>
    </div>
   <%-- <div align="center">
    <table>
        <tr>
            <td>
                <asp:ImageButton ID="btnFirst" runat="server" ImageUrl="~/image/DataPager/begin.gif" OnClick="btnFirst_Click" Style="width: 18px" />
                <asp:ImageButton ID="btnPrev" runat="server" ImageUrl="~/image/DataPager/last.gif"
                    OnClick="btnPrev_Click" />
                <asp:DropDownList ID="ddlPage" runat="server"   AutoPostBack="true" 
                    OnSelectedIndexChanged="ddlPage_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:ImageButton ID="btnNext" runat="server" ImageUrl="~/image/DataPager/next.gif"
                    OnClick="btnNext_Click" />
                <asp:ImageButton ID="btnLast" runat="server" ImageUrl="~/image/DataPager/end.gif"
                    OnClick="btnLast_Click" />共<asp:Label ID="labCountNum" runat="server"></asp:Label>条记录，当前第<asp:Label ID="labCurrentPageIndex" runat="server"></asp:Label>页
                /共<asp:Label ID="labCountPage" runat="server"></asp:Label>页
            </td>
        </tr>
    </table>
    </div>--%>
    <div id="dlg3" class="easyui-dialog" title="My Dialog" style="width:500px;height:350px;"   
        data-options="iconCls:'icon-save',resizable:true,modal:true,closed:true">
        <iframe id="frm3" width='99%' height='99%' frameborder='0'></iframe>
    </div>
</asp:Content>
