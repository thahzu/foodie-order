<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Foodie.User.Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script>
          /*Để thông báo biến mất sau một khoảng thời gian*/
          window.onload = function () {
              var seconds = 5;
              setTimeout(function () {
                  document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
          }, seconds * 1000);
          };
      </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <!-- Phần đặt bàn -->
  <section class="book_section layout_padding">
    <div class="container">
      <div class="heading_container">
          <div class="align-self-end">
              <asp:Label ID="lblMsg" runat="server"></asp:Label>
          </div>
        <h2>
          Đặt Bàn
        </h2>
      </div>
      <div class="row">
        <div class="col-md-6">
          <div class="form_container">
            <form action="">
              <div>
                <input type="text" class="form-control" placeholder="Tên Của Bạn" />
              </div>
              <div>
                <input type="text" class="form-control" placeholder="Số Điện Thoại" />
              </div>
              <div>
                <input type="email" class="form-control" placeholder="Email Của Bạn" />
              </div>
              <div>
                <select class="form-control nice-select wide">
                  <option value="" disabled selected>
                    Số người?
                  </option>
                  <option value="">
                    2
                  </option>
                  <option value="">
                    3
                  </option>
                  <option value="">
                    4
                  </option>
                  <option value="">
                    5
                  </option>
                </select>
              </div>
              <div>
                <input type="date" class="form-control">
              </div>
              <div class="btn_box">
                <button>
                  Đặt Ngay
                </button>
              </div>
            </form>
          </div>
        </div>
        <div class="col-md-6">
          <div class="map_container ">
            <div id="googleMap"></div>
          </div>
        </div>
      </div>
    </div>
  </section>
  <!-- Kết thúc phần đặt bàn -->
</asp:Content>
