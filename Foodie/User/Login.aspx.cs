using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;

namespace Foodie.User
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Nếu người dùng đã đăng nhập, chuyển hướng đến trang chính
            if (Session["userId"] != null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == "Admin" && txtPassword.Text.Trim() == "123")
            {
                Session["admin"] = txtUsername.Text.Trim();
                Response.Redirect("../Admin/Dashboard.aspx");
            }
            else
            {
                con = new SqlConnection(Connection.GetConnectionString());
                cmd = new SqlCommand("User_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "SELECT4LOGIN");
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());

                // So sánh trực tiếp mật khẩu
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());

                cmd.CommandType = CommandType.StoredProcedure;
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);

                // Debug: Kiểm tra giá trị của Username và Password
                Response.Write("Username: " + txtUsername.Text.Trim());
                Response.Write("Password: " + txtPassword.Text.Trim());

                // Kiểm tra kết quả đăng nhập
                if (dt.Rows.Count == 1)
                {
                    // Đăng nhập thành công
                    Session["username"] = dt.Rows[0]["Username"].ToString();
                    Session["userId"] = dt.Rows[0]["UserId"].ToString();
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    // Debug: Thông báo lỗi nếu không có kết quả
                    Response.Write("No matching user found.");

                    // Đăng nhập thất bại
                    lblMsg.Visible = true;
                    lblMsg.Text = "Invalid Credentials.";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
        }
    }
}
