﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace prototype2
{
    public partial class MyPage : TabbedPage
    {
        public MyPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

        }

    }
}