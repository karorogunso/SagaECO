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
                Say(pc, 11000739, 131, pc.Name + "您好!$R;", "酒馆店员");
            }

            switch (Select(pc, "想做什么呢?", "", "有什么样的任务?", "挑战任务", "什么也不做"))
            {
                case 1:
                    Say(pc, 11000739, 131, "任务的内容，其实很简单喔!$R;" +
                                           "$R「酒馆」除了卖粮食也介绍任务。$R;" +
                                           "$P人们的口碑大都很好，$R;" +
                                           "所以「阿克罗波利斯」周围，$R;" +
                                           "也开设了许多分店。$R;" +
                                           "$P任务内容包括了，$R;" +
                                           "「击退魔物」、「收集道具」$R;" +
                                           "以及「搬运道具」等。$R;" +
                                           "$R当然根据任务的内容，$R;" +
                                           "给予的酬劳也会不同哦!$R;" +
                                           "$P不同的任务，$R;" +
                                           "执行方式也不一样。$R;" +
                                           "$R相关的内容请到$R;" +
                                           "「阿克罗尼亚平原」的$R;" +
                                           "任务服务台查询哦!!$R;", "酒馆店员");
                    break;

                case 2:
                    Say(pc, 11000739, 131, "「泰迪」那里有任务委托。$R;" +
                                           "$R随便做做吧?$R;" +
                                           "$P需要耗费任务点数『1』。$R;", "酒馆店员");
                    HandleQuest(pc, 100);
                    //Say(pc, 0, 0, "目前尚未实装$R;", " ");
                    break;
            }
        }

        void 初次與咖啡館店員進行對話(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.已經與咖啡館店員進行第一次對話, true);

            Say(pc, 11000739, 131, "借用一下商人叔叔的飞空庭，$R;" +
                                   "受很多罪了。$R;" +
                                   "$R我平常都在「阿克罗波利斯」，$R;" +
                                   "给予冒险者任务哦!$R;" +
                                   "$P嗯，在这座岛上，$R;" +
                                   "也可以发放任务的。$R;", "酒馆店员");
        }
    }
}




