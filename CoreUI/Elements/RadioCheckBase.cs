﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoreUI.Elements
{
    public abstract class RadioCheckBase : Element
    {
        protected String mText = "";
        protected String DispText = "";
        protected Point TextPos = new Point();
        protected bool? mIsChecked = false;
        protected bool mIsThreeState = false;
        protected bool mIsPressed = false;

        public bool? IsChecked
        {
            get { return mIsChecked; }
            set { mIsChecked = value; }
        }
        public bool IsThreeState
        {
            get { return mIsThreeState; }
            set { mIsThreeState = value; }
        }
        public String Text
        {
            get { return mText; }
            set
            {
                mText = value;
                CalculateText();
                InvalidateVisual();
            }
        }
        protected internal override void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mIsPressed = false;
                if (IsChecked == null)
                    IsChecked = false;
                else if (IsChecked == false)
                    IsChecked = true;
                else if (IsThreeState)
                    IsChecked = null;
                else
                    IsChecked = false;
                InvalidateVisual();
            }
            base.OnMouseUp(sender, e);
        }
        protected internal override void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mIsPressed = true;
                InvalidateVisual();
            }
            base.OnMouseDown(sender, e);
        }
        private void CalculateText()
        {
            DispText = mText;
            PointF size = CoreUIEngine.mDrawEngine.getTextSize(DispText, mFontInt);
            if (Bounds.Width < 15)
                DispText = "";
            else if (size.X > Bounds.Width - 15)
            {
                StringBuilder sb = new StringBuilder(DispText);
                while (size.X > Bounds.Width)
                {
                    sb.Remove(0, 1);
                    sb.Remove(sb.Length - 1, 1);
                    size = CoreUIEngine.mDrawEngine.getTextSize(sb.ToString(), mFontInt);
                }
                DispText = sb.ToString();
            }
            TextPos = new Point(Bounds.Left + 15, (int)(Bounds.Top + (Bounds.Height / 2) - (size.Y / 2)));
             
        }
    }
}