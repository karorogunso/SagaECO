using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
using SagaDB.Item;
using SagaDB.Quests;
using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20070104
{
    public class S11001380 : Event
    {
        public S11001380()
        {
            this.EventID = 11001380;
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

            if (jjxs_mask.Test(jjxs.开始第二次测试))
            {
                jjxs_mask.SetValue(jjxs.开始第二次测试, false);
                try
                {
                    if (DateTime.Parse(pc.CStr["TIME"]).AddMinutes(4) > DateTime.Now)
                    {
                        Say(pc, 131, "ご苦労様、これで試験は終了だ。$R;" +
                        "君は合格だよ、良くがんばった。$R;", "ブリーダーギルドマスター");
                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10052310, 1);
                        Say(pc, 0, 131, "荷物を渡した。$R;", " ");

                        Say(pc, 131, "して、これがブリーダーである$R;" +
                        "証のブローチだ。$R;", "ブリーダーギルドマスター");
                        GiveItem(pc, 50057300, 1);
                        Say(pc, 0, 131, "『ブリーダーブローチ』を手に入れた！$R;", " ");
                        jjxs_mask.SetValue(jjxs.入手徽章, true);
                        Say(pc, 131, "このブローチを身に付ければ$R;" +
                        "いつでもブリーダーとしての$R;" +
                        "能力が発揮できるようになる。$R;" +
                        "$R後はペットとの楽しい$R;" +
                        "毎日をエンジョイしたまえ！$R;", "ブリーダーギルドマスター");

                        Say(pc, 131, "ん？制服？あぁそれなら受付で$R;" +
                        "買えるから、欲しければ買うといいよ。$R;" +
                        "$Pえ？くれないのかって？$R;" +
                        "ブリーダーギルドの運営は$R;" +
                        "君らが制服を買う資金で$R;" +
                        "まかなっているんだ。$R;" +
                        "$Pブリーダーギルドの今後の$R;" +
                        "発展のためにも買ってくれたまえ！$R;", "ブリーダーギルドマスター");
                    }
                    else
                        Say(pc, 131, "殘念...$R;", "フラム");
                }
                catch
                {
                    Say(pc, 131, "殘念...$R;", "フラム");
                }
            }
            Warp(pc, 30130001, 3, 2);
        }
    }
}