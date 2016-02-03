using System;

using Xamarin.Forms;
using System.Diagnostics;

namespace chatmessenger
{
	public class ChatPage : ContentPage
	{
		public ChatPage ()
		{
			Title = "Friends Chat";
			ToolbarItems.Add(new ToolbarItem("Filter", "Icon-Small.png", async () =>
				{
					await DisplayAlert("Button Clicked","This is just a demo to show a button can be clicked","Ok");

				}));


		}

		//set the public fields that describes the sender
		public static string senderName { get; set;} = "Ravi";

		public static string senderID { get; set;} = "2";






	}
}


