using System;

using Xamarin.Forms;

namespace chatmessenger
{
	public class customInputForm : ContentPage
	{
		public customInputForm ()
		{
			Content = new StackLayout { 
				Children = {
					new ScrollView()
					{
						Content =  new StackLayout()
						{
							Children = {new ScalableEntry(){ MinimumHeightRequest=30}}
						}
					}

					}
					
			};
		}
	}
}


