using System;
using Xamarin.Forms;
using Xamarin.Forms.Labs.Controls;

namespace TodoAzurePcl
{
	class ItemCell : ViewCell
	{
		public ItemCell()
		{

//			var itemLabel = new Label()
//			{
//				HorizontalOptions = LayoutOptions.StartAndExpand
//			};
//			itemLabel.SetBinding(Label.TextProperty, "Text");
//
//			var doneSwitch = new Switch();
//			doneSwitch.HorizontalOptions = LayoutOptions.End;
//			doneSwitch.SetBinding(Switch.IsToggledProperty, "Complete");
//
//			var viewLayout = new StackLayout()
//			{
//				Orientation = StackOrientation.Horizontal,
//				Children = { itemLabel, doneSwitch }
//			};
//			View = viewLayout;

			var checkBox = new CheckBox ();
			checkBox.SetBinding (CheckBox.CheckedProperty, "Complete");
			checkBox.SetBinding (CheckBox.DefaultTextProperty, "Text");
			View = checkBox;
		}
	}
}

