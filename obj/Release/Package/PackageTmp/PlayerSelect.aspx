<%@ Language="C#" MasterPageFile="~/CanamSite.Master" AutoEventWireup="true" CodeBehind="PlayerSelect.aspx.cs" Inherits="CanamLiveFA.PlayerSelect" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">
    <div style="padding-left:15%;">
        <div style="width:80%;background-color:white;border-color:black">
            <div style="margin-left:20px;width:100%;"">
                <br />
                <asp:Label text="Player Selection Window" runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                <br />
                <br />
                <asp:Label text="Select a player from the below list. To place a bid click the hyperlink beside the players name." runat="server"></asp:Label>
                <br />
                <br />
                <table>
                    <tr>
                        <td width="47%">
                            <asp:Label ID="lblUnsignedPlayers" runat="server" Text="Unsigned Players"></asp:Label>
                            <asp:GridView ID="grdUnsignedPlayers" runat="server" AutoGenerateColumns="false" AllowSorting="true" CellPadding="5" OnSorting="grdUnsignedPlayers_Sorting" 
                                HeaderStyle-ForeColor="Black" AllowPaging="true" OnPageIndexChanging="grdUnsignedPlayers_PageIndexChanging" >
                                <Columns>
                                    <asp:HyperLinkField HeaderText="Player Name" SortExpression="Name" DataTextField="Name" DataNavigateUrlFields ="Id" DataNavigateUrlFormatString="~/PlayerView.aspx?id={0}"></asp:HyperLinkField>
                                    <asp:BoundField HeaderText="Current Team" DataField="TeamName" SortExpression="TeamName" />
                                    <asp:BoundField HeaderText="Level" DataField="PlayerLevel" SortExpression="PlayerLevel" />
                                    <asp:BoundField HeaderText="Highest Bid" DataField="bid" SortExpression="Bid" />
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td width="53%" valign="top">
                            <asp:PlaceHolder ID="plcSignedPlayers" runat="server" Visible="false">
                            <asp:Label ID="lblSignedPlayers" runat="server" Text="Signed Players"></asp:Label>
                            <asp:GridView ID="grdSignedPlayers" runat="server" AutoGenerateColumns="false" AllowSorting="true" CellPadding="5" OnSorting="grdSignedPlayers_Sorting" 
                                HeaderStyle-ForeColor="Black" AllowPaging="true" OnPageIndexChanging="grdSignedPlayers_PageIndexChanging" Style="text-wrap:avoid">
                                <Columns>
                                    <asp:BoundField HeaderText="Player Name" DataField="Name" SortExpression="Name" />
                                    <asp:BoundField HeaderText="Signing Team" DataField="TeamName" SortExpression="TeamName" >
                                         <HeaderStyle Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Level" DataField="PlayerLevel" SortExpression="PlayerLevel" />
                                    <asp:BoundField HeaderText="Contract Value" DataField="Bid" SortExpression="Bid"  />
                                    <asp:BoundField HeaderText="Compensation" DataField="Compensation" SortExpression="Compensation" />
                                </Columns>
                            </asp:GridView>
                            </asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
