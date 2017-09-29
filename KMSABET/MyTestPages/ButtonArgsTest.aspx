<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ButtonArgsTest.aspx.cs" Inherits="KMSABET.MyTestPages.ButtonArgsTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="test" runat="server" CommandArgument="MyVal" CommandName="ThisBtnClick" OnClick="MyBtnHandler" />
    </div>
    </form>
</body>
</html>
