﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace go
{
    //should try to change it to struct
    public class Rectangle
    {
        int _x = 0;
        int _y = 0;
        int _width = 0;
        int _height = 0;

        public Rectangle()
        { }
        public Rectangle(Point p, Size s)
        {
            _x = p.X;
            _y = p.Y;
            _width = s.Width;
            _height = s.Height;
        }
        public Rectangle(Size s)
        {
            _x = 0;
            _y = 0;
            _width = s.Width;
            _height = s.Height;
        }
        public Rectangle(int x, int y, int width, int height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
        public int Left
        {
            get { return _x; }
            set { _x = value; }
        }
        public int Top
        {
            get { return _y; }
            set { _y = value; }
        }
        public int Right
        {
            get { return _x + _width; }
        }
        public int Bottom
        {
            get { return _y + _height; }
        }

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
        public Size Size
        {
            get { return new Size(Width, Height); }
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }
        public Point TopLeft
        {
            set
            {
                X = value.X;
                Y = value.Y;
            }
            get { return new Point(X, Y); }
        }
        public Point TopRight
        {
            get { return new Point(Right, Y); }
        }
        public Point BottomLeft
        {
            get { return new Point(X, Bottom); }
        }
        public Point BottomRight
        {
            get { return new Point(Right, Bottom); }
        }
        public void Inflate(int xDelta, int yDelta)
        {
            this.X -= xDelta;
            this.Width += 2 * xDelta;
            this.Y -= yDelta;
            this.Height += 2 * yDelta;
        }
        public bool Contains(Point p)
        {
            return (p.X >= X && p.X <= X + Width && p.Y >= Y && p.Y <= Y + Height) ?
                true : false;
        }
        public bool Contains(Rectangle r)
        {
            return r.TopLeft >= this.TopLeft && r.BottomRight <= this.BottomRight ? true : false;
        }
        public bool Intersect(Rectangle r)
        {
            int maxLeft = Math.Max(this.Left, r.Left);
            int minRight = Math.Min(this.Right, r.Right);
            int maxTop = Math.Max(this.Top, r.Top);
            int minBottom = Math.Min(this.Bottom, r.Bottom);

            if (maxLeft < minRight && maxTop < minBottom)
                return true;

            return false;

        }
        public Rectangle Intersection(Rectangle r)
        {
            Rectangle result = new Rectangle();
            
            if (r.Left >= this.Left)
                result.Left = r.Left;
            else
                result.TopLeft = this.TopLeft;

            if (r.Right >= this.Right)
                result.Width = this.Right - result.Left;
            else
                result.Width = r.Right - result.Left;

            if (r.Top >= this.Top)
                result.Top = r.Top;
            else
                result.Top = this.Top;

            if (r.Bottom >= this.Bottom)
                result.Height = this.Bottom - result.Top;
            else
                result.Height = r.Bottom - result.Top;

            return result;
        }
        public static implicit operator Rectangle(System.Drawing.Rectangle r)
        {
            return new Rectangle(r.X, r.Y, r.Width, r.Height);
        }
        public static implicit operator System.Drawing.Rectangle(Rectangle r)
        {
            return new System.Drawing.Rectangle(r.X, r.Y, r.Width, r.Height);
        }
        public static implicit operator Cairo.Rectangle(Rectangle r)
        {
            return new Cairo.Rectangle((double)r.X, (double)r.Y, (double)r.Width, (double)r.Height);
        }
        public static Rectangle operator +(Rectangle r1, Rectangle r2)
        {
            int x = Math.Min(r1.X, r2.X);
            int y = Math.Min(r1.Y, r2.Y);
            int x2 = Math.Max(r1.Right, r2.Right);
            int y2 = Math.Max(r1.Bottom, r2.Bottom);
            return new Rectangle(x, y, x2 - x, y2 - y);
        }
        public static bool operator ==(Rectangle r1, Rectangle r2)
        {
            return r1.TopLeft == r2.TopLeft && r1.Size == r2.Size ? true : false;
        }
        public static bool operator !=(Rectangle r1, Rectangle r2)
        {
            return r1.TopLeft == r2.TopLeft && r1.Size == r2.Size ? false : true;
        }

        public RectanglesRelations test(Rectangle r)
        {
            if (r == this)
                return RectanglesRelations.Equal;

            int nbrPtIncluded = 0;

            if (this.Contains(r.TopLeft))
                nbrPtIncluded++;
            if (this.Contains(r.TopRight))
                nbrPtIncluded++;
            if (this.Contains(r.BottomLeft))
                nbrPtIncluded++;
            if (this.Contains(r.BottomRight))
                nbrPtIncluded++;

            switch (nbrPtIncluded)
            {
                case 0:
                    return RectanglesRelations.NoRelation;
                case 4:
                    return RectanglesRelations.Contains;
                default:
                    return RectanglesRelations.Intersect;
            }
        }
        public Rectangle Clone
        {
            get
            {
                return new Rectangle(X, Y, Width, Height);
            }
        }
        public static Rectangle Zero
        {
            get { return new Rectangle(0, 0, 0, 0); }
        }
        public static Rectangle Empty
        {
            get { return Zero; }
        }
        public override string ToString()
        {
            return string.Format("({0},{1},{2},{3})", X, Y, Width, Height);
        }
    }

}
