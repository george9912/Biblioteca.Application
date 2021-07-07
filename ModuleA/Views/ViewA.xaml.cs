using System;
using System.Windows.Controls;

namespace ModuleA.Views
{
    /// <summary>
    /// Interaction logic for ViewA
    /// </summary>
    public partial class ViewA : UserControl
    {
        public ViewA()
        {
            InitializeComponent();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            IsEnabled = false;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            IsEnabled = true;
        }
    }
}
