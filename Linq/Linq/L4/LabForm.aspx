<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabForm.aspx.cs" Inherits="LabForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>4LD - 12</title>
    <style type="text/css">
        .body-style {
            height: 650px;
            width: 1200px;
            background-image:url('App_Images/bck.png');
            background-repeat:no-repeat;
            background-attachment:fixed;
            background-size: cover;
        }
        .mainStyle {
            font-family: Arial, Helvetica, sans-serif;
            font-size: medium;
            font-weight: bold;
            font-style: italic;
            font-variant: normal;
            text-transform: uppercase;
            text-align: center;
            vertical-align: middle;
            height: 650px;
            width: 1200px;
        }
        .infoLabelStyle {
            font-family: Arial, Helvetica, sans-serif;
            font-size: medium;
            font-weight: bold;
            font-style: italic;
            color: #008080;
            vertical-align: middle;
            text-align: center;
        }
        .buttonStyle {
            font-style: italic;
            border-style: outset;
            color: cadetblue;
        }
        .informationStyle {
            font-style: italic;
            color: #008080;
            vertical-align: middle;
            text-align: center;
            font-size: small;
            font-weight: normal;
        }
        .tableStyle {
            font-family: Arial, Helvetica, sans-serif;
            font-style: italic;
            font-variant: normal;
            color: #FFFFFF;
            vertical-align: middle;
            background-color: #5F9EA0;
            border-color: #008080;
            border-style: outset;
            border-collapse: separate;
            font-weight: normal;
        }
        .errorBoxStyle {
            font-family: Arial, Helvetica, sans-serif;
            font-size: medium;
            font-style: italic;
            color: #006666;
            vertical-align: middle;
            text-align: center;
        }
    </style>
</head>
<body class="body-style" >
    <form id="form1" runat="server">
        <div class="mainStyle">
            <br />
            <br />
            <asp:Label ID="LabelInfo" runat="server" CssClass="infoLabelStyle" Text="Catalogue of e-mails sent between servers"></asp:Label>
            <br />
            <br />
            <asp:Label ID="LabelText" runat="server" CssClass="informationStyle" Text="Find servers &amp; time of the day, when 0 bytes were transferred between servers"></asp:Label>
            <br />
            <br />
            <asp:FileUpload ID="UploadData" runat="server" AllowMultiple="True" CssClass="buttonStyle" />
            <br />
            <br />
            &nbsp;<asp:Button ID="ButtonProceed" runat="server" CssClass="buttonStyle" Text="Proceed" OnClick="ButtonProceed_Click" />
            <asp:CustomValidator ID="ValidatorProceed" runat="server" ErrorMessage="." ForeColor="#006666">*</asp:CustomValidator>
            &nbsp;<asp:Button ID="ButtonData" runat="server" CssClass="buttonStyle" OnClick="ButtonData_Click" Text="Data" />
            <asp:CustomValidator ID="ValidatorData" runat="server" ForeColor="#006666">*</asp:CustomValidator>
&nbsp;<asp:Button ID="ButtonView" runat="server" CssClass="buttonStyle" Text="View" OnClick="ButtonView_Click" />
            <asp:CustomValidator ID="ValidatorView" runat="server" ErrorMessage="." ForeColor="#006666">*</asp:CustomValidator>
&nbsp;<asp:Button ID="ButtonExport" runat="server" CssClass="buttonStyle" Text="Export to file" OnClick="ButtonExport_Click" />
            <asp:CustomValidator ID="ValidatorExport" runat="server" ErrorMessage="." ForeColor="#006666">*</asp:CustomValidator>
            <br />
            <br />
            <asp:Label ID="LabelSuccess" runat="server" CssClass="informationStyle" EnableViewState="False" Visible="False"></asp:Label>
            <br />
            <br />
            <asp:ValidationSummary ID="ValidationInfo" runat="server" CssClass="errorBoxStyle" EnableViewState="False" Visible="False" HeaderText="Program error:" DisplayMode="List" />
            <br />
            <asp:Table ID="TableResults" runat="server" CssClass="tableStyle" EnableViewState="False" HorizontalAlign="Center">
            </asp:Table>
        </div>
    </form>
</body>
</html>
