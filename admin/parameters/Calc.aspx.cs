using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_parameters_Calc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        int add = Int32.Parse(TextBoxV1.Text) + Int32.Parse(TextBoxV2.Text);
        TextBoxV3.Text = Convert.ToString(add);
    }

    protected void ButtonSub_Click(object sender, EventArgs e)
    {
        int sub = Int32.Parse(TextBoxV1.Text) - Int32.Parse(TextBoxV2.Text);
        TextBoxV3.Text = Convert.ToString(sub);
    }

    protected void ButtonMul_Click(object sender, EventArgs e)
    {
        int mul = Int32.Parse(TextBoxV1.Text) * Int32.Parse(TextBoxV2.Text);
        TextBoxV3.Text = Convert.ToString(mul);
    }

    protected void ButtonDiv_Click(object sender, EventArgs e)
    {
        try
        {
            int div = Int32.Parse(TextBox1.Text) / Int32.Parse(TextBox2.Text);
            TextBox3.Text = Convert.ToString(div);
        }
        catch
        {
            TextBox3.Text = Convert.ToString("You Cannot Divide By 0");
        }

    }
}