using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace SSPEditor
{
    /*  ECO使用的TGA并非标准TGA，需要自己解析
     * sTGAHeader与sTGAHeaderParts为TGA的头结构
     * 其中首位format共有0 1 2三种，分别对应16bppArgb1555, 16bppArgb4444, 32bppArgb8888 */
    //struct sTGAHeader
    //{
    //    //TGA文件头结构 
    //    public uint format;
    //    public ushort width;
    //    public ushort height;
    //    public ushort split;
    //    public ushort maxlen;
    //    public sTGAHeaderParts[] parts;
    //}
    //struct sTGAHeaderParts
    //{
    //    public ushort left;
    //    public ushort top;
    //    public ushort width;
    //    public ushort height;
    //    public uint size;
    //}

    class SspHeader//因为每一个技能的size固定，因此每个技能的偏移地址也是固定的，因此Header的内容同样是固定的，实际并不需要…
    {
        public uint offset =0;
        public ushort size = 0;
    }

    [Serializable]      // 表示该类可以被序列化  
    [XmlRoot("YGGSkill")] //XML根名
    public class SkillData : INotifyPropertyChanged, IComparable<SkillData>  //通知 & 可排序
    {
        //通知前台更新UI
        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private String name;
        public string Name { get => name; set { name = value; Notify("Name"); } }

        /*icon为0时，技能读取自身id同名tga文件
         * icon不为0时，读取icon对应tga文件
         * 点火等多个技能共用图标时用
         * 如果icon为0，并且技能自身也没有对应tga图标，则会使用默认被动技能图标*/
        private ushort id;
        public ushort Id { get => id; set { id = value; Notify("Id"); } }
        private ushort icon;
        public ushort Icon { get => icon; set { icon = value; Notify("Icon"); } }

        public string Description { get; set; }

        private byte lv;
        public byte Lv { get => lv; set { lv = value; Notify("Lv"); } }

        public byte MaxLv { get; set; }
        public byte Joblv { get; set; } //这个值并非jobLv，实际为无意义flag。

        //Range与Target2需要同时更新2个值
        private sbyte range;
        public sbyte Range { get => range; set { range = value; NHumei6= value; } }
        public byte Target { get; set; }
        private byte target2;
        public byte Target2 { get => target2; set { target2 = value; NHumei5 = value; }}

        public byte CastRange { get; set; }
        public byte EffectRange { get; set; }

        public byte Active { get; set; }
        
        //YGG专有
        public int CastTime { get; set; }
        public int Delay { get; set; }
        public int SingleCD { get; set; }

        public ushort Mp { get; set; }
        public ushort Sp { get; set; }
        public ushort Ep { get; set; }

        [XmlIgnore] //BitArray类不能被序列化，因此在序列化时需要忽略
        public BitArray SkillFlag { get; set; }
        [XmlIgnore]
        public BitArray EquipFlag { get; set; }


        [XmlAttribute(AttributeName = "SkillFlag")]  //将Dword类型的位存储数据拆成BitArray，此类将会代替SkillFlag被序列化
        public int SkillFlagValue  //此接口本质上操作的是BitArray SkillFlag
        {
            get
            {
                int[] intByBitArray = new int[1];
                SkillFlag.CopyTo(intByBitArray, 0);
                return (intByBitArray[0]);
            }
            set
            {
                int[] a = { value };
                SkillFlag = new BitArray(a);
            }
        }

        [XmlAttribute(AttributeName = "EquipFlag")] 
        public int EquipFlagValue
        {
            get
            {
                int[] intByBitArray = new int[1];
                EquipFlag.CopyTo(intByBitArray, 0);
                return (intByBitArray[0]);
            }
            set
            {
                int[] a = { value };
                EquipFlag = new BitArray(a);
            }
        }
        public short EFlag1 { get; set; } //特效标记
        public int EFlag2 { get; set; }//演习标记
        public int EFlag3 { get; set; } //演习凭依标记并非演习凭依用，同样是未知flag
        public ushort NHumei5 { get; set; }  //值与target2相同
        public short NHumei6 { get; set; }  //值与range相同
        public ushort NHumei7 { get; set; }//永远为0
        public ushort NAnim1 { get; set; }//发动动作
        public ushort NAnim2 { get; set; }//咏唱动作
        public ushort NAnim3 { get; set; }//施放动作
        
        public uint NHumei2 { get; set; } //没有用的值
        public uint Effect1 { get; set; } //咏唱者
        public uint Effect2 { get; set; } //咏唱目标
        public uint Effect3 { get; set; } //施放者
        public uint Effect4 { get; set; } //射出物体
        public uint Effect5 { get; set; } //施放目标
        public uint Effect6 { get; set; } //未知
        public uint Effect7 { get; set; } //狂暴、毒沼用
        public uint Effect8 { get; set; } //地面
        public uint Effect9 { get; set; } //物体

        public SkillData()
        {
            SkillFlag = new BitArray(32);
            EquipFlag = new BitArray(32);
            Name = "";
            Description = "";
        }

        public int CompareTo(SkillData other)//对比函数，用于List内建排序。先按照ID，再按照技能等级排序。
        {
            if (other == null)
                return 1;
            int value = this.Id - other.Id;
            if (value == 0)
                value = this.Lv - other.Lv;
            return value;
        }

        public SkillData Clone()
        {
            SkillData newskill = (SkillData)MemberwiseClone();
            newskill.SkillFlag = new BitArray(SkillFlag);
            newskill.EquipFlag = new BitArray(EquipFlag);
            return newskill;//object类的MemberwiseClone();实现克隆。由于是保护的方法，所以需要给自己的类添加Clone方法间接调用而不能直接使用。
        }

        public override string ToString()
        {
            return Name;
        }
    }


    public partial class MainWindow : Window
    {
        TGA.LoadTGA tga = new TGA.LoadTGA();//初始化tga文件处理器

        public SkillData ClipBoard;//全局变量，用于实现剪贴板
        public MainWindow()
        {
            InitializeComponent();
            InitSSP(Directory.GetCurrentDirectory() + @"\data\effect\effect.ssp");//载入ssp文件
        }

       //[System.Runtime.InteropServices.DllImport("gdi32.dll")]
        //public static extern bool DeleteObject(IntPtr hObject); //载入图标时，将BitMap转为image控件使用的Source后，需要使用DeleteObject释放内存。


        //开个加载事件！
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tga.Open("data\\sprite\\skillicon\\skillicon.hed");
            TGA.LoadTGA.Files = tga.headers;
        }

        public void InitSSP(string path)//读取SSP并初始化技能列表
        {
            List<SspHeader> header = new List<SspHeader>();
            List<SkillData> skill = new List<SkillData>();
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                BinaryReader br = new BinaryReader(fs);
                for (int i = 0; i < 30000; i++) //技能最多可以有32767个，不是10000个（日服技能已经超过1w个）
                {
                    SspHeader newHead = new SspHeader()
                    {
                        offset = br.ReadUInt32(),
                        size = 0x02d8
                    };
                    if (newHead.offset == 0)
                        break;
                    header.Add(newHead);
                }

                fs.Position = 0x2FFF0;
                if (br.ReadByte() != 0x2A)
                    throw new Exception("加载技能列表失败…");

                foreach (SspHeader i in header)
                {
                    fs.Position = i.offset;
                    SkillData tskill = new SkillData() { Id = br.ReadUInt16() };
                    if (tskill.Id == 0)
                        continue;
                    tskill.Icon = br.ReadUInt16();
                    //tskill.Name = br.ReadChars(116);
                    string buf = Encoding.Unicode.GetString(br.ReadBytes(116));//名称后12字节被设定为castTime、delay、singleCD
                    tskill.Name = buf.Remove(buf.IndexOf('\0'));

                    tskill.CastTime = br.ReadInt32();
                    tskill.Delay = br.ReadInt32();
                    tskill.SingleCD = br.ReadInt32();

                    buf = Encoding.Unicode.GetString(br.ReadBytes(512));
                    tskill.Description = buf.Remove(buf.IndexOf('\0')).Replace("$R", Environment.NewLine);

                    tskill.Active = br.ReadByte();
                    tskill.MaxLv = br.ReadByte();
                    tskill.Lv = br.ReadByte();
                    tskill.Joblv = br.ReadByte();
                    tskill.Mp = br.ReadUInt16();
                    tskill.Sp = br.ReadUInt16();
                    tskill.Ep = br.ReadUInt16();
                    tskill.Range = br.ReadSByte();
                    tskill.Target = br.ReadByte();
                    tskill.Target2 = br.ReadByte();
                    tskill.EffectRange = br.ReadByte();
                    tskill.EquipFlagValue = br.ReadInt32();
                    tskill.NHumei2 = br.ReadUInt32();
                    tskill.SkillFlagValue = br.ReadInt32();
                    tskill.EFlag2 = br.ReadInt32();   

                    tskill.EFlag3 = br.ReadInt32();
                    tskill.EFlag1 = br.ReadInt16();   
                    tskill.NHumei5 = br.ReadUInt16();   //类型
                    tskill.NHumei6 = br.ReadInt16();    //射程
                    tskill.NHumei7 = br.ReadUInt16();


                    tskill.Effect1 = br.ReadUInt32();
                    tskill.Effect2 = br.ReadUInt32();
                    tskill.Effect3 = br.ReadUInt32();

                    tskill.Effect4 = br.ReadUInt32();
                    tskill.Effect5 = br.ReadUInt32();
                    tskill.Effect6 = br.ReadUInt32();

                    tskill.Effect7 = br.ReadUInt32();
                    tskill.Effect8 = br.ReadUInt32();
                    tskill.Effect9 = br.ReadUInt32();

                    tskill.NAnim1 = br.ReadUInt16();
                    tskill.NAnim2 = br.ReadUInt16();
                    tskill.NAnim3 = br.ReadUInt16();
                    tskill.PropertyChanged += new PropertyChangedEventHandler(Skill_PropertyChanged);

                    skill.Add(tskill);
                }
                fs.Close();
                br.Close();

                listView1.ItemsSource = skill;
                Tips.Text = "共加载" + skill.Count.ToString()+ "个技能";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);//八成是没找到文件…
            }
        }

        public bool LoadTGA()//加载TGA图标
        {
            try
            {
                //如果没有这个文件那基本意味着没有解冻，大多数图标都无法正常显示。
                //if (!File.Exists(Directory.GetCurrentDirectory() + @"\unpack\data\sprite\skillicon\skillicon.dat\SI_0100.TGA"))
                //    return false;
                //icon为0时，使用技能自己的同名图标
                ushort icon = ((SkillData)listView1.SelectedItem).Icon;
                if (icon == 0)
                    icon = ((SkillData)listView1.SelectedItem).Id;

                System.Drawing.Image img = TGA.TgaFactory.Instance.ShowTGA(icon, tga);
                //没有图标时，读取默认的SI_0100.TGA
                if (img == null)
                    img = TGA.TgaFactory.Instance.ShowTGA(100, tga);
                //SI_0100.TGA都没有的话，那就是真没有了。
                if (img == null)
                    return false;
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new MemoryStream(ms.ToArray()); 
                bi.EndInit();
                ImageIcon.Source = bi;
                ms.Close();


                //残忍的注释老夏的代码！！
                //string iconpath = Directory.GetCurrentDirectory() + @"\unpack\data\sprite\skillicon\skillicon.dat\SI_" + icon.ToString() + ".TGA";

                ////图标？不存在的
                //if (!File.Exists(Directory.GetCurrentDirectory() + @"\unpack\data\sprite\skillicon\skillicon.dat\SI_" + icon.ToString() + ".TGA"))
                //    iconpath = Directory.GetCurrentDirectory() + @"\unpack\data\sprite\skillicon\skillicon.dat\SI_0100.TGA";

                //Bitmap bmp = new Bitmap(1, 1);
                //sTGAHeader th;
                //FileStream fs = new FileStream(iconpath, FileMode.Open);
                //BinaryReader br = new BinaryReader(fs);
                //th.format = br.ReadUInt32();
                //th.width = br.ReadUInt16();
                //th.height = br.ReadUInt16();
                //th.split = br.ReadUInt16();
                //th.maxlen = br.ReadUInt16();
                //th.parts = new sTGAHeaderParts[th.split];
                //for (int i = 0; i < th.split; i++)
                //{
                //    th.parts[i].left = br.ReadUInt16();
                //    th.parts[i].top = br.ReadUInt16();
                //    th.parts[i].width = br.ReadUInt16();
                //    th.parts[i].height = br.ReadUInt16();
                //    th.parts[i].size = br.ReadUInt32();
                //}
                ////Ints用于32bppArgb8888，shorts用于16bppArgb1555。16bppArgb4444为非标准格式，因此手动处理成32bppArgb8888进行绘制。
                //int[] Ints = new int[th.width * th.height];
                //short[] Shorts = new short[th.width * th.height];

                //int ByteCount = 0;
                //for (int i = 0; i < th.split; i++)
                //    for (int j = 0; j < th.parts[i].height; j++)
                //        for (int k = 0; k < th.parts[i].width; k++)
                //            switch (th.format)
                //            {
                //                /*32bppArgb8888和16bppArgb1555可以直接读入数组然后用系统内常规方式绘制，就很爽
                //                 * 16bppArgb4444在读入16位像素后，需要扩展4bit的Alpha通道和红绿蓝三色信息到8bit。
                //                 * 注意：4bit下的F(15/16)在8bit中对应F0(240/256)而不是0F！要整体乘以0x10。*/
                //                case 2:
                //                    Ints[ByteCount++] = br.ReadInt32();
                //                    break;
                //                case 1:
                //                    ushort b = br.ReadUInt16();//就很蠢…
                //                    Ints[ByteCount] += b / 0x1000 * 0x10000000;
                //                    Ints[ByteCount] += b % 0x1000 / 0x100 * 0x100000;
                //                    Ints[ByteCount] += b % 0x100 / 0x10 * 0x1000;
                //                    Ints[ByteCount] += b % 0x10 * 0x10;
                //                    ByteCount++;
                //                    break;
                //                case 0:
                //                    Shorts[ByteCount++] = br.ReadInt16();
                //                    break;
                //                default:
                //                    break;
                //            }
                //br.Close();
                //fs.Close();
                //switch (th.format)
                //{
                //    case 2:
                //    case 1:
                //        //16bppArgb4444被处理为32bppArgb8888后绘制
                //        bmp = new Bitmap(th.width, th.height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                //        break;
                //    case 0:
                //        bmp = new Bitmap(th.width, th.height, System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);
                //        break;
                //    default:
                //        break;
                //}

                //Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                //System.Drawing.Imaging.BitmapData bmpData;

                ////锁定内存 
                //bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly, bmp.PixelFormat);

                ////得到内存指针
                //IntPtr ptr = bmpData.Scan0;

                ////拷贝数据到指针内存 
                //switch (th.format)
                //{
                //    case 2:
                //    case 1:
                //        System.Runtime.InteropServices.Marshal.Copy(Ints, 0, ptr, th.width * th.height);
                //        break;
                //    case 0:
                //        System.Runtime.InteropServices.Marshal.Copy(Shorts, 0, ptr, th.width * th.height);
                //        break;
                //    default:
                //        break;
                //}
                ////解锁 
                //bmp.UnlockBits(bmpData);
                //ptr = bmp.GetHbitmap();

                ////Bitmap转BitmapSource，Image控件不能直接使用BitMap类型
                //BitmapSource result =
                //    System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                //        ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                ////施放资源
                //DeleteObject(ptr);

                //ImageIcon.Source = result;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        private void listView1_SizeChanged(object sender, SizeChangedEventArgs e) //大小改变时自动更新list1的列宽
        {
            try
            {
                gridView1.Columns[0].Width = listView1.ActualWidth - gridView1.Columns[1].Width - gridView1.Columns[2].Width - 25;
            }
            catch (Exception)
            {
                //啥都不做，懒
            }

        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadTGA();
        }

        private void TextBoxName_GotFocus(object sender, RoutedEventArgs e)
        {
            //获得焦点时，自适应高度（auto）显示全部技能名
            TextBoxName.Height = Double.NaN;
        }

        private void TextBoxName_LostFocus(object sender, RoutedEventArgs e)
        {
            //失去焦点时，收缩回原始高度节约空间
            TextBoxName.Height = 23;
        }

        private void Skill_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //当Id或Icon更新通知时，重新加载图标
            if (e.PropertyName == "Id" || e.PropertyName == "Icon")
                LoadTGA();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//保存SSP文件
        {
            string filename = Directory.GetCurrentDirectory() + @"\data\effect\effect.ssp";
            FileStream fs;
            BinaryWriter bw;
            //创建文件
            try
            {
                fs = new FileStream(filename, FileMode.Create);
                bw = new BinaryWriter(fs,Encoding.Unicode);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message + "无法创建文件");
                return;
            }
            try
            {
                //头文件部分直接自己计算就可以出来。完全没有存在的必要。
                for (int i = 0; i < listView1.Items.Count; i++)
                    bw.Write((uint)(0x30000 + 0x02D8 * i));
                fs.Position = 0x20000;
                for (int i = 0; i < listView1.Items.Count; i++)
                    bw.Write((ushort)0x02D8);//每一个SkillData长度是固定的

                fs.Position = 0x2FFF0;
                bw.Write((byte)0x2A);
                
                fs.Position = 0x30000;

                foreach (SkillData tskill in listView1.Items)
                {
                    bw.Write(tskill.Id);
                    bw.Write(tskill.Icon);
                    char[] cname = tskill.Name.ToCharArray();
                    Array.Resize<char>(ref cname, 58); //char类型占用2字节，因此116/2=58。此外，这里可以不声明<char>，直接使用Array.Resize(ref cname, 58);
                    bw.Write(cname);

                    bw.Write(tskill.CastTime);
                    bw.Write(tskill.Delay);
                    bw.Write(tskill.SingleCD);

                    char[] cdes = tskill.Description.Replace(Environment.NewLine, "$R").ToCharArray();
                    Array.Resize<char>(ref cdes, 256);
                    bw.Write(cdes);

                    bw.Write(tskill.Active);
                    bw.Write(tskill.MaxLv);
                    bw.Write(tskill.Lv);
                    bw.Write(tskill.Joblv);
                    bw.Write(tskill.Mp);
                    bw.Write(tskill.Sp);
                    bw.Write(tskill.Ep);
                    bw.Write(tskill.Range);
                    bw.Write(tskill.Target);
                    bw.Write(tskill.Target2);
                    bw.Write(tskill.EffectRange);
                    bw.Write(tskill.EquipFlagValue);
                    bw.Write(tskill.NHumei2);
                    bw.Write(tskill.SkillFlagValue);
                    bw.Write(tskill.EFlag2);
                    bw.Write(tskill.EFlag3);
                    bw.Write(tskill.EFlag1);
                    bw.Write(tskill.NHumei5);
                    bw.Write(tskill.NHumei6);
                    bw.Write(tskill.NHumei7);

                    bw.Write(tskill.Effect1);
                    bw.Write(tskill.Effect2);
                    bw.Write(tskill.Effect3);
                    bw.Write(tskill.Effect4);
                    bw.Write(tskill.Effect5);
                    bw.Write(tskill.Effect6);
                    bw.Write(tskill.Effect7);
                    bw.Write(tskill.Effect8);
                    bw.Write(tskill.Effect9);

                    bw.Write(tskill.NAnim1);
                    bw.Write(tskill.NAnim2);
                    bw.Write(tskill.NAnim3);
                }
                bw.Close();
                fs.Close();
                MessageBox.Show("保存成功");

            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message + "写入失败");
                return;
            }
        }

        private void ButtonSort_Click(object sender, RoutedEventArgs e)
        {
            //排序，然后刷新listview
            ((List<SkillData>)listView1.ItemsSource).Sort();
            listView1.Items.Refresh();
        }

        private void MenuItemCut_Click(object sender, RoutedEventArgs e)
        {
            Tips.Text = "技能已剪切至剪贴板";
            ClipBoard = ((SkillData)listView1.SelectedItem).Clone();
            ((List<SkillData>)listView1.ItemsSource).RemoveAt(listView1.SelectedIndex);
            listView1.Items.Refresh();
        }

        private void MenuItemCopy_Click(object sender, RoutedEventArgs e)
        {
            Tips.Text = "技能已复制至剪贴板";
            ClipBoard = ((SkillData)listView1.SelectedItem).Clone();
        }

        private void MenuItemPaste_Click(object sender, RoutedEventArgs e)
        {
            Tips.Text = "已粘贴剪贴板中的技能";
            ((List<SkillData>)listView1.ItemsSource)[listView1.SelectedIndex] = ClipBoard.Clone();
            listView1.Items.Refresh();

        }

        private void MenuItemInsertFromClip_Click(object sender, RoutedEventArgs e)
        {
            Tips.Text = "已插入剪贴板中的技能";
            ((List<SkillData>)listView1.ItemsSource).Insert (listView1.SelectedIndex,ClipBoard.Clone());
            listView1.Items.Refresh();
        }

        private void MenuItemCopySkill_Click(object sender, RoutedEventArgs e)
        {
            Tips.Text = "已创建技能副本";
            ((List<SkillData>)listView1.ItemsSource).Insert(listView1.SelectedIndex, ((SkillData)listView1.SelectedItem).Clone());
            listView1.Items.Refresh();
        }

        private void MenuItemInsertNew_Click(object sender, RoutedEventArgs e)
        {
            Tips.Text = "已插入空白技能";
            ((List<SkillData>)listView1.ItemsSource).Insert(listView1.SelectedIndex, new SkillData());
            listView1.Items.Refresh();
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            Tips.Text = "已移除"+ listView1.SelectedItems.Count.ToString()+"个技能";
            foreach (SkillData i in listView1.SelectedItems)
            {
                ((List<SkillData>)listView1.ItemsSource).Remove(i);
            }
            listView1.Items.Refresh();
        }

        private void MenuItemExport_Click(object sender, RoutedEventArgs e)
        {
            //将选中所有技能序列化，导出到单一的Skill.ssl文件，文件中包含选中的所有技能。
            Tips.Text = "已导出" + listView1.SelectedItems.Count.ToString() + "个技能";
            List<SkillData> temp = listView1.SelectedItems.Cast<SkillData>().ToList();  //SelectedItems是IList类型，需要转换为List<SkillData>类型。
            FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "/Skill.ssl", FileMode.Create);
            XmlSerializer xs = new XmlSerializer(typeof(List<SkillData>));
            xs.Serialize(fs, temp);
            fs.Close();
        }
        private void MenuItemExportSingle_Click(object sender, RoutedEventArgs e)
        {
            //将选中所有技能序列化，分别导出到独立的ss文件，文件名为技能ID+技能名+等级。
            foreach (SkillData i in listView1.SelectedItems)
            {
                FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "/"+ i.Id+i.Name+i.Lv+".ss", FileMode.Create);
                XmlSerializer xs = new XmlSerializer(typeof(SkillData));
                xs.Serialize(fs, i);
                fs.Close();
            }
            Tips.Text = "已导出" + listView1.SelectedItems.Count.ToString() + "个技能";
        }

        private void MenuItemImport_Click(object sender, RoutedEventArgs e)
        {
            //读取技能文件并将其实例化，然后插入列表。同时兼容ss与ssl文件。
            OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {
                Filter = "YGG技能文件(*.ssl;*ss)|*.ssl;*ss|所有文件 (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true,
                Multiselect = true //可以多选
            };
            if (openFileDialog1.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog1.FileNames)
                {
                    try
                    {
                        FileStream fs = new FileStream(filename, FileMode.Open);

                        if (filename.EndsWith(".ssl"))//如果是技能列表文件
                        {
                            XmlSerializer xs = new XmlSerializer(typeof(List<SkillData>));
                            List<SkillData> listPers = xs.Deserialize(fs) as List<SkillData>;
                            if (listPers != null)
                                for (int i = 0; i < listPers.Count; i++)
                                    ((List<SkillData>)listView1.ItemsSource).Insert(listView1.SelectedIndex, listPers[i]);
                            Tips.Text = "已导入" + listPers.Count.ToString() + "个技能";
                        }
                        else//如果是单个技能文件
                        {
                            XmlSerializer xs = new XmlSerializer(typeof(SkillData));
                            SkillData p = xs.Deserialize(fs) as SkillData;   //这一行与下一行可以缩写为 if (xs.Deserialize(fs) is SkillData p)
                            if (p != null)
                            {
                                ((List<SkillData>)listView1.ItemsSource).Insert(listView1.SelectedIndex, p);
                                Tips.Text = "已导入1个技能";
                            }
                        }
                        fs.Close();
                    }
                    catch (IOException)
                    {
                        Tips.Text = "打不开文件";
                        return;
                    }
                }
            }
            listView1.Items.Refresh();
        }
        //搜索
        private void ButtonSearch_Click_1(object sender, RoutedEventArgs e)
        {
            int start = listView1.SelectedIndex;//记录起始位置
            for (int i= listView1.SelectedIndex + 1; i < listView1.Items.Count; i++)//从下一个技能开始搜到结尾
                if (((SkillData)listView1.Items[i]).Name.Contains(TextBoxSearch.Text))
                {
                    listView1.SelectedIndex = i;
                    listView1.ScrollIntoView(listView1.Items[i]);
                    Tips.Text = "已定位至技能"+ ((SkillData)listView1.Items[i]).Name;
                    return;
                }
            for (int i =0; i < start; i++)//如果到了结尾还没搜到，则返回开头一直搜索回到起始位置
                if (((SkillData)listView1.Items[i]).Name.Contains(TextBoxSearch.Text))
                {
                    listView1.SelectedIndex = i;
                    listView1.ScrollIntoView(listView1.Items[i]);
                    Tips.Text = "已定位至技能" + ((SkillData)listView1.Items[i]).Name;
                    return;
                }

            //循环可以合并成这样for (int i= listView1.SelectedIndex + 1; i !=listView1.SelectedIndex + 1; i++,i %=listView1.Items.Count )，但是读起来就会很难过。
            //如果这还没搜到，那就是没了
            Tips.Text = "找不到技能";
        }

        private void listView1_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (ClipBoard == null)
            {
                MenuItemPaste.IsEnabled = false;
                MenuItemPasteInsert.IsEnabled = false;
            }
            else
            {
                MenuItemPaste.IsEnabled = true;
                MenuItemPasteInsert.IsEnabled = true;
            }

        }
    }
}
