using FontBuddyLib;
using GameTimer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;
using System;
using System.Collections.Generic;

namespace FontBuddyPlusLib
{
	public class FontBuddyPlus : IFontBuddy
	{
		#region Properties

		public DynamicSpriteFont DynamicSpriteFont { get; private set; }
		public float FontSize { get; set; }

		public SpriteFont Font { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public SpriteEffects SpriteEffects { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public float Rotation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		#endregion //Properties

		#region Methods

		public FontBuddyPlus()
		{
		}

		public FontBuddyPlus(float fontSize)
		{
			FontSize = fontSize;
		}

		public void LoadContent(ContentManager content, string resourceName)
		{
			DynamicSpriteFont = DynamicSpriteFont.FromTtf(content.Load<byte[]>(resourceName), FontSize);
		}

		public Vector2 MeasureString(string text)
		{
			return DynamicSpriteFont.MeasureString(text);
		}

		public float Write(string text, Vector2 position, Justify justification, float scale, Color color, SpriteBatch spriteBatch, GameClock time)
		{
			//if this thing is empty, dont do anything
			if (string.IsNullOrEmpty(text))
			{
				return position.X;
			}

			position = LineFormatter.JustifiedPosition(text, position, justification, scale, this);

			//okay, draw the actual string
			DynamicSpriteFont.DrawString(spriteBatch, text, position, color, new Vector2(scale));

			//return the end of that string
			return position.X + (MeasureString(text).X * scale);
		}

		public float Write(string text, Point position, Justify justification, float scale, Color color, SpriteBatch spriteBatch, GameClock time)
		{
			return Write(text, position.ToVector2(), justification, scale, color, spriteBatch, time);
		}

		public List<string> BreakTextIntoList(string text, int rowWidth)
		{
			return LineFormatter.BreakTextIntoList(text, rowWidth, this);
		}

		public float ScaleToFit(string text, int rowWidth)
		{
			return LineFormatter.ScaleToFit(text, rowWidth, this);
		}

		public float ShrinkToFit(string text, int rowWidth)
		{
			return LineFormatter.ShrinkToFit(text, rowWidth, this);
		}

		public bool NeedsToShrink(string text, float scale, int rowWidth)
		{
			return LineFormatter.NeedsToShrink(text, scale, rowWidth, this);
		}

		#endregion //Methods
	}
}
