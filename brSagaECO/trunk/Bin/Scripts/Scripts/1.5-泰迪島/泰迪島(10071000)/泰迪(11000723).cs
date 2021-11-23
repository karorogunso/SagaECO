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
                         "$R哦？嚇著了？這裡是哪兒？$R;" +
                         "$P這裡是泰迪島喔$R;" +
                         "$R只能在夢裡才能看到的神秘之島唷$R;" +
                         "$P我叫泰迪，是『活動木偶泰迪』！$R;" +
                         "$P這座島上到處都是我的朋友，$R去找找吧$R;" +
                         "$P先聽完別人的話，$R然後再來跟我說話吧$R;");
            Say(pc, 11000723, 131, "哦？什麼叫『活動木偶』呢？$R;" +
                         "$R『活動木偶』指的是，$R靈魂可以附體的，$R神秘道具唷$R;" +
                         "$P在這裡經常出現，$R靈魂附在貌似人的物體內的現象$R;" +
                         "$P活動木偶$R是像我一樣的玩偶或機器人、植物等$R精靈的力量作為形象的$R;" +
                         "$R形態非常多樣$R;" +
                         "$P變身的時間只需3分鐘$R;" +
                         "$R但變身解除後5分鐘内$R無法變身，請注意唷$R;");
        }

        void 跟泰迪同伴打完招呼后去变身(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);
            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.跟泰迪的伙伴们打完招呼, true);
            Say(pc, 11000723, 131, "跟我的同伴活動木偶們打招呼了嗎？$R最後我再介紹一下。$R;" +
                                   "$P我叫泰迪『$R活動木偶泰迪』$R;");
            Say(pc, 11000723, 131, "我是玩偶外形的$R神秘活動木偶$R;" +
                                   "$P變成我後，可以使用$R『嗚嗚嗚啊~』，$R把周圍的魔物搞混亂，$R弄的它們動彈不得。$R;" +
                                   "$P還有變身期間$R『MP』會自動恢復唷$R;");
            Say(pc, 11000723, 131, "怎麼樣？想變成我嗎？$R;");
            switch (Select(pc, "怎麼辦呢？", "", "變身", "放棄"))
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
                "$P哎呀，太没勁了，$R再玩一會兒嗎？$R;" +
                "$R不過，執意要回去…$R;" +
                "$P跟湖邊的女孩見見面$R再回來吧。$R;" +
                "$R她是叫『微微』的孩子$R;" +
                "$R要不然走不了$R;" +
                "$P這個島裡有很多人，$R;" +
                "聽聽故事會有好處的唷。$R;");

        }

        void 可以回城咯(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);
            BitMask<Acropolisut_01> Acropolisut_01_mask = pc.CMask["Acropolisut_01"];
            byte x, y;
            Say(pc, 11000723, 131, "怎麼辦？回去嗎？$R;");
            switch (Select(pc, "怎麼辦呢？", "", "回去", "放棄"))
            {
                case 1:
                    Say(pc, 11000723, 131, "哦？$R真的要回去嗎？$R;");
                    switch (Select(pc, "怎麼辦呢？", "", "回去", "放棄"))
                    {
                        case 1:
                            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.指导地图的泰迪标示去过泰迪岛, true);
                            Say(pc, 11000723, 131, "是嗎？$R那没辦法了$R;" +
                                         "$R我把您送到您的世界吧$R;" +
                                         "$P泰迪島是夢之島$R;" +
                                         "$R隨時過來，我會等您唷$R;");
                            /*
                            if (Acropolisut_01_mask.Test(Acropolisut_01.上城泰迪那里传送到泰迪岛))
                            {
                                Wait(pc, 990);
                                
                                x = (byte)Global.Random.Next(124, 130);
                                y = (byte)Global.Random.Next(150, 156);

                                Warp(pc, 10023000, x, y);
                                return;
                            }
                            Warp(pc, pc.SaveMap, pc.SaveX, pc.SaveY);
                            */
                            Wait(pc, 990);
                            x = (byte)Global.Random.Next(124, 130);
                            y = (byte)Global.Random.Next(150, 156);
                            Warp(pc, 10023000, x, y);
                            break;

                        case 2:
                            if (!Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟泰迪的伙伴们打完招呼))
                            {
                                Say(pc, 11000723, 131, ".....$R還沒找到夥伴們么？$R;");
                            }
                            Say(pc, 11000723, 131, ".....$R..........$R;");
                            break; 
                    }
                    break;

                case 2:
                    if (!Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟泰迪的伙伴们打完招呼))
                    {
                        Say(pc, 11000723, 131, ".....$R還沒找到夥伴們么？$R;");
                    }
                    Say(pc, 11000723, 131, ".....$R..........$R;");
                    break;
            }
            }

    }
}


