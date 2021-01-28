<%@ Page Language="C#" MasterPageFile="~/Camp.Master" AutoEventWireup="true" CodeBehind="RegisterActivity.aspx.cs" Inherits="TheSpamAssassins.RegisterActivity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Activity Registration &mdash; Camp Summer</title>
    <link href="Styles/Register.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content id="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="register_activity_content">
        <div class="select_campday_container content-text">
            <asp:Label CssClass="select_campday_text" ID="SelectCampDayLbl" runat="server" Text="Select Camp Day:"></asp:Label>
            <asp:DropDownList
                ID="campDayDDL"
                AutoPostBack="true"
                OnSelectedIndexChanged="campDayDDL_SelectedIndexChanged"
                runat="server">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:Label CssClass="error_message" ID="errorMsgLbl" runat="server"></asp:Label>
        <asp:GridView ID="activitiesGV"
            CssClass="grid main-text"
            runat="server"
            AutoGenerateColumns="False"
            AlternatingRowStyle-CssClass="alt"
            OnRowCommand="activitiesGV_RowCommand">
            <Columns>
                <asp:BoundField DataField="ActivityId" HeaderText="Activity ID" />
                <asp:BoundField HeaderText="Activity Name" DataField="Name" />
                <asp:BoundField DataField="Capacity" HeaderText="Activity Capacity" />
                <asp:ButtonField ButtonType="Button" CommandName="RegisterActivity" Text="Register" />
            </Columns>
        </asp:GridView> 
    </div>
</asp:Content>