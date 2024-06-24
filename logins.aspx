<%@ Page Title="" Language="C#" MasterPageFile="~/loginsystem.master" AutoEventWireup="true" CodeFile="logins.aspx.cs" Inherits="logins" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
     <h2>Sign In </h2> 
    <div class="main">
        <div class="form-left-to-w3l">
            <asp:TextBox ID="txtUsername" placeholder="Username" runat="server" Width="350" ></asp:TextBox>
            <asp:TextBox ID="txtusertype" placeholder="Username" runat="server" Width="350"  Visible="false"></asp:TextBox>
            </div>
         <div class="form-left-to-w3l">
            <asp:TextBox ID="txtPassword" placeholder="Password" TextMode="Password" runat="server" Width="350" ></asp:TextBox>
            </div>
         <div class="btnn">
             <asp:Button ID="signin" type="submit" runat="server" Text="Login" OnClick="signin_Click" />
              
             </div>
        
        
        </div>
</asp:Content>

