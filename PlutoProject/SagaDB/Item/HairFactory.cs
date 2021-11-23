using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaLib.VirtualFileSystem;
namespace SagaDB.Item
{
    public class HairFactory : Singleton<HairFactory>
    {
        List<Hair> hairs = new List<Hair>();
        public List<Hair> Hairs { get { return hairs; } }
        public void Init(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);

            DateTime time = DateTime.Now;

            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');

                    Hair hair = new Hair();
                    hair.ItemID = uint.Parse(paras[0]);
                    hair.Gender = byte.Parse(paras[1]);
                    hair.HairStyle = short.Parse(paras[2]);
                    hair.HairWig = short.Parse(paras[3]);
                    hair.ManageGroup = short.Parse(paras[4]);
                    hair.HairName = paras[5];
                    hair.WigName = paras[6];
                    hair.NpcBeforeSay = paras[7];
                    hair.NpcAfterSay = paras[8];
                    hair.Remarks = paras[9];

                    Hairs.Add(hair);
                }
                catch (Exception ex)
                {
                }
            }

            sr.Close();
        }

    }
}
