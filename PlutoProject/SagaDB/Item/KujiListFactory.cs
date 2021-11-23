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
        public Dictionary<uint, Dictionary<int, List<uint>>> NewKujilist = new Dictionary<uint, Dictionary<int, List<uint>>>();
        public List<uint> NotInKujiSocksList = new List<uint>();
        public List<uint> NotInKujiWeaponsList = new List<uint>();
        public List<uint> NotInKujiClothesList = new List<uint>();
        public List<uint> NotInKujiAccesuryList = new List<uint>();
        public List<uint> InKujiSocksList = new List<uint>();
        public List<uint> InKujiWeaponsList = new List<uint>();
        public List<uint> InKujiClothesList = new List<uint>();
        public List<uint> InKujiAccesuryList = new List<uint>();
        public List<uint> AllItemsInKuji = new List<uint>();
        public List<uint> RidePartnerList = new List<uint>();
        public List<uint> PartnerList = new List<uint>();
        public List<uint> ZeroPriceList = new List<uint>();
        public List<uint> EventKujiList = new List<uint>();
        public Dictionary<uint, List<Kuji>> KujiList { get { return kujilist; } }

        public class ItemTransform
        {
            public uint product;
            public List<uint> Stuffs = new List<uint>();
        }
        public Dictionary<uint, ItemTransform> ItemTransformList = new Dictionary<uint, ItemTransform>();


        public string kujiname;

        public void InitTransformList(string path, Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
            string[] paras;
            uint TID = 0;
            ItemTransform it;
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
                    if (paras.Length <= 0) continue;
                    if (paras[0] == "") continue;
                    if (paras[0] == "TRANSFORM")
                    {
                        it = new ItemTransform();
                        it.product = uint.Parse(paras[2]);
                        TID = uint.Parse(paras[1]);
                        ItemTransformList.Add(TID, it);
                    }
                    else
                    {
                        ItemTransformList[TID].Stuffs.Add(uint.Parse(paras[0]));
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
            sr.Close();
        }
        public void InitEventKujiList(string path, Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
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
                    uint id = uint.Parse(paras[0]);
                    if (!EventKujiList.Contains(id))
                        EventKujiList.Add(id);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
            sr.Close();
        }

        public void BuildNotInKujiItemsList()
        {
            foreach (var item in KujiList)
            {
                foreach (var i in item.Value)
                {
                    if (!AllItemsInKuji.Contains(i.itemid)) AllItemsInKuji.Add(i.itemid);
                }
            }
            //foreach (var item in ExchangeFactory.Instance.ExchangeItems)
            //{
            //    foreach (var i in item.Value)
            //        if (!AllItemsInKuji.Contains(i)) AllItemsInKuji.Add(i);
            //}
            foreach (var item in ItemFactory.Instance.Items)
            {
                ItemType ty = item.Value.itemType;
                if (item.Key > 80000000) continue;
                if ((ty == ItemType.SOCKS || ty == ItemType.SHOES || ty == ItemType.SLACKS || ty == ItemType.OVERALLS || ty == ItemType.HALFBOOTS
                    || ty == ItemType.ARMOR_LOWER || ty == ItemType.BOOTS || ty == ItemType.LONGBOOTS))
                {
                    if (!NotInKujiSocksList.Contains(item.Key) && !AllItemsInKuji.Contains(item.Key))
                        NotInKujiSocksList.Add(item.Key);
                    else
                        InKujiSocksList.Add(item.Key);
                }
                if ((ty == ItemType.CLAW || ty == ItemType.HAMMER || ty == ItemType.STAFF || ty == ItemType.SWORD || ty == ItemType.AXE || ty == ItemType.SPEAR ||
                  ty == ItemType.BOW || ty == ItemType.GUN || ty == ItemType.ETC_WEAPON || ty == ItemType.ACCESORY_FINGER || ty == ItemType.SHORT_SWORD || ty == ItemType.RAPIER ||
                  ty == ItemType.STRINGS || ty == ItemType.BOOK || ty == ItemType.DUALGUN || ty == ItemType.RIFLE || ty == ItemType.THROW || ty == ItemType.ROPE ||
                  ty == ItemType.CARD || ty == ItemType.SHIELD))
                {
                    if (!NotInKujiWeaponsList.Contains(item.Key) && !AllItemsInKuji.Contains(item.Key))
                        NotInKujiWeaponsList.Add(item.Key);
                    else
                        InKujiWeaponsList.Add(item.Key);
                }
                if ((ty == ItemType.ARROW || ty == ItemType.ARMOR_UPPER || ty == ItemType.ARMOR_LOWER || ty == ItemType.ONEPIECE || ty == ItemType.COSTUME || ty == ItemType.BODYSUIT ||
                      ty == ItemType.WEDDING || ty == ItemType.OVERALLS || ty == ItemType.FACEBODYSUIT || ty == ItemType.SLACKS))
                {
                    if (!NotInKujiClothesList.Contains(item.Key) && !AllItemsInKuji.Contains(item.Key))
                        NotInKujiClothesList.Add(item.Key);
                    else
                        InKujiClothesList.Add(item.Key);
                }
                if ((ty == ItemType.ACCESORY_NECK || ty == ItemType.BACKPACK || ty == ItemType.ACCESORY_FINGER || ty == ItemType.HELM || ty == ItemType.JOINT_SYMBOL
                    || ty == ItemType.ACCESORY_FACE))
                {
                    if (!NotInKujiAccesuryList.Contains(item.Key) && !AllItemsInKuji.Contains(item.Key))
                        NotInKujiAccesuryList.Add(item.Key);
                    else
                        InKujiAccesuryList.Add(item.Key);
                }
                if (ty == ItemType.RIDE_PARTNER)
                    RidePartnerList.Add(item.Key);
                if (ty == ItemType.PARTNER)
                    PartnerList.Add(item.Key);
            }
        }
        public void InitZeroCPList(string path, Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);

            DateTime time = DateTime.Now;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    uint id = uint.Parse(line);
                    if (!ZeroPriceList.Contains(id))
                        ZeroPriceList.Add(id);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
            sr.Close();
        }
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
                    string name = i.Attributes["kujinumber"].Value + "：" + i.Attributes["name"].Value + "     ";
                    kujiname += name;
                    XmlNodeList items = i.ChildNodes;

                    Dictionary<int, List<uint>> newkujis = new Dictionary<int, List<uint>>();

                    foreach (object j2 in items)
                    {
                        XmlElement i2;
                        if (j2.GetType() != typeof(XmlElement)) continue;
                        i2 = (XmlElement)j2;
                        string srank = i2.Attributes["rank"].Value;
                        uint itemid = uint.Parse(i2.InnerText);
                        int rank = int.Parse(srank);
                        Kuji kuji = new Kuji();
                        kuji.rank = rank;
                        kuji.itemid = itemid;
                        kuji.rate = GetKujiRate(rank);
                        kujis.Add(kuji);

                        if (!newkujis.ContainsKey(GetKujiRate(rank)))
                            newkujis.Add(GetKujiRate(rank), new List<uint>() { itemid });
                        else
                            newkujis[GetKujiRate(rank)].Add(itemid);
                    }
                    NewKujilist.Add(KujiID, newkujis);
                    KujiList.Add(KujiID, kujis);
                }
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
        }
        private int GetKujiRate(int rank)
        {
            int[] rates = new int[10] { 3, 17, 80, 100, 160, 200, 260, 280, 300, 600 };
            if (rank > 10)
            {
                rank = 10;
            }
            if (rank < 1)
            {
                rank = 1;
            }
            return rates[rank - 1];
        }

    }
}
