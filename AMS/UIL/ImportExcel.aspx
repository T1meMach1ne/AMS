<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportExcel.aspx.cs" Inherits="UIL.ImportExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<script src="Scripts/easyui/jquery-1.11.3.js" type="text/javascript"></script>
<link href="css/common.css" rel="stylesheet" type="text/css" />
    <title></title>
    <style type="text/css">
    .required {
	           color: Red;
	           margin-right: 2px;
              }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#fuFile").change(function () {
                if ($("#fuFile").val() != "") {
                    $("#lblMsg").text("");
                }
            })
        });
  </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center">
          <asp:FileUpload ID="fuFile" runat="server" Width="400px" />
          <asp:Button ID="btnImport" runat="server" Text="导入" CssClass="btn" 
            onclick="btnImport_Click" />
          <br />
          <span class="required"><asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></span>
        
    </div>
    </form>
</body>
</html>
