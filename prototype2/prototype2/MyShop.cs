using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prototype2
{
    public partial class MyShop : Products
    {
        protected override void LayoutPage()
        {
            Title = "My Shop";

            for (int i = 0; i < Data.favourites.Count; i++)
            {
                base.AddProductToStack(Data.favourites[i]);
            }
        }

        protected override void Clicked_btnFavourite(object sender, EventArgs eventArgs)
        {
            Button button = (Button)sender;
            stackProducts.Children.Remove((View)button.Parent.Parent);
            RemoveFavourite(button);
        }
    }
}
