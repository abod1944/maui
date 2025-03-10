﻿using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls.CustomAttributes;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Graphics;

#if UITEST
using Microsoft.Maui.Controls.Compatibility.UITests;
using Xamarin.UITest;
using NUnit.Framework;
#endif

namespace Microsoft.Maui.Controls.ControlGallery.Issues
{
#if UITEST
	[Category(UITestCategories.InputTransparent)]
	[Category(UITestCategories.ManualReview)]
#endif
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Github, 2775, "ViewCell background conflicts with ListView Semi-Transparent and Transparent backgrounds")]
	public class Issue2775 : TestContentPage
	{
		protected override void Init()
		{
			var list = new ListView
			{
				ItemsSource = GetList("Normal BG Blue"),
				BackgroundColor = Colors.Blue,
				ItemTemplate = new DataTemplate(typeof(NormalCell))
			};

			var listTransparent = new ListView
			{
				ItemsSource = GetList("Normal BG Transparent"),
				BackgroundColor = Colors.Transparent,
				ItemTemplate = new DataTemplate(typeof(NormalCell))
			};

			var listSemiTransparent = new ListView
			{
				ItemsSource = GetList("Normal BG SEMI Transparent"),
				BackgroundColor = Color.FromArgb("#801B2A39"),
				ItemTemplate = new DataTemplate(typeof(NormalCell))
			};

			var listContextActions = new ListView
			{
				ItemsSource = GetList("ContextActions BG PINK"),
				BackgroundColor = Colors.Pink,
				ItemTemplate = new DataTemplate(typeof(ContextActionsCell))
			};

			var listContextActionsTransparent = new ListView
			{
				ItemsSource = GetList("ContextActions BG Transparent"),
				BackgroundColor = Colors.Transparent,
				ItemTemplate = new DataTemplate(typeof(ContextActionsCell))
			};

			var listContextActionsSemiTransparent = new ListView
			{
				ItemsSource = GetList("ContextActions BG Semi Transparent"),
				BackgroundColor = Color.FromArgb("#801B2A39"),
				ItemTemplate = new DataTemplate(typeof(ContextActionsCell))
			};

			Content = new StackLayout
			{
				AutomationId = "TestReady",
				Children = {
					list,
					listTransparent,
					listSemiTransparent,
					listContextActions,
					listContextActionsTransparent,
					listContextActionsSemiTransparent
				},
				BackgroundColor = Colors.Red
			};
		}

		[Preserve(AllMembers = true)]
		internal class ContextActionsCell : ViewCell
		{
			public ContextActionsCell()
			{
				ContextActions.Add(new MenuItem { Text = "action" });
				var label = new Label();
				label.SetBinding(Label.TextProperty, "Name");
				View = label;
			}
		}

		[Preserve(AllMembers = true)]
		internal class NormalCell : ViewCell
		{
			public NormalCell()
			{
				var label = new Label();
				label.SetBinding(Label.TextProperty, "Name");
				View = label;
			}
		}

		List<ListItemViewModel> GetList(string description)
		{
			var itemList = new List<ListItemViewModel>();
			for (var i = 1; i < 3; i++)
			{
				itemList.Add(new ListItemViewModel() { Name = description });
			}
			return itemList;
		}

		[Preserve(AllMembers = true)]
		public class ListItemViewModel
		{
			public string Name { get; set; }
		}

#if UITEST
		[Test]
		public void Issue2775Test()
		{
			RunningApp.WaitForElement("TestReady");
			RunningApp.Screenshot("I am at Issue 2775");
			RunningApp.Screenshot("I see the Label");
		}
#endif
	}
}
