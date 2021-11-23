using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaLib.VirtualFileSystem;
using System.Xml;
namespace SagaDB.Item
{
    public class KujiListFactory : Singleton<KujiListFactory>
    {
        Dictionary<uint, List<Kuji>> kujilist = new Dictionary<uint, List<Kuji>>();
        public Dictionary<uint, List<Kuji>> KujiList { get { return kujilist; } }

        public void InitXML(string path, System.Text.Encoding encoding)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                XmlElement root;
                XmlNodeList list;
                xml.Load(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path));
                root = xml["KujiDB"];
                list = root.ChildNodes;

                foreach (object j in list)
                {
                    XmlElement i;
                    if (j.GetType() != typeof(XmlElement)) continue;
                    i = (XmlElement)j;
                    List<Kuji> kujis = new List<Kuji>();
                    uint KujiID = uint.Parse(i.Attributes["kujinumber"].Value);
                    XmlNodeList items = i.ChildNodes;
                    foreach (object j2 in items)
                    {
                        XmlElement i2;
                        if (j2.GetType() != typeof(XmlElement)) continue;
                        i2 = (XmlElement)j2;
                        string rank = i2.Attributes["rank"].Value;
                        uint itemid = uint.Parse(i2.InnerText);
                        Kuji kuji = new Kuji();
                        kuji.rank = rank;
                        kuji.itemid = itemid;
                        kujis.Add(kuji);
                    }
                    KujiList.Add(KujiID, kujis);
                }
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
        }

    }
}
