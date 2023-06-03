<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="testAPI.aspx.vb" Inherits="BOSCenter.testAPI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Amount
        <asp:TextBox ID="txtAmount" runat="server">10</asp:TextBox>
        <br />
        APi Key
        <asp:TextBox ID="txtApiKey" runat="server">UTwHXNFqMTAUPrW5wktuluSARpx7SQ2k3lFh14sZ</asp:TextBox>
        <br />
        Phone No
        <asp:TextBox ID="txtPhoneNumber" runat="server">9212345320</asp:TextBox>
        <br />
        Operator Name
        <asp:TextBox ID="txtOperatorName" runat="server">AIRTEL</asp:TextBox>
        <br />
        Order ID<asp:TextBox ID="txtOrderID" runat="server">123</asp:TextBox>
&nbsp;<br />
        Type Name<asp:TextBox ID="txtTypeName" runat="server">RECH</asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lblResult" runat="server" Text="Reult Will Show Here ..."></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnCallAPI" runat="server" Text="Call Recharge API" />
        &nbsp;<asp:Button ID="btnCallDMTAPI" runat="server" Text="Call DMT API" />
        <br />
    
    </div>
    </form>
</body>
</html>
