using System;
using System.Collections.Generic;
using System.Text;

namespace SagaLib
{
    public class Vector2D
    {
        ushort x, y;

        public ushort X { get { return this.x; } set { this.x = value; } }
        public ushort Y { get { return this.y; } set { this.y = value; } }

        public Vector2D(ushort x, ushort y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2D operator +(Vector2D a, Vector2D b)
        {
            return new Vector2D((ushort)(a.X + b.X), (ushort)(a.Y + b.Y)); 
        }

        public static Vector2D operator -(Vector2D a, Vector2D b)
        {
            return new Vector2D((ushort)(b.X - a.X), (ushort)(b.Y - a.Y)); 
        }

        public static ushort GetDistance(Vector2D a, Vector2D b)
        {
            Vector2D c = b - a;
            return (ushort)Math.Sqrt(c.X * c.X + c.Y * c.Y);
        }
    }
}
