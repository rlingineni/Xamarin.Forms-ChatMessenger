using System;

using Xamarin.Forms;

namespace chatmessenger
{
	public class modalBasePage : ContentPage
	{
		public modalBasePage ()
		{
			Button button = new Button (){Text="Open ChatPage"};

			button.Clicked += (object sender, EventArgs e) => {Navigation.PushAsync(new NavigationPage(new ChatPage()));};
			Content = new StackLayout { 
				Children = {
					new Label { Text = "This is a modal page" },button
				}
			};
		}
	}
}


