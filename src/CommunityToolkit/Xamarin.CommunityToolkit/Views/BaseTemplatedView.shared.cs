﻿using System;
using Xamarin.Forms;

namespace Xamarin.CommunityToolkit.UI.Views.Internals
{
	/// <summary>
	/// Abstract class that templated views should inherit
	/// </summary>
	/// <typeparam name="TControl">The type of the control that this template will be used for</typeparam>
	public abstract class BaseTemplatedView<TControl> : TemplatedView where TControl : View, new()
	{
		TControl? control;

		protected TControl? Control => control;

		/// <summary>
		/// Constructor of <see cref="BaseTemplatedView{TControl}" />
		/// </summary>
		public BaseTemplatedView()
			=> ControlTemplate = new ControlTemplate(typeof(TControl));

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			if (control != null)
				control.BindingContext = BindingContext;
		}

		protected override void OnChildAdded(Element child)
		{
			if (control == null && child is TControl content)
			{
				control = content;
				OnControlInitialized(control);
			}

			base.OnChildAdded(child);
		}

		protected abstract void OnControlInitialized(TControl control);
	}
}