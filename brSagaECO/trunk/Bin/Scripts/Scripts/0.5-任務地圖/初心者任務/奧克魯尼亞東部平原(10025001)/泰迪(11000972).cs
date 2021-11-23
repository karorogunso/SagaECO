using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:泰迪(11000972) X:8 Y:128
namespace SagaScript.M10025001
{
    public class S11000972 : Event
    {
        public S11000972()
        {
            this.EventID = 11000972;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000972, 131, "$R我有兄弟在$R;" +
                "$P阿高普路斯市上城哦!$R;" +
                "$P等你熟悉這個世界后,$R;" +
                "$P有想去泰迪島的話,$R;" +
                "$P就去那邊找它吧!$R;", "泰迪");
            /*BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            byte x, y;

            if (!Beginner_01_mask.Test(Beginner_01.埃米爾給予埃米爾介紹書))
            {
                尚未與提多對話(pc);
                return;
            }

            SetHomePoint(pc, 10025001, 9, 129);
            Say(pc, 0, 0, "…!!$R;" +
                          "$P活動木偶泰迪?$R;" +
                          "$R不是，不是活動木偶泰迪，$R;" +
                          "也不是石像呀!$R;" +
                          "$R但是泰迪怎麼在這裡站著啊…?$R;", " ");

            Say(pc, 11000972, 131, "喜歡神奇的東西嗎?$R;", "泰迪");

            Say(pc, 0, 0, "……!!$R;" +
                          "泰迪說話了嗎?$R;", " ");

            Say(pc, 11000972, 131, "想不想去神奇的地方呢?$R;", "泰迪");

            switch (Select(pc, "想去嗎?", "", "想去", "不想去"))
            {
                case 1:
                    Say(pc, 11000972, 131, "$R那麼閉上眼睛吧。$R;" +
                                           "$P我喊三聲!$R;" +
                                           "$P一、二、三!!$R;", "泰迪");

                    Say(pc, 0, 0, "……??$R;" +
                                  "哎呀，怎麼這麼睏呢?$R;" +
                                  "$P……$R;", " ");

                    Wait(pc, 990);

                    x = (byte)Global.Random.Next(243, 250);
                    y = (byte)Global.Random.Next(80, 86);

                    Warp(pc, 10071000, x, y);
                    break;
                    
                case 2:
                    Say(pc, 11000972, 131, "嗯? 不去嗎?$R;" +
                                           "真是愛管閒事的傢伙呀!$R;", "泰迪");
                    break;
            }
        }

        void 尚未與提多對話(ActorPC pc)
        {
            Say(pc, 0, 0, "…!!$R;" +
                          "$P活動木偶泰迪?$R;" +
                          "$R不是，不是活動木偶泰迪，$R;" +
                          "也不是石像呀!$R;" +
                          "$R但是泰迪怎麼在這裡站著啊…?$R;", " ");

            Say(pc, 11000972, 131, "和別人的談話都結束了嗎?$R;" +
                                   "$R先跟那邊的「提多」說話吧?", "泰迪");

            Say(pc, 0, 0, "…!!$R;" +
                          "泰迪說話了!?$R;", " ");
        }*/
        }
    }
}