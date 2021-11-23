namespace XLSTool
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.Drawing;

    public class SplitState
    {
        private int[] _Height;
        private int[] _Width;

        public SplitState(Image[] ImageList, Point[] PosList)
        {
            this._Width = null;
            this._Height = null;
            Point point = PosList[Information.UBound(PosList, 1)];
            int[] numArray2 = new int[point.X + 1];
            int[] numArray = new int[point.Y + 1];
            int x = point.X;
            for (int i = 0; i <= x; i++)
            {
                int num6 = Information.UBound(PosList, 1);
                for (int k = 0; k <= num6; k++)
                {
                    if (PosList[k].X == i)
                    {
                        numArray2[i] = ImageList[k].Width;
                        break;
                    }
                }
            }
            int y = point.Y;
            for (int j = 0; j <= y; j++)
            {
                int num8 = Information.UBound(PosList, 1);
                for (int m = 0; m <= num8; m++)
                {
                    if (PosList[m].Y == j)
                    {
                        numArray[j] = ImageList[m].Height;
                        break;
                    }
                }
            }
            this._Width = numArray2;
            this._Height = numArray;
        }

        public SplitState(int[] Width, int[] height)
        {
            this._Width = null;
            this._Height = null;
            this._Width = Width;
            this._Height = height;
        }

        public void Add(bool axis, int value)
        {
            bool flag = false;
            if (axis)
            {
                int num = 0;
                ArrayList list = new ArrayList();
                int num9 = Information.UBound(this._Height, 1);
                for (int i = 0; i <= num9; i++)
                {
                    num += this._Height[i];
                    if ((num > value) & !flag)
                    {
                        int num3 = num - value;
                        list.Add(this._Height[i] - num3);
                        list.Add(num3);
                        flag = true;
                    }
                    else
                    {
                        list.Add(this._Height[i]);
                    }
                }
                this._Height = new int[(list.Count - 1) + 1];
                int num10 = list.Count - 1;
                for (int j = 0; j <= num10; j++)
                {
                    this._Height[j] = Conversions.ToInteger(list[j]);
                }
            }
            else
            {
                int num5 = 0;
                ArrayList list2 = new ArrayList();
                int num11 = Information.UBound(this._Height, 1);
                for (int k = 0; k <= num11; k++)
                {
                    num5 += this._Width[k];
                    if ((num5 > value) & !flag)
                    {
                        int num7 = num5 - value;
                        list2.Add(this._Width[k] - num7);
                        list2.Add(num7);
                        flag = true;
                    }
                    else
                    {
                        list2.Add(this._Width[k]);
                    }
                }
                this._Width = new int[(list2.Count - 1) + 1];
                int num12 = list2.Count - 1;
                for (int m = 0; m <= num12; m++)
                {
                    this._Width[m] = Conversions.ToInteger(list2[m]);
                }
            }
        }

        public void RemoveAt(bool axis, int index)
        {
            ArrayList list3;
            int num6;
            if (axis)
            {
                ArrayList list = new ArrayList();
                list.Add(this._Height[0]);
                int num5 = Information.UBound(this._Height, 1);
                for (int i = 1; i <= num5; i++)
                {
                    if (i == index)
                    {
                        list3 = list;
                        num6 = i - 1;
                        list3[num6] = Operators.AddObject(list3[num6], this._Height[i]);
                    }
                    else
                    {
                        list.Add(this._Height[i]);
                    }
                }
                this._Height = new int[(list.Count - 1) + 1];
                int num7 = list.Count - 1;
                for (int j = 0; j <= num7; j++)
                {
                    this._Height[j] = Conversions.ToInteger(list[j]);
                }
            }
            else
            {
                ArrayList list2 = new ArrayList();
                list2.Add(this._Width[0]);
                int num8 = Information.UBound(this._Height, 1);
                for (int k = 1; k <= num8; k++)
                {
                    if (k == index)
                    {
                        list3 = list2;
                        num6 = k - 1;
                        list3[num6] = Operators.AddObject(list3[num6], this._Width[k]);
                    }
                    else
                    {
                        list2.Add(this._Width[k]);
                    }
                }
                this._Width = new int[(list2.Count - 1) + 1];
                int num9 = list2.Count - 1;
                for (int m = 0; m <= num9; m++)
                {
                    this._Width[m] = Conversions.ToInteger(list2[m]);
                }
            }
        }

        public int Column
        {
            get
            {
                return this._Width.Length;
            }
        }

        public int Height
        {
            get
            {
                int num2 = 0;
                int num4 = Information.UBound(this._Height, 1);
                for (int i = 0; i <= num4; i++)
                {
                    num2 += this._Height[i];
                }
                return num2;
            }
        }

        public int Height_(Point Pos)
        {
            return this._Height[Pos.Y];
        }

        public Point Location(Point Pos)
        {

            int x = 0;
            int y = 0;
            int num5 = Pos.X - 1;
            for (int i = 0; i <= num5; i++)
            {
                x += this._Width[i];
            }
            int num6 = Pos.Y - 1;
            for (int j = 0; j <= num6; j++)
            {
                y += this._Height[j];
            }
            return new Point(x, y);

        }

        public Rectangle Rectangle(Point Pos)
        {

            return new Rectangle(this.Location(Pos), this.Size_(Pos));

        }

        public int Row
        {
            get
            {
                return this._Height.Length;
            }
        }

        public System.Drawing.Size Size
        {
            get
            {
                return new System.Drawing.Size(this.Width, this.Height);
            }
        }

        public System.Drawing.Size Size_(Point Pos)
        {

            return new System.Drawing.Size(this.Width_(Pos), this.Height_(Pos));

        }

        public int SplitCount
        {
            get
            {
                return (this.Row * this.Column);
            }
        }

        public int Width
        {
            get
            {
                int num2 = 0;
                int num4 = Information.UBound(this._Width, 1);
                for (int i = 0; i <= num4; i++)
                {
                    num2 += this._Width[i];
                }
                return num2;
            }
        }

        public int Width_(Point Pos)
        {

            return this._Width[Pos.X];

        }
    }
}

