using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript
{


    public class P10001511 : Event
    {
        public P10001511()
        {
            this.EventID = 10001511;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<jjxs> jjxs_mask = new BitMask<jjxs>(pc.CMask["jjxs"]);
            if (pc.PossesionedActors.Count != 0)
            {
                Say(pc, 131, "请解除凭依后进入!$R");
            }
            if (jjxs_mask.Test(jjxs.面试通过))
            {
                Warp(pc, 30130001, 7, 10);
                return;
            }

            Warp(pc, 30130002, 7, 10);


        }
    }

    public class P10001512 : Event
    {
        public P10001512()
        {
            this.EventID = 10001512;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10052310)
                    TakeEquipment(pc, EnumEquipSlot.PET);
            TakeItem(pc, 10052310, 1);
            Warp(pc, 10057000, 41, 153);
        }
    }

    public class P10001514 : Event
    {
        public P10001514()
        {
            this.EventID = 10001514;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 30130001, 3, 2);
        }
    }

    public class P10001515 : Event
    {
        public P10001515()
        {
            this.EventID = 10001515;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 30130001, 3, 2);
        }
    }

    public class P10001517 : Event
    {
        public P10001517()
        {
            this.EventID = 10001517;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<jjxs> jjxs_mask = new BitMask<jjxs>(pc.CMask["jjxs"]);
            if (pc.PossesionedActors.Count == 0 && pc.CStr["TIME"] != null)
            {
                if (jjxs_mask.Test(jjxs.开始第二次测试) &&
                   !jjxs_mask.Test(jjxs.入手徽章))
                {
                    Warp(pc, 20070104, 22, 86);
                    return;
                }
                else if (jjxs_mask.Test(jjxs.开始第一次测试) &&
                        !jjxs_mask.Test(jjxs.测试通过))
                {
                    Warp(pc, 20070103, 23, 86);
                }
            }
        }
    }
}