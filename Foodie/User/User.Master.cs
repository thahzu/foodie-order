using System;
using System.Web;
using System.Web.UI;

namespace Foodie.User
{
    public partial class User : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Kiểm tra xem trang hiện tại có phải là Default.aspx hay không
            if (!Request.Url.AbsoluteUri.ToLower().Contains("default.aspx"))
            {
                // Nếu không phải, thêm class 'sub_page' vào form
                form1.Attributes.Add("class", "sub_page");
            }
            else
            {
                // Nếu là Default.aspx, loại bỏ class 'sub_page' và load SliderUserControl
                form1.Attributes.Remove("class");

                // Load SliderUserControl và thêm vào pnlSliderUC
                Control siderUserControl = Page.LoadControl("SliderUserConTrol.ascx");
                pnlSliderUC.Controls.Add(siderUserControl);
            }

            // Kiểm tra trạng thái phiên đăng nhập và cập nhật nút login/logout
            if (Session["userId"] != null)
            {
                lbLoginOrLogout.Text = "Logout";
                Utils utils = new Utils();
                Session["cartCount"] = utils.cartCount(Convert.ToInt32(Session["userId"])).ToString();
            }
            else
            {
                lbLoginOrLogout.Text = "Login";
                Session["cartCount"] = "0";
            }
        }

        protected void lbLoginOrLogout_Click(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Session.Abandon();
                Response.Redirect("Login.aspx");
            }
        }

        protected void lblRegisterOrProfile_Click(object sender, EventArgs e)
        {
            if (Session["userId"] != null)
            {
                lbRegisterOrProfile.ToolTip = "User Profile"; 
                Response.Redirect("Profile.aspx");
            }
            else
            {
                lbRegisterOrProfile.ToolTip = "User Registration";
                Response.Redirect("Registration.aspx");

            }
        }
    }
}
