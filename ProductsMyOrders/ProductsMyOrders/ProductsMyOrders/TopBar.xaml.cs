using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductsMyOrders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TopBar : Grid
    {
        public TopBar()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TitleBarTextProperty = BindableProperty.Create(nameof(TitleBarText), typeof(String), typeof(TopBar), "Placeholder");

        public String TitleBarText
        {
            get
            {
                return (String)GetValue(TitleBarTextProperty);
            }
            set
            {
                SetValue(TitleBarTextProperty, value);
            }
        }
    }
}