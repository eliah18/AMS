<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Calc.aspx.cs" Inherits="admin_parameters_Calc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label1" runat="server" Text="Value1" ForeColor="Blue"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxV1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label2" runat="server" Text="Value2" ForeColor="Blue"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxV2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="ButtonAdd" runat="server" OnClick="ButtonAdd_Click" Text="+" Width="41px" />
                    <asp:Button ID="ButtonSub" runat="server" Text="-" Width="41px" />
                    <asp:Button ID="ButtonMul" runat="server" Text="*" Width="41px" />
                    <asp:Button ID="ButtonDiv" runat="server" Text="/" Width="41px" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label3" runat="server" Text="Result" ForeColor="Blue"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxV3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
