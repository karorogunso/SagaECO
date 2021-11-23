using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:行會宮殿大樓中庭(30110000) NPC基本信息:行會宮殿嚮導(11000000) X:10 Y:16
namespace SagaScript.M30110000
{
    public class S11000000 : Event
    {
        public S11000000()
        {
            this.EventID = 11000000;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Acropolisut_01> Acropolisut_01_mask = new BitMask<Acropolisut_01>(pc.CMask["Acropolisut_01"]);

            int selection;

            if (!Acropolisut_01_mask.Test(Acropolisut_01.已經與行會宮殿嚮導進行第一次對話))
            {
                初次與行會宮殿嚮導進行對話(pc);
                return;
            }

            Say(pc, 11000000, 131, "欢迎来到「行会宫殿」!$R;", "行会宫殿向导");

            selection = Select(pc, "想听那里的说明呢?", "", "大楼中庭", "2楼", "3楼", "4楼", "5楼", "什么也不听");

            while (selection != 6)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000000, 131, "这里就是大楼中庭哦!$R;" +
                                               "$R我是「行会宫殿向导」，$R;" +
                                               "各楼层的传送点都在这里。$R;", "行会宫殿向导");
                        break;

                    case 2:
                        Say(pc, 11000000, 131, "2楼设有战士系的职业行会!$R;" +
                                               "$R想到剑士、骑士、盗贼$R;" +
                                               "和弓手行会的朋友们，$R;" +
                                               "请上2楼吧。$R;", "行会宫殿向导");
                        break;

                    case 3:
                        Say(pc, 11000000, 131, "3楼主要是生产系的职业行会!$R;" +
                                               "$R除此之外，$R;" +
                                               "还有魔法师和元素使的魔法系行会。$R;" +
                                               "$P想到魔法系、农夫、矿工$R;" +
                                               "和机械师行会的朋友们，$R;" +
                                               "请上3楼吧。$R;", "行会宫殿向导");
                        break;

                    case 4:
                        Say(pc, 11000000, 131, "4楼全是生产系的职业行会!$R;" +
                                               "$R想到商人、炼金术师、冒险家$R;" +
                                               "和人偶师行会的朋友们，$R;" +
                                               "请上4楼吧。$R;", "行会宫殿向导");
                        break;

                    case 5:
                        Say(pc, 11000000, 131, "5楼是为了招待异世界的朋友们，$R;" +
                                               "而特别设置的专用楼层!$R;" +
                                               "$R所以埃米尔的朋友们，$R;" +
                                               "是无法进入的。$R;", "行会宫殿向导");
                        break;
                }
                selection = Select(pc, "想听哪里的说明呢?", "", "大楼中庭", "2楼", "3楼", "4楼", "5楼", "什么也不听");
            }

            Say(pc, 11000000, 131, "那请您到处逛逛吧。$R;", "行会宫殿向导");
        }
        

        void 初次與行會宮殿嚮導進行對話(ActorPC pc)
        {
            BitMask<Acropolisut_01> Acropolisut_01_mask = new BitMask<Acropolisut_01>(pc.CMask["Acropolisut_01"]);

            Say(pc, 11000000, 131, pc.Name + "欢迎光临!!$R;" +
                                   "$R欢迎来到「行会宫殿」!$R;" +
                                   "$P您是第一次到这里来吗?$R;" +
                                   "$R要听听有关「行会宫殿」的说明吗?$R;", "行会宫殿向导");

            switch (Select(pc, "要听关于「行会宫殿」的说明吗?", "", "想听", "不听"))
            {
                case 1:
                    Acropolisut_01_mask.SetValue(Acropolisut_01.已經與行會宮殿嚮導進行第一次對話, true);

                    Say(pc, 11000000, 131, "「行会宫殿」是一个5层楼的建筑物，$R;" +
                                           "里面有各种行会的办公室!$R;" +
                                           "$P主要事务是办理$R;" +
                                           "各职业的行会入会手续，$R;" +
                                           "以及任务的承接。$R;" +
                                           "$P有关各职业的消息，$R;" +
                                           "可以在2楼到4喽的各行会总部询问!$R;" +
                                           "$P对了，5楼是为了招待异世界的$R;" +
                                           "朋友们的楼层哦!$R;" +
                                           "$R所以埃米尔的朋友们，$R;" +
                                           "是无法进入的。$R;", "行会宫殿向导");
                    break;

                case 2:
                    Say(pc, 11000000, 131, "那请您到处逛逛吧。$R;", "行会宫殿向导");
                    break;
            }
        }
    }
}
