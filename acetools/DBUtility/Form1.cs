using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DBUtility
{
    public partial class Form1 : Form
    {
        string LastItemCsv_FirstLine = "#ItemID,PictID,IconID,名前,種別ID,値段,重量,容量,装備時容量,憑依重量,修理,強化ID,処理フラグ0,Event,レシピ可,染色,ストック,両手,使用時消滅,色,耐久度,装備時変更職業ＩＤ,初期スロット数,最大スロット数,イベントＩＤ,エフェクトＩＤ,発動Skill,使用可能Skill,パッシブスキル,憑依時可能Skill,憑依パッシブSkill,ターゲットタイプ,発動タイプ,射程,効果時間,効果範囲,効果次元,キャスト,ディレイ,HP上昇,MP上昇,SP上昇,最大重量上昇,最大容量上昇,移動力上昇,STR,MAG,VIT,DEX,AGI,INT,LUK,CHA,物理攻撃力(叩),物理攻撃力(斬),物理攻撃力(突),魔法攻撃力,物理防御力,魔法防御力,近命中力,遠命中力,魔法命中力,近回避力,遠回避力,魔回避力,クリ力,クリ回避,回復力,魔法回復力,無,火,水,風,土,光,闇,毒,石化,麻痺,睡眠,沈黙,鈍足,混乱,凍結,気絶,人間,天使,悪魔,その他,男,女,LV,装備可STR,装備可MAG,装備可VIT,装備可DEX,装備可AGI,装備可INT,装備可LUK,装備可CHA,ノービス,ソードマン,ブレマス,バウンテ,フェンサ,ナイト,ダークスト,スカウト,アサシン,コマンド,アーチャ,ストライ,ガンナー,ウィザ,ソーサラ,セージ,シャーマン,エレメン,エンチャン,ウァテス,ドルイド,バード,ウォーロ,カバリ,ネクロマ,タタラベ,ブラスミ,マシンナ,ファーマ,アルケミ,マリオネ,レンジャ,エクスプ,トレジャ,マーチャ,トレーダ,ギャンブラー,東軍,西軍,南軍,北軍,所属５,所属６,所属７,所属８,ブリーダー,ガーデナー,フラグ３,フラグ４,フラグ５,フラグ６,フラグ７,変身ＩＤ,封印モンＩＤ,片手モーションID,両手モーションID,右手憑依者モーション,左手憑依者モーション,胸アクセ憑依者モーション,鎧憑依者モーション,アイテム欄コメント,国内ITEM,国内ITEM2,LV,";

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_EncConvFolder_Click(object sender, EventArgs e)
        {
            if (FD.ShowDialog() == DialogResult.OK)
            {
                string[] files = System.IO.Directory.GetFiles(FD.SelectedPath);
                foreach (string i in files)
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(i, Encoding.GetEncoding(tb_SrcEncUtil.Text));
                    string content = sr.ReadToEnd();
                    sr.Close();
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(i, false, Encoding.GetEncoding(tb_DstEncUtil.Text));
                    sw.Write(content);
                    sw.Flush();
                    sw.Close();
                }
                MessageBox.Show("Finished");
            }
        }

        private void btn_EncConvFile_Click(object sender, EventArgs e)
        {
            OD.Filter = "";
            if (OD.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(OD.FileName, Encoding.GetEncoding(tb_SrcEncUtil.Text));
                string content = sr.ReadToEnd();
                sr.Close();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(OD.FileName, false, Encoding.GetEncoding(tb_DstEncUtil.Text));
                sw.Write(content);
                sw.Flush();
                sw.Close();
                MessageBox.Show("Finished");
            }
        }

        private void btn_SrcTrans_Click(object sender, EventArgs e)
        {
            OD.Filter = "*.csv|*.csv";
            if (OD.ShowDialog() == DialogResult.OK)
            {
                tb_SrcTrans.Text = OD.FileName;
                System.IO.StreamReader sr = new System.IO.StreamReader(OD.FileName, Encoding.GetEncoding(tb_SrcEncTrans.Text));
                string line="";
                line = sr.ReadLine();
                if (line.IndexOf("#") != -1)
                    line = line.Remove(line.IndexOf("#"));
                while (line == "")
                {
                    line = sr.ReadLine();
                    if (line.IndexOf("#") != -1)
                        line = line.Remove(line.IndexOf("#"));                
                }
                sr.Close();
                string[] para = line.Split(',');
                string res = "";
                int count = 0;
                foreach (string i in para)
                {
                    res += (i + "(" + count.ToString() + "),");
                    count++;
                }
                tb_SrcIndexPre.Text = res;
            }
        }

        private void btn_TarTrans_Click(object sender, EventArgs e)
        {
            OD.Filter = "*.csv|*.csv";
            if (OD.ShowDialog() == DialogResult.OK)
            {
                tb_TarTrans.Text = OD.FileName;
                System.IO.StreamReader sr = new System.IO.StreamReader(OD.FileName, Encoding.GetEncoding(tb_DstEncTrans.Text));
                string line = "";
                line = sr.ReadLine();
                if (line.IndexOf("#") != -1)
                    line = line.Remove(line.IndexOf("#"));
                while (line == "")
                {
                    line = sr.ReadLine();
                    if (line.IndexOf("#") != -1)
                        line = line.Remove(line.IndexOf("#"));
                }
                sr.Close();
                string[] para = line.Split(',');
                string res = "";
                int count = 0;
                foreach (string i in para)
                {
                    res += (i + "(" + count.ToString() + "),");
                    count++;
                }
                tb_TarIndexPre.Text = res;
            }
        }

        private void btn_OutTrans_Click(object sender, EventArgs e)
        {
             SD.Filter = "*.csv|*.csv";
             if (SD.ShowDialog() == DialogResult.OK)
             {
                 tb_OutTrans.Text = SD.FileName;
             }
        }

        private void btn_StartTrans_Click(object sender, EventArgs e)
        {
            if (tb_SrcTrans.Text == "" || tb_TarTrans.Text == "" || tb_OutTrans.Text == "")
            {
                MessageBox.Show("Please choose the right path of the files!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Dictionary<string, List<string>> names = new Dictionary<string, List<string>>();

            System.IO.StreamReader sr = new System.IO.StreamReader(tb_SrcTrans.Text, Encoding.GetEncoding(tb_SrcEncTrans.Text));
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                string[] paras;
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');

                    string id = paras[0];
                    string[] index = tb_SrcIndex.Text.Split(',');
                    names.Add(id, new List<string>());
                    foreach(string i in index)
                    {
                        int tmp = int.Parse(i);
                        if (!names[id].Contains(paras[tmp]))
                            names[id].Add(paras[tmp]);
                    }
                }
                catch { }
            }
            sr.Close();

            sr = new System.IO.StreamReader(tb_TarTrans.Text, Encoding.GetEncoding(tb_DstEncTrans.Text));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(tb_OutTrans.Text, false, Encoding.GetEncoding(tb_OutEncTrans.Text));
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                string[] paras;
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                    {
                        sw.WriteLine(line);
                        continue;
                    }
                    paras = line.Split(',');

                    string id = paras[0];
                    if (names.ContainsKey(id))
                    {
                        string[] index = tb_TarIndex.Text.Split(',');
                        for (int i = 0; i < index.Length; i++)
                        {
                            paras[int.Parse(index[i])] = names[id][i];
                        }
                    }
                    string buf = "";
                    foreach (string i in paras)
                    {
                        buf += (i + ",");
                    }
                    sw.WriteLine(buf.Substring(0, buf.Length - 1));
                }
                catch { }
            }
            sr.Close();
            sw.Flush();
            sw.Close();
            MessageBox.Show("Finished");
        }

        class sspHeader
        {
            public uint offset;
            public uint size;
        }

        class effect
        {
            public uint id;
            public string name, desc;
            public byte active, maxLv, lv, range, target, target2, effectRange;
            public ushort mp, sp, ap;
            public int equipFlag, skillFlag;
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void btn_SrcSkill_Click(object sender, EventArgs e)
        {
            OD.Filter = "effect.ssp|effect.ssp";
            if (OD.ShowDialog() == DialogResult.OK)
            {
                tb_SrcSkill.Text = OD.FileName;
            }
        }

        private void btn_TarSkill_Click(object sender, EventArgs e)
        {
            OD.Filter = "effect.ssp|effect.ssp";
            if (OD.ShowDialog() == DialogResult.OK)
            {
                tb_TarSkill.Text = OD.FileName;
            }
        }

        private void btn_StartSkill_Click(object sender, EventArgs e)
        {
            List<sspHeader> header = new List<sspHeader>();
            List<effect> skills = new List<effect>();
            System.Text.Encoding encoder = System.Text.Encoding.Unicode;

            if (tb_SrcSkill.Text != "" && tb_TarSkill.Text != "")
            {
                System.IO.FileStream fs = new System.IO.FileStream(tb_SrcSkill.Text, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                for (int i = 0; i < 10000; i++)
                {
                    sspHeader newHead = new sspHeader();
                    newHead.offset = br.ReadUInt32();
                    if (newHead.offset != 0)
                        header.Add(newHead);
                }
                for (int i = 0; i < 10000; i++)
                {
                    if (i < header.Count)
                    {
                        header[i].size = br.ReadUInt16();
                    }
                    else
                        fs.Position += 2;
                }
                foreach (sspHeader i in header)
                {
                    fs.Position = i.offset;
                    effect skill = new effect();
                    skill.id = br.ReadUInt16();
                    byte[] buff = br.ReadBytes(128);
                    string buf = encoder.GetString(buff);
                    if(buf.IndexOf('\0')!=-1)
                        buf = buf.Remove(buf.IndexOf('\0'));
                    skill.name = buf;
                    buf = encoder.GetString(br.ReadBytes(512));
                    if (buf.IndexOf('\0') != -1)
                        buf = buf.Remove(buf.IndexOf('\0'));
                    skill.desc = buf;
                    skill.active = br.ReadByte();
                    skill.maxLv = br.ReadByte();
                    skill.lv = br.ReadByte();
                    fs.Position += 1;
                    skill.mp = br.ReadUInt16();
                    skill.sp = br.ReadUInt16();
                    skill.ap = br.ReadUInt16();
                    skill.range = br.ReadByte();
                    skill.target = br.ReadByte();
                    skill.target2 = br.ReadByte();
                    skill.effectRange = br.ReadByte();
                    skill.equipFlag = br.ReadInt32();
                    fs.Position += 4;
                    skill.skillFlag = br.ReadInt32();
                    skills.Add(skill);
                }
                fs.Close();

                header.Clear();

                fs = new System.IO.FileStream(tb_TarSkill.Text, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                br = new System.IO.BinaryReader(fs);
                System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
                for (int i = 0; i < 10000; i++)
                {
                    sspHeader newHead = new sspHeader();
                    newHead.offset = br.ReadUInt32();
                    if (newHead.offset != 0)
                        header.Add(newHead);
                }
                for (int i = 0; i < 10000; i++)
                {
                    if (i < header.Count)
                    {
                        header[i].size = br.ReadUInt16();
                    }
                    else
                        fs.Position += 2;
                }
                foreach (sspHeader i in header)
                {
                    fs.Position = i.offset;
                    uint id = br.ReadUInt16();
                    var query =
                        from s in skills
                        where s.id == id
                        select s;
                    if (query.Count() != 0)
                    {
                        effect info = query.First();
                        byte[] buf = encoder.GetBytes(info.name + "\0");
                        bw.Write(buf);
                        fs.Position = i.offset + 130;
                        buf = encoder.GetBytes(info.desc + "\0");
                        bw.Write(buf);
                    }
                }
                fs.Close();
                MessageBox.Show("Finished");
            }
        }

        private void btn_Map_Click(object sender, EventArgs e)
        {
            OD.Filter = "mapname.csv|mapname.csv";
            if (OD.ShowDialog() == DialogResult.OK)
            {
                tb_Map.Text = OD.FileName;
            }
        }

        private void btn_StartMap_Click(object sender, EventArgs e)
        {
            string[] files;
            if (tb_Map.Text == "")
                return;
            MessageBox.Show("Please choose the folder of map data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (FD.ShowDialog() == DialogResult.OK)
            {
                files = System.IO.Directory.GetFiles(FD.SelectedPath);
                List<string> filenames = new List<string>();
                foreach (string i in files)
                {
                    filenames.Add(System.IO.Path.GetFileName(i));
                }
                System.IO.StreamReader sr = new System.IO.StreamReader(tb_Map.Text, System.Text.Encoding.GetEncoding(tb_MapEnc.Text));
                while (!sr.EndOfStream)
                {
                    string line;
                    line = sr.ReadLine();
                    string[] paras;
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "")
                            paras[i] = "0";
                    }
                    uint id = uint.Parse(paras[0]);
                    string name = paras[1];
                    if (filenames.Contains(id + ".map"))
                    {
                        string path = files[filenames.IndexOf(id + ".map")];
                        byte[] buf = System.Text.Encoding.UTF8.GetBytes(name);
                        byte[] buff = new byte[32];
                        int size = buf.Length;
                        if (size > 32) size = 32;
                        Array.Copy(buf, 0, buff, 0, size);
                        System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
                        fs.Position = 4;
                        fs.Write(buff, 0, 32);
                        fs.Close();
                    }
                }
                sr.Close();
                MessageBox.Show("Finished");
            }
        }

        private void btnItemDBPath_Click(object sender, EventArgs e)
        {
            OD.FileName = "";
            OD.ShowDialog();
            if (OD.FileName != "")
            {
                tbItemDBPath.Text = OD.FileName;
            }
        }

        private void btnItemDBStart_Click(object sender, EventArgs e)
        {
            if (tbItemDBPath.Text == "")
            {
                MessageBox.Show("Please select the ItemDB.csv file");
                return;
            }
            if(!File.Exists(tbItemDBPath.Text))
            {
                MessageBox.Show(string.Format("Cannot find file Item.csv in {0}",tbItemDBPath.Text));
                return;
            }
            try
            {
                
                StreamReader sr = new StreamReader(tbItemDBPath.Text);
                string line = sr.ReadLine();
                char[] sps={','};
                string[] sNewFirstLine=LastItemCsv_FirstLine.Split(',');
                string[] sFirstLine = line.Split(',');//sps, StringSplitOptions.RemoveEmptyEntries);
                if (line != LastItemCsv_FirstLine)
                {
                    string f = SD.Filter;
                    SD.Filter = "*.csv|*.csv";
                    SD.ShowDialog();
                    if(SD.FileName=="")
                    {
                        return;
                    }
                    SD.Filter = f;
                    StreamWriter sw = new StreamWriter(SD.FileName);
                    sw.WriteLine(LastItemCsv_FirstLine);
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if (line[0] == '#')
                        {
                            continue;
                        }
                        string[] sLine = line.Split(',');
                        int j = 0;
                        for (int i = 0; i < sNewFirstLine.Length; i++)
                        {
                            if (sFirstLine[j] == sNewFirstLine[i])
                            {
                                try
                                {
                                    sw.Write(sLine[j]);
                                    if (i != sNewFirstLine.Length - 1)
                                    {
                                        sw.Write(",");
                                    }
                                    j++;
                                }
                                catch (Exception)
                                {
                                    sw.Write(0);
                                    if (i != sNewFirstLine.Length - 1)
                                    {
                                        sw.Write(",");
                                    }
                                    j++;
                                }
                            }
                            else
                            {
                                sw.Write("0,");
                            }
                        }
                        sw.WriteLine();
                        sw.Flush();
                    }
                    sw.Close();
                    MessageBox.Show("Finish!");
                }
                else if (line == LastItemCsv_FirstLine)
                {
                    //New Version
                    MessageBox.Show("This file is the newest version.");
                    return;
                }
                else
                { 
                    //unknow version
                    MessageBox.Show("Unknow File Version!!");
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! - " + ex.ToString());
            }
        }

        private void btn_mergeSource_Click(object sender, EventArgs e)
        {
            OD.Filter = "*.csv|*.csv";
            if (OD.ShowDialog() == DialogResult.OK)
            {
                tb_mergeSource.Text = OD.FileName;
            }
        }

        private void btn_mergeTarget_Click(object sender, EventArgs e)
        {
            OD.Filter = "*.csv|*.csv";
            if (OD.ShowDialog() == DialogResult.OK)
            {
                tb_mergeTarget.Text = OD.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tb_mergeSource.Text != "" && tb_mergeTarget.Text != "")
            {
                SD.Filter = "*.csv|*.csv";
                if (SD.ShowDialog() == DialogResult.OK)
                {
                    string outputPath = SD.FileName;
                    System.IO.StreamReader sr = new StreamReader(tb_mergeSource.Text, true);
                    System.IO.StreamReader sr2 = new StreamReader(tb_mergeTarget.Text, true);
                    System.IO.StreamWriter sw = new StreamWriter(outputPath, false, Encoding.UTF8);
                    
                    Dictionary<string, string[]> src1 = new Dictionary<string, string[]>();
                    Dictionary<string, string[]> src2 = new Dictionary<string, string[]>();

                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(',');
                        if (!src1.ContainsKey(line[0]))
                            src1.Add(line[0], line);
                    }
                    while (!sr2.EndOfStream)
                    {
                        string[] line = sr2.ReadLine().Split(',');
                        if (!src2.ContainsKey(line[0]))
                            src2.Add(line[0], line);
                    }
                    foreach (string i in src1.Keys)
                    {
                        string[] line = src1[i];
                        foreach (string j in line)
                        {
                            sw.Write(j + ",");
                        }
                        sw.WriteLine();
                        if (src2.ContainsKey(i))
                            src2.Remove(i);
                    }

                    foreach (string i in src2.Keys)
                    {
                        string[] line = src2[i];
                        foreach (string j in line)
                        {
                            sw.Write(j + ",");
                        }
                        sw.WriteLine();
                    }

                    sw.Flush();
                    sw.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FD.ShowDialog();
            string[] files = System.IO.Directory.GetFiles(FD.SelectedPath);
            foreach (string i in files)
            {
                Image tex = Eco.Texture.Load(i);
                tex.Save(i.Replace(".TGA", ".jpg").Replace(".tga", ".jpg"));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (FD.ShowDialog() == DialogResult.OK)
            {
                string[] files = System.IO.Directory.GetFiles(FD.SelectedPath, "*.cs", SearchOption.AllDirectories);
                foreach (string i in files)
                {
                    System.IO.StreamReader sr = new StreamReader(i, Encoding.GetEncoding("gb2312"));                    
                    string ansi = sr.ReadToEnd();
                    sr.Close();
                    sr = new StreamReader(i, Encoding.UTF8);
                    string utf8 = sr.ReadToEnd();
                    sr.Close();
                    if (ansi != utf8)
                    {
                        System.IO.StreamWriter sw = new StreamWriter(i, false, Encoding.UTF8);
                        sw.Write(ansi);
                        sw.Flush();
                        sw.BaseStream.Flush();
                        sw.Close();
                    }
                }
            }
        }
    }
}