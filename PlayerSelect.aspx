<%@ Language="C#" MasterPageFile="~/CanamSite.Master" AutoEventWireup="true" CodeBehind="PlayerSelect.aspx.cs" Inherits="CanamLiveFA.PlayerSelect" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">
    <div style="width:100%;background-color:white;border-color:black">
        <div style="margin-left:20px;width:100%;"">
            <br />
            <asp:Label text="Player Selection Window" runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
            <br />
            <br />
            <asp:Label text="Select a player from the below list. To place a bid click the hyperlink on the players name." runat="server"></asp:Label>
            <br />
            <br />
            <table>
                <tr>
                    <td width="500px">
                        <asp:Label ID="lblUserPlayers" runat="server" Text="Your Players"></asp:Label>
                        <asp:GridView ID="grdUserPlayers" runat="server" AutoGenerateColumns="false" AllowSorting="true" CellPadding="5" OnSorting="grdUserPlayers_Sorting"  GridLines="None"
                            HeaderStyle-ForeColor="White" AllowPaging="true" OnPageIndexChanging="grdUserPlayers_PageIndexChanging" CssClass="mGrid"  
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" PagerStyle-ForeColor="White" >
                            <Columns>
                                <asp:HyperLinkField HeaderText="Player Name" SortExpression="Name" DataTextField="Name" DataNavigateUrlFields ="Id" DataNavigateUrlFormatString="~/PlayerView.aspx?id={0}"
                                    ItemStyle-ForeColor="Black"></asp:HyperLinkField>
                                <asp:BoundField HeaderText="Current Team" DataField="TeamName" SortExpression="TeamName" />
                                <asp:BoundField HeaderText="Level" DataField="PlayerLevel" SortExpression="PlayerLevel" />
                                <asp:BoundField HeaderText="Highest Bid" DataField="bid" SortExpression="Bid" />
                                <asp:BoundField HeaderText="Bidding Team" DataField="BiddingTeam" SortExpression="BiddingTeam" />
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td width="500px">
                        <asp:PlaceHolder ID="plcUnsignedPlayers" runat="server" Visible="false">
                        <asp:Label ID="lblUnsignedPlayers" runat="server" Text="Unsigned Players"></asp:Label>
                        <asp:GridView ID="grdUnsignedPlayers" runat="server" AutoGenerateColumns="false" AllowSorting="true" CellPadding="5" OnSorting="grdUnsignedPlayers_Sorting"  GridLines="None"
                            HeaderStyle-ForeColor="White" AllowPaging="true" OnPageIndexChanging="grdUnsignedPlayers_PageIndexChanging" CssClass="mGrid"  
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" PagerStyle-ForeColor="White" >
                            <Columns>
                                <asp:HyperLinkField HeaderText="Player Name" SortExpression="Name" DataTextField="Name" DataNavigateUrlFields ="Id" DataNavigateUrlFormatString="~/PlayerView.aspx?id={0}"
                                    ItemStyle-ForeColor="Black"></asp:HyperLinkField>
                                <asp:BoundField HeaderText="Current Team" DataField="TeamName" SortExpression="TeamName" />
                                <asp:BoundField HeaderText="Level" DataField="PlayerLevel" SortExpression="PlayerLevel" />
                                <asp:BoundField HeaderText="Highest Bid" DataField="bid" SortExpression="Bid" />
                                <asp:BoundField HeaderText="Bidding Team" DataField="BiddingTeam" SortExpression="BiddingTeam" />
                            </Columns>
                        </asp:GridView>
                        </asp:PlaceHolder>
                    </td>
                    <td width="500px" valign="top">
                        <asp:PlaceHolder ID="plcSignedPlayers" runat="server" Visible="false">
                        <asp:Label ID="lblSignedPlayers" runat="server" Text="Signed Players"></asp:Label>
                        <asp:GridView ID="grdSignedPlayers" runat="server" AutoGenerateColumns="false" AllowSorting="true" CellPadding="5" OnSorting="grdSignedPlayers_Sorting" GridLines="None" 
                            HeaderStyle-ForeColor="White" AllowPaging="true" OnPageIndexChanging="grdSignedPlayers_PageIndexChanging" Style="text-wrap:avoid" 
                            CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" PagerStyle-ForeColor="White"  >
                            <Columns>
                                <asp:HyperLinkField HeaderText="Player Name" SortExpression="Name" DataTextField="Name" DataNavigateUrlFields ="Id" DataNavigateUrlFormatString="~/PlayerView.aspx?id={0}"
                                    ItemStyle-ForeColor="Black">
                                    <HeaderStyle Wrap="false" />
                                </asp:HyperLinkField>
                                <asp:BoundField HeaderText="Signing Team" DataField="TeamName" SortExpression="TeamName" >
                                        <HeaderStyle Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Level" DataField="PlayerLevel" SortExpression="PlayerLevel" />
                                <asp:BoundField HeaderText="Contract Value" DataField="Bid" SortExpression="Bid"  >
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Compensation" DataField="Compensation" SortExpression="Compensation" >
                                    <HeaderStyle Wrap="false"  />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        </asp:PlaceHolder>
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </div>
</asp:Content>
