using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30152001
{
    public class S11001075 : Event
    {
        public S11001075()
        {
            this.EventID = 11001075;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_06> Neko_06_amask = pc.AMask["Neko_06"];
            BitMask<Neko_06> Neko_06_cmask = pc.CMask["Neko_06"];

            if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                Neko_05_amask.Test(Neko_05.茜子任务结束) &&
                Neko_06_cmask.Test(Neko_06.獲知恢復的方法) &&
                !Neko_06_cmask.Test(Neko_06.獲得茜子的碎片))
            {
                Wait(pc, 990);
                ShowEffect(pc, 5058);
                Wait(pc, 990);
                ShowEffect(pc, 5179);
                Wait(pc, 1980);

                Say(pc, 131, "あら？$R;" +
                "$Rかわいい子ネコちゃん。$R;" +
                "$Rどうかしたの？$R;");

                Say(pc, 0, 131, "$R（……あっ、$R;" +
                "$Rなんだか懐かしい気配がする…？$R;" +
                "$Rこの気配は………茜！）$R;", " ");
                 
                Say(pc, 0, 131, "にゃお〜〜〜ん！$R;", " ");
                Wait(pc, 990);
                PlaySound(pc, 4012, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 5940);

                Say(pc, 0, 131, "『背負い魔・ネコマタ（茜）』$Rの心を取り戻した！$R;", " ");

                Say(pc, 0, 131, "お姉ちゃん！$R;", "ネコマタ（杏）");

                Say(pc, 0, 131, "…ん…ハレルヤ…$R;" +
                "$R……あっ！…あたし…？$R;" +
                "$R…杏？…杏なの？$R;", "ネコマタ（茜）");

                Say(pc, 0, 131, "（ほっ…$R;" +
                "やっぱりここだった…よかった。）$R;", " ");

                Neko_06_cmask.SetValue(Neko_06.獲得茜子的碎片, true);
                return;
            }

            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017907)
                {
                    Say(pc, 131, "茜……、おかえり。$R;" +
                        "元気にしてた？$R;");
                    Say(pc, 0, 131, "（うん……。）$R;", "ネコマタ（茜）");
                    Say(pc, 131, "どう、冒険は楽しい？$R;");
                    Say(pc, 0, 131, "（まぁまぁってところね。$R;" +
                        "……悪くはないわ。）$R;", "ネコマタ（茜）");
                    Say(pc, 131, "そう、茜。$R;" +
                        "$Rとっても楽しいのね。$R;" +
                        "$R言葉は通じないけど$R;" +
                        "あなたのその顏を見ればわかるわ。$R;");
                    Say(pc, 0, 131, "（ちょっ、ちょっと！$R;" +
                        "何、勝手な解釈してるのよ！$R;" +
                        "$Rまぁまぁって言ってるでしょ！$R;" +
                        "もぉーっ！$R;" +
                        "言葉が通じないのがもどかしい！）$R;", "ネコマタ（茜）");
                    return;
                }
            }
            Say(pc, 131, "哎呀，客人您来了$R;" +
                "欢迎欢迎$R;");
            switch (Select(pc, "怎么办呢？", "", "制作石像", "设计魔物外型", "什么也不做"))
            {
                case 1:
                    Synthese(pc, 2038, 1);
                    break;
                case 2:
                    Synthese(pc, 2024, 1);
                    break;
                case 3:
                    break;
            }
        }
    }
}