using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using SagaLib;
using SagaDB.Actor;

namespace SagaDB.Synthese
{
    public class SyntheseFactory : Factory<SyntheseFactory, SyntheseInfo>
    {
        public SyntheseFactory()
        {
            this.loadingTab = "Loading synthese database";
            this.loadedTab = " syntheses loaded.";
            this.databaseName = "synthese";
            this.FactoryType = FactoryType.CSV;
        }

        protected override uint GetKey(SyntheseInfo item)
        {
            return item.ID;
        }

        protected override void ParseCSV(SyntheseInfo item, string[] paras)
        {
            //throw new NotImplementedException();
            item.ID = uint.Parse(paras[0]);
            item.SkillID = ushort.Parse(paras[5]);
            item.SkillLv = byte.Parse(paras[6]);
            item.Gold = uint.Parse(paras[8]);
            item.RequiredTool = uint.Parse(paras[9]);
            for (int i = 10; i < 17; i += 2)
            {
                ItemElement tmp = new ItemElement();
                tmp.ID = uint.Parse(paras[i]);
                tmp.Count = ushort.Parse(paras[i + 1]);
                item.Materials.Add(tmp);
            }
            for (int i = 18; i < 41; i += 3)
            {
                ItemElement tmp = new ItemElement();
                tmp.ID = uint.Parse(paras[i].Split(':')[0]);
                tmp.Count = ushort.Parse(paras[i + 1]);
                tmp.Rate = int.Parse(paras[i + 2]);
                tmp.Exp = int.Parse(paras[41]);
                item.Products.Add(tmp);
            }
        }

        protected override void ParseXML(XmlElement root, XmlElement current, SyntheseInfo item)
        {/*
            switch (root.Name.ToLower())
            {                
                case "synthese":
                    switch (current.Name.ToLower())
                    {
                        case "id":
                            item.ID = uint.Parse(current.InnerText);
                            break;
                        case "skillid":
                            item.SkillID = ushort.Parse(current.InnerText);
                            break;
                        case "skilllv":
                            item.SkillLv = byte.Parse(current.InnerText);
                            break;
                        case "gold":
                            item.Gold = uint.Parse(current.InnerText);
                            break;
                        case "requiredtool":
                            item.RequiredTool = uint.Parse(current.InnerText);
                            break;
                        case "material":
                            {
                                ItemElement tmp = new ItemElement();
                                tmp.ID = uint.Parse(current.GetAttribute("id"));
                                tmp.Count = ushort.Parse(current.GetAttribute("count"));
                                item.Materials.Add(tmp);
                            }
                            break;
                        case "product":
                            {
                                ItemElement tmp = new ItemElement();
                                tmp.ID = uint.Parse(current.GetAttribute("id"));
                                tmp.Count = ushort.Parse(current.GetAttribute("count"));
                                tmp.Rate = int.Parse(current.GetAttribute("rate"));
                                item.Products.Add(tmp);
                            }
                            break;                        
                    }
                    break;
            }*/
            throw new NotImplementedException();
        }
    }
}
