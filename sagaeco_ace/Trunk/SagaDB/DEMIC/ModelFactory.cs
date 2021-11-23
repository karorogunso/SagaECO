using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaLib.VirtualFileSystem;

namespace SagaDB.DEMIC
{
    public class ModelFactory : Singleton<ModelFactory>
    {
        Dictionary<uint, Model> models = new Dictionary<uint, Model>();

        public Dictionary<uint, Model> Models { get { return models; } }

        public void Init(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
            int count = 0;
#if !Web
            string label = "Loading Chip model database";
            Logger.ProgressBarShow(0, (uint)sr.BaseStream.Length, label);
#endif
            DateTime time = DateTime.Now;
            bool modelBegin = false;
            uint currentID = 0;
            int y = 0;
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

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i].ToLower() == "null")
                            paras[i] = "0";
                    }

                    if (paras[0] == "model")
                    {
                        modelBegin = true;
                        Model model = new Model();
                        model.ID = uint.Parse(paras[1]);
                        currentID = model.ID;
                        models.Add(model.ID, model);
                        y = 0;
                        count++;
                        continue;
                    }
                    if (modelBegin)
                    {
                        Model model = models[currentID];
                        for (int i = 0; i < paras.Length; i++)
                        {
                            if (paras[i] != "0")
                                model.Cells.Add(new byte[] { (byte)i, (byte)y });
                            if (byte.Parse(paras[i]) > 100)
                            {
                                model.CenterX = (byte)i;
                                model.CenterY = (byte)y;
                            }
                        }
                        y++;
                    }
                }
                catch (Exception ex)
                {
#if !Web
                    Logger.ShowError("Error on parsing mob db!\r\nat line:" + line);
                    Logger.ShowError(ex);
#endif
                }
            }
#if !Web
            Logger.ProgressBarHide(count + " models loaded.");
#endif
            sr.Close();
        }

    }
}
