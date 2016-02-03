using System;
using JSQMessagesViewController;
using System.Threading.Tasks;
using System.Collections.Generic;
using UIKit;
using Foundation;
using Xamarin.Forms;
using System.Timers;

namespace chatmessenger.iOS
{
	public class JSQmessages:MessagesViewController
	{
		MessagesBubbleImage outgoingBubbleImageData;
		MessagesBubbleImage incomingBubbleImageData;

		public List<Message> messages = new List<Message> ();

		public User sender {get;set;} //look at the model, sender is given from the forms page

		//just created this to demo how to create a new chat user
		User friend = new User {Id = "BADB229", DisplayName = "Tom Anderson"};

		MessageFactory messageFactory = new MessageFactory();

		public event EventHandler closePage;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// You must set your senderId and display name
			SenderId = sender.Id;
			SenderDisplayName = sender.DisplayName;


			// These MessagesBubbleImages will be used in the GetMessageBubbleImageData override
			var bubbleFactory = new MessagesBubbleImageFactory ();
			outgoingBubbleImageData = bubbleFactory.CreateOutgoingMessagesBubbleImage (UIColorExtensions.MessageBubbleLightGrayColor);
			incomingBubbleImageData = bubbleFactory.CreateIncomingMessagesBubbleImage (UIColorExtensions.MessageBubbleBlueColor);

			// Remove the AccessoryButton as we will not be sending pics
			InputToolbar.ContentView.LeftBarButtonItem = null;

			// Remove the Avatars
			CollectionView.CollectionViewLayout.IncomingAvatarViewSize = CoreGraphics.CGSize.Empty;
			CollectionView.CollectionViewLayout.OutgoingAvatarViewSize = CoreGraphics.CGSize.Empty;


			// Load some messages to start
				messages.Add (new Message (friend.Id, friend.DisplayName, NSDate.DistantPast, "Hi There"));
			//	messages.Add (new Message (friend.Id, friend.DisplayName, NSDate.DistantPast, "I'm sorry, my responses are limited. You must ask the right questions."));


			//we use this to generate random messages
			Timer timer = new Timer(2000);
			timer.Elapsed += async ( sender, e ) => await HandleTimer();
			timer.Start();

			// Remove the Avatars
			//CollectionView.CollectionViewLayout.IncomingAvatarViewSize = CoreGraphics.CGSize.Empty;
			//CollectionView.CollectionViewLayout.OutgoingAvatarViewSize = CoreGraphics.CGSize.Empty;
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			this.CollectionView.CollectionViewLayout.SpringinessEnabled = true;
		}
		public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath) 
		{
			var cell =  base.GetCell (collectionView, indexPath) as MessagesCollectionViewCell;

			// Override GetCell to make modifications to the cell
			// In this case darken the text for the sender
			var message = messages [indexPath.Row];
			if (message.SenderId == SenderId)
				cell.TextView.TextColor = UIColor.Black;

			return cell;
		}

		public override nfloat GetMessageBubbleTopLabelHeight (MessagesCollectionView collectionView, MessagesCollectionViewFlowLayout collectionViewLayout, NSIndexPath indexPath)
		{
			return 20.0f;
		}

		public override NSAttributedString GetMessageBubbleTopLabelAttributedText (MessagesCollectionView collectionView, NSIndexPath indexPath)
		{
			var message = messages [indexPath.Row];

			Console.WriteLine (message.SenderDisplayName);
			if (message.SenderId == SenderId)
			{
				return new NSAttributedString (message.SenderDisplayName);
			}

			if (indexPath.Length -1 > 1) {
				var previousMessage = messages [indexPath.Row-1];
				if (previousMessage.SenderId == SenderId) {
					return null;
				}
			} 
				
			return new NSAttributedString (message.SenderDisplayName);

		}

		public override nint GetItemsCount (UICollectionView collectionView, nint section)
		{
			return messages.Count;
		}

		public override IMessageData GetMessageData (MessagesCollectionView collectionView, NSIndexPath indexPath)
		{
			return messages [indexPath.Row];
		}

		public override IMessageBubbleImageDataSource GetMessageBubbleImageData (MessagesCollectionView collectionView, NSIndexPath indexPath)
		{
			var message = messages [indexPath.Row];
			if (message.SenderId == SenderId)
				return outgoingBubbleImageData;
			return incomingBubbleImageData;

		}

		public override IMessageAvatarImageDataSource GetAvatarImageData (MessagesCollectionView collectionView, NSIndexPath indexPath)
		{
			return null;
		}

		public override async void PressedSendButton (UIButton button, string text, string senderId, string senderDisplayName, NSDate date)
		{
			SystemSoundPlayer.PlayMessageSentSound ();

			var message = new Message (SenderId, SenderDisplayName, NSDate.Now, text);
			messages.Add (message);

			FinishSendingMessage (true);

			await Task.Delay (500);

			await SimulateDelayedMessageReceived ();
		}

		async Task SimulateDelayedMessageReceived ()
		{
			ShowTypingIndicator = true;

			ScrollToBottom (true);

			var delay = System.Threading.Tasks.Task.Delay (1500);
			var message = await messageFactory.CreateMessageAsync (friend);
			await delay;

			messages.Add (message);

			ScrollToBottom (true);

			SystemSoundPlayer.PlayMessageReceivedSound ();

			FinishReceivingMessage (true);
		}
			
		private async Task HandleTimer()
		{
			await SimulateDelayedMessageReceived();
		}


	}
}

