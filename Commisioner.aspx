<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CanamSite.Master" CodeBehind="Commisioner.aspx.cs" Inherits="CanamLiveFA.Commisioner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="server">
        <table ID="Table1" runat="server" Height="160px" Width="80%" align="center" border="1" style="background-color:white;">
            <tr>
                <td width="33%" align="center" >
                    Free Agents File<br />
                    <asp:FileUpload ID="fupFreeAgents" runat="server" />
                    <br />
                    <asp:Label ID="lblQualifyingOffer" runat="server" Text="Qualifying Offer Value" />
                    <asp:TextBox ID="txtQualifyingOffer" runat="server" /><br />
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" /><br />
                    <asp:Label ID="lblFileError" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                </td>
                <td width="33%" align="center">
                    <asp:Label ID="lblPlayerEndTime" runat="server" Text="Player Reset Hours"></asp:Label>
                    <asp:TextBox ID="txtPlayerEndTime" runat="server" TextMode="Number" step="0.01" ></asp:TextBox>
                    
                    <br />
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                    <br />
                    <asp:PlaceHolder ID="plcError" runat="server" Visible="false">
                        <asp:Label ID="lblError" runat="server" Text="Error: Enter an end time" ForeColor="Red"></asp:Label>
                    </asp:PlaceHolder>
                </td>
                <td width="33%" align="center">
                    <asp:Button ID="btnSignPlayers" runat="server" Text="Export Signings" OnClick="btnSignPlayers_Click" />
                    <br />
                    <asp:Label ID="lblSigned" Text ="Signings Calculated" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Go Back" />
                </td>
            </tr>
        </table>
        
        <br />
    
    </div>
</asp:Content>
