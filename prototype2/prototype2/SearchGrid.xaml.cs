using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prototype2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchGrid : Grid
    {        
        private Boolean setUp, settingUp;
        private double heightSearchBar;
        private BoxView boxViewBackground;
        private SearchBarProductsTool searchBarProductsTool;

        public SearchGrid()
        {
            InitializeComponent();

            boxViewBackground = boxSearchBackground;

            searchBarProductsTool = new SearchBarProductsTool(stackSearch, boxViewBackground, this);
            gridSearchBar.Children.Add(searchBarProductsTool, 0, 0);

            setUp = false;
            settingUp = false;
        }
        /*public SearchGrid(SearchSuggestionsStack stackSearch, BoxView boxViewBackground)
        {
            InitializeComponent();

            this.stackSearch = stackSearch;
            this.boxViewBackground = boxViewBackground;

            setUp = false;
            settingUp = false;
        }*/

        public void Clicked_ButtonSearchTool()
        {
            if (!settingUp)
            {
                if (!setUp)
                {
                    settingUp = true;
                    heightSearchBar = gridSearchBar.Height;
                    PlaceOutOfView();
                }
                else
                {
                    if (!IsVisible)
                    {
                        IsVisible = true;
                        AnimateIntoView();
                    }
                    else
                    {
                        AnimateAway();
                    }
                }
            }
                                   
        }

        private async void PlaceOutOfView()
        {
            await gridSearchBar.TranslateTo(0, -heightSearchBar, 0, Easing.Linear);

            Opacity = 1;
            InputTransparent = false;
            setUp = true;

            AnimateIntoView();

            settingUp = false;
        }

        private async void AnimateAway()
        {
            await Task.WhenAll(
                gridSearchBar.TranslateTo(0, -heightSearchBar, 200, Easing.Linear),
                boxSearchBackground.FadeTo(0, 200, Easing.Linear)
                );
            
            IsVisible = false;
        }

        private async void AnimateIntoView()
        {
            await Task.WhenAll(
                gridSearchBar.TranslateTo(0, 0, 200, Easing.Linear),
                boxSearchBackground.FadeTo(0.5, 200, Easing.Linear)
                );
            searchBarProductsTool.Focus();
        }

        public void Searched_SearchBarProducts(List<int> suggestions)
        {
            Navigation.PushAsync(new AccountPage(suggestions));
        }

        private void OnBoxViewGestureRecognizerTapped(object sender, EventArgs eventArgs)
        {
            AnimateAway();
        }        

        private async void AnimateBackgroundToVisible()
        {
            boxViewBackground.IsVisible = true;

            await boxViewBackground.FadeTo(0.5, 250, Easing.Linear);
        }

        private async void AnimateBackgroundToInvisible()
        {
            await boxViewBackground.FadeTo(0, 250, Easing.Linear);

            boxViewBackground.IsVisible = false;
        }
    }
}