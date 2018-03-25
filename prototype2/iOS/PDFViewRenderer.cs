using Foundation;
using prototype2;
using prototype2.iOS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PDFWebView), typeof(PDFViewRenderer))]
namespace prototype2.iOS
{
    public class PDFViewRenderer : ViewRenderer<PDFWebView, UIWebView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<PDFWebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                SetNativeControl(new UIWebView());
            }
            if (e.OldElement != null)
            {
                // Cleanup
            }
            if (e.NewElement != null)
            {
                var customWebView = Element as PDFWebView;
                string fileName = Path.Combine(NSBundle.MainBundle.BundlePath, string.Format("Content/{0}", WebUtility.UrlEncode(customWebView.Uri)));
                Control.LoadRequest(new NSUrlRequest(new NSUrl(fileName, false)));
                Control.ScalesPageToFit = true;
            }
        }
    }
}