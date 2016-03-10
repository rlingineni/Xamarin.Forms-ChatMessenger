using System;

using Xamarin.Forms;

namespace chatmessenger
{
	public class ScalableEntry : Editor
	{
		public ScalableEntry ()
		{
			this.TextChanged += (sender, e) => { this.InvalidateMeasure(); };
		}
	}
}


