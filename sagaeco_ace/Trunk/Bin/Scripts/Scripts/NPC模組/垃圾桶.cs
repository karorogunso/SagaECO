using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public abstract class 垃圾桶 : Event
    {
        public 垃圾桶()
        {

        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_01> Neko_01_amask = new BitMask<Neko_01>(pc.AMask["Neko_01"]);
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            BitMask<Neko_06> Neko_06_amask = pc.AMask["Neko_06"];
            BitMask<Neko_06> Neko_06_cmask = pc.CMask["Neko_06"];

            if (!World_01_mask.Test(World_01.第一次使用垃圾桶))
            {
                初次使用垃圾桶(pc);
                return;
            }

            if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                Neko_06_cmask.Test(Neko_06.獲知恢復的方法) &&
                !Neko_06_cmask.Test(Neko_06.獲得桃子的碎片))
            {
                Wait(pc, 990);
                ShowEffect(pc, 5058);
                Wait(pc, 990);
                ShowEffect(pc, 5179);
                Wait(pc, 1980);

                Say(pc, 0, 131, "（……あっ！$R;" +
                "$Rなんだか懐かしい気配がする…？！$R;" +
                "$Rこの気配は………桃！！）$R;", " ");

                Say(pc, 0, 131, "にゃお〜〜〜ん！$R;", " ");
                Wait(pc, 990);
                PlaySound(pc, 4012, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 5940);

                Say(pc, 0, 131, "『背負い魔・ネコマタ（桃）』$Rの心を取り戻した！$R;", " ");

                Say(pc, 0, 131, "お姉ちゃん！$R;", "ネコマタ（杏）");

                Say(pc, 0, 131, "杏！？$R;" +
                "$Rあれ？…あたしどうしてたんだろう…？$R;" +
                "$Rどうしてゴミ箱に…。$R;", "ネコマタ（桃）");

                Say(pc, 0, 131, "（……よかった。）$R;", " ");
                Neko_06_cmask.SetValue(Neko_06.獲得桃子的碎片, true);
                return;
            }

            if (!Neko_01_amask.Test(Neko_01.桃子任務開始) &&
                !Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                pc.Fame > 9 &&
                pc.Level > 19)
            {
                桃子任務1(pc);
                return;
            }

            if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                !Neko_01_amask.Test(Neko_01.得到不明物體的鬍鬢) &&
                pc.Fame > 9 &&
                pc.Level > 19)
            {
                桃子任務2(pc);
            }

            pc.CInt["Disposition"] += 1;

            NPCTrade(pc);
        }

        void 初次使用垃圾桶(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = pc.CMask["World_01"];

            World_01_mask.SetValue(World_01.第一次使用垃圾桶, true);

            Say(pc, 0, 65535, "这是「垃圾桶」，$R;" +
                              "要保持这个世界的清洁，$R;" +
                              "请把垃圾丢在垃圾桶。$R;" +
                              "$R神不论何时何地都在守护您的。$R;", " ");

            switch (Select(pc, "要看使用方法嗎?", "", "看", "不看"))
            {
                case 1:
                    Say(pc, 0, 65535, "在垃圾桶里面，丢什么都可以。$R;" +
                                      "$R一般不能丢的特殊道具，$R;" +
                                      "当然也可以丢进垃圾桶。$R;" +
                                      "$P丢弃的道具，$R;" +
                                      "『永远』不能还原。$R;" +
                                      "$R扔掉道具时要注意呀!$R;" +
                                      "$P只要你认为它是垃圾，$R;" +
                                      "即使本来是很贵的，$R;" +
                                      "也只不过是垃圾。$R;" +
                                      "$P垃圾呢，可以透过交易视窗扔掉的。$R;" +
                                      "$P把想扔掉的道具拉到交易视窗，$R;" +
                                      "按「确认」键和「交易」键即可$R;", " ");
                    NPCTrade(pc);
                    break;

                case 2:
                    NPCTrade(pc);
                    break;
            }
        }

        void 桃子任務1(ActorPC pc)
        {
            BitMask<Neko_01> Neko_01_amask = pc.AMask["Neko_01"];

            Neko_01_amask.SetValue(Neko_01.桃子任務開始, true);

            Say(pc, 0, 65535, "喵!$R;", " ");
            Say(pc, 0, 65535, "……?!$R;", " ");
            Say(pc, 0, 65535, "哪里传来的哭声…?$R;", " ");
        }

        void 桃子任務2(ActorPC pc)
        {
            BitMask<Neko_01> Neko_01_amask = pc.AMask["Neko_01"];

            int WORK0;

            WORK0 = Global.Random.Next(1, 100);

            if (WORK0 < 10)
            {
                if (CheckInventory(pc, 10035610, 1))
                {
                    Neko_01_amask.SetValue(Neko_01.得到不明物體的鬍鬢, true);

                    Say(pc, 0, 65535, "……?$R;", " ");
                    Say(pc, 0, 65535, "什么轰的一声闪过了?$R;", " ");

                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 10035610, 1);
                    Say(pc, 0, 65535, "得到『不明物体的胡须』!$R;", " ");
                }
            }
        }

    }
}