<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabForm.aspx.cs" Inherits="LabForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>3 LD - 12</title>
    <style type="text/css">        
        .buttonStyle {
            font-style: italic;
            color: #660033;
            border-style: outset;
        }        
        .tableStyle {
            border-collapse: separate;
            caption-side: top;
            border: thin outset #000000;
            vertical-align: middle;
            font-weight: normal;
            font-variant: normal;
            font-style: normal;
            color: #000000;
            font-family: Cambria, Cochin, Georgia, Times, "Times New Roman", serif;
        }
        .infoLabelStyle {
            font-style: italic;
            font-weight: bold;
            color: #660033;
            font-family: Cambria, Cochin, Georgia, Times, "Times New Roman", serif;
        }
        .labelStyle {
            color: #000066;
            font-weight: bold;
            font-style: italic;
            text-align: center;
            font-family: Cambria, Cochin, Georgia, Times, "Times New Roman", serif;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 1784px; text-align: center;">
            <asp:Label ID="InfoLabel" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Large" Font-Strikeout="False" Height="50px" Text="Modules Lists' Overview" Width="500px" CssClass="labelStyle"></asp:Label>
            <br />
            <asp:Image ID="ImageLeft" runat="server" Height="100px" ImageAlign="Left" ImageUrl="~/Images/WikiProject_Council_project_list_icon.svg.png" Width="100px" EnableTheming="True" />
            <asp:FileUpload ID="DataUpload" runat="server" CssClass="buttonStyle" AllowMultiple="True" />
&nbsp;<asp:CustomValidator ID="DataValidator" runat="server" ErrorMessage="Loaded data is empty!" Font-Bold="False" Font-Italic="True" ForeColor="#333399">*</asp:CustomValidator>
&nbsp;<asp:Button ID="ProceedButton" runat="server" BorderStyle="Outset" Font-Italic="True" ForeColor="#660033" Text="Proceed data" OnClick="ProceedButton_Click" CssClass="buttonStyle" />
&nbsp;<asp:CustomValidator ID="ProceedValidator" runat="server" ErrorMessage="You must proceed data first!" Font-Bold="False" Font-Italic="True" ForeColor="#333399">*</asp:CustomValidator>
            &nbsp;<asp:Button ID="ShowListsButton" runat="server" BorderStyle="Outset" Font-Italic="True" ForeColor="#660033" Text="Show formed lists" OnClick="ShowListsButton_Click" CssClass="buttonStyle" />
            <asp:Image ID="ImageRight" runat="server" Height="100px" ImageAlign="Right" ImageUrl="~/Images/WikiProject_Council_project_list_icon.svg.png" Width="100px" />
            <br />
            <br />
            <asp:ValidationSummary ID="ValidationSummary" runat="server" Font-Italic="True" Width="1000px" style="text-align: center" CssClass="infoLabelStyle" DisplayMode="List" Font-Bold="True" />
            <br />
            <asp:Label ID="SuccessLabel" runat="server" Font-Italic="True" Visible="False" CssClass="infoLabelStyle" Font-Bold="True"></asp:Label>
            <br />
            <br />
            <asp:Table ID="ChosenTable" runat="server" Visible="False" BorderColor="Black" BorderStyle="Outset" Caption="Chosen Modules" CaptionAlign="Top" GridLines="Both" HorizontalAlign="Center" CssClass="tableStyle" EnableViewState="False">
            </asp:Table>
            &nbsp;&nbsp;&nbsp;
            <asp:Table ID="UnchosenTable" runat="server" Visible="False" BorderColor="Black" BorderStyle="Outset" Caption="Unchosen Modules" CaptionAlign="Top" GridLines="Both" HorizontalAlign="Center" EnableViewState="False" CssClass="tableStyle">
            </asp:Table>
            <br />
            <br />
            <asp:Label ID="StudentLabel" runat="server" Font-Bold="True" Font-Italic="True" Height="30px" Text="Student who chose most modules" CssClass="labelStyle" Font-Size="Medium"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <br />
            <asp:Label ID="StudentInfoLabel" runat="server" Visible="False" Font-Bold="True" Font-Italic="True" EnableViewState="False" CssClass="infoLabelStyle"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <br />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="ViewStudentButton" runat="server" BorderStyle="Outset" Font-Italic="True" ForeColor="#660033" Text="View student" OnClick="ViewStudentButton_Click" CssClass="buttonStyle" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <br />
            <asp:Label ID="CreateLabel" runat="server" Font-Bold="True" Font-Italic="True" Height="30px" Text="Create a list of chosen modules by specific student" CssClass="labelStyle" Font-Size="Medium"></asp:Label>
            <br />
&nbsp;
            <asp:TextBox ID="InsertInfoBox" runat="server" Font-Italic="True" ForeColor="#660033" Width="250px" ></asp:TextBox>
            <asp:CustomValidator ID="InputValidator" runat="server" ErrorMessage="Data list doesn't contain such student or your input is invalid." Font-Italic="True" ForeColor="#660033">*</asp:CustomValidator>
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="CreateButton" runat="server" BorderStyle="Outset" Font-Italic="True" ForeColor="#660033" Text="Create" OnClick="CreateButton_Click" CssClass="buttonStyle" />
            <br />
            <br />
            <asp:Table ID="StudentTable" runat="server" BorderColor="Black" BorderStyle="Outset" GridLines="Both" Visible="False" HorizontalAlign="Center" EnableViewState="False" CssClass="tableStyle">
            </asp:Table>
            <br />
            <asp:Label ID="OutputFileLabel" runat="server" CssClass="labelStyle" Text="Export results to file" Font-Size="Medium"></asp:Label>
            <br />
            <br />
            <asp:Button ID="ExportButton" runat="server" CssClass="buttonStyle" Text="Export" OnClick="ExportButton_Click" />
            <br />
            <br />
            <asp:Label ID="ExportLabel" runat="server" CssClass="infoLabelStyle" EnableViewState="False" Text="Results exported successfully!" Visible="False"></asp:Label>
        </div>
    </form>
</body>
</html>
