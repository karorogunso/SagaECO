using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:泰迪島(10071000) NPC基本信息:泰迪(11000723) X:243 Y:84
namespace SagaScript.M10071000
{
    public class S11000723 : Event
    {
        public S11000723()
        {
            this.EventID = 11000723;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            if (!Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟泰迪的伙伴们打完招呼) && Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶愛伊斯打完招呼) &&
                Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶瑪歐斯打完招呼) && Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶綠礦石精靈打完招呼) &&
                Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶塞爾曼德打完招呼) && Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶曼陀蘿打完招呼) &&
                Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶虎姆拉打完招呼) && Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶塔依打完招呼) &&
                Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶皮諾打完招呼))
            {
                跟泰迪同伴打完招呼后去变身(pc);
                return;
            }

            if (Tinyis_Land_01_mask.Test(Tinyis_Land_01.已經與泰迪進行第一次對話))
            {
                第二次跟泰迪对话(pc);
                return;
            }

            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.已經與泰迪進行第一次對話, true);
            Say(pc, 11000723, 131, "您好$R;" +
                         "$R哦？吓着了？这里是哪儿？$R;" +
                         "$P这里是泰迪岛喔$R;" +
                         "$R只能在梦里才能看到的神秘之岛哦$R;" +
                         "$P我叫泰迪，是『活动木偶泰迪』！$R;" +
                         "$P这座岛上到处都是我的朋友，$R去找找吧$R;" +
                         "$P先听完别人的话，$R然后再来跟我说话吧$R;");
            Say(pc, 11000723, 131, "哦？什么叫『活动木偶』呢？$R;" +
                         "$R『活动木偶』指的是，$R灵魂可以附体的，$R神秘道具唷$R;" +
                         "$P在这里经常出现，$R灵魂附在貌似人的物体内的现象$R;" +
                         "$P活动木偶$R是像我一样的玩偶或机器人、植物等$R精灵的力量作为形象的$R;" +
                         "$R形态非常多样$R;" +
                         "$P变身的时间只有3分钟$R;" +
                         "$R但变身解除后5分钟内$R无法变身，请注意哦$R;");
        }

        void 跟泰迪同伴打完招呼后去变身(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);
            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.跟泰迪的伙伴们打完招呼, true);
            Say(pc, 11000723, 131, "跟我的同伴活动木偶们打招呼了吗？$R最后我再介绍一下。$R;" +
                                   "$P我叫泰迪『$R活动木偶泰迪』$R;");
            Say(pc, 11000723, 131, "我是玩偶外形的$R神秘活动木偶$R;" +
                                   "$P变成我后，可以使用$R『呜呜呜啊~』，$R把周围的魔物搞混乱，$R弄的它们动弹不得。$R;" +
                                   "$P还有变身期间$R『MP』会自动恢复哦$R;");
            Say(pc, 11000723, 131, "怎么样？想变成我吗？$R;");
            switch (Select(pc, "怎么办呢？", "", "变身", "放弃"))
            {
                case 1:
                    ActivateMarionette(pc, 10022000);
                    ShowEffect(pc, 8015);
                    Heal(pc);
                    break;

                case 2:
                    break;
            }
        }

        void 第二次跟泰迪对话(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = pc.CMask["Tinyis_Land_01"];
            Say(pc, 11000723, 131, "您好$R;");
            if (Tinyis_Land_01_mask.Test(Tinyis_Land_01.已經與湖畔的微微進行第一次對話))
            {
                可以回城咯(pc);
                return;
            }
            Say(pc, 11000723, 131, "??$R;" +
                "是不是想回去了？$R;" +
                "$P哎呀，太没劲了，$R再玩一会儿吗？$R;" +
                "$R不过，执意要回去…$R;" +
                "$P跟湖边的女孩见见面$R再回来吧。$R;" +
                "$R她是叫『蒂塔』的孩子$R;" +
                "$R要不然走不了$R;" +
                "$P这个岛里有很多人，$R;" +
                "听听故事会有好处的哦。$R;");

        }

        void 可以回城咯(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);
            BitMask<Acropolisut_01> Acropolisut_01_mask = pc.CMask["Acropolisut_01"];
            byte x, y;
            Say(pc, 11000723, 131, "怎么办？回去吗？$R;");
            switch (Select(pc, "怎么办呢？", "", "回去", "放弃"))
            {
                case 1:
                    Say(pc, 11000723, 131, "哦？$R真的要回去吗？$R;");
                    switch (Select(pc, "怎么办呢？", "", "回去", "放弃"))
                    {
                        case 1:
                            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.指导地图的泰迪标示去过泰迪岛, true);
                            Say(pc, 11000723, 131, "是吗？$R那没办法了$R;" +
                                         "$R我把您送到您的世界吧$R;" +
                                         "$P泰迪岛是梦之岛$R;" +
                                         "$R随时过来，我会等您哦$R;");
                            Warp(pc, 10023000, 127, 150);
                            //if (Acropolisut_01_mask.Test(Acropolisut_01.上城泰迪那里传送到泰迪岛))
                            //{
                            //    Wait(pc, 990);

                            //    x = (byte)Global.Random.Next(124, 130);
                            //    y = (byte)Global.Random.Next(150, 156);

                            //    Warp(pc, 10023000, x, y);
                            //    return;
                            //}
                            //Warp(pc, pc.SaveMap, pc.SaveX, pc.SaveY);
                            break;

                        case 2:
                            if (!Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟泰迪的伙伴们打完招呼))
                            {
                                Say(pc, 11000723, 131, ".....$R还没找到伙伴们么？$R;");
                            }
                            Say(pc, 11000723, 131, ".....$R..........$R;");
                            break; 
                    }
                    break;

                case 2:
                    if (!Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟泰迪的伙伴们打完招呼))
                    {
                        Say(pc, 11000723, 131, ".....$R还没找到伙伴们么？$R;");
                    }
                    Say(pc, 11000723, 131, ".....$R..........$R;");
                    break;
            }
            }

    }
}


