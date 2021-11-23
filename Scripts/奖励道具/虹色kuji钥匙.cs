
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S950000032 : Event
    {
        public S950000032()
        {
            this.EventID = 950000032;//金色钥匙ID为950000031
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 950000032) >= 1)
            {
                Dictionary<uint, List<Kuji>> boxes = SagaDB.Item.KujiListFactory.Instance.KujiList;
                List<uint> myKujis = new List<uint>();//定义一个List，来装玩家有的KUJI
                List<string> ops = new List<string>();//定一个装字符串的list

                List<uint> BIDs = new List<uint>();//定义一个List，来装KUJI奖品
                List<string> Bops = new List<string>();//定一个装KUJI奖品的名字

                foreach (uint kujiID in boxes.Keys)//遍历Boxes的键值，这里代表的是所有Kuji盒子的道具ID
                {
                    uint ID = kujiID + 120000000;
                    if (CountItem(pc, ID) >= 1)//判断玩家身上有没有这个KUJI
                    {
                        myKujis.Add(ID);//如果有这个KUJI，就加进List里面
                    }
                }
                if(myKujis.Count == 0)
                {
                    Say(pc, 0, "身上似乎没有KUJI盒子呢");
                    return;
                }
                for (int i = 0; i <myKujis.Count; i++)//循环myKujis.Count
                {
                    uint itemid = myKujis[i];//获取list里第i个kujiID
                    SagaDB.Item.Item kuji = SagaDB.Item.ItemFactory.Instance.GetItem(itemid);//从服务器上获取这个ID的道具信息
                    string kujiname = kuji.BaseData.name;//获取当前KUJI的名字
                    ops.Add(kujiname);//把获取的KUJI名字，装进list里
                }
                ops.Add("离开");//加一个离开，供玩家取消
                int set = Select(pc, "请选择要用虹色钥匙开启的盒子", "" ,ops.ToArray());//弹出选项
                if (set == ops.Count) return;//玩家选择了离开
                uint SetkujiID = myKujis[set - 1];//根据玩家的选择，从myKujis取指定的kujiID
                List<Kuji> KujiBouns = boxes[SetkujiID - 120000000];//从字典里取出指定的KUJI奖品列表
                for (int i = 0; i < KujiBouns.Count; i++)
                {
                    Kuji b = KujiBouns[i];//获得当前的奖品信息
                    SagaDB.Item.Item it = SagaDB.Item.ItemFactory.Instance.GetItem(b.itemid);//从服务器上获取这个ID的道具信息
                    string bname = it.BaseData.name;//获取当前道具名字
                    if (!BIDs.Contains(b.itemid))
                    {
                        BIDs.Add(b.itemid);//添加奖励ID到列表里
                        Bops.Add(bname);//添加奖励ID到列表里
                    }
                }
                Bops.Add("离开");//加一个离开，供玩家取消
                int Bset = Select(pc, "请选择奖励", "", Bops.ToArray());//弹出选项
                if (Bset == Bops.Count) return;//玩家选择了离开
                uint BSetItemID = BIDs[Bset - 1];//根据玩家的选择，从BIDs取指定奖励的Itemid

                if (CountItem(pc, 950000032) >= 1 && CountItem(pc,SetkujiID) >= 1
                    && Select(pc,"真的要拿「" +  SagaDB.Item.ItemFactory.Instance.GetItem(BSetItemID).BaseData + "」吗？","","是的","算了") ==1)//给奖励前判断一次钥匙，防止有玩家选择之前把钥匙消灭 和是否拥有KUJI盒子
                {
                    GiveItem(pc, BSetItemID, 1);//给奖励！
                    TakeItem(pc, 950000032, 1);
                    TakeItem(pc, SetkujiID, 1);
                }


            }
            else
            {
                Say(pc, 0, "我怀疑你想空手套白狼。");
            }
        }
    }
}

