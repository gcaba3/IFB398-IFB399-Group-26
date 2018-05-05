using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prototype2.MyOrders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BillingAddressView : StackLayout
    {
        private bool billingDropDownOpen, deliveryDropDownOpen;
        public BillingAddressView()
        {
            InitializeComponent();

            billingDropDownOpen = false;
        }

        private void Clicked_BillingDropDown(object sender, System.EventArgs e)
        {
            AnimateDropDown();
            

            billingDropDownOpen = !billingDropDownOpen;

            viewBillingAddress.IsVisible = billingDropDownOpen;
        }

        private void AnimateDropDown()
        {
            if (billingDropDownOpen)
                imageBillingArrow.RotateTo(-180, 250, Easing.Linear);
            else
                imageBillingArrow.RotateTo(0, 250, Easing.Linear);
        }
    }
}