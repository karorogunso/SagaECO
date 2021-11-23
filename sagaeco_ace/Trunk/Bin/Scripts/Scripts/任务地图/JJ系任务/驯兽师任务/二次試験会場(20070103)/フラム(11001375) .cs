using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
using SagaDB.Item;
using SagaDB.Quests;
using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20070103
{
    public class S11001375 : Event
    {
        public S11001375()
        {
            this.EventID = 11001375;
        }
        public override void OnEvent(ActorPC pc)
        {
            BitMask<jjxs> jjxs_mask = new BitMask<jjxs>(pc.CMask["jjxs"]);
            if (CountItem(pc, 10052310) == 0)
            {
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID != 10052310)
                    {
                        Warp(pc, 30130001, 3, 2);
                        return;
                    }
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10052310)
                    TakeEquipment(pc, EnumEquipSlot.PET);
            TakeItem(pc, 10052310, 1);
            if (jjxs_mask.Test(jjxs.开始第一次测试))
            {
                jjxs_mask.SetValue(jjxs.开始第一次测试, false);
                try
                {
                    if (DateTime.Parse(pc.CStr["TIME"]).AddMinutes(4) > DateTime.Now)
                    //if (pc.Quest.ID == 25200001 && pc.Quest.Status == QuestStatus.COMPLETED)
                    {
                        Say(pc, 131, "ちゃんと騎乗しないか。$R;" +
                        "でないと合格にしないぞ。$R;", "フラム");
                        jjxs_mask.SetValue(jjxs.测试通过, true);
                        TakeItem(pc, 10052310, 1);
                        Say(pc, 131, "おめでとう、とりあえずは$R;" +
                        "第一関門突破というところだな。$R;" +
                        "$Rこの試験はまだ続くから$R;" +
                        "気を抜いちゃダメだぞ。$R;", "フラム");
                    }
                    else
                        Say(pc, 131, "殘念...$R;", "フラム");
                }
                catch
                {
                    Say(pc, 131, "殘念...$R;", "フラム");
                }
            }
            Warp(pc, 30130001, 3, 2);//*/
        }
    }
}