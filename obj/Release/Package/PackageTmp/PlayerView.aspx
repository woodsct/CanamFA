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
    </script>
    <div style="background-color:white">
        <div style="margin-left:20px;background-color:white">
            <br />
            Player Bid Window For
            <asp:Label ID="lblPlayerName" runat="server" ></asp:Label>
            <br />
            <br />
            Choose Number of years from the drop down box and enter theAmount per Year in the correct box. When ready click Place Bid.<br />
            <br />
            <br />
            <asp:PlaceHolder ID="plcBidChart" runat="server">
                <asp:Label ID="lblPlayerBids" runat="server" Text="----------------Current Bids----------------"></asp:Label>
                <br />
                <asp:GridView ID="grdPlayerBids" runat="server" AllowPaging="true" CellPadding="5" AutoGenerateColumns="false" OnPageIndexChanging="grdPlayerBids_PageIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText="Team" DataField="TeamName" />
                        <asp:BoundField HeaderText="Years" DataField="Years" />
                        <asp:BoundField HeaderText="No Trade" DataField="NoTrade" />
                        <asp:BoundField HeaderText="Yearly Amount" DataField="Amount" />
                        <asp:BoundField HeaderText="Total Contract Value" DataField="Bid" />
                    </Columns>
                </asp:GridView>
                <br />
                <br />
            </asp:PlaceHolder>
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
            <p>
                <asp:Button ID="btnPlaceBid" runat="server" Text="Place Bid" Height="50px" Width="150px" OnClick="btnPlaceBid_Click" />

                <asp:Button ID="btnGoBack" runat="server" Height="50px" Text="Go Back" Width="150px" OnClick="btnGoBack_Click" />
            </p>
        
            <asp:PlaceHolder ID="plcError" runat="server" Visible="false">
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                <br />
            </asp:PlaceHolder>
            <br />
        </div>
    </div>
</asp:Content>
