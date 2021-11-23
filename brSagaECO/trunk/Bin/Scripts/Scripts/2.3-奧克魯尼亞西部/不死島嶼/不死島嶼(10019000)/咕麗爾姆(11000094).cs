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
                Say(pc, 131, "這個就是『紋章紙』?$R;" +
                    "$R只要您燃燒這張紙$R;" +
                    "不死之島的封印會暫時解開$R;" +
                    "這樣才能進去島上$R;");
                switch (Select(pc, "要過去不死之島嗎?", "", "不過去", "過去"))
                {
                    case 1:
                        break;
                    case 2:
                        mask.SetValue(BSDFlags.使用過紋章紙, true);
                        TakeItem(pc, 10024900, 1);
                        PlaySound(pc, 2218, false, 100, 50);
                        Say(pc, 131, "『紋章紙』燒起來了$R;");

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
                        Say(pc, 131, "島上盛開的『黑暗之花』$R;" +
                            "因爲是生長在污穢的大地上$R;" +
                            "所以隱約散發出微弱的黑暗光華$R;" +
                            "$R總有一天，只要白色的花開了$R;" +
                            "通往島上的路也會解放吧?$R;");
                        break;
                    case 2:
                        Say(pc, 131, "看到那變成廢墟的地方嗎?$R;" +
                            "那個到底是誰的墳墓呢?$R;" +
                            "$R有關於島上的消息，完全沒人知道$R;");
                        break;
                }
                return;
            }
            selection = Global.Random.Next(1, 2);
            switch (selection)
            {
                case 1:
                    Say(pc, 131, "我想在這裡研究，查明不死系$R;" +
                        "為什麼會出現$R;");
                    break;
                case 2:
                    Say(pc, 131, "從這可以看到那邊就是不死之島$R;" +
                        "$R但是因爲現在被封印了無法過去!$R;" +
                        "所以爲了過去必須取得『紋章紙』$R;");
                    break;
            }
        }

        void 魔攻師轉職任務(ActorPC pc)
        {
            byte x, y;

            Say(pc, 11000094, 131, "為了成為魔攻師正在找「闇之精靈」?$R;" +
                                   "$P「闇之精靈」就在對面的島上，$R;" +
                                   "本來沒有『紋章紙』是$R;" +
                                   "無法到那座島的，$R;" +
                                   "但是您取得了特別許可。$R;" +
                                   "$R島上的魔物比較強，$R;" +
                                   "所以您一定要小心才可以啊!", "咕麗爾姆");

            x = (byte)Global.Random.Next(93, 93);
            y = (byte)Global.Random.Next(104, 104);

            Warp(pc, 10019100, x, y);
        }
    }
}
