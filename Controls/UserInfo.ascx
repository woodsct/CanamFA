<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.ascx.cs" Inherits="CanamLiveFA.Controls.UserInfo" %>

<div style="padding-left:20%">
    <table style="background-color:white;" border="1" cellpadding="5" width="70%">
        <tr>
            <td style="width:20%" align="center">
                <br />
                <asp:Label Text="UserName: " runat="server"></asp:Label>
                <asp:Label ID="lblUser" runat="server"></asp:Label>    
                <br />
                <asp:LinkButton ID="lnkLogout" OnClick="lnkLogout_Click" runat="server" Text="Logout"></asp:LinkButton> <br />
            </td>
            <td style="width:20%" align="center">
                <br />
                <asp:Label Text="Team: " runat="server"></asp:Label>
                <asp:Label ID="lblTeam" runat="server"></asp:Label>
                <br /><br />
            </td>
            <td style="width:20%" align="center">
                <asp:Label ID="lblFreeAgencyStarted" runat="server" ForeColor="Red" ></asp:Label><br />
            </td>
            <asp:PlaceHolder ID="plcCommissioner" runat="server" Visible="false">
                <td style="width:20%" align="center">
                    <asp:HyperLink ID="hlnkCommissioner" runat="server" Text="Site Controls" NavigateUrl="~/Commisioner.aspx"></asp:HyperLink>
                </td>
            </asp:PlaceHolder>
        </tr>
    </table>
</div>