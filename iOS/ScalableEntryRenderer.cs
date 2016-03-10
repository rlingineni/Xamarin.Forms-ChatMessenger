using System;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using UIKit;
using Foundation;
using CoreGraphics;
using chatmessenger.iOS;
using chatmessenger;

[assembly: ExportRenderer (typeof(ScalableEntry), typeof(ScalableEntryRenderer))]
namespace chatmessenger.iOS
{

	public class ScalableEntryRenderer : EditorRenderer
	{
		static readonly UIColor InputBackgroundColor = UIColor.FromWhiteAlpha (250, 1);
		static readonly UIColor InputBorderColor = UIColor.FromRGB (200, 200, 205);
		const float BorderWidth = 0.5f;
		const float CornerRadius = 5;


		protected override void OnElementChanged (ElementChangedEventArgs<Editor> e)
		{
			base.OnElementChanged (e);

			var toolbar = new UIToolbar(new CGRect(0.0f, 0.0f, Control.Frame.Size.Width, 44.0f));

			toolbar.Items = new[]
			{
				new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
				new UIBarButtonItem("Send",UIBarButtonItemStyle.Plain, delegate { Control.ResignFirstResponder(); })
			};



			this.Control.InputAccessoryView = toolbar;

			Control.TranslatesAutoresizingMaskIntoConstraints = false;
			if (Control != null) 
			{
				Control.BackgroundColor = InputBackgroundColor;
				Control.ScrollIndicatorInsets = new UIEdgeInsets (CornerRadius, 0f, CornerRadius, 0f);
				Control.TextContainerInset = new UIEdgeInsets (4f, 2f, 4f, 2f);
				Control.ContentInset = new UIEdgeInsets (1f, 0f, 10f, 0f);
				Control.ScrollEnabled = false;
				Control.ScrollsToTop = false;
				Control.UserInteractionEnabled = true;
				Control.Font = UIFont.SystemFontOfSize (16f);
				Control.TextAlignment = UITextAlignment.Natural;

				//Control.ContentMode = UIViewContentMode.Redraw;

			}

			Control.Layer.BorderColor = InputBorderColor.CGColor;
			Control.Layer.BorderWidth = BorderWidth;
			Control.Layer.CornerRadius = CornerRadius;

			Control.TextColor = UIColor.Gray; 
			Control.Text = "Type a Message...";

			Control.Started += (object sender, EventArgs m) =>
			{
				Control.ScrollEnabled = false;
				Control.ScrollsToTop = false;
				Control.UserInteractionEnabled = true;
				Control.TextColor = UIColor.Black; 
				Control.Text = "";
			};

			Control.Changed += (object sender, EventArgs m) =>
			{

			};

			Control.Ended += (object sender, EventArgs m) => {
				Control.Text = Control.Text.Remove(0,Control.Text.Length-1);
				Control.TextColor = UIColor.LightGray; 
			};
		}



	}

}
