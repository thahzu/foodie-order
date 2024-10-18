<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Foodie.Admin.Users" %>
<%@ Import Namespace="Foodie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        /* Để thông báo biến mất sau một khoảng thời gian */
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="pcoded-inner-content pt-0">
        <div class="align-align-self-end">
            <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
        </div>

        <div class="main-body">
            <div class="page-wrapper">
                <div class="page-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="card">
                                <div class="card-header"></div>
                                <div class="card-block">
                                    <div class="row">
                                        <div class="col-12 mobile-inputs">
                                            <h4 class="sub-title">Danh sách người dùng</h4>
                                            <div class="card-block table-border-style">
                                                <div class="table-reponsive">
                                                    <asp:Repeater ID="rUsers" runat="server" OnItemCommand="rUsers_ItemCommand">
                                                        <HeaderTemplate>
                                                            <table class="table data-table-export table-hover nowrap">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="table-plus">STT</th>
                                                                        <th>Họ và Tên</th>
                                                                        <th>Tên Đăng Nhập</th>
                                                                        <th>Email</th>
                                                                        <th>Ngày Tham Gia</th>
                                                                        <th class="datable-nosort">Xóa</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="table-plus"><%# Eval("SrNo") %></td>
                                                                <td><%# Eval("Name") %></td>
                                                                <td><%# Eval("Username") %></td>
                                                                <td><%# Eval("Email") %></td>
                                                                <td><%# Eval("CreateDate")%> </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkDelete" Text="Xóa" runat="server" CommandName="delete"
                                                                        CssClass="badge bg-danger" CommandArgument='<%# Eval("UserId") %>'
                                                                        OnClientClick="return confirm('Bạn có chắc chắn muốn xóa người dùng này?');">
                                                                        <i class="ti-trash"></i>
                                                                    </asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody>
                                                            </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Đóng div row -->
                                </div>
                                <!-- Đóng div card-block -->
                            </div>
                            <!-- Đóng div card -->
                        </div>
                        <!-- Đóng div col-sm-12 -->
                    </div>
                    <!-- Đóng div row -->
                </div>
                <!-- Đóng div page-body -->
            </div>
            <!-- Đóng div page-wrapper -->
        </div>
        <!-- Đóng div main-body -->
    </div>

</asp:Content>
