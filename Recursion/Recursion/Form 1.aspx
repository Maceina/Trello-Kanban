<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form 1.aspx.cs" Inherits="Form_1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>1 LD - 12</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 838px">
    
        <asp:Label ID="NameLabel" runat="server" Text="Audrius Maceina"></asp:Label>
        <br />
        <asp:Label ID="LabLabel" runat="server" Text="1 LD - 12"></asp:Label>
        <br />
        <asp:ValidationSummary ID="ErrorSummary" runat="server" ForeColor="Red" />
        <br />
        <asp:Label ID="DataFileLabel" runat="server" Text="Data file"></asp:Label>
        <br />
        <asp:Button ID="LoadButton" runat="server" OnClick="LoadButton_Click" Text="Load data" />
&nbsp;<asp:CustomValidator ID="DataChecker" runat="server" ErrorMessage="You must load data first! No loaded data found!" ForeColor="Red">*</asp:CustomValidator>
&nbsp;<asp:Label ID="OrLabel" runat="server" Text="or"></asp:Label>
&nbsp;
        <asp:Button ID="ShowDataButton" runat="server" OnClick="ShowDataButton_Click" 
                    Text="Show data" />
&nbsp;<br />
        <asp:Label ID="DataLoadedLabel" runat="server" Font-Italic="True" ForeColor="Blue" 
                   Text="Data loaded successfully, you may view the data." 
                   Visible="False"></asp:Label>
        <br />
        <br />
        <asp:Label ID="ProcessDataLabel" runat="server" Text="Process data"></asp:Label>
        <br />
        <asp:Button ID="ProceedButton" runat="server" OnClick="ProceedButton_Click" 
                    Text="Proceed" />
        <br />
        <asp:Label ID="SuccessLabel" runat="server" Font-Italic="True" ForeColor="Blue" 
                   Text="Data processed successfully, you may view the results." 
                   Visible="False"></asp:Label>
        <br />
        <br />
        <asp:Label ID="ResultsLabel" runat="server" Text="Results"></asp:Label>
        <br />
        <asp:Button ID="ResultsButton" runat="server" OnClick="ResultsButton_Click" 
                    Text="Show results" />
        <br />
        <br />
        <asp:Table ID="TableData" runat="server" BorderColor="Black" BorderStyle="Solid" 
                   Caption="Your Sudoku" CaptionAlign="Top" ForeColor="Black" 
                   GridLines="Both" Height="250px" HorizontalAlign="Left" Width="400px">
        </asp:Table>
        <br />
    
    </div>
    </form>
</body>
</html>
