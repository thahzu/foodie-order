<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Foodie.User.Default" %>
<%@ Import Namespace="Foodie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- phần ưu đãi -->

  <section class="offer_section layout_padding-bottom">
    <div class="offer_container">
      <div class="container ">
        <div class="row">
            <asp:Repeater ID="rCategory" runat="server">
                <ItemTemplate>
                           <div class="col-md-6  ">
         <div class="box ">
           <div class="img-box">
               <a hidden="Menu.aspx?id=<%# Eval("CategoriId") %>">
                   <img src="<%# Utils.GetImageUrl(Eval("ImageUrl")) %>" alt="">
               </a>
           </div>
           <div class="detail-box">
             <h5>
               <%# Eval("Name") %>
             </h5>
             <h6>
               <span>Giảm 20%</span>
             </h6>
             <a href="Menu.aspx?id=<%# Eval("CategoriId") %>">
               Đặt hàng ngay
                 <svg version="1.1" id="Capa_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" viewBox="0 0 456.029 456.029" style="enable-background:new 0 0 456.029 456.029;" xml:space="preserve">
                 <g>
                   <g>
                     <path d="M345.6,338.862c-29.184,0-53.248,23.552-53.248,53.248c0,29.184,23.552,53.248,53.248,53.248
                  c29.184,0,53.248-23.552,53.248-53.248C398.336,362.926,374.784,338.862,345.6,338.862z" />
                   </g>
                 </g>
                 <g>
                   <g>
                     <path d="M439.296,84.91c-1.024,0-2.56-0.512-4.096-0.512H112.64l-5.12-34.304C104.448,27.566,84.992,10.67,61.952,10.67H20.48
                  C9.216,10.67,0,19.886,0,31.15c0,11.264,9.216,20.48,20.48,20.48h41.472c2.56,0,4.608,2.048,5.12,4.608l31.744,216.064
                  c4.096,27.136,27.648,47.616,55.296,47.616h212.992c26.624,0,49.664-18.944,55.296-45.056l33.28-166.4
                  C457.728,97.71,450.56,86.958,439.296,84.91z" />
                   </g>
                 </g>
                 <g>
                   <g>
                     <path d="M215.04,389.55c-1.024-28.16-24.576-50.688-52.736-50.688c-29.696,1.536-52.224,26.112-51.2,55.296
                  c1.024,28.16,24.064,50.688,52.224,50.688h1.024C193.536,443.31,216.576,418.734,215.04,389.55z" />
                   </g>
                 </g>
                 <g>
                 </g>
                 <g>
                 </g>
                 <g>
                 </g>
                 <g>
                 </g>
                 <g>
                 </g>
                 <g>
                 </g>
                 <g>
                 </g>
                 <g>
                 </g>
                 <g>
                 </g>
                 <g>
                 </g>
                 <g>
                 </g>
                 <g>
                 </g>
                 <g>
                 </g>
                 <g>
                 </g>
                 <g>
                 </g>
               </svg>
             </a>
           </div>
         </div>
       </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
      </div>
    </div>
  </section>

  <!-- kết thúc phần ưu đãi -->

     <!-- phần giới thiệu -->

  <section class="about_section layout_padding_bottom">
    <div class="container  ">

      <div class="row">
        <div class="col-md-6 ">
          <div class="img-box">
            <img src="../TemplateFile/images/about-img.png" alt="">
          </div>
        </div>
        <div class="col-md-6">
          <div class="detail-box">
            <div class="heading_container">
              <h2>
                Chúng Tôi Website Đặt Đồ Ăn Nhanh Trực Tuyến
              </h2>
            </div>
            <p>
Website của chúng tôi cung cấp dịch vụ đặt món ăn nhanh tiện lợi và chất lượng, giúp bạn dễ dàng chọn lựa các món ăn yêu thích từ nhiều nhà hàng đối tác hàng đầu. Chỉ với vài thao tác đơn giản, bạn có thể duyệt qua menu đa dạng bao gồm bánh mì kẹp, pizza, gà rán, sushi, và nhiều món ăn quốc tế khác.            </p>
            <a href="About.aspx">
              Đọc Thêm
            </a>
          </div>
        </div>
      </div>
    </div>
  </section>

  <!-- kết thúc phần giới thiệu -->

    <!-- phần đánh giá khách hàng -->

  <section class="client_section layout_padding-bottom pt-5">
    <div class="container">
      <div class="heading_container heading_center psudo_white_primary mb_45">
        <h2>
          Khách Hàng Nói Gì Về Chúng Tôi
        </h2>
      </div>
      <div class="carousel-wrap row ">
        <div class="owl-carousel client_owl-carousel">
          <div class="item">
            <div class="box">
              <div class="detail-box">
                <p>
                 Trang web này thực sự tiện lợi! Tôi chỉ mất vài phút để chọn món và thanh toán. Giao diện dễ sử dụng, đặc biệt là phần menu có hình ảnh rõ ràng, giúp tôi dễ dàng quyết định mình muốn ăn gì. Thời gian giao hàng nhanh, đồ ăn đến nơi vẫn còn nóng hổi. Tôi rất thích các chương trình khuyến mãi thường xuyên được cập nhật, tiết kiệm được kha khá mà vẫn có bữa ăn ngon. Một điểm cộng lớn nữa là đội ngũ giao hàng rất lịch sự và đúng giờ. Tôi sẽ tiếp tục sử dụng dịch vụ và giới thiệu cho bạn bè!
                </p>
                <h6>
                  Braum
                </h6>
                <p>
                  magna aliqua
                </p>
              </div>
              <div class="img-box">
                <img src="../TemplateFile/images/client1.jpg" alt="" class="box-img">
              </div>
            </div>
          </div>
          <div class="item">
            <div class="box">
              <div class="detail-box">
                <p>
                  Website này là lựa chọn số một của tôi mỗi khi không có thời gian nấu ăn. Tôi rất ấn tượng với sự đa dạng món ăn, từ đồ ăn nhanh như pizza, gà rán đến các món ăn nhẹ lành mạnh. Đặc biệt, tôi có thể dễ dàng theo dõi đơn hàng và biết chính xác khi nào đồ ăn sẽ được giao đến. Phần thanh toán rất linh hoạt, có thể thanh toán online hoặc tiền mặt khi nhận hàng, rất tiện lợi. Giao hàng luôn nhanh chóng và đồ ăn vẫn giữ nguyên chất lượng. Đây chắc chắn là dịch vụ tôi sẽ tiếp tục sử dụng lâu dài!
                </p>
                <h6>
                  Karma
                </h6>
                <p>
                  magna aliqua
                </p>
              </div>
              <div class="img-box">
                <img src="../TemplateFile/images/client2.jpg" alt="" class="box-img">
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>

  <!-- kết thúc phần đánh giá khách hàng -->
</asp:Content>
