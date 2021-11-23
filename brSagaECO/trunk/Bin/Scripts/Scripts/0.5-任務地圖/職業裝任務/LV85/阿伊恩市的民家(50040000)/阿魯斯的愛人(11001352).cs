using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50040000
{
    public class S11001352 : Event
    {
        public S11001352()
        {
            this.EventID = 11001352;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<LV85_Clothes> LV85_Clothes_mask = pc.CMask["LV85_Clothes"];
            int[] a = { 60002951, 60101351, 60101451, 60101551, 60101651, 60101751, 60101851, 60101951, 60102051, 60102151, 60102251, 60102351, 60102451, 60102551, 60102651, 60102751, 60102851, 60102951, 60103051, 60103151, 60103251, 60103351, 60103451, 60103551 };
            uint[] b = { 60002950, 60101350, 60101450, 60101550, 60101650, 60101750, 60101850, 60101950, 60102050, 60102150, 60102250, 60102350, 60102450, 60102550, 60102650, 60102750, 60102850, 60102950, 60103050, 60103150, 60103250, 60103350, 60103450, 60103550 };
            if (LV85_Clothes_mask.Test(LV85_Clothes.已与阿魯斯的爱人对话))
            {
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.UPPER_BODY))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.UPPER_BODY].Refine == 5)
                    {
                        int te = -1;
                        for (int i = 0; i < a.Length; i++)
                        {
                            if (pc.Inventory.Equipments[EnumEquipSlot.UPPER_BODY].ItemID == a[i])
                            {
                                te = i;
                                break;
                            }
                        }
                        if (te != -1)
                        {
                                Say(pc, 131, "フフフ……。$R;" +
                                "じゃあ、さっそく儀式を始めましょう。$R;" +
                                "$Pあ、これ、ただの作業着だから。$R;" +
                                "とくに怖いことなんてないのよ。$R;" +
                                "$Rだから、安心して、ね♪$R;", "アルスの彼女");
                                switch (Select(pc, "どうする？", "", "やっぱりやめる", "防具の呪いをとく"))
                                {
                                    case 2:
                                        LV85_Clothes_mask.SetValue(LV85_Clothes.詛咒解除, true);
                                        TakeEquipment(pc, EnumEquipSlot.UPPER_BODY);

                                        Say(pc, 11000282, 131, "これから、儀式をはじめます。$R;" +
                                        "途中で、何があっても$R;" +
                                        "叫んだりしないようお願いします……。$R;" +
                                        "$Pぶつぶつ……、ぶつぶつ……。$R;" +
                                        "現れよ……。$R;", "アルスの彼女");
                                        ShowEffect(pc, 5020);
                                        Wait(pc, 1980);

                                        Say(pc, 0, 131, "イヤダァ…キエタクナイィィ……。$R;", "ヘンナコエ");
                                        ShowEffect(pc, 5619);
                                        Wait(pc, 990);

                                        Say(pc, 11000282, 131, "うぬぅぅぅ、抵抗が激しい。$R;" +
                                        "おのれ、秘儀、ナゲット投げ！$R;", "アルスの彼女");
                                        ShowEffect(pc, 8006);

                                        Say(pc, 0, 131, "頭に、鉄のナゲットがあたった……。$R;", " ");

                                        Say(pc, 11000282, 131, "一気に、いくぞよ……！$R;", "アルスの彼女");
                                        ShowEffect(pc, 5188);
                                        Wait(pc, 660);
                                        ShowEffect(pc, 5615);
                                        Wait(pc, 990);
                                        ShowEffect(pc, 5615);
                                        Wait(pc, 990);
                                        ShowEffect(pc, 5615);
                                        Wait(pc, 990);

                                        Say(pc, 0, 131, "ウヒィィィィ……。$R;", "ヘンナコエ");

                                        Say(pc, 0, 131, "呪いが解除された。$R;", " ");

                                        Say(pc, 131, "儀式は無事終了しました。$R;" +
                                        "$Rさあ、呪いのとけた防具を$R;" +
                                        "きてみてください。$R;", "アルスの彼女");
                                        GiveItem(pc, b[te], 1);
                                        return;
                                }
                        }
                    }
                }
            }
            Say(pc, 131, "では、私も着替えてくるわ。$R;" +
            "ちょっと待っててね。$R;", "アルスの彼女");
            Warp(pc, 30071000, 3, 5);
        }
    }
}