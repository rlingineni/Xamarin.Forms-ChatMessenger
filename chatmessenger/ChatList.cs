using System;

using Xamarin.Forms;

namespace chatmessenger
{
	public class ChatList : ContentPage
	{
		public ChatList ()
		{

			Button openMessenger = new Button () {
				Text = "Open Chat"
			};

			Title = "Chat Rooms"; 
			openMessenger.Clicked += (object sender, EventArgs e) => {Navigation.PushAsync(new ChatPage()); };
			
			Content = new StackLayout { 
				Children = {
					new Label { Text = "All of the different chat rooms will be listed here, this is the root navigation page" },
					openMessenger
				}
			};


		}
	}
}


