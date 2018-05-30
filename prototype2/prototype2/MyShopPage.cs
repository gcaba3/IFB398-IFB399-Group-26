using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prototype2
{
    public partial class MyShopPage : ProductsPage
    {
        protected override void LayoutPage()
        {
            Title = "My Shop";
            NavigationBar.ChangeMyShopTabColor();

            for (int i = 0; i < Data.favourites.Count; i++)
            {
                base.AddProductToStack(Data.favourites[i]);
            }
        }

        protected override void Clicked_btnFavourite(object sender, EventArgs eventArgs)
        {
            Button button = (Button)sender;
            AnimateFavouriteRemoval((View)button.Parent.Parent, (Button) sender);            
            RemoveFavourite(button);
        }

        private async void AnimateFavouriteRemoval(View view, Button button)
        {
            AnimateFavouriteButton(button);
            await view.FadeTo(0, 500, Easing.Linear);
            stackProducts.Children.Remove(view);
        }
    }
}
