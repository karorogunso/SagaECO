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
        /// <summary>
        /// 左FACEID 右道具ID
        /// </summary>
        public Dictionary<uint, uint> Faces { get { return faces; } }
        public void Init(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
#if !Web
            string label = "load face data...";
            Logger.ProgressBarShow(0, (uint)sr.BaseStream.Length, label);
#endif
            DateTime time = DateTime.Now;
            int count = 0;
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
                    if (Faces.ContainsKey(FaceID))
                        continue;
                    else
                        Faces.Add(FaceID, itemID);

#if !Web
                    if ((DateTime.Now - time).TotalMilliseconds > 40)
                    {
                        time = DateTime.Now;
                        Logger.ProgressBarShow((uint)sr.BaseStream.Position, (uint)sr.BaseStream.Length, label);
                    }
#endif
                }
                catch (Exception ex)
                {
#if !Web
                    Logger.ShowError("Error on parsing Face db!\r\nat line:" + line);
                    Logger.ShowError(ex);
#endif
                }
                count++;
            }
#if !Web
            Logger.ProgressBarHide(count + " Face group loaded.");
#endif
            sr.Close();
        }

    }
}
