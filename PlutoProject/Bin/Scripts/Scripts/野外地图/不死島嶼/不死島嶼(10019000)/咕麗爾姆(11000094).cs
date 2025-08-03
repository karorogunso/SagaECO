using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:不死島嶼(10019000)NPC基本信息:咕麗爾姆(11000094) X:109 Y:112
namespace SagaScript.M10019000
{
    public class S11000094 : Event
    {
        public S11000094()
        {
            this.EventID = 11000094;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);
            BitMask<BSDFlags> mask = new BitMask<BSDFlags>(pc.CMask["BSD"]);
            int selection;
            byte x, y;

            if (JobBasic_08_mask.Test(JobBasic_08.選擇轉職為魔攻師) &&
                !JobBasic_08_mask.Test(JobBasic_08.已經從闇之精靈那裡把心染為黑暗) &&
                pc.Race == PC_RACE.TITANIA &&
                pc.Job == PC_JOB.NOVICE)
            {
                魔攻師轉職任務(pc);
                return;
            }
            if (CountItem(pc, 10024900) >= 1)
            {
                Say(pc, 131, "这个就是『纹章纸』?$R;" +
                    "$R只要您燃烧这张纸$R;" +
                    "不死之岛的封印会暂时解开$R;" +
                    "这样才能进去岛上$R;");
                switch (Select(pc, "要过去不死之岛吗?", "", "不过去", "过去"))
                {
                    case 1:
                        break;
                    case 2:
                        mask.SetValue(BSDFlags.使用過紋章紙, true);
                        TakeItem(pc, 10024900, 1);
                        PlaySound(pc, 2218, false, 100, 50);
                        Say(pc, 131, "『纹章纸』烧起来了$R;");

                        x = (byte)Global.Random.Next(93, 93);
                        y = (byte)Global.Random.Next(104, 104);

                        Warp(pc, 10019100, x, y);
                        break;
                }
                return;
            }
            if (!mask.Test(BSDFlags.使用過紋章紙))
            {
                selection = Global.Random.Next(1, 2);
                switch (selection)
                {
                    case 1:
                        Say(pc, 131, "岛上盛开的『黑暗之花』$R;" +
                            "因为是生长在污秽的大地上$R;" +
                            "所以隐约散发出微弱的黑暗光华$R;" +
                            "$R总有一天，只要白色的花开了$R;" +
                            "通往岛上的路也会解放吧?$R;");
                        break;
                    case 2:
                        Say(pc, 131, "看到那变成废墟的地方吗?$R;" +
                            "那个到底是谁的坟墓呢?$R;" +
                            "$R有关岛上的消息，完全没人知道$R;");
                        break;
                }
                return;
            }
            selection = Global.Random.Next(1, 2);
            switch (selection)
            {
                case 1:
                    Say(pc, 131, "我想在这里研究，查明不死系$R;" +
                        "为什么会出现$R;");
                    break;
                case 2:
                    Say(pc, 131, "从这可以看到那边就是不死之岛$R;" +
                        "$R但是因为现在被封印了无法过去!$R;" +
                        "所以为了过去必须取得『纹章纸』$R;");
                    break;
            }
        }

        void 魔攻師轉職任務(ActorPC pc)
        {
            byte x, y;

            Say(pc, 11000094, 131, "为了成为暗术使正在找「暗之精灵」?$R;" +
                                   "$P「暗之精灵」就在对面的岛上，$R;" +
                                   "本来没有『纹章纸』是$R;" +
                                   "无法到那座岛的，$R;" +
                                   "但是您取得了特别许可。$R;" +
                                   "$R岛上的魔物比较强，$R;" +
                                   "所以您一定要小心才可以啊!", "格列尔莫");

            x = (byte)Global.Random.Next(93, 93);
            y = (byte)Global.Random.Next(104, 104);

            Warp(pc, 10019100, x, y);
        }
    }
}
