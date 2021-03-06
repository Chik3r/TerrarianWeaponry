﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace TerrarianWeaponry.UI
{
	internal class DraggableUIPanel : UIPanel
	{
		internal Action OnCloseBtnClicked;
		internal UIPanel header;
		internal static Vector2 lastPos = new Vector2(400, 400);
		private static bool isDraggable = true;

		public DraggableUIPanel(float width, float height)
		{
			// Set the width and height to the passed parameters
			Width.Set(width, 0);
			Height.Set(height, 0);
			SetPadding(0);

			// Create a new header, with custom OnMouse events
			header = new UIPanel();
			header.SetPadding(0);
			header.Width = Width;
			header.Height = new StyleDimension(30, 0);
			header.BackgroundColor.A = 255;
			header.OnMouseDown += Header_OnMouseDown;
			header.OnMouseUp += Header_OnMouseUp;
			Append(header);

			// Create a close button
			var closeBtn = new UITextPanel<char>('X');
			closeBtn.SetPadding(7);
			closeBtn.Width = new StyleDimension(40, 0);
			closeBtn.Left = new StyleDimension(-40, 1);
			closeBtn.BackgroundColor.A = 255;
			closeBtn.OnClick += (evt, elm) => OnCloseBtnClicked?.Invoke();
			header.Append(closeBtn);

			// Create a lock button
			var lockBtn = new UIImageButton(ModContent.GetTexture("Terraria/Lock_" + (isDraggable ? 1 : 0)));
			lockBtn.Width = new StyleDimension(24, 0);
			lockBtn.Height = new StyleDimension(24, 0);
			lockBtn.Left = new StyleDimension(width - 65, 0);
			lockBtn.Top = new StyleDimension(5, 0);
			lockBtn.SetPadding(0);
			lockBtn.OnClick += UpdateLock;
			header.Append(lockBtn);
		}

		private void UpdateLock(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(listeningElement is UIImageButton imageBtn))
				return;

			isDraggable = !isDraggable;
			imageBtn.SetImage(ModContent.GetTexture("Terraria/Lock_" + (isDraggable ? 1 : 0)));

			Main.PlaySound(SoundID.Unlock);
		}

		#region Dragging Code
		// Based on https://github.com/tModLoader/tModLoader/blob/master/ExampleMod/UI/DragableUIPanel.cs

		private Vector2 offset;
		private bool dragging;

		private void Header_OnMouseUp(UIMouseEvent evt, UIElement listeningElement)
		{
			base.MouseUp(evt);

			if (!isDraggable)
				return;

			Vector2 end = evt.MousePosition;
			dragging = false;

			Left.Set(end.X - offset.X, 0f);
			Top.Set(end.Y - offset.Y, 0f);

			Recalculate();
		}

		private void Header_OnMouseDown(UIMouseEvent evt, UIElement listeningElement)
		{
			base.MouseDown(evt);

			if (!isDraggable)
				return;

			dragging = true;
			offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (ContainsPoint(Main.MouseScreen))
				Main.LocalPlayer.mouseInterface = true;

			if (dragging && isDraggable)
			{
				Left.Set(Main.mouseX - offset.X, 0f);
				Top.Set(Main.mouseY - offset.Y, 0f);

				Recalculate();

				lastPos = new Vector2(Left.Pixels, Top.Pixels);
			}

			// Here we check if the DragableUIPanel is outside the Parent UIElement rectangle. 
			// (In our example, the parent would be ExampleUI, a UIState. This means that we are checking that the DragableUIPanel is outside the whole screen)
			// By doing this and some simple math, we can snap the panel back on screen if the user resizes his window or otherwise changes resolution.
			var parentSpace = Parent.GetDimensions().ToRectangle();
			if (!GetDimensions().ToRectangle().Intersects(parentSpace))
			{
				Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
				Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
				// Recalculate forces the UI system to do the positioning math again.
				Recalculate();
			}
		}

		#endregion
	}
}
