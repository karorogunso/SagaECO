using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript
{
    //下面开始是ECO城
    public class P10001661 : RandomPortal
    {
        public P10001661()
        {
            Init(10001661, 11027000, 168, 154, 169, 157);
        }
    }
    //ECO温泉到沙滩

    public class P10001662 : RandomPortal
    {
        public P10001662()
        {
            Init(10001662, 11027000, 114, 105, 116, 109);
        }
    }
    //大厅到沙滩

    public class P10001663 : RandomPortal
    {
        public P10001663()
        {
            Init(10001663, 11027000, 178, 49, 179, 52);
        }
    }
    //市场到沙滩

    public class P10001664 : RandomPortal
    {
        public P10001664()
        {
            Init(10001664, 31300000, 45, 88, 47, 90);
        }
    }
    //沙滩到大厅

    public class P10001665 : RandomPortal
    {
        public P10001665()
        {
            Init(10001665, 31300000, 45, 19, 47, 20);
        }
    }
    //赌场到大厅1
    /*
        public class P11001907 : RandomPortal
        {
            public P11001907()
            {
                Init(11001907, 31300000, 45, 19, 47, 20);
            }
        }
    */
    //赌场到大厅2

    public class P10001666 : RandomPortal
    {
        public P10001666()
        {
            Init(10001666, 31300000, 72, 45, 74, 47);
        }
    }
    //市场到大厅

    public class P10001667 : RandomPortal
    {
        public P10001667()
        {
            Init(10001667, 31300000, 19, 45, 20, 47);
        }
    }
    //温泉入口到大厅

    public class P10001668 : RandomPortal
    {
        public P10001668()
        {
            Init(10001668, 31302000, 41, 76, 43, 76);
        }
    }
    //沙滩到市场

    public class P10001669 : RandomPortal
    {
        public P10001669()
        {
            Init(10001669, 31302000, 14, 44, 15, 47);
        }
    }
    //大厅到市场

    public class P10001670 : RandomPortal
    {
        public P10001670()
        {
            Init(10001670, 31301000, 15, 62, 18, 62);
        }
    }
    //大厅到赌场


    public class P10001671 : Event
    {
        public P10001671()
        {
            this.EventID = 10001671;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 31304000, 47, 83);
            NPCHide(pc, 11001905);
            NPCShow(pc, 11001865);
            NPCShow(pc, 11001864);
            NPCShow(pc, 11001863);
            NPCShow(pc, 11001862);
            NPCShow(pc, 11001861);
            NPCShow(pc, 11001860);
            NPCShow(pc, 11001859);
            NPCShow(pc, 11001858);
            NPCShow(pc, 11001857);
            NPCShow(pc, 11001856);
            NPCShow(pc, 11001855);
            NPCShow(pc, 11001854);
            NPCShow(pc, 11001853);
            NPCShow(pc, 11001852);
            NPCShow(pc, 11001851);
            NPCShow(pc, 11001850);
            NPCShow(pc, 11001849);
            NPCShow(pc, 11001848);
        }
    }
    //大厅到ECO温泉入口

    public class P10001672 : RandomPortal
    {
        public P10001672()
        {
            Init(10001672, 31304000, 29, 69, 30, 70);
        }
    }
    //男更衣室到温泉入口

    public class P10001673 : RandomPortal
    {
        public P10001673()
        {
            Init(10001673, 31304000, 65, 71, 66, 72);
        }
    }
    //女更衣室到温泉入口

    public class P10001674 : RandomPortal
    {
        public P10001674()
        {
            Init(10001674, 31304000, 5, 44, 9, 49);
        }
    }
    //温泉入口到男更衣室

    public class P10001675 : RandomPortal
    {
        public P10001675()
        {
            Init(10001675, 31304000, 8, 15, 10, 17);
        }
    }
    //温泉到男更衣室
    public class P10001676 : Event
    {
        public P10001676()
        {
            this.EventID = 10001676;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 31304000, 86, 44);
            NPCHide(pc, 11001905);
            NPCShow(pc, 11001865);
            NPCShow(pc, 11001864);
            NPCShow(pc, 11001863);
            NPCShow(pc, 11001862);
            NPCShow(pc, 11001861);
            NPCShow(pc, 11001860);
            NPCShow(pc, 11001859);
            NPCShow(pc, 11001858);
            NPCShow(pc, 11001857);
            NPCShow(pc, 11001856);
            NPCShow(pc, 11001855);
            NPCShow(pc, 11001854);
            NPCShow(pc, 11001853);
            NPCShow(pc, 11001852);
            NPCShow(pc, 11001851);
            NPCShow(pc, 11001850);
            NPCShow(pc, 11001849);
            NPCShow(pc, 11001848);
        }
    }
    //温泉入口到女更衣室

    public class P10001677 : RandomPortal
    {
        public P10001677()
        {
            Init(10001677, 31304000, 85, 15, 86, 17);
        }
    }
    //温泉到女更衣室

    public class P10001678 : RandomPortal
    {
        public P10001678()
        {
            Init(10001678, 31303000, 15, 76, 17, 79);
        }
    }
    //男更衣室到ECO温泉

    public class P10001679 : RandomPortal
    {
        public P10001679()
        {
            Init(10001679, 31303000, 43, 76, 45, 77);
        }
    }
    //女更衣室到ECO温泉

    public class P10001680 : RandomPortal
    {
        public P10001680()
        {
            Init(10001680, 31303000, 47, 4, 49, 6);
        }
    }
    //沙滩到ECO温泉

    public class P10001681 : RandomPortal
    {
        public P10001681()
        {
            Init(10001681, 31302000, 27, 8, 30, 9);
        }
    }
    //赌场到市场

    public class P10001682 : RandomPortal
    {
        public P10001682()
        {
            Init(10001682, 31301000, 56, 31, 57, 35);
        }
    }
    //市场到赌场

    public class P10001683 : RandomPortal
    {
        public P10001683()
        {
            Init(10001683, 10071000, 65, 212, 65, 212);
        }
    }
    //从沙滩回泰迪岛



    //下面开始是深渊

    public class P10001686 : RandomPortal
    {
        public P10001686()
        {
            Init(10001686, 21193000, 125, 105, 129, 107);
        }
    }

    public class P10001687 : RandomPortal
    {
        public P10001687()
        {
            Init(10001687, 21193000, 125, 141, 130, 143);
        }
    }

    public class P10001688 : RandomPortal
    {
        public P10001688()
        {
            Init(10001688, 21193000, 94, 112, 97, 114);
        }
    }

    public class P10001689 : RandomPortal
    {
        public P10001689()
        {
            Init(10001689, 21193000, 157, 101, 158, 106);
        }
    }

    public class P10001690 : RandomPortal
    {
        public P10001690()
        {
            Init(10001690, 21193000, 61, 142, 63, 145);
        }
    }

    public class P10001691 : RandomPortal
    {
        public P10001691()
        {
            Init(10001691, 21193000, 69, 113, 74, 115);
        }
    }

    public class P10001692 : RandomPortal
    {
        public P10001692()
        {
            Init(10001692, 21193000, 125, 38, 129, 40);
        }
    }

    public class P10001693 : RandomPortal
    {
        public P10001693()
        {
            Init(10001693, 21193000, 78, 25, 79, 29);
        }
    }

    public class P10001909 : Event
    {
        public P10001909()
        {
            this.EventID = 10001909;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "大海の孤島を離れます", "", "『天まで続く塔の島』へ行く", "『ウォーターレイアー』へ行く", "やめる"))
            {
                case 1:
                    Warp(pc, 11058000, 125, 244);
                    NPCHide(pc, 11002347);
                    break;

                case 2:
                    Warp(pc, 11053000, 21, 232);
                    break;
            }

        }

    }
    public class P10001815 : Event
    {
        public P10001815()
        {
            this.EventID = 10001815;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 0, 0, "――システムエラー$R;" +
                "――システムエラー$R;" +
                "$R――転送装置ヲ起動シマス$R;", "");
                ShowEffect(pc, 5212);
                Wait(pc, 1485);
                Warp(pc, 11073000, 251, 8);
            }
            else
            {
                Say(pc, 0, 0, "不思議な遺跡がある。$R;", "");
            }

        }

    }
    public class P10001817 : Event
    {
        public P10001817()
        {
            this.EventID = 10001817;
        }

        public override void OnEvent(ActorPC pc)
        {

            Warp(pc, 11073000, 98, 9);
        }

    }
    public class P10001816 : Event
    {
        public P10001816()
        {
            this.EventID = 10001816;
        }

        public override void OnEvent(ActorPC pc)
        {

            Warp(pc, 11073000, 237, 10);
        }

    }
    //庭园遗迹入口
    public class P10002019 : Event
    {
        public P10002019()
        {
            this.EventID = 10002019;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 0, 0, "空間の歪みが東西に$R;" +
            "のびているように見える。$R;", "");
            switch (Select(pc, "どうする？", "", "東へ進む", "西へ進む", "やめる"))
            {
                case 1:
                    Warp(pc, 11073050, 34, 59);
                    break;
                case 2:
                    Warp(pc, 11073051, 34, 59);
                    break;
            }
        }

    }
    //庭园遗迹出口
    public class P10002021 : Event
    {
        public P10002021()
        {
            this.EventID = 10002021;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 11073000, 171, 30);
        }

    }
    //ECOタウン跡去東アクロニア平原（タイタニア世界）
    public class P10001819 : Event
    {
        public P10001819()
        {
            this.EventID = 10001819;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 11025000, 254, 120);
        }

    }
    //東アクロニア平原（タイタニア世界）去ECOタウン跡
    public class P10001818 : Event
    {
        public P10001818()
        {
            this.EventID = 10001818;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 11073000, 1, 107);
        }

    }
    //エル・シエル
    public class P10001821 : Event
    {
        public P10001821()
        {
            this.EventID = 10001821;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 11024000, 127, 228);
        }

    }
    public class P10001820 : Event
    {
        public P10001820()
        {
            this.EventID = 10001820;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 11025000, 0, 127);
        }

    }
    //エル・シエル传送下層階：南東口
    public class P10001822 : Event
    {
        public P10001822()
        {
            this.EventID = 10001822;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "エル・シエル転送テレポート", "", "上層階ブロックへ移動する", "下層階ブロックへ移動する", "やめる"))
            {
                case 1:
                    switch (Select(pc, "エル・シエル転送テレポート", "", "上層階：北口に移動する", "上層階：南口に移動する", "やめる"))
                    {
                        case 1:
                            Warp(pc, 11023000, 127, 33);
                            break;
                        case 2:
                            Warp(pc, 11023000, 127, 221);
                            break;

                    }
                    break;
                case 2:
                    switch (Select(pc, "エル・シエル転送テレポート", "", "下層階：北東口に移動する", "下層階：北西口に移動する", "下層階：南西口に移動する", "やめる"))
                    {
                        case 1:
                            Warp(pc, 11024000, 145, 27);
                            break;
                        case 2:
                            Warp(pc, 11024000, 110, 27);
                            break;
                        case 3:
                            Warp(pc, 11024000, 110, 228);
                            break;

                    }
                    break;
            }

        }

    }
    //エル・シエル传送下層階：北東口
    public class P10001825 : Event
    {
        public P10001825()
        {
            this.EventID = 10001825;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "エル・シエル転送テレポート", "", "上層階ブロックへ移動する", "下層階ブロックへ移動する", "やめる"))
            {
                case 1:
                    switch (Select(pc, "エル・シエル転送テレポート", "", "上層階：北口に移動する", "上層階：南口に移動する", "やめる"))
                    {
                        case 1:
                            Warp(pc, 11023000, 127, 33);
                            break;
                        case 2:
                            Warp(pc, 11023000, 127, 221);
                            break;

                    }
                    break;
                case 2:
                    switch (Select(pc, "エル・シエル転送テレポート", "", "下層階：北西口に移動する", "下層階：南東口に移動する", "下層階：南西口に移動する", "やめる"))
                    {
                        case 1:
                            Warp(pc, 11024000, 110, 27);
                            break;
                        case 2:
                            Warp(pc, 11024000, 145, 228);
                            break;
                        case 3:
                            Warp(pc, 11024000, 110, 228);
                            break;

                    }
                    break;
            }

        }

    }
    //エル・シエル传送下層階：北西口
    public class P10001824 : Event
    {
        public P10001824()
        {
            this.EventID = 10001824;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "エル・シエル転送テレポート", "", "上層階ブロックへ移動する", "下層階ブロックへ移動する", "やめる"))
            {
                case 1:
                    switch (Select(pc, "エル・シエル転送テレポート", "", "上層階：北口に移動する", "上層階：南口に移動する", "やめる"))
                    {
                        case 1:
                            Warp(pc, 11023000, 127, 33);
                            break;
                        case 2:
                            Warp(pc, 11023000, 127, 221);
                            break;

                    }
                    break;
                case 2:
                    switch (Select(pc, "エル・シエル転送テレポート", "", "下層階：北東口に移動する", "下層階：南東口に移動する", "下層階：南西口に移動する", "やめる"))
                    {
                        case 1:
                            Warp(pc, 11024000, 145, 27);
                            break;
                        case 2:
                            Warp(pc, 11024000, 145, 228);
                            break;
                        case 3:
                            Warp(pc, 11024000, 110, 228);
                            break;

                    }
                    break;
            }

        }

    }
    //エル・シエル传送下層階：南西口
    public class P10001823 : Event
    {
        public P10001823()
        {
            this.EventID = 10001823;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "エル・シエル転送テレポート", "", "上層階ブロックへ移動する", "下層階ブロックへ移動する", "やめる"))
            {
                case 1:
                    switch (Select(pc, "エル・シエル転送テレポート", "", "上層階：北口に移動する", "上層階：南口に移動する", "やめる"))
                    {
                        case 1:
                            Warp(pc, 11023000, 127, 33);
                            break;
                        case 2:
                            Warp(pc, 11023000, 127, 221);
                            break;

                    }
                    break;
                case 2:
                    switch (Select(pc, "エル・シエル転送テレポート", "", "下層階：北東口に移動する", "下層階：南東口に移動する", "下層階：南西口に移動する", "やめる"))
                    {
                        case 1:
                            Warp(pc, 11024000, 145, 27);
                            break;
                        case 2:
                            Warp(pc, 11024000, 145, 228);
                            break;
                        case 3:
                            Warp(pc, 11024000, 110, 228);
                            break;

                    }
                    break;
            }

        }

    }
    public class P10001919 : Event
    {
        public P10001919()
        {
            this.EventID = 10001919;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 11025000, 0, 127);
        }
    }
    //原始地圖:奧克魯尼亞東部平原（塔妮亞世界）(11025000)
    //目標地圖:大海的孤島(11075000)
    //目標坐標:(252,121) ~ (52,47)
    public class P10001814 : Event
    {
        public P10001814()
        {
            this.EventID = 10001814;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 11075000, 52, 47);
        }
    }



}