using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SagaDB.Map;

namespace APathfinding
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        Point src = new Point(-1, -1);

        MapInfoFactory mapinfofactory = new MapInfoFactory();
        SagaMap.Manager.MapManager mapmanager = new SagaMap.Manager.MapManager();

        public class MapInfoObject
        {
            private uint mapid;

            public uint MapID
            {
                get { return mapid; }
                set { mapid = value; }
            }
            private string mapname;

            public string MapName
            {
                get { return mapname; }
                set { mapname = value; }
            }
            public string DisplayMember { get { return mapid.ToString() + "," + mapname; } }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            OD.Filter = "*.zip|*.zip";
            if (OD.ShowDialog() == DialogResult.OK)
            {
                mapinfofactory.Init(OD.FileName);
                mapmanager.MapInfos = mapinfofactory.MapInfo;
                mapmanager.LoadMaps();
                mapinfofactory.ApplyMapObject();
                listBox1.Items.Clear();
                SagaMap.Manager.MapManager.Instance.Maps.Clear();
                listBox1.DisplayMember = "DisplayMember";
                listBox1.ValueMember = "MapInfoObject.MapID";
                listBox1.BeginUpdate();
                listBox1.DataSource = null;
                List<MapInfoObject> list = new List<MapInfoObject>();
                foreach (uint i in mapinfofactory.MapInfo.Keys)
                {
                    MapInfoObject mio = new MapInfoObject();
                    mio.MapID = i;
                    mio.MapName = mapinfofactory.MapInfo[i].name;
                    list.Add(mio);
                    SagaMap.Manager.MapManager.Instance.Maps.Add(i, new SagaMap.Map(mapinfofactory.MapInfo[i]));
                }
                listBox1.DataSource = list;

                listBox1.EndUpdate();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MapInfo info = mapinfofactory.MapInfo[uint.Parse(listBox1.SelectedValue.ToString())];
            Bitmap bmp = new Bitmap(info.width, info.height);
            Graphics g = Graphics.FromImage(bmp);
            SolidBrush back = new SolidBrush(Color.Black);
            g.FillRectangle(back, 0, 0, info.width, info.height);
            g.Dispose();
            for (int i = 0; i < info.width; i++)
            {
                for (int j = 0; j < info.height; j++)
                {
                    if (info.walkable[i, j] == 2)
                    {
                        bmp.SetPixel(i, j, Color.White);
                    }
                }
            }
            listBox2.Items.Clear();
            if (mapinfofactory.MapObjects != null)
            {
                if (mapinfofactory.MapObjects.ContainsKey(listBox1.SelectedValue.ToString()))
                {
                    foreach (SagaDB.Map.MapObject i in mapinfofactory.MapObjects[listBox1.SelectedValue.ToString()])
                    {
                        listBox2.Items.Add(string.Format("{0},0x{1:X2}", i.Name, i.Flag.Value));
                    }
                }
            }
            pictureBox1.Image = bmp;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs arg = (MouseEventArgs)e;
            if (src.X == -1)
            {
                src = new Point(arg.X, arg.Y);
            }
            else
            {
                SagaDB.Actor.ActorMob mob = new SagaDB.Actor.ActorMob();
                mob.MapID = uint.Parse(listBox1.SelectedItem.ToString());
                SagaMap.Map map = new SagaMap.Map(mapinfofactory.MapInfo[mob.MapID]);
                SagaMap.Mob.MobAI ai = new SagaMap.Mob.MobAI(mob, true);
                ai.map = map;
                List<SagaMap.Mob.MapNode> path = ai.FindPath((byte)src.X, (byte)src.Y, (byte)arg.X, (byte)arg.Y);
                if (path != null)
                {
                    foreach (SagaMap.Mob.MapNode i in path)
                    {
                        ((Bitmap)pictureBox1.Image).SetPixel(i.x, i.y, Color.Red);
                    }
                }
                pictureBox1.Refresh();
                src = new Point(-1, -1);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OD.Filter = "MapObjects.dat|MapObjects.dat";
            if (OD.ShowDialog() == DialogResult.OK)
            {
                //SagaDB.Map.MapInfoFactory.Instance.LoadMapObjects(OD.FileName);
                mapinfofactory.LoadMapObjects(OD.FileName);
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                MapObject obj = mapinfofactory.MapObjects[listBox1.SelectedValue.ToString()][listBox2.SelectedIndex];
                Bitmap bmp = (Bitmap)(pictureBox1.Image);
                for (int i = 0; i < obj.Width; i++)
                {
                    int[,][] matrix = obj.PositionMatrix;
                    for (int j = 0; j < obj.Height; j++)
                    {
                        int x = obj.X + matrix[i, j][0];
                        int y = obj.Y + matrix[i, j][1];
                        if (x < bmp.Width && y < bmp.Height && x >= 0 && y >= 0)
                            bmp.SetPixel(x, y, Color.Red);
                    }
                }
                pictureBox1.Refresh();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SagaDB.Map.MapInfoFactory.Instance.ApplyMapObject();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count > 0)
            {
                Bitmap bmp = (Bitmap)(pictureBox1.Image);
                foreach (var item in mapinfofactory.MapObjects[listBox1.SelectedValue.ToString()])
                {
                    for (int i = 0; i < item.Width; i++)
                    {
                        int[,][] matrix = item.PositionMatrix;
                        for (int j = 0; j < item.Height; j++)
                        {
                            int x = item.X + matrix[i, j][0];
                            int y = item.Y + matrix[i, j][1];
                            if (x < bmp.Width && y < bmp.Height && x >= 0 && y >= 0)
                                bmp.SetPixel(x, y, Color.Red);
                        }
                    }
                }
                pictureBox1.Refresh();
            }
        }
    }
}
