using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

using SagaDB.Quests;
//所在地圖:泰迪島(10071000) NPC基本信息:咖啡館店員(11000739) X:131 Y:220
namespace SagaScript.M10071000
{
    public class S11000739 : Event
    {
        public S11000739()
        {
            this.EventID = 11000739;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            if (!Tinyis_Land_01_mask.Test(Tinyis_Land_01.已經與咖啡館店員進行第一次對話))
            {
                初次與咖啡館店員進行對話(pc);
            }
            else
            {
                Say(pc, 11000739, 131, pc.Name + "您好!$R;", "咖啡館店員");            
            }

            switch (Select(pc, "想做什麼呢?", "", "有什麼樣的任務?", "挑戰任務", "什麼也不做"))
            {
                case 1:
                    Say(pc, 11000739, 131, "任務的內容，其實很簡單喔!$R;" +
                                           "$R「咖啡館」除了賣糧食也介紹任務。$R;" +
                                           "$P人們的口碑較都很好，$R;" +
                                           "所以「阿高普路斯市」周圍，$R;" +
                                           "也開設了許多分店。$R;" +
                                           "$P任務內容包括了，$R;" +
                                           "「擊退魔物」、「收集道具」$R;" +
                                           "以及「搬運道具」等。$R;" +
                                           "$R當然根據任務的內容，$R;" +
                                           "給予的酬勞也會不同唷!$R;" +
                                           "$P不同的任務，$R;" +
                                           "執行方式也不一樣。$R;" +
                                           "$R相關的內容請到$R;" +
                                           "「奧克魯尼亞平原」的$R;" +
                                           "任務服務台查詢唷!!$R;", "咖啡館店員");
                    break;

                case 2:
                    Say(pc, 11000739, 131, "「泰迪」那裡有任務委託。$R;" +
                                           "$R隨便做做吧?$R;" +
                                           "$P需要耗費任務點數『1』。$R;", "咖啡館店員");

                    Say(pc, 0, 0, "目前尚未實裝$R;", " ");
                    break;
            }
        }

        void 初次與咖啡館店員進行對話(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.已經與咖啡館店員進行第一次對話, true);

            Say(pc, 11000739, 131, "借用一下商人叔叔的飛空庭，$R;" +
                                   "受很多罪了。$R;" +
                                   "$R我平常都在「阿高普路斯市」，$R;" +
                                   "給予冒險者任務唷!$R;" +
                                   "$P嗯，在這座島上，$R;" +
                                   "也可以發放任務的。$R;", "咖啡館店員");
        }
    }
}




