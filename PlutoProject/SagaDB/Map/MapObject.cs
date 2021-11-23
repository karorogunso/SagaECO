using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaDB.Map
{
    [Serializable]
    public class MapObject
    {
        string name;
        int width, height;
        int centerX, centerY;
        byte x, y;
        BitMask flag;
        byte dir;

        public string Name { get { return this.name; } set { this.name = value; } }

        public int Width { get { return this.width; } set { this.width = value; } }

        public int Height { get { return this.height; } set { this.height = value; } }

        public int CenterX { get { return this.centerX; } set { this.centerX = value; } }

        public int CenterY { get { return this.centerY; } set { this.centerY = value; } }

        public byte X { get { return this.x; } set { this.x = value; } }

        public byte Y { get { return this.y; } set { this.y = value; } }


        public BitMask Flag { get { return this.flag; } set { this.flag = value; } }

        public byte Dir { get { return this.dir; } set { this.dir = value; } }

        public MapObject Clone
        {
            get
            {
                MapObject obj = new MapObject();
                obj.Name = this.name;
                obj.width = this.width;
                obj.height = this.height;
                obj.centerX = this.centerX;
                obj.centerY = this.centerY;
                obj.flag = new BitMask(this.flag.Value);
                return obj;
            }
        }

        public int[,][] PositionMatrix
        {
            get
            {
                int[,][] basic = new int[width, height][];
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        basic[i, j] = new int[2];
                        basic[i, j][0] = i - (centerX);
                        basic[i, j][1] = j - (centerY);
                    }
                }

                double theta = ((dir * 45) * Math.PI) / 180;
                double[,] rotationMatrix = new double[2, 2]
                {
                    {Math.Cos(theta),Math.Sin(theta)},
                    {-Math.Sin(theta),Math.Cos(theta)}
                };

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        double x, y;
                        x = Math.Round(rotationMatrix[0, 0] * basic[i, j][0] + rotationMatrix[1, 0] * -basic[i, j][1], 3);
                        y = Math.Round(-(rotationMatrix[0, 1] * basic[i, j][0] + rotationMatrix[1, 1] * -basic[i, j][1]), 3);
                        if (x > 0)
                            basic[i, j][0] = (int)Math.Ceiling(x);
                        else
                            basic[i, j][0] = (int)Math.Floor(x);
                        if (y > 0)
                            basic[i, j][1] = (int)Math.Ceiling(y);
                        else
                            basic[i, j][1] = (int)Math.Floor(y);

                    }
                }
                return basic;
            }
        }

    }
}
