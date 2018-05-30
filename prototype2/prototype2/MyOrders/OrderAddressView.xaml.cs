using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prototype2.MyOrders
{
    public enum AddressType { BillingAddress, DeliveryAddress};

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderAddressView : StackLayout
    {
        private bool dropDownOpen;
        private ScrollView scrollView;
        private List<Address> addresses;
        private string checkBoxUnchecked = "blank_check_box.png";
        private string checkBoxChecked = "check_box_with_check.png";
        public OrderAddressView(ScrollView scrollView, List<Address> addresses, AddressType addressType, string addressTitle)
        {
            InitializeComponent();

            if (addressType == AddressType.BillingAddress)
            {
                stackAddress.Children.Remove(stackAddress.Children[0]);
                stackAddress.Children.Remove(stackAddress.Children[0]);
            }

            labelAddressTitle.Text = addressTitle;

            dropDownOpen = false;

            this.addresses = addresses;

            SetUpPickerAddress();

            this.scrollView = scrollView;
        }

        public bool CheckBoxChecked()
        {
            return btnCheckBox.Image == checkBoxChecked;
        }

        private void Clicked_DropDown(object sender, EventArgs eventArgs)
        {
            dropDownOpen = !dropDownOpen;

            stackAddress.IsVisible = dropDownOpen;

            AnimateDropDown();
        }

        private void AnimateDropDown()
        {
            if (dropDownOpen)
            {
                imageArrow.RotateTo(-180, 250, Easing.Linear);
                scrollView.ScrollToAsync(btnDropDown, ScrollToPosition.Start, true);
            }                
            else
                imageArrow.RotateTo(0, 250, Easing.Linear);
        }

        private void SetUpPickerAddress()
        {
            pickerAddress.Items.Add("New Address");

            if (addresses.Count > 0)
            {
                Address address = addresses[0];          
            }            

            foreach (Address address in addresses)
            {
                pickerAddress.Items.Add(address.StreetAndNumber + " " + address.City + " " + address.State);
            }            

            pickerAddress.SelectedItem = pickerAddress.Items[1];

            if (addresses.Count > 0)
                ChangeAddress(0);
        }

        private void SelectedIndexChanged_AddressPicker(object sender, EventArgs eventArgs)
        {
            if (pickerAddress.SelectedIndex == 0)
                ChangeToAddressNew();
            else
                ChangeAddress(pickerAddress.SelectedIndex - 1);
        }

        private void ChangeAddress(int addressIndex)
        {
            Address address = addresses[addressIndex];

            labelPickerAddress.Text = address.Name + "\n" + address.StreetAndNumber + "\n" + address.City + " " + address.State + " " + address.PostCode + "\n" + address.Country;

            entryName.Text = address.Name;
            entryEmail.Text = address.Email;
            entryPhoneNumber.Text = address.PhoneNumber;
            entryPostCode.Text = address.PostCode;
            entryCity.Text = address.City;
            entryStreetAndNumber.Text = address.StreetAndNumber;
            entryState.Text = address.State;
            entryCountry.Text = address.Country;
        }

        private void ChangeToAddressNew()
        {
            labelPickerAddress.Text = "";

            entryName.Text = "";
            entryEmail.Text = "";
            entryPhoneNumber.Text = "";
            entryPostCode.Text = "";
            entryCity.Text = "";
            entryStreetAndNumber.Text = "";
            entryState.Text = "";
            entryCountry.Text = "";
        }

        private void Clicked_GridAddressPicker(object sender, EventArgs eventArgs)
        {
            pickerAddress.Focus();
        }

        private void Clicked_BtnCheckBox(object sender, EventArgs eventArgs)
        {
            if (ValidateAddress())
            {
                Button button = (Button)sender;
                if (button.Image == checkBoxUnchecked) ConfirmAddress(button);
                else UndoConfirmAddress(button);
                AnimateCheckBox(button);
            }
            else
            {
                DisplayAlertInvalidAddress();
            }
            
        }

        private bool ValidateAddress()
        {
            if (string.IsNullOrWhiteSpace(entryName.Text))
                return false;
            if (string.IsNullOrWhiteSpace(entryEmail.Text))
                return false;
            if (string.IsNullOrWhiteSpace(entryPhoneNumber.Text))
                return false;
            if (string.IsNullOrWhiteSpace(entryPostCode.Text))
                return false;
            if (string.IsNullOrWhiteSpace(entryCity.Text))
                return false;
            if (string.IsNullOrWhiteSpace(entryCity.Text))
                return false;
            if (string.IsNullOrWhiteSpace(entryCity.Text))
                return false;
            if (string.IsNullOrWhiteSpace(entryCity.Text))
                return false;
            
            return true;
        }

        

        public delegate void DisplayAlertInvalidAddressDelegate();

        public DisplayAlertInvalidAddressDelegate DisplayAlertInvalidAddress { get; set; }

        private void ConfirmAddress(Button button)
        {
            btnCheckBox.Image = checkBoxChecked;
        }

        private void UndoConfirmAddress(Button button)
        {
            btnCheckBox.Image = checkBoxUnchecked;
        }

        private async void AnimateCheckBox(Button button)
        {
            await button.ScaleTo(1.2, 50, Easing.CubicIn);
            await button.ScaleTo(1, 50, Easing.CubicIn);
        }
    }
}