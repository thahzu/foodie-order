using Foodie.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Foodie.User
{
    public partial class Cart : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        decimal grandTotal = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userId"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    getCartItem();
                }
            }
        }

        void getCartItem()
        {
            using (con = new SqlConnection(Connection.GetConnectionString()))
            {
                using (cmd = new SqlCommand("Cart_Crud", con))
                {
                    cmd.Parameters.AddWithValue("@Action", "SELECT");
                    cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
                    cmd.CommandType = CommandType.StoredProcedure;

                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        rCartItem.FooterTemplate = null;
                        rCartItem.FooterTemplate = new CustomTemplate(ListItemType.Footer);
                    }

                    rCartItem.DataSource = dt;
                    rCartItem.DataBind();
                }
            }
        }

        protected void rCartItem_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Utils utils = new Utils();

            if (e.CommandName == "remove")
            {
                using (SqlConnection con = new SqlConnection(Connection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("Cart_Crud", con))
                    {
                        cmd.Parameters.AddWithValue("@Action", "DELETE");
                        cmd.Parameters.AddWithValue("@ProductId", e.CommandArgument);
                        cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
                        cmd.CommandType = CommandType.StoredProcedure;

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            getCartItem();

                            if (Session["userId"] != null)
                            {
                                Session["cartCount"] = utils.cartCount(Convert.ToInt32(Session["userId"]));
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Web.HttpContext.Current.Response.Write("<script>alert('Error - " + ex.Message + "');</script>");
                        }
                    }
                }
            }

            if (e.CommandName == "updateCart")
            {
                bool isCartUpdated = false;

                for (int item = 0; item < rCartItem.Items.Count; item++)
                {
                    if (rCartItem.Items[item].ItemType == ListItemType.Item || rCartItem.Items[item].ItemType == ListItemType.AlternatingItem)
                    {
                        TextBox quantity = rCartItem.Items[item].FindControl("txtQuantity") as TextBox;
                        HiddenField _productId = rCartItem.Items[item].FindControl("hdnProductId") as HiddenField;
                        HiddenField _quantity = rCartItem.Items[item].FindControl("hdnQuantity") as HiddenField;

                        int quantityFromCart = Convert.ToInt32(quantity.Text);
                        int ProductId = Convert.ToInt32(_productId.Value);
                        int quantityFromDB = Convert.ToInt32(_quantity.Value);
                        bool isTrue = false;
                        int updatedQuantity = 1;

                        if (quantityFromCart > quantityFromDB)
                        {
                            updatedQuantity = quantityFromCart;
                            isTrue = true;
                        }
                        else if (quantityFromCart < quantityFromDB)
                        {
                            updatedQuantity = quantityFromCart;
                            isTrue = true;
                        }

                        if (isTrue)
                        {
                            isCartUpdated = utils.updateCartQuantity(updatedQuantity, ProductId, Convert.ToInt32(Session["userId"]));
                        }
                    }
                }

                getCartItem();
            }

            if (e.CommandName == "checkout")
            {
                bool isTrue = false;
                string pName = string.Empty;

                for (int item = 0; item < rCartItem.Items.Count; item++)
                {
                    if (rCartItem.Items[item].ItemType == ListItemType.Item || rCartItem.Items[item].ItemType == ListItemType.AlternatingItem)
                    {
                        HiddenField _productId = rCartItem.Items[item].FindControl("hdnProductId") as HiddenField;
                        HiddenField _cartQuantity = rCartItem.Items[item].FindControl("hdnQuantity") as HiddenField;
                        HiddenField _productQuantity = rCartItem.Items[item].FindControl("hdnPrdQuantity") as HiddenField;
                        Label productName = rCartItem.Items[item].FindControl("lblName") as Label;

                        int cartQuantity = Convert.ToInt32(_cartQuantity.Value);
                        int productId = Convert.ToInt32(_productId.Value);
                        int productQuantity = Convert.ToInt32(_productQuantity.Value);

                        if (productQuantity >= cartQuantity && productQuantity > 2)
                        {
                            isTrue = true;
                        }
                        else
                        {
                            isTrue = false;
                            pName = productName.Text;
                            break;
                        }
                    }
                }

                if (isTrue)
                {
                    Response.Redirect("Payment.aspx");
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Sản phẩm <b>'" + pName + "'</b> hiện không còn đủ số lượng trong kho. Vui lòng cập nhật lại giỏ hàng.";
                    lblMsg.CssClass = "alert alert-warning";
                }
            }
        }

        protected void rCartItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label totalPrice = e.Item.FindControl("lblTotalPrice") as Label;
                Label productPrice = e.Item.FindControl("lblPrice") as Label;
                TextBox quantity = e.Item.FindControl("txtQuantity") as TextBox;

                // Chuyển đổi Price và Quantity sang kiểu decimal và tính tổng giá tiền
                decimal price = Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Price"));
                int qty = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Quantity"));

                decimal calTotalPrice = price * qty;
                totalPrice.Text = String.Format(new System.Globalization.CultureInfo("vi-VN"), "{0:C0}", calTotalPrice);

                grandTotal += calTotalPrice;
            }

            // Cập nhật tổng giá trị vào session
            Session["grandTotalPrice"] = String.Format(new System.Globalization.CultureInfo("vi-VN"), "{0:C0}", grandTotal);
        }


        private sealed class CustomTemplate : ITemplate
        {
            private ListItemType ListItemType { get; set; }

            public CustomTemplate(ListItemType type)
            {
                ListItemType = type;
            }

            public void InstantiateIn(Control container)
            {
                if (ListItemType == ListItemType.Footer)
                {
                    var footer = new LiteralControl("<tr><td colspan='5'><b>Giỏ hàng của bạn trống.</b><a href='Menu.aspx' class='badge badge-info ml-2'>Tiếp tục mua sắm</a></td></tr></tbody></table>");
                    container.Controls.Add(footer);
                }
            }
        }
    }
}
