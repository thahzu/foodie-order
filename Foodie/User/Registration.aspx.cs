using System;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Foodie.User
{
    public partial class Registration : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out int userId))
                {
                    getUserDetails(userId);
                }
                else if (Session["userId"] != null)
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        private void getUserDetails(int userId)
        {
            using (SqlConnection con = new SqlConnection(Connection.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("User_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "SELECT4PROFILE");
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    DataRow row = dt.Rows[0];
                    txtName.Text = row["Name"].ToString();
                    txtUsername.Text = row["Username"].ToString();
                    txtMobile.Text = row["Mobile"].ToString();
                    txtEmail.Text = row["Email"].ToString();
                    txtAddress.Text = row["Address"].ToString();
                    txtPostCode.Text = row["PostCode"].ToString();

                    // Xử lý hình ảnh
                    string imageUrl = row["ImageUrl"]?.ToString();
                    imgUser.ImageUrl = string.IsNullOrEmpty(imageUrl) ? "../Images/No_image.png" : "../" + imageUrl;
                    imgUser.Height = 200;
                    imgUser.Width = 200;

                    txtPassword.TextMode = TextBoxMode.SingleLine;
                    txtPassword.ReadOnly = true;
                    txtPassword.Text = row["Password"].ToString();
                }

                lblHeaderMsg.Text = "<h2>Edit Profile</h2>";
                btnRegister.Text = "Update";
                lblAlreadyUser.Text = "";
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string actionName, imagePath = string.Empty, fileExtension = string.Empty;
            bool isValidToExecute = true;

            int userId = 0;
            if (Request.QueryString["id"] != null)
            {
                int.TryParse(Request.QueryString["id"], out userId);
            }

            using (SqlConnection con = new SqlConnection(Connection.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("User_Crud", con);
                cmd.Parameters.AddWithValue("@Action", userId == 0 ? "INSERT" : "UPDATE");
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@PostCode", txtPostCode.Text.Trim());

                // Lưu mật khẩu trực tiếp (không cần mã hóa)
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());

                // Xử lý ảnh tải lên
                if (fuUserImage.HasFile)
                {
                    if (Utils.IsValidExtension(fuUserImage.FileName))
                    {
                        Guid obj = Guid.NewGuid();
                        fileExtension = Path.GetExtension(fuUserImage.FileName);
                        imagePath = "Images/User/" + obj.ToString() + fileExtension;
                        fuUserImage.PostedFile.SaveAs(Server.MapPath("~/Images/User/") + obj.ToString() + fileExtension);
                        cmd.Parameters.AddWithValue("@ImageUrl", imagePath);
                    }
                    else
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Please select a JPG, JPEG, or PNG image.";
                        lblMsg.CssClass = "alert alert-danger";
                        isValidToExecute = false;
                    }
                }
                else if (userId != 0)
                {
                    // Nếu không có ảnh mới, giữ lại đường dẫn ảnh cũ
                    cmd.Parameters.AddWithValue("@ImageUrl", imgUser.ImageUrl.Replace("../", ""));
                }

                if (isValidToExecute)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        actionName = userId == 0 ? "registration is successful! <b><a href='Login.aspx'>Click here</a></b> to log in." :
                                    "details updated successfully! <b><a href='Profile.aspx'>Check here</a></b>";
                        lblMsg.Visible = true;
                        lblMsg.Text = "<b>" + txtUsername.Text.Trim() + "</b> " + actionName;
                        lblMsg.CssClass = "alert alert-success";

                        if (userId != 0)
                        {
                            Response.AddHeader("REFRESH", "1;URL=Profile.aspx");
                        }
                        Clear();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "<b>" + txtUsername.Text.Trim() + "</b> username already exists, try a new one.";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Error - " + ex.Message;
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Error - " + ex.Message;
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }
            }
        }

        private void Clear()
        {
            txtName.Text = "";
            txtUsername.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtPostCode.Text = "";
            txtPassword.Text = "";
        }
    }
}
