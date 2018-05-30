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
    public partial class FilterGridProducts : Grid
    {
        private string category, manufacturer;
        private string optionAll = "All";
        private string checkBoxUnchecked = "blank_check_box.png";
        private string checkBoxChecked = "check_box_with_check.png";
        Image selectedCategory, checkBoxCategoryAll; //, selectedManufacturer;
        public FilterGridProducts()
        {
            InitializeComponent();
            category = optionAll;
            manufacturer = optionAll;
            AddCategories();
            //AddManufacturers();
        }

        private void AddCategories()
        {
            Image imageCheckBoxAll = CreateCategoryOptionImage(optionAll);
            selectedCategory = imageCheckBoxAll;
            stackCategories.Children.Add(CreateFilterOption(optionAll, imageCheckBoxAll));

            foreach (string category in Product.categories)
            {
                Image imageCheckBox = CreateCategoryOptionImage(category);
                Grid gridCategory = CreateFilterOption(category, imageCheckBox);

                stackCategories.Children.Add(gridCategory);
            }
        }
        /*
        private void AddManufacturers()
        {        
            Image imageCheckBox = CreateManufacturerOptionImage(optionAll);
            stackManufacturers.Children.Add(CreateFilterOption(optionAll, imageCheckBox));
            foreach (string manufacturer in Data.manufacturers)
            {
                imageCheckBox = CreateManufacturerOptionImage(manufacturer);
                Grid gridManufacturer = CreateFilterOption(manufacturer, imageCheckBox);

                stackManufacturers.Children.Add(gridManufacturer);
            }
        }*/

        private Image CreateCategoryOptionImage(string option)
        {
            Image imageCheckBox = new Image
            {
                Source = checkBoxUnchecked,
                HorizontalOptions = LayoutOptions.End,
                ClassId = option,
            };         

            imageCheckBox.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Clicked_BtnCategoryCheckBox(imageCheckBox))
            });

            return imageCheckBox;
        }

        /*private Image CreateManufacturerOptionImage(string option)
        {
            Image imageCheckBox = new Image
            {
                Source = checkBoxUnchecked,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.End,
                ClassId = option,
            };

            selectedManufacturer = imageCheckBox;

            imageCheckBox.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Clicked_BtnManufacturerCheckBox(imageCheckBox))
            });

            return imageCheckBox;
        }       */ 

        private Grid CreateFilterOption(string option, Image imageCheckBox)
        {
            Grid gridOption = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(70, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) },
                },
            };

            Label labelOption = new Label
            {
                Text = option,
                VerticalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Center,
            };

            gridOption.Children.Add(new Button //just added to fix spacing issues
            {
                InputTransparent = true,
                BackgroundColor = Color.Transparent
            }, 2, 0);            

            if (option == optionAll)
            {
                imageCheckBox.Source = checkBoxChecked;
                checkBoxCategoryAll = imageCheckBox;
            }                

            gridOption.Children.Add(labelOption, 1, 0);
            gridOption.Children.Add(imageCheckBox, 2, 0);

            return gridOption;
        }        

        public void ToggleVisibility()
        {
            if (IsVisible)
                AnimatePopupOn();
            else
            {
                AnimatePopupOff();
            }

        }

        private void OnBoxViewGestureRecognizerTapped(object sender, EventArgs eventArgs)
        {
            ToggleVisibility();
        }

        private async void AnimatePopupOn()
        {
            await Task.WhenAll(
                framePopupFilters.ScaleTo(0.5, 250, Easing.Linear),
                framePopupFilters.FadeTo(0, 250, Easing.Linear),
                boxBackgroundFilters.FadeTo(0, 250, Easing.Linear)
                );

            IsVisible = false;
        }

        private async void AnimatePopupOff()
        {
            IsVisible = true;

            await Task.WhenAll(
                framePopupFilters.ScaleTo(1, 250, Easing.Linear),
                framePopupFilters.FadeTo(1, 250, Easing.Linear),
                boxBackgroundFilters.FadeTo(0.5, 250, Easing.Linear)
                );
        }

        private void Clicked_BtnCategories(object sender, EventArgs eventArgs)
        {            
            if (!stackCategories.IsVisible)
            {
                imageCategoriesDropDownArrow.RotateTo(-180, 250, Easing.Linear);
                stackCategories.IsVisible = true;
            }
            else
            {
                imageCategoriesDropDownArrow.RotateTo(0, 250, Easing.Linear);
                stackCategories.IsVisible = false;
            }
        }

        private void Clicked_BtnCategoryCheckBox(Image checkBox)
        {          
            if ((string)checkBox.Source.GetValue(FileImageSource.FileProperty) == checkBoxUnchecked)
            {
                selectedCategory.Source = checkBoxUnchecked; //uncheck previously selected category

                checkBox.Source = checkBoxChecked;
                category = checkBox.ClassId;
            }
            else
            {
                if (checkBox.ClassId != optionAll)
                {
                    checkBox.Source = checkBoxUnchecked;
                }
                else
                {
                    checkBox.Source = checkBoxChecked;
                }
                
                category = optionAll;
            }

            selectedCategory = checkBox;

            AnimateCheckBox(checkBox);
        }

        /*private void Clicked_BtnManufacturerCheckBox(Image checkBox)
        {
            if ((string)checkBox.Source.GetValue(FileImageSource.FileProperty) == checkBoxUnchecked)
            {
                selectedManufacturer.Source = checkBoxUnchecked;

                checkBox.Source = checkBoxChecked;
                manufacturer = checkBox.ClassId;
            }
            else
            {
                if (checkBox.ClassId != optionAll)
                {
                    checkBox.Source = checkBoxUnchecked;
                }
                else
                {
                    checkBox.Source = checkBoxChecked;
                }

                manufacturer = optionAll;
            }

            selectedManufacturer = checkBox;

            AnimateCheckBox(checkBox);
        }

        private void Clicked_BtnManufacturers(object sender, EventArgs eventArgs)
        {
            if (!stackManufacturers.IsVisible)
            {
                imageManufacturersDropDownArrow.RotateTo(-180, 250, Easing.Linear);
                stackManufacturers.IsVisible = true;
            }
            else
            {
                imageManufacturersDropDownArrow.RotateTo(0, 250, Easing.Linear);
                stackManufacturers.IsVisible = false;
            }
        }*/

        public delegate void OnBtnApplyClickedDelegate(string category);

        public OnBtnApplyClickedDelegate OnBtnApplyClicked { get; set; }

        private async void AnimateCheckBox(Image checkBox)
        {
            await checkBox.ScaleTo(1.2, 50, Easing.CubicIn);
            await checkBox.ScaleTo(1, 50, Easing.CubicIn);
        }        

        private void Clicked_BtnApply(object sender, EventArgs eventArgs)
        {
            ToggleVisibility();
            OnBtnApplyClicked(category);            
        }
    }
}