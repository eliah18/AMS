<%@ Page Title="" Language="C#" MasterPageFile="~/admin/parameters/Parameter.master" AutoEventWireup="true" CodeFile="Counters.aspx.cs" Inherits="admin_parameters_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .center {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="page-breadcrumb">
        <div class="row">
            <div class="col-12 d-flex no-block align-items-center">
                <h4 class="page-title">Counters </h4>
                <br />
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">[Add]</asp:LinkButton>
                <div class="ml-auto text-right">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="#">Home</a></li>
                            <li class="breadcrumb-item active" aria-current="page">Library</li>
                        </ol>
                    </nav>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <asp:Panel ID="grdpanel" Visible="true" runat="server">
            <div class="row">
                <div class="col-8">
                    <div class="card">
                        <div class="card-body">

                            <div class="search-container">
                                <asp:Label ID="searchlabel" runat="server" Text="Search by Company Name"></asp:Label>
                                <asp:TextBox runat="server" ID="txtSearch" placeholder="Search" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged" />
                                <%--<asp:LinkButton ID="buttonsearch" runat="server" OnClick="buttonsearch_Click">[Search]</asp:LinkButton>--%>
                            </div>

                            <asp:GridView ID="grdApps" runat="server" CellPadding="1" CssClass="table table-bordered table-striped tablestyle success" EmptyDataRowStyle-CssClass="text-warning text-center" EmptyDataText="No Actuary!" ForeColor="#333333" GridLines="None" HorizontalAlign="Left" AllowPaging="True" OnPageIndexChanging="grdApps_PageIndexChanging">
                                <AlternatingRowStyle BackColor="White" CssClass="altrowstyle" />
                                <EditRowStyle BackColor="#2461BF" />
                                <EmptyDataRowStyle CssClass="text-warning text-center" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" CssClass="header info" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" CssClass="rowstyle" />

                                <Columns>


                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" ID="lnkDiscard" runat="server" CommandArgument="<%# Bind('id') %>" OnClick="linkDiscard">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkedit" runat="server" CommandArgument="<%# Bind('id') %>" OnClick="lnkedit">Edit</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkdelete" runat="server" CommandName="Delete" >Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="usersPanel" Visible="false" runat="server">
            <div class="row">
                <div class="col-md-6">
                    <div class="card">
                        <%--<form class="form-horizontal">--%>
                        <div class="card-body">
                            <h4 class="card-title">Counters</h4>


                            <div class="form-group row">
                                <asp:Label ID="Label2" runat="server" Text="Counter Name" class="col-sm-3 text-right control-label col-form-label"></asp:Label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtSurname" class="form-control" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="txtID" class="form-control" runat="server" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <asp:Label ID="Label1" runat="server" Text="Abbreviation" class="col-sm-3 text-right control-label col-form-label"></asp:Label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtContactDetails" class="form-control" runat="server"></asp:TextBox>

                                </div>
                            </div>
                            <div class="form-group row">
                                <asp:Label ID="Label3" runat="server" Text="Type" class="col-sm-3 text-right control-label col-form-label"></asp:Label>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="cmbCounter" class="form-control" runat="server">
                                        <asp:ListItem Value="Prescribed">Prescribed</asp:ListItem>
                                        <asp:ListItem Value="Other">Other</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TextBox1" class="form-control" runat="server" Visible="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row">
                                <asp:Label ID="Label4" runat="server" Text="Is listed property" class="col-sm-3 text-right control-label col-form-label"></asp:Label>
                                <div class="col-sm-9">


                                    <asp:CheckBox runat="server" ID="chkproperty"></asp:CheckBox>

                                </div>
                            </div>



                        </div>
                        <div class="border-top">
                            <div class="card-body">
                                <div class="center">
                                    <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Save" OnClick="Button1_Click" />
                                    <asp:Button ID="Button2" runat="server" class="btn btn-primary" Visible="false" Text="Save" OnClick="Button2_Click" />

                                    <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="Back" OnClick="Button3_Click" />
                                </div>
                            </div>

                        </div>

                        <%-- </form>--%>
                    </div>
                </div>
            </div>
        </asp:Panel>

    </div>


</asp:Content>

