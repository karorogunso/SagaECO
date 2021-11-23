namespace XLSTool
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;

    public class Texture
    {
        private SplitState _SplitState;
        private System.Drawing.Image _Texture;

        public Texture(Stream stream)
        {
            this._Texture = null;
            this._SplitState = null;
            this._Texture = Load(stream, ref this._SplitState);
        }

        private Texture(System.Drawing.Image Image, SplitState SplitState, string FileName)
        {
            this._Texture = null;
            this._SplitState = null;
            this._Texture = Image;
            this._SplitState = SplitState;
        }

        public void AddSplit(bool axis, int value)
        {
            this._SplitState.Add(axis, value);
        }

        private static void BitmapStreamWriter(BinaryWriter wTga, Bitmap bmap, PixelFormat ColorFormat, [Optional, DefaultParameterValue(0)] int Attribute)
        {
            Point point;
            Point point2;
            switch (ColorFormat)
            {
                case PixelFormat.Format32bppArgb:
                    {
                        int num14 = bmap.Height - 1;
                        for (int i = 0; i <= num14; i++)
                        {
                            int num15 = bmap.Width - 1;
                            for (int j = 0; j <= num15; j++)
                            {
                                point2 = new Point(j, i);
                                point = GetPixelPos(bmap, point2, Attribute);
                                Color pixel = bmap.GetPixel(point.X, point.Y);
                                wTga.Write(new byte[] { pixel.B, pixel.G, pixel.R, pixel.A });
                            }
                        }
                        break;
                    }
                case PixelFormat.Format24bppRgb:
                    {
                        int num16 = bmap.Height - 1;
                        for (int k = 0; k <= num16; k++)
                        {
                            int num17 = bmap.Width - 1;
                            for (int m = 0; m <= num17; m++)
                            {
                                point2 = new Point(m, k);
                                point = GetPixelPos(bmap, point2, Attribute);
                                Color color2 = bmap.GetPixel(point.X, point.Y);
                                wTga.Write(new byte[] { color2.B, color2.G, color2.R });
                            }
                        }
                        break;
                    }
                case PixelFormat.Format16bppArgb1555:
                    {
                        int num18 = bmap.Height - 1;
                        for (int n = 0; n <= num18; n++)
                        {
                            int num19 = bmap.Width - 1;
                            for (int num7 = 0; num7 <= num19; num7++)
                            {
                                point2 = new Point(num7, n);
                                point = GetPixelPos(bmap, point2, Attribute);
                                Color color3 = bmap.GetPixel(point.X, point.Y);
                                ushort num6 = 0;
                                if (color3.A > 0)
                                {
                                    num6 = (ushort)(num6 + 0x8000);
                                }
                                num6 = (ushort)(num6 + ((color3.R / 8) << 10));
                                num6 = (ushort)(num6 + ((color3.G / 8) << 5));
                                num6 = (ushort)(num6 + (color3.B / 8));
                                wTga.Write(num6);
                            }
                        }
                        break;
                    }
                case PixelFormat.Format16bppRgb565:
                    {
                        int num20 = bmap.Height - 1;
                        for (int num8 = 0; num8 <= num20; num8++)
                        {
                            int num21 = bmap.Width - 1;
                            for (int num10 = 0; num10 <= num21; num10++)
                            {
                                point2 = new Point(num10, num8);
                                point = GetPixelPos(bmap, point2, Attribute);
                                Color color4 = bmap.GetPixel(point.X, point.Y);
                                ushort num9 = 0;
                                num9 = (ushort)(num9 + ((color4.A / 0x11) << 12));
                                num9 = (ushort)(num9 + ((color4.R / 0x11) << 8));
                                num9 = (ushort)(num9 + ((color4.G / 0x11) << 4));
                                num9 = (ushort)(num9 + (color4.B / 0x11));
                                wTga.Write(num9);
                            }
                        }
                        break;
                    }
                case PixelFormat.Format8bppIndexed:
                    {
                        int num22 = bmap.Height - 1;
                        for (int num11 = 0; num11 <= num22; num11++)
                        {
                            int num23 = bmap.Width - 1;
                            for (int num12 = 0; num12 <= num23; num12++)
                            {
                                point2 = new Point(num12, num11);
                                point = GetPixelPos(bmap, point2, Attribute);
                                Color color5 = bmap.GetPixel(point.X, point.Y);
                                int num13 = ((byte)(((byte)(color5.B + color5.G)) + color5.R)) / 3;
                                wTga.Write((byte)num13);
                            }
                        }
                        break;
                    }
            }
        }

        public static Texture Create(Stream stream)
        {
            Texture texture2 = null;
            try
            {
                texture2 = new Texture(stream);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                texture2 = null;
                ProjectData.ClearProjectError();
            }
            return texture2;
        }

        public static Texture Create(System.Drawing.Image Image, string FileName)
        {
            Texture texture2;
            SplitState splitState = new SplitState(new int[] { Image.Width }, new int[] { Image.Height });
            try
            {
                texture2 = new Texture(Image, splitState, FileName);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                texture2 = null;
                ProjectData.ClearProjectError();
            }
            return texture2;
        }

        private static Point GetPixelPos(Bitmap bmap, Point Point, int Attribute)
        {
            Point point2 = new Point();
            if ((Attribute & 0x10) > 0)
            {
                point2.X = (bmap.Width - 1) - Point.X;
            }
            else
            {
                point2.X = Point.X;
            }
            if ((Attribute & 0x20) > 0)
            {
                point2.Y = Point.Y;
                return point2;
            }
            point2.Y = (bmap.Height - 1) - Point.Y;
            return point2;
        }

        private static bool IsEcoTexture(Stream input)
        {
            BinaryReader reader = new BinaryReader(input);
            try
            {
                int num = reader.ReadInt32();
                int num4 = reader.ReadInt16();
                int num3 = reader.ReadInt16();
                int num2 = reader.ReadInt16();
                if ((reader.ReadInt16() != 0x100) & (reader.ReadInt32() != 0))
                {
                    throw new Exception("NotEcoTga");
                }
                int num9 = num2 - 1;
                for (int i = 0; i <= num9; i++)
                {
                    int num7 = reader.ReadInt16();
                    int num5 = reader.ReadInt16();
                    int num6 = reader.ReadInt32();
                    switch (num)
                    {
                        case 0:
                        case 1:
                            if (((num7 * num5) * 2) != num6)
                            {
                                throw new Exception("NotEcoTga");
                            }
                            break;

                        case 2:
                            if (((num7 * num5) * 4) != num6)
                            {
                                throw new Exception("NotEcoTga");
                            }
                            break;
                    }
                    if ((num2 - 1) > i)
                    {
                        reader.ReadInt32();
                    }
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
                return false;
            }
            finally
            {
            }
            return true;
        }

        public static System.Drawing.Image Load(System.IO.Stream stream)
        {
            SplitState splitState = null;
            return Load(stream, ref splitState);
        }

        public static System.Drawing.Image Load(System.IO.Stream stream, ref SplitState SplitState)
        {
            System.Drawing.Image image = null;
            try
            {
                image = System.Drawing.Image.FromStream(stream);
                SplitState = new SplitState(new int[] { image.Width }, new int[] { image.Height });
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                stream.Position = 0;
                try
                {
                    if (IsEcoTexture(stream))
                    {
                        image = LoadEcoTexture(stream, ref SplitState);
                        if (image == null)
                        {
                            SplitState = null;
                        }
                    }
                    else
                    {
                        image = LoadTga(stream);
                        if (image != null)
                        {
                            SplitState = new SplitState(new int[] { image.Width }, new int[] { image.Height });
                        }
                    }
                }
                catch (Exception exception2)
                {
                    ProjectData.SetProjectError(exception2);
                    Exception exception = exception2;
                    image = null;
                    ProjectData.ClearProjectError();
                }
                ProjectData.ClearProjectError();
            }
            return image;
        }

        private static Bitmap LoadBitmapStream(Stream Buffer, int Width, int Height, PixelFormat PixelFormat, [Optional, DefaultParameterValue(0)] int Attribute)
        {
            Bitmap bmap = new Bitmap(Width, Height);
            BinaryReader reader = new BinaryReader(Buffer);
            try
            {
                Point point2;
                Point point = new Point();
                PixelFormat format = PixelFormat;
                switch (format)
                {
                    case PixelFormat.Format24bppRgb:
                        {
                            int num29 = bmap.Height - 1;
                            for (int j = 0; j <= num29; j++)
                            {
                                int num30 = bmap.Width - 1;
                                for (int k = 0; k <= num30; k++)
                                {
                                    byte blue = reader.ReadByte();
                                    byte green = reader.ReadByte();
                                    byte red = reader.ReadByte();
                                    point2 = new Point(k, j);
                                    point = GetPixelPos(bmap, point2, Attribute);
                                    bmap.SetPixel(point.X, point.Y, Color.FromArgb(red, green, blue));
                                }
                            }
                            return bmap;
                        }
                    case PixelFormat.Format32bppArgb:
                        {
                            int num31 = bmap.Height - 1;
                            for (int m = 0; m <= num31; m++)
                            {
                                int num32 = bmap.Width - 1;
                                for (int n = 0; n <= num32; n++)
                                {
                                    byte num8 = reader.ReadByte();
                                    byte num10 = reader.ReadByte();
                                    byte num11 = reader.ReadByte();
                                    byte alpha = reader.ReadByte();
                                    point2 = new Point(n, m);
                                    point = GetPixelPos(bmap, point2, Attribute);
                                    bmap.SetPixel(point.X, point.Y, Color.FromArgb(alpha, num11, num10, num8));
                                }
                            }
                            return bmap;
                        }
                    case PixelFormat.Format16bppArgb1555:
                        {
                            int num33 = bmap.Height - 1;
                            for (int num12 = 0; num12 <= num33; num12++)
                            {
                                int num34 = bmap.Width - 1;
                                for (int num16 = 0; num16 <= num34; num16++)
                                {
                                    int num13;
                                    int num14 = reader.ReadInt16();
                                    if ((num14 & 0x8000) > 0)
                                    {
                                        num13 = 0xff;
                                    }
                                    else
                                    {
                                        num13 = 0;
                                    }
                                    int num18 = ((num14 & 0x7c00) >> 10) * 8;
                                    int num17 = ((num14 & 0x3e0) >> 5) * 8;
                                    int num15 = (num14 & 0x1f) * 8;
                                    point2 = new Point(num16, num12);
                                    point = GetPixelPos(bmap, point2, Attribute);
                                    Color color = Color.FromArgb(num13, num18, num17, num15);
                                    bmap.SetPixel(point.X, point.Y, color);
                                }
                            }
                            return bmap;
                        }
                    case PixelFormat.Format16bppRgb565:
                        {
                            int num35 = bmap.Height - 1;
                            for (int num19 = 0; num19 <= num35; num19++)
                            {
                                int num36 = bmap.Width - 1;
                                for (int num23 = 0; num23 <= num36; num23++)
                                {
                                    int num21 = reader.ReadInt16();
                                    int num20 = ((num21 & 0xf000) >> 12) * 0x11;
                                    int num25 = ((num21 & 0xf00) >> 8) * 0x11;
                                    int num24 = ((num21 & 240) >> 4) * 0x11;
                                    int num22 = (num21 & 15) * 0x11;
                                    Color color2 = Color.FromArgb(num20, num25, num24, num22);
                                    point2 = new Point(num23, num19);
                                    point = GetPixelPos(bmap, point2, Attribute);
                                    bmap.SetPixel(point.X, point.Y, color2);
                                }
                            }
                            return bmap;
                        }
                }
                if (format != PixelFormat.Format8bppIndexed)
                {
                    return bmap;
                }
                int num37 = bmap.Height - 1;
                for (int i = 0; i <= num37; i++)
                {
                    int num38 = bmap.Width - 1;
                    for (int num27 = 0; num27 <= num38; num27++)
                    {
                        int num28 = reader.ReadByte();
                        Color color3 = Color.FromArgb(num28, num28, num28);
                        point2 = new Point(num27, i);
                        point = GetPixelPos(bmap, point2, Attribute);
                        bmap.SetPixel(point.X, point.Y, color3);
                    }
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
                return null;
            }
            return bmap;
        }

        private static Bitmap LoadEcoTexture(Stream input, ref SplitState SplitState)
        {
            Bitmap bitmap;
            BinaryReader reader = new BinaryReader(input);
            input.Position = 0;
            try
            {
                int num = reader.ReadInt32();
                int width = reader.ReadInt16();
                int height = reader.ReadInt16();
                bitmap = new Bitmap(width, height);
                int num2 = reader.ReadInt16();
                if ((reader.ReadInt16() != 0x100) & (reader.ReadInt32() != 0))
                {
                    throw new Exception("NotEcoTga");
                }
                Bitmap[] imageList = new Bitmap[(num2 - 1) + 1];
                int[] numArray3 = new int[(num2 - 1) + 1];
                int[] numArray = new int[(num2 - 1) + 1];
                int[] numArray2 = new int[(num2 - 1) + 1];
                Point[] posList = new Point[(num2 - 1) + 1];
                posList[0] = new Point(0, 0);
                numArray3[0] = reader.ReadInt16();
                numArray[0] = reader.ReadInt16();
                numArray2[0] = reader.ReadInt32();
                switch (num)
                {
                    case 0:
                    case 1:
                        if (((numArray3[0] * numArray[0]) * 2) != numArray2[0])
                        {
                            throw new Exception("NotEcoTga");
                        }
                        break;

                    case 2:
                        if (((numArray3[0] * numArray[0]) * 4) != numArray2[0])
                        {
                            throw new Exception("NotEcoTga");
                        }
                        break;
                }
                int num9 = num2 - 1;
                for (int i = 1; i <= num9; i++)
                {
                    posList[i] = new Point((int)Math.Round((double)(((double)reader.ReadInt16()) / 256.0)), (int)Math.Round((double)(((double)reader.ReadInt16()) / 256.0)));
                    numArray3[i] = reader.ReadInt16();
                    numArray[i] = reader.ReadInt16();
                    numArray2[i] = reader.ReadInt32();
                    switch (num)
                    {
                        case 0:
                        case 1:
                            if ((numArray3[i] * numArray[i]) != numArray2[i])
                            {
                                throw new Exception("NotEcoTga");
                            }
                            break;

                        case 2:
                            if (((numArray3[i] * numArray[i]) * 4) != numArray2[i])
                            {
                                throw new Exception("NotEcoTga");
                            }
                            break;
                    }
                }
                int num11 = num2 - 1;
                for (int j = 0; j <= num11; j++)
                {
                    switch (num)
                    {
                        case 0:
                            {
                                MemoryStream buffer = new MemoryStream(reader.ReadBytes((numArray3[j] * numArray[j]) * 2));
                                imageList[j] = LoadBitmapStream(buffer, numArray3[j], numArray[j], PixelFormat.Format16bppArgb1555, 0x20);
                                break;
                            }
                        case 1:
                            {
                                MemoryStream stream3 = new MemoryStream(reader.ReadBytes((numArray3[j] * numArray[j]) * 2));
                                imageList[j] = LoadBitmapStream(stream3, numArray3[j], numArray[j], PixelFormat.Format16bppRgb565, 0x20);
                                break;
                            }
                        case 2:
                            {
                                MemoryStream stream2 = new MemoryStream(reader.ReadBytes((numArray3[j] * numArray[j]) * 4));
                                imageList[j] = LoadBitmapStream(stream2, numArray3[j], numArray[j], PixelFormat.Format32bppArgb, 0x20);
                                break;
                            }
                    }
                }
                SplitState = new SplitState(imageList, posList);
                Graphics graphics = Graphics.FromImage(bitmap);
                int num13 = Information.UBound(imageList, 1);
                for (int k = 0; k <= num13; k++)
                {
                    graphics.DrawImage(imageList[k], SplitState.Location(posList[k]));
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
                return null;
            }
            return bitmap;
        }

        private static Bitmap LoadTga(System.IO.Stream input)
        {
            Bitmap bitmap;
            BinaryReader fr = new BinaryReader(input);
            input.Position = 0;
            try
            {
                MemoryStream stream2;
                fr.BaseStream.Seek(2L, SeekOrigin.Begin);
                int num4 = fr.ReadByte();
                fr.BaseStream.Seek(9L, SeekOrigin.Current);
                int width = fr.ReadInt16();
                int height = fr.ReadInt16();
                int colorBit = fr.ReadByte();
                int attribute = fr.ReadByte();
                bitmap = new Bitmap(width, height);
                switch (num4)
                {
                    case 2:
                    case 3:
                        stream2 = new MemoryStream(fr.ReadBytes((int)Math.Round((double)((width * height) * (((double)colorBit) / 8.0)))));
                        break;

                    case 10:
                    case 11:
                        stream2 = new MemoryStream(RLEDecode(fr, width * height, colorBit));
                        break;

                    default:
                        throw new Exception("読めませんが何か？");
                }
                int num7 = colorBit;
                switch (num7)
                {
                    case 0x20:
                        return LoadBitmapStream(stream2, width, height, PixelFormat.Format32bppArgb, attribute);

                    case 0x18:
                        return LoadBitmapStream(stream2, width, height, PixelFormat.Format24bppRgb, attribute);

                    case 0x10:
                        return LoadBitmapStream(stream2, width, height, PixelFormat.Format16bppArgb1555, attribute);

                    case 8:
                        bitmap = LoadBitmapStream(stream2, width, height, PixelFormat.Format8bppIndexed, 0);
                        break;
                }
                if (num7 != 8)
                {
                    throw new Exception("CannotReadFormat");
                }
                bitmap = LoadBitmapStream(stream2, width, height, PixelFormat.Format8bppIndexed, 0);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
                return null;
            }
            finally
            {
            }
            return bitmap;
        }

        public void MoveSplit(bool axis, int index, int value)
        {
            this._SplitState.RemoveAt(axis, index);
            this._SplitState.Add(axis, value);
        }

        public void RemoveSplit(bool axis, int index)
        {
            this._SplitState.RemoveAt(axis, index);
        }

        private static byte[] RLEDecode(BinaryReader fr, int Size, int ColorBit)
        {
            byte[] buffer = new byte[((int)Math.Round((double)((Size * (((double)ColorBit) / 8.0)) - 1.0))) + 1];
            BinaryWriter writer = new BinaryWriter(new MemoryStream(buffer, true));
            try
            {
                while (writer.BaseStream.Position < writer.BaseStream.Length)
                {
                    int num = fr.ReadByte();
                    if ((num & 0x80) > 0)
                    {
                        byte[] buffer3 = fr.ReadBytes((int)Math.Round((double)(((double)ColorBit) / 8.0)));
                        int num4 = num & 0x7f;
                        for (int i = 0; i <= num4; i++)
                        {
                            writer.Write(buffer3);
                        }
                    }
                    else
                    {
                        int num5 = num & 0x7f;
                        for (int j = 0; j <= num5; j++)
                        {
                            writer.Write(fr.ReadBytes((int)Math.Round((double)(((double)ColorBit) / 8.0))));
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
                return null;
            }
            finally
            {
                writer.Close();
            }
            return buffer;
        }

        public void Save(string FileName)
        {
            if (LikeOperator.LikeString(Path.GetExtension(FileName).ToLower(), ".tga", CompareMethod.Binary))
            {
                FileStream output = new FileStream(FileName, FileMode.Create, FileAccess.Write);
                BinaryWriter wTga = new BinaryWriter(output);
                try
                {
                    wTga.Write(new byte[] { 0, 0 });
                    wTga.Write((short)2);
                    wTga.Write(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
                    wTga.Write((short)this.Image.Width);
                    wTga.Write((short)this.Image.Height);
                    wTga.Write(new byte[] { 0x20, 0 });
                    BitmapStreamWriter(wTga, (Bitmap)this.Image, PixelFormat.Format32bppArgb, 0);
                }
                finally
                {
                    wTga.Close();
                    output.Close();
                }
            }
            else
            {
                this.Image.Save(FileName);
            }
        }

        public void Save(string FileName, int ColorFormat)
        {
            FileStream output = new FileStream(FileName, FileMode.Create, FileAccess.Write);
            BinaryWriter wTga = new BinaryWriter(output);
            try
            {
                wTga.Write(ColorFormat);
                wTga.Write((short)this.Width);
                wTga.Write((short)this.Height);
                wTga.Write(this._SplitState.SplitCount);
                wTga.Write((short)0x100);
                wTga.Write(0);
                int num7 = this._SplitState.Column - 1;
                for (int i = 0; i <= num7; i++)
                {
                    int num8 = this._SplitState.Row - 1;
                    for (int k = 0; k <= num8; k++)
                    {
                        Point pos = new Point(i, k);
                        if (!((k == 0) & (i == 0)))
                        {
                            wTga.Write((short)(i * 0x100));
                            wTga.Write((short)(k * 0x100));
                        }
                        short num3 = (short)this._SplitState.Width_(pos);
                        short num2 = (short)this._SplitState.Height_(pos);
                        wTga.Write(num3);
                        wTga.Write(num2);
                        switch (ColorFormat)
                        {
                            case 0:
                            case 1:
                                wTga.Write((int)(((short)(num3 * num2)) * 2));
                                break;

                            case 2:
                                wTga.Write((int)(((short)(num3 * num2)) * 4));
                                break;
                        }
                    }
                }
                int num10 = this._SplitState.Column - 1;
                for (int j = 0; j <= num10; j++)
                {
                    int num11 = this._SplitState.Row - 1;
                    for (int m = 0; m <= num11; m++)
                    {
                        Point point2 = new Point(j, m);
                        switch (ColorFormat)
                        {
                            case 0:
                                BitmapStreamWriter(wTga, (Bitmap)this.Image_(point2), PixelFormat.Format16bppArgb1555, 0x20);
                                break;

                            case 1:
                                BitmapStreamWriter(wTga, (Bitmap)this.Image_(point2), PixelFormat.Format16bppRgb565, 0x20);
                                break;

                            case 2:
                                BitmapStreamWriter(wTga, (Bitmap)this.Image_(point2), PixelFormat.Format32bppArgb, 0x20);
                                break;
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                //throw new EcoSaveException();
            }
            finally
            {
                wTga.Close();
                output.Close();
            }
        }

        public int[] ColumnSplitLine
        {
            get
            {
                int[] numArray2 = new int[(this._SplitState.Column - 1) + 1];
                int num = 0;
                int num3 = this._SplitState.Column - 1;
                for (int i = 0; i <= num3; i++)
                {
                    Point pos = new Point(i, 0);
                    numArray2[i] = num + this._SplitState.Width_(pos);
                }
                return numArray2;
            }
        }

        public int Height_(Point Pos)
        {
            return this._SplitState.Height_(Pos);
        }

        public int Height
        {
            get
            {
                return this._Texture.Height;
            }
        }

        public System.Drawing.Image Image
        {
            get
            {
                return this._Texture;
            }
        }

        public System.Drawing.Image Image_(Point Pos)
        {
            System.Drawing.Image image = new Bitmap(this._SplitState.Width_(Pos), this._SplitState.Height_(Pos));
            Graphics.FromImage(image).DrawImage(this._Texture, 0, 0, this._SplitState.Rectangle(Pos), GraphicsUnit.Pixel);
            return image;
        }

        public bool IsEmpty
        {
            get
            {
                return (this._Texture == null);
            }
        }

        public int[] RowSplitLine
        {
            get
            {
                int[] numArray = new int[(this._SplitState.Row - 1) + 1];
                int num = 0;
                int num3 = this._SplitState.Row - 1;
                for (int i = 0; i <= num3; i++)
                {
                    Point pos = new Point(0, i);
                    numArray[i] = num + this._SplitState.Height_(pos);
                }
                return numArray;
            }
        }

        public int SplitCount
        {
            get
            {
                return this._SplitState.SplitCount;
            }
        }

        public int Width_(Point Pos)
        {
            return this._SplitState.Width_(Pos);
        }

        public int Width
        {
            get
            {
                return this._Texture.Width;
            }
        }
    }
}

