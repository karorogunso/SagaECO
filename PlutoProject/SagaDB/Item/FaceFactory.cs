using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaLib.VirtualFileSystem;
namespace SagaDB.Item
{
    public class FaceFactory : Singleton<FaceFactory>
    {
        /*List<Face> faces = new List<Face>();
        public List<Face> Faces { get { return faces; } }*/
        Dictionary<uint, uint> faces = new Dictionary<uint, uint>();
        List<uint> faceitemidlist = new List<uint>();
        /// <summary>
        /// 左FACEID 右道具ID
        /// </summary>
        public Dictionary<uint, uint> Faces { get { return faces; } }

        public List<uint> FaceItemIDList { get { return faceitemidlist; } }
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
                    uint itemID = uint.Parse(paras[0]);
                    uint FaceID = uint.Parse(paras[1]);
                    if(!Faces.ContainsKey(FaceID))
                    Faces.Add(FaceID, itemID);
                    if (!FaceItemIDList.Contains(itemID)) FaceItemIDList.Add(itemID);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }

            sr.Close();
        }

    }
}
