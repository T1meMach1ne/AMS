<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UserManager.aspx.cs" Inherits="Apply.UserManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #table1 tr
        {
            height: 30px;
        }
        #table1 tr td
        {
            width: 80px;
        }
    </style>
    <link href="css/main.css" rel="stylesheet" type="text/css" />
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
    <script src="Scripts/ToolKit.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //绑定员工信息表
            BindUserInfos();
            //绑定部门下拉框
            $.ajax({
                type: "get",
                url: "/Handlers/UserInfoHandler.ashx",
                dataType: "json",
                data: {
                    Method: "BindDept"
                },
                success: function (data) {
                    var s = "<option value='0'>--请选择--</option>";
                    $.each(data, function (index, o) {
                        s += "<option value='" + o.DeptID + "'>" + o.DeptName + "</option>";
                    });
                    $("#selDept1,#selDpet2").empty();
                    $("#selDept1,#selDpet2").append(s);
                }
            });
            $("#btnSelect").click(function () {
                BindUserInfos();
            });
            //点击弹出新增用户的窗口
            $("#btnInsertUser").click(function () {
                $("#dlg1").dialog({ width: 500, height: 350 }).dialog("setTitle", "新建用户信息").dialog("open");
            });
            //关闭窗口
            $("#btnEsc1").click(function () {
                $("#dlg1").dialog("close");
            });
            //关闭窗口
            $("#btnEsc2").click(function () {
                $("#dlg2").dialog("close");
            });
            //新增部门信息
            $("#btnSubmit1").click(function () {
                $.ajax({
                    type: "post", //键值对  用来指定HTTP请求的方式
                    url: "/Handlers/UserInfoHandler.ashx", //专门用来设置请求对应的路径 设置一般处理程序所对应的路径
                    data: {                     //参数data的功能：就是用来指定浏览器向服务器传递的数据
                        Method: "InsertUserInfo",
                        UserID: $("#txtUserID1").val(),
                        Name: $("#txtName1").val(),
                        Pwd: $("#txtPwd1").val(),
                        Phone: $("#txtPhone1").val(),
                        Mail: $("#txtMail1").val(),
                        DeptID: $("#selDept1").val()
                    },
                    success: function (data) {//参数data专门用来接收服务器返回给浏览器的数据
                        if (data == "2") {
                            //表示添加失败
                            $.messager.alert('温馨提示', '该用户ID已经存在！', 'info');
                        }
                        else {
                            if (data == "1") {
                                //表示新增成功
                                $("#dlg1").dialog("close");
                                $.messager.alert('温馨提示', '新增成功！', 'info');
                                BindUserInfos();
                            }
                            else {
                                $.messager.alert('温馨提示', "新增失败！", 'info');
                            }
                        }
                    }
                });
            });

        });
        function BindUserInfos() {
            //通过AJAX实现无刷新效果
            $.ajax({
                type: "get", //键值对  用来指定HTTP请求的方式
                url: "/Handlers/UserInfoHandler.ashx", //专门用来设置请求对应的路径 设置一般处理程序所对应的路径
                data: {                     //参数data的功能：就是用来指定浏览器向服务器传递的数据
                    Method: "GetPagedUserInfo",
                    UserID: $("#txtUserID").val(),
                    Name: $("#txtName").val(),
                    DeptID: $("#ContentPlaceHolder2_ddlDept").val()
                },
                dataType: "json",
                success: function (data) {//参数data专门用来接收服务器返回给浏览器的数据
                    var s = "";
                    $.each(data, function (index, o) {
                        s += "<tr>";
                        s += "<td>" + (index + 1) + "</td>";
                        s += "<td>" + o.UserInfo.UserID + "</td>";
                        s += "<td>" + o.UserInfo.Name + "</td>";
                        s += "<td>" + o.UserInfo.Email + "</td>";
                        s += "<td>" + o.UserInfo.Telephone + "</td>";
                        s += "<td>" + o.Department.DeptName + "</td>";
                        s += "<td>";
                        s += "<img title='编辑' onclick='EditUserInfos(\"" + o.UserInfo.UserID + "\")' src='image/common/edit.png' alt=''/>";
                        if (o.UserInfo.Name != '管理员') {
                            s += "<img title='删除' onclick='DeleteUserInfos(\"" + o.UserInfo.UserID + "\")' src='image/common/delete.png' alt=''/>";
                        }
                        s += "</td>";
                        s += "</tr>";
                    });
                    $("#tableUserInfo tr:gt(0)").remove(); //在页面上查找id="tableUserInfo"的元素，并且把下标大于0的tr都删掉
                    $("#tableUserInfo").append(s); //把变量s追加到id="tableUserInfo"的里面去
                }
            });
        }

        function EditUserInfos(UserID) {
            $("#dlg2").dialog({ width: 460, height: 300 }).dialog("setTitle", "修改用户信息").dialog("open");
            $("#txtUserID2").attr("readonly", true);
            //赋值
            $.ajax({
                type: "post",
                url: "/Handlers/UserInfoHandler.ashx",
                data: {
                    method: "GetSingleUserInfo",
                    ID: UserID
                },
                dataType: "json",
                success: function (data) {
                    $("#txtUserID2").val(data.UserID),
                                 $("#txtName2").val(data.Name),
                                 $("#txtPhone2").val(data.Telephone),
                                 $("#txtMail2").val(data.Email),
                                 $("#selDpet2").val(data.DeptID)
                }
            });
            //修改用户信息
            $("#btnSubmit2").click(function () {
                $.ajax({
                    type: "post",
                    url: "/Handlers/UserInfoHandler.ashx",
                    data: {
                        Method: "UpdateUserInfo",
                        ID: UserID,
                        Name: $("#txtName2").val(),
                        Phone: $("#txtPhone2").val(),
                        Mail: $("#txtMail2").val(),
                        DeptId: $("#selDpet2").val(),
                        NewPwd: $("#txtNewPwd").val()
                    },
                    success: function (data) {
                        if (data == "1") {
                            //表示添加成功
                            //关闭窗口
                            $("#dlg2").dialog("close");
                            //即时刷新列表上面的数据
                            BindUserInfos();
                            $.messager.alert('温馨提示', '恭喜您，修改成功！', 'info');
                        }
                        else {
                            //表示添加失败
                            $.messager.alert('温馨提示', '对不起，修改失败！', 'info');
                        }
                    }
                });
            });
        }
        // 删除方法
        function DeleteUserInfos(UserID) {
            if (confirm("确定要删除该用户吗？")) {
                $.ajax({
                    type: "post",
                    url: "/Handlers/UserInfoHandler.ashx",
                    data: {
                        method: "DeleteUserInfo",
                        ID: UserID
                    },
                    success: function (data) {
                        if (data == "1") {
                            //即时刷新列表上面的数据
                            BindUserInfo();
                            $.messager.alert('温馨提示', "恭喜您，删除成功！", 'info');
                        }
                        else {
                            //表示添加失败
                            $.messager.alert('温馨提示', "对不起，删除失败！", 'info');
                        }
                    }
                });
            }
        }
        function validatesInsert() {
            var msg = "";
            if ($("#txtUserID1").val() == "") {
                alert("用户ID不能为空！")
                return false;
                msg += "1";
            }
            if ($("#txtName1").val() == "") {
                alert("姓名不能为空！")
                return false;
                msg += "1";
            }
            if ($("#txtPwd1").val() == "") {
                alert("密码不能为空！")
                return false;
                msg += "1";
            }
            if ($("#txtPwd1").val() != $("#txtPwd2").val()) {
                alert("两次输密码不一致！")
                return false;
                msg += "1";
            }
            if ($("#txtPwd2").val() == "") {
                alert("重复不能为空！")
                return false;
                msg += "1";
            }
            if ($("#selDept").val() == "0") {
                alert("请选择部门！")
                return false;
                msg += "1";
            }
            if (msg = "") {
                return true;
            }

        }
        function validatesUpdate() {
            var msg = "";
            if ($("#txtUserID2").val() == "") {
                alert("用户ID不能为空！")
                return false;
                msg += "1";
            }
            if ($("#txtName2").val() == "") {
                alert("姓名不能为空！")
                return false;
                msg += "1";
            }
            if ($("#selDpet2").val() = "0") {
                alert("请选择部门！")
                return false;
                msg += "1";
            }
            if (msg == "") {
                return true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table align="center">
        <tr>
            <td colspan="4">
                <input id="btnInsertUser" class="btn" type="button" value="新  建" />&nbsp;&nbsp;
            </td>
            <td>
                用户ID：<input type="text" id="txtUserID" />
            </td>
            <td>
                用户名称：<input type="text" id="txtName" />
            </td>
            <td>
                所在部门：<asp:DropDownList ID="ddlDept" runat="server">
                    <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <input id="btnSelect" class="btn" type="button" value="查询" />
            </td>
        </tr>
    </table>
    <table id="tableUserInfo" class="grid" border="1">
        <tr>
            <th>
                序号
            </th>
            <th>
                用户ID
            </th>
            <th>
                姓名
            </th>
            <th>
                邮件地址
            </th>
            <th>
                联系电话
            </th>
            <th>
                所属部门
            </th>
            <th>
                管理
            </th>
        </tr>
    </table>
    <div id="dlg1" class="easyui-dialog" title="My Dialog" style="width: 400px; height: 300px;"
        data-options="iconCls:'icon-save',resizable:true,modal:true,closed:true">
        <table align="center" id="table1">
            <tr>
                <td>
                    <span style="color: Red">*</span>用户ID：
                </td>
                <td>
                    <input type="text" id="txtUserID1" style="width: 250px" />
                </td>
            </tr>
            <tr>
                <td>
                    <span style="color: Red">*</span>姓 名：
                </td>
                <td>
                    <input type="text" id="txtName1" style="width: 250px" />
                </td>
            </tr>
            <tr>
                <td>
                    <span style="color: Red">*</span>密 码：
                </td>
                <td>
                    <input type="text" id="txtPwd1" style="width: 250px" />
                </td>
            </tr>
            <tr>
                <td>
                    <span style="color: Red">*</span>重复密码：
                </td>
                <td>
                    <input type="text" id="txtPwd2" style="width: 250px" />
                </td>
            </tr>
            <tr>
                <td>
                    电 话：
                </td>
                <td>
                    <input type="text" id="txtPhone1" style="width: 250px" />
                </td>
            </tr>
            <tr>
                <td>
                    邮 件：
                </td>
                <td>
                    <input type="text" id="txtMail1" style="width: 250px" />
                </td>
            </tr>
            <tr>
                <td>
                    <span style="color: Red">*</span>部 门：
                </td>
                <td>
                    <select id="selDept1" name="sel1">
                    </select>
                </td>
            </tr>
            <tr align="center" style="height: 50px">
                <td colspan="2">
                    <input id="btnSubmit1" type="button" value="确  定" onclick="return validatesInsert()"
                        class="btn" style="width: 50px" />&nbsp;&nbsp;
                    <input id="btnEsc1" type="button" value="取  消" class="btn" style="width: 50px" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dlg2" class="easyui-dialog" title="My Dialog" style="width: 400px; height: 300px;"
        data-options="iconCls:'icon-save',resizable:true,modal:true,closed:true">
        <table align="center" id="table2">
            <tr>
                <td>
                    <span style="color: Red">*</span>用户ID：
                </td>
                <td>
                    <input type="text" id="txtUserID2" style="width: 250px" />
                </td>
            </tr>
            <tr>
                <td>
                    <span style="color: Red">*</span>姓 名：
                </td>
                <td>
                    <input type="text" id="txtName2" style="width: 250px" />
                </td>
            </tr>
            <tr>
                <td>
                    电 话：
                </td>
                <td>
                    <input type="text" id="txtPhone2" style="width: 250px" />
                </td>
            </tr>
            <tr>
                <td>
                    邮 件：
                </td>
                <td>
                    <input type="text" id="txtMail2" style="width: 250px" />
                </td>
            </tr>
            <tr>
                <td>
                    <span style="color: Red">*</span>部 门：
                </td>
                <td>
                    <select id="selDpet2" name="sel2">
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    重置密码：
                </td>
                <td>
                    <input type="text" id="txtNewPwd" style="width: 250px" />
                </td>
            </tr>
            <tr align="center" style="height: 50px">
                <td colspan="2">
                    <input id="btnSubmit2" type="button" value="确  定" onclick="return validatesUpdate()"
                        class="btn" style="width: 50px" />&nbsp;&nbsp;
                    <input id="btnEsc2" type="button" value="取  消" class="btn" style="width: 50px" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
