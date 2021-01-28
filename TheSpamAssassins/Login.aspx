<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TheSpamAssassins.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login &mdash; Camp Summer</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="Styles/Main.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="welcome_container">
        <div class="welcome_container_top">
            <img src="Images/sun.svg" />
        </div>
        <div class="welcome_container_bottom">
            <h1>Camp Summer</h1>
            <asp:Label ID="messageLbl" class="quote_large" runat="server"></asp:Label>
        </div>
    </div>
    <form id="form1" runat="server">
    <div class="login_container main-text">
        <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="35pt" Text="Oracle Login"></asp:Label>
        <div class="form_container">
            <div class="form_item">
                <asp:Label ID="lblUser" runat="server" Text="Username"></asp:Label>
                <asp:TextBox ID="tboxUsername" class="textbox_large transparent_bg_strong" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="validName" runat="server" ControlToValidate="tboxUsername" ErrorMessage="You must enter a username." ForeColor="Orange"></asp:RequiredFieldValidator>
            </div>
            <div class="form_item">
                <asp:Label ID="lblPass" runat="server" Text="Password"></asp:Label>
                <asp:TextBox ID="tboxPassword" class="textbox_large transparent_bg_strong" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="validPass" runat="server" ControlToValidate="tboxPassword" ErrorMessage="You must enter a password." ForeColor="Orange"></asp:RequiredFieldValidator>
            </div>
            <div class="form_item"><asp:linkbutton class="button_emphasis" ID="loginBtn" runat="server" OnClick="loginBtn_Click">LOGIN</asp:linkbutton></div>
            <div class="form_item fail_message"><asp:Label ID="lblFail" runat="server" Font-Bold="True" ForeColor="Orange" Text="LOGIN FAILED" Visible="False"></asp:Label></div>
            <div class="form_item">
                <img class="form_footer_icon" src="Images/medal.svg" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
