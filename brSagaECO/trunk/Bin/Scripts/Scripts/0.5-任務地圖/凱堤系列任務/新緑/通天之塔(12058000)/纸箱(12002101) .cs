using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M12002101
{
    public class S12002101 : Event
    {
        public S12002101()
        {
            this.EventID = 12002101;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_09> Neko_09_mask = new BitMask<Neko_09>(pc.CMask["Neko_09"]);

            if (Neko_09_mask.Test(Neko_09.完成))
            {
                Say(pc, 0, 131, "普通的纸箱。$R;" +
                    "里面什么都没有。$R;", " ");
                return;
            }
            if (CountItem(pc, 10002002) > 0)
            {
                if (Neko_09_mask.Test(Neko_09.获得灵魂碎片_01) &&
                    Neko_09_mask.Test(Neko_09.获得灵魂碎片_02) &&
                    Neko_09_mask.Test(Neko_09.获得灵魂碎片_03) &&
                    Neko_09_mask.Test(Neko_09.获得灵魂碎片_04))
                {
                    黑暗圣杯满了(pc);
                    return;
                }
            }
            if (Neko_09_mask.Test(Neko_09.黑暗圣杯入手) ||
                Neko_09_mask.Test(Neko_09.綠色三角巾入手) ||
                Neko_09_mask.Test(Neko_09.新绿任务开始))
            {
                猫(pc);
                return;
            }
            if(pc.Level >= 30 && pc.Fame >= 10)
            {
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900 ||
                        pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903 ||
                        pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017905 ||
                        pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017906 ||
                        pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017907 ||
                        pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017908 ||
                        pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017910 ||
                        pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017912)
                    {
                        对话1(pc);
                        return;
                    }
                }
            }
            Say(pc, 0, 131, "普通的纸箱。$R;" +
                "里面什么都没有。$R;", " ");

        }

        void 黑暗圣杯满了(ActorPC pc)
        {
            Say(pc, 0, 131, "黒の聖堂司祭の$R;" +
            "言葉を思い出した……。$R;" +
            "$P（闇の力が最も強まる夜。$R;" +
            "$R　……この魂水を$R;" +
            "　魂の砕け散った場所に注ぐのだ。）$R;" +
            "$P日没までまだ時間がある。$R;" +
            "どこか休める場所は……？$R;", " ");
        }

        void 猫(ActorPC pc)
        {
         Say(pc, 0, 131, "ネコマタの気配はない……。$R;", " ");

         Say(pc, 0, 131, "にゃぁ……。$R;", "ネコマタ");
        }

        void 对话1(ActorPC pc)
        {
            BitMask<Neko_09> Neko_09_mask = new BitMask<Neko_09>(pc.CMask["Neko_09"]);
            //int selection;
            Say(pc, 0, 131, "普通のダンボール箱だ。$R;" +
            "中には、何も入っていない。$R;" +
            "$Rしかし、何かの気配を感じる……。$R;", " ");
            ShowEffect(pc, 12002101, 8015);

            NPCChangeView(pc, 12002101, 11001687);
            Wait(pc, 1980);

            Say(pc, 0, 131, "にゃ～～っ！？$R;", "ネコマタ");
            ShowEffect(pc, 12002101, 5241);
            Wait(pc, 330);

            NPCChangeView(pc, 12002101, 12002101);
            Wait(pc, 3630);

            Say(pc, 0, 131, "んにゃっ！？$R;" +
            "$Pにゃ～、にゃ～！！$R;" +
            "$Rにゃんにゃにゃっ！！！$R;" +
            "にゃお～にゃにゃにゃにゃにゃ！$R;" +
            "にゃんにゃっ！！にゃっ！！！$R;", "ネコマタ");

            Say(pc, 0, 131, "今、一瞬見えたのは…ネコマタ！？$R;", " ");
            Neko_09_mask.SetValue(Neko_09.新绿任务开始, true);
        }
    }
}
            
            
        
     
    