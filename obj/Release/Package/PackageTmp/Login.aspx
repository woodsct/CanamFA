<%@ Page Language="C#" MasterPageFile="~/CanamSite.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CanamLiveFA.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="server">
    <div style="padding-left:37%;padding-top:5%">
        <div align="center" style="border: medium groove #000000; background-color: #FFFFFF; width: 406px; height: 185px;">
    
            <br />
    
            <asp:Label ID="lblUserName" runat="server" Text="User Name:" Style="padding-right:190px"></asp:Label>
            <br />
            <asp:TextBox ID="txtUserName" runat="server" Width="250px" BorderStyle="Solid" ></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblPassword" runat="server" Text="Password:" Style="padding-right:190px"></asp:Label>
            <br />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="250px" BorderStyle="Solid" ></asp:TextBox>
            <br />
            <asp:PlaceHolder ID="plcError" runat="server" Visible="false">
                <asp:Label ID="lblError" runat="server" Text="Your username and password do not match" ForeColor="Red"></asp:Label>
            </asp:PlaceHolder>
            <br />
            <asp:Button ID="btnSave" runat="server" Text="Login" BackColor="#CCCCCC" BorderStyle="Solid" OnClick="btnSave_Click" Height="26px" />
    
        </div>
    </div>
</asp:Content>
