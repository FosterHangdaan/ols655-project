<%@ Page Language="C#" MasterPageFile="~/Camp.Master" AutoEventWireup="true" CodeBehind="CamperActivities.aspx.cs" Inherits="TheSpamAssassins.CamperActivities" %>
<%@ MasterType VirtualPath="~/Camp.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Your Activities &mdash; Camp Summer</title>
    <link href="Styles/Activity.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" class="content_placeholder" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="camper_activities_content">
        <asp:GridView ID="ActivityGridView"
            runat="server"
            AutoGenerateColumns="False"
            OnRowCommand="ActivityGridView_RowCommand"
            OnRowDataBound="ActivityGridView_RowDataBound"
            AlternatingRowStyle-CssClass="alt"
            CssClass="grid main-text">
            <Columns>
                <asp:BoundField HeaderText="ID" Visible="False" DataField="ActivityId" />
                <asp:BoundField HeaderText="Activity Name" DataField="ActivityName" />
                <asp:BoundField HeaderText="Camp Day" DataField="CampDay" />
                <asp:ButtonField ButtonType="Button" CommandName="UnregisterActivity" ShowHeader="True" Text="Unregister" />
            </Columns>
        </asp:GridView>
        <asp:Label CssClass="error_message" ID="errorMsgLbl" runat="server"></asp:Label>
    </div>
</asp:Content>

