using Xamarin.Forms;

namespace playaround
{
    using System;
    using Xamarin.Forms.Xaml;

    public partial class playaroundPage : ContentPage
    {
        public playaroundPage()
        {
            InitializeComponent();
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            valueLabel.Text = args.NewValue.ToString("F3"); // changes value label text to the string value of args
            //valueLabel.Text = ((Slider)sender).Value.ToString("F3"); // obtains the value of the slider object and changes the label text to that
		}

        async void OnButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            await DisplayAlert("Clicked!", "The button labeled '" + button.Text + "' has been clicked", "OK");
        }

		async void OpenPage(object sender, EventArgs args)
		{
			Button button = (Button)sender;
			await DisplayAlert("Clicked!", "The button labeled '" + button.Text + "' has been clicked", "OK");
		}
    }
}
