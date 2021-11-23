
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using System.Text;
using System.Diagnostics;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.FF
{
    public class S80000002 : Event
    {
        public S80000002()
        {
            this.EventID = 80000002;
        }


        public override void OnEvent(ActorPC pc)
        {
            PlaySound(pc, 2559, false, 100, 50);
            Say(pc, 0, "这里是发型卷扭蛋机$R$R从橱窗里望去$R里面有好多发型卷哦。$R$R突然发现下边还有一个按钮呢..", "发型扭蛋机");
            switch (Select(pc, "要怎么办呢？", "", "投入一枚[代币][抽发型]", "按下边的按钮[抽染色]", "离开吧"))
            {
                case 1:
                    if (CountItem(pc, 950000000) >= 1)
                    {
                        TakeItem(pc, 950000000, 1);
                        uint id = getrandomhairid();
                        if (!ItemFactory.Instance.Items.ContainsKey(id))
                        {
                            GiveItem(pc, 950000000, 1);
                            Say(pc, 0, "“呸——”", "发型扭蛋机");
                            Say(pc, 0, "被退币了..似乎故障了！");
                            return;
                        }
                        GiveItem(pc, id, 1);
                        PlaySound(pc, 2040, false, 100, 50);
                    }
                    else
                    {
                        Say(pc, 0, "嗯...似乎没有代币呢");
                    }
                    break;
                case 2:
                    if (pc.Account.GMLevel > 250)
                    {
                        Say(pc, 131, "GM特权——改外形");
                        OpenChangePCForm(pc);
                    }
                    Say(pc, 131, "当当当当！$R$R真厉害居然被你发现了这个按钮！$R$R只要你给我15,000G$R我就随机给你一个染发剂哦！$R(机器不知道从哪里伸出了一只手来$R似乎想要接钱呢)$R", "机器的声音");
                    switch (Select(pc, "要怎么办呢？", "", "放15,000G在手上","对着它砸150,000G！（10连抽）", "这机器真智障，还是离开吧"))
                    {
                        case 1:
                            if (pc.Gold >= 15000)
                            {
                                pc.Gold -= 15000;
                                抽(pc);
                            }
                            else
                            {
                                Say(pc, 131, "哎哟——$R$R你钱不够呢！", "机器的声音");
                            }
                            break;
                        case 2:
                            if (pc.Gold >= 150000)
                            {
                                pc.Gold -= 150000;
                                Say(pc,131,"哟呵————", "机器的声音");
                                for (int i = 0; i < 10; i++)
                                {
                                    抽(pc);
                                }
                            }
                            else
                            {
                                Say(pc, 131, "哎哟——$R$R你钱不够呢！", "机器的声音");
                            }
                            break;
                    }
                    return;
            }
        }
        void 抽(ActorPC pc)
        {
            List<uint> colors = new List<uint>();
            uint id = 10031301;
            for (int i = 0; i < 32; i++)
            {
                colors.Add(id);
                id++;
            }
            colors.Add(10031364);
            colors.Add(10031365);
            colors.Add(10031366);
            colors.Add(10031367);
            colors.Add(10031368);
            int rate = Global.Random.Next(0, 300);
            if (rate < 30)
            {
                GiveItem(pc, colors[Global.Random.Next(0, colors.Count)], 1);
                Say(pc, 131, "哐当！！", "机器的声音");
                return;
            }
            else
            {
                GiveItem(pc, colors[Global.Random.Next(0, colors.Count - 13)], 1);
                Say(pc, 131, "哐当！", "机器的声音");
                return;
            }
        }
        uint getrandomhairid()
        {
            Dictionary<int, uint> ids = new Dictionary<int, uint>();
            int count = 0;
            foreach (var item in HairFactory.Instance.Hairs)
            {
                if (!ids.ContainsValue(item.ItemID))
                {
                    ids.Add(count, item.ItemID);
                    count++;
                }
            }
            if (SagaLib.Global.Random.Next(0, 100) < 30)
                return ids[SagaLib.Global.Random.Next(1, 230)];
            else return ids[SagaLib.Global.Random.Next(131, ids.Count - 1)];
        }
    }
}
/*List<uint> ids = new List<uint>();
foreach (var item in HairFactory.Instance.Hairs)
{
    if(!ids.Contains(item.ItemID))
    {
        ids.Add(item.ItemID);
        GiveItem(pc, item.ItemID,1);
    }
}
System.IO.FileStream fs = new System.IO.FileStream("ddd.csv", System.IO.FileMode.Create);
System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
foreach (var item in ids)
{
    sw.WriteLine("hairList.add(" + item + ",100);");
}
sw.Close();
fs.Close();*/

