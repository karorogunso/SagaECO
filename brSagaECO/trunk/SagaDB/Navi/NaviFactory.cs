using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaLib.VirtualFileSystem;
using SagaDB.Actor;

namespace SagaDB.Navi
{
    public class NaviFactory : Singleton<NaviFactory>
    {
        Navi navi;
        public Navi Navi
        {
            get
            {
                return this.navi;
            }
            set
            {
                this.navi = value;
            }
        }
        public void Init(string path, System.Text.Encoding encoding)
        {

            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
            string[] paras;
            navi = new Navi();
            uint eventId  = 0, categoryId = 0, stepId = 0, stepUniqueId = 0;
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
                    stepUniqueId = uint.Parse(paras[0]);
                    categoryId = uint.Parse(paras[1]);
                    eventId = uint.Parse(paras[2]);
                    stepId = uint.Parse(paras[3]);
                    if (!navi.Categories.ContainsKey(categoryId))
                    {
                        navi.Categories.Add(categoryId, new Category(categoryId));
                    }
                    Category c = navi.Categories[categoryId];
                    if (!c.Events.ContainsKey(eventId))
                    {
                        c.Events.Add(eventId, new Event(eventId));
                    }
                    Event e = c.Events[eventId];
                    Step s = new Step(stepId, stepUniqueId, e);
                    e.Steps.Add(stepId, s);
                    navi.UniqueSteps.Add(stepUniqueId, s);
                }
                catch (Exception ex)
                {
                    Logger.ShowError("Error on parsing Navi db!\r\nat line:" + line);
                    Logger.ShowError(ex);
                }
            }
        }
    }
}
