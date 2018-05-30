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
    public partial class EditQuotePopup : Grid
    {
        public bool newProduct;
        public EditQuotePopup()
        {
            InitializeComponent();

            newProduct = false;

            SetUpPopup();
        }

        private void SetUpPopup()
        {
            //gridEditQuotePopup.ScaleTo(0.5, 0, Easing.Linear);
            framePopup.ScaleTo(0.5, 0, Easing.Linear);
        }        

        public void SetLabelProductName(string productName)
        {
            labelPopupProductName.Text = productName;
        }

        public void SetBtnConfirmClassId(string productId)
        {
            btnConfirm.ClassId = productId;
        }

        public string EntryQuantityText
        {
            get
            {
                return entryQuantity.Text;
            }
            set
            {
                entryQuantity.Text = value;
            }
        }

        public void SetEditQuotePopupNew()
        {
            btnRemove.IsVisible = false;
            labelPopupType.Text = "Add to quote";            
        }

        public void SetEditQuotePopupChange(string productQuantity)
        {
            btnRemove.IsVisible = true;
            labelPopupType.Text = "Change quantity";
            entryQuantity.Text = productQuantity;
            btnConfirm.IsEnabled = true;
            btnConfirm.BackgroundColor = (Color)App.Current.Resources["SPBlue"];
        }

        public delegate void OnCancelButtonClickedDelegate();

        public OnCancelButtonClickedDelegate OnCancelButtonClicked { get; set; }

        private void Clicked_btnCancel(object sender, EventArgs eventArgs)
        {
            DisableButtons();            
            OnCancelButtonClicked?.Invoke();
        }

        public delegate void OnRemoveButtonClickedDelegate(string productId);

        public OnRemoveButtonClickedDelegate OnRemoveButtonClicked { get; set; }

        private void Clicked_btnRemove(object sender, EventArgs eventArgs)
        {
            DisableButtons();
            OnRemoveButtonClicked?.Invoke(btnConfirm.ClassId);
        }

        public delegate void OnConfirmButtonClickedDelegate(string productId);

        public OnConfirmButtonClickedDelegate OnConfirmButtonClicked { get; set; }

        private void Clicked_btnConfirm(object sender, EventArgs eventArgs)
        {
            DisableButtons();
            OnConfirmButtonClicked?.Invoke(btnConfirm.ClassId);
        }

        private void DisableButtons()
        {
            btnCancel.IsEnabled = false;
            btnRemove.IsEnabled = false;
            btnConfirm.IsEnabled = false;
        }

        private void EnableButtons()
        {
            btnCancel.IsEnabled = true;
            if (!newProduct)
            {
                btnRemove.IsEnabled = true;
                btnConfirm.IsEnabled = true;
            }
        }

        public void ToggleAddToQuotePopup()
        {
            if (IsVisible)
                AnimatePopupOn();
            else
            {
                AnimatePopupOff();
                EnableButtons();
            }
                
        }

        private async void AnimatePopupOn()
        {
            await Task.WhenAll(
                framePopup.ScaleTo(0.5, 250, Easing.Linear),
                framePopup.FadeTo(0, 250, Easing.Linear),
                boxBackground.FadeTo(0, 250, Easing.Linear)
                );

            IsVisible = false;
        }

        private async void AnimatePopupOff()
        {
            IsVisible = true;

            await Task.WhenAll(
                framePopup.ScaleTo(1, 250, Easing.Linear),
                framePopup.FadeTo(1, 250, Easing.Linear),
                boxBackground.FadeTo(0.5, 250, Easing.Linear)
                );
        }

        public bool IsInteger(string input)
        {
            return Int32.TryParse(input, out int output);
        }

        private void TextChanged_Quantity(object sender, TextChangedEventArgs args)
        {
            ValidateQuantity((Entry)sender);
        }

        private void ValidateQuantity(Entry quantity)
        {
            if (IsInteger(quantity.Text))
            {
                if (newProduct)
                {
                    if (Int32.Parse(quantity.Text) > 0)
                    {
                        btnConfirm.IsEnabled = true;
                        btnConfirm.BackgroundColor = (Color)App.Current.Resources["SPBlue"];
                    }
                    else
                    {
                        btnConfirm.IsEnabled = false;
                        btnConfirm.BackgroundColor = Color.Default;
                    }
                }
                else
                {
                    btnConfirm.IsEnabled = true;
                    btnConfirm.BackgroundColor = (Color)App.Current.Resources["SPBlue"];
                }

            }
            else
            {
                btnConfirm.IsEnabled = false;
                btnConfirm.BackgroundColor = Color.Default;
            }
        }
        private void OnBoxViewGestureRecognizerTapped(object sender, EventArgs eventArgs)
        {
            ToggleAddToQuotePopup();
        }
    }
}