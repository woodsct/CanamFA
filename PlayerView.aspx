<%@ Page Language="C#" MasterPageFile="~/CanamSite.Master" AutoEventWireup="true" CodeBehind="PlayerView.aspx.cs" Inherits="CanamLiveFA.PlayerView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="server">
    <script type="text/javascript" language="javascript" >
        function CalculateContract() {
            var amount = document.getElementById('<%=txtAmountPerYear.ClientID%>').value;
            var ddlYears = document.getElementById('<%=ddlNumOfYears.ClientID%>');
            var years = ddlYears.options[ddlYears.selectedIndex].value;
            var noTrade = document.getElementById('<%=chkNoTrade.ClientID%>');

            var value = (parseInt(years) + 1) * amount;
            if (noTrade.checked) {
                value = value * 1.1
            }

            document.getElementById('<%=lblTotalContractValue.ClientID%>').innerHTML = '$' + parseInt(value, 0);
        }

        function TimeRemaining(endTime) {
            var clock = document.getElementById('<%=timeLeft.ClientID%>');

            function updateTime() {
                var timeRemaining = Date.parse(endTime) - Date.now()
                var seconds = Math.floor((timeRemaining / 1000) % 60);
                var minutes = Math.floor((timeRemaining / 1000 / 60) % 60);
                var hours = Math.floor((timeRemaining / (1000 * 60 * 60)) % 24);
                clock.innerHTML = hours + ':' + minutes + ':' + seconds;

                if (seconds < 0) {
                    clock.innerHTML = 'Time has expired';
                    clock.style.color = 'red';
                    clearInterval(timeinterval);
                }
            }

            updateTime();
            var timeinterval = setInterval(updateTime, 1000);
        }
    </script>
    <div style="background-color:white">
        <table>
            <tr>
                <td width="1275px">
                    <div style="margin-left:20px;background-color:white;">
                        <br />
                        Player Bid Window For
                        <asp:Label ID="lblPlayerName" runat="server" ></asp:Label>
                        <br />
                        <br />
                        Choose Number of years from the drop down box and enter theAmount per Year in the correct box. When ready click Place Bid.<br />
                        <br />
                        <br />
                        <asp:Label runat="server" Text="Time Remaining:" />
                        <asp:Label runat="server" ID="timeLeft" Font-Bold="true" />
                        <br />
                        <asp:PlaceHolder ID="plcBidChart" runat="server">
                            <asp:Label ID="lblPlayerBids" runat="server" Text="----------------Current Bids----------------"></asp:Label>
                            <br />
                            <asp:PlaceHolder ID="plcQualified" runat="server" Visible="false">
                                <asp:Label ID="lblQualified" runat="server" Text="PLAYER IS QUALIFIED" ForeColor="Red" Font-Bold="true"></asp:Label>
                            </asp:PlaceHolder>
                            <br />
                            <asp:GridView ID="grdPlayerBids" runat="server" AllowPaging="true" CellPadding="5" AutoGenerateColumns="false" OnPageIndexChanging="grdPlayerBids_PageIndexChanging"
                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <Columns>
                                    <asp:BoundField HeaderText="Team" DataField="TeamName" />
                                    <asp:BoundField HeaderText="Years" DataField="Years" />
                                    <asp:BoundField HeaderText="No Trade" DataField="NoTrade" />
                                    <asp:BoundField HeaderText="Yearly Amount" DataField="Amount" />
                                    <asp:BoundField HeaderText="Total Contract Value" DataField="Bid" />
                                    <asp:TemplateField >
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" CommandArgument='<%# Eval("Team") %>' Text="Remove" OnClick="lnkRemove_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <br />
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="plcBidding" runat="server">
                        <br />
                        <asp:Label ID="lblNumOfYears" runat="server" Text="-----Number Of Years-----"></asp:Label>
                        <asp:DropDownList ID="ddlNumOfYears" runat="server" Height="20px" Width="128px" onchange="javascript:CalculateContract()">
                            <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="2" Value="2" ></asp:ListItem>
                            <asp:ListItem Text="3" Value="3" ></asp:ListItem>
                            <asp:ListItem Text="4" Value="4" ></asp:ListItem>
                            <asp:ListItem Text="5" Value="5" ></asp:ListItem>
                        </asp:DropDownList>
        
                        <asp:Label ID="lblNoTrade" runat="server" Text="-----No Trade (Yes =Check /No = No Check)-----"></asp:Label>
        
                        <asp:CheckBox runat="server" ID="chkNoTrade" onchange="javascript:CalculateContract()" />
                        <asp:Label ID="lblAmountPerYear" runat="server" Text="-----Amount Per Year-----"></asp:Label>    
                        <asp:TextBox ID="txtAmountPerYear" runat="server" Height="16px" onchange="javascript:CalculateContract()" ValidateRequestMode="Enabled" ></asp:TextBox>
                        <asp:Label ID="lblTotalContract" runat="server" Text="----Total Contract Value----"></asp:Label>
                        <asp:Label ID="lblTotalContractValue" runat="server" Text="0"></asp:Label>
                        <br />
                        <asp:Label ID="lblAmountError" ForeColor="Red" Text="Please Enter A Yearly Amount" runat="server" Visible="false" ></asp:Label>
                        <br />
                        </asp:PlaceHolder>
                        <asp:Label runat="server" Text="All bids are in thousands eg. 1000 = 1million" ForeColor="Red"></asp:Label><br />
                        <p>
                            <asp:Button ID="btnPlaceBid" runat="server" Text="Place Bid" CssClass="button" OnClick="btnPlaceBid_Click" />

                            <asp:Button ID="btnGoBack" runat="server" Text="Go Back" CssClass="button" OnClick="btnGoBack_Click" />

                            <asp:Button ID="btnMatch" runat="server" Text="Match" CssClass="button" OnClick="btnMatch_Click" Visible="false" />
                        </p>
        
                        <asp:PlaceHolder ID="plcError" runat="server" Visible="false">
                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                            <br />
                        </asp:PlaceHolder>
                        <br />
                    </div>
                </td>
                <td width="200px">
                    <div style="padding-left:20px;background-color:white;">
                        <asp:Label runat="server">Receive Email Notification</asp:Label>
                        <asp:CheckBox runat="server" ID="chkEmailNotify" OnCheckedChanged="chkEmailNotify_CheckedChanged" AutoPostBack="true" />
                        <br />
                        <br />
                        Yearly Minimums <br />
                        1 year  Majors - 100 <br />
                        1 year Dev - 40 <br />
                        2 years - 500 <br />
                        3 years - 600 <br />
                        4 years - 700 <br />
                        5 years - 800 <br />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
