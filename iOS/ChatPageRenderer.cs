using Xamarin.Forms.Platform.iOS;
using chatmessenger;
using Xamarin.Forms;
using AuthenticateUIType = UIKit.UIViewController;
using UIKit;
using System;
using Foundation;
using System.Threading;
using System.Diagnostics;
using CoreGraphics;
using chatmessenger.iOS;
using JSQMessagesViewController;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel;

[assembly: ExportRenderer (typeof(ChatPage), typeof(ChatPageRenderer))]
namespace chatmessenger.iOS
{
	public class ChatPageRenderer : PageRenderer
	{
		public  UINavigationController  navigationController;

		public static UIWindow window;

		public  JSQmessages viewController;

		public static EventHandler finished;

		public static UINavigationController navigation;

		protected async override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);

			navigation = NavigationController;

			window = new UIWindow(UIScreen.MainScreen.Bounds);

			//This is the class which actually implements the component a couple of elements to make it work
			viewController = new JSQmessages(); 


			//sender is a public field of JSQmessages and it is populated using the public fields we defined in the orginal forms ChatPage.
			viewController.sender = new User (){ Id = ChatPage.senderID, DisplayName = ChatPage.senderName };

			viewController.View.Frame = this.View.Frame;

			navigationController = new UINavigationController();
			navigationController.PushViewController(viewController, false);

			AddChildViewController (viewController);

			/*the Frame is smaller than the entire screen, this lets us still retain the original Navigation bar from Xamarin Forms, 
			 * so it's easy to dismiss the page from the view hierarchy if we retain the navbar that forms created for us */
			viewController.View.Frame = new CGRect(this.View.Frame.X,this.View.Frame.Y,this.View.Bounds.Width,this.View.Bounds.Height - 64f);
			this.View.AddSubview (viewController.View);
			this.DidMoveToParentViewController (viewController); 



		} 



	






	}
		









}

