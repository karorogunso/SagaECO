
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
    public class S80000001 : Event
    {
        public S80000001()
        {
            this.EventID = 80000001;
        }


        public override void OnEvent(ActorPC pc)
        {
            PlaySound(pc, 2559, false, 100, 50);
            Say(pc, 0, "这台机器..$R有很多被人踢的凹陷...", "KUJI扭蛋机");
            int count = 1;
            switch (Select(pc, "怎么办呢?", "", "投入1个【KUJI币】", "投入10个【KUJI币】", "离开吧..."))
            {
                case 1:
                    count = 1;
                    break;
                case 2:
                    count = 10;
                    break;
                case 3:
                    return;
            }
            do
            {
                if (CountItem(pc, 951000000) < count)
                {
                    Say(pc, 0, "咕咕咕？（KUJI币不够哦）", "KUJI扭蛋机");
                    return;
                }
                ShowEffect(pc, 8056);
                TakeItem(pc, 951000000, (ushort)count);
SInt[pc.Name + "抽取KUJI数"] += count;
                TitleProccess(pc, 13, (uint)count);

                pc.AInt["鉴赏家称号"] += count;
                /*if (pc.AInt["鉴赏家称号"] >= 200 && pc.AInt["鉴赏家称号领取"] != 1)
                {
                    pc.AInt["鉴赏家称号领取"] = 1;
                    //GiveItem(pc, 130000029, 1);
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("在雷KUJI池累计抽了200次，获得【鉴赏家】称号！");
                }*/
                for (int i = 0; i < count; i++)
                {
                    int rate = Global.Random.Next(1, 10);
                    List<uint> list = KujiListFactory.Instance.AllItemsInKuji;
                    if (rate == 1)
                        list = KujiListFactory.Instance.NotInKujiAccesuryList;
                    if (rate == 2 || rate == 9 || rate == 10)
                        list = KujiListFactory.Instance.NotInKujiClothesList;
                    if (rate == 3)
                        list = KujiListFactory.Instance.NotInKujiSocksList;
                    if (rate == 4)
                    {
                        if (Global.Random.Next(0, 100) < 20)
                            list = KujiListFactory.Instance.NotInKujiWeaponsList;
                        else
                            rate++;
                    }
                    if (rate == 5)
                        list = KujiListFactory.Instance.InKujiAccesuryList;
                    if (rate == 6)
                        list = KujiListFactory.Instance.InKujiClothesList;
                    if (rate == 7)
                        list = KujiListFactory.Instance.InKujiSocksList;
                    if (rate == 8)
                        list = KujiListFactory.Instance.InKujiWeaponsList;
                    uint id = 0;
                    if (Global.Random.Next(0, 100) < 19)
                        id = list[Global.Random.Next(0, list.Count)];
                    else
                        id = list[Global.Random.Next(0, list.Count) / 3];

                    if (ItemFactory.Instance.GetItem(id) == null)
                        GiveItem(pc, 951000000, 1);
                    else
                        GiveItem(pc, id, 1);
                }
            }
            while ((Select(pc, "再来？", "", "来！", "离开") == 1));
        }
    }
}
