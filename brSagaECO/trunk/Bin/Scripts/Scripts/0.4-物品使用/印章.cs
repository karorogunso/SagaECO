using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public class 印章 : Item
    {
        public 印章()
        {
            //皮露露印章
            Init(10050900, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Pururu, StampSlot.One))
                    TakeItem(pc, 10050900, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //乳白皮丘 印章
            Init(10050901, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Pururu, StampSlot.Two))
                    TakeItem(pc, 10050901, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //琥珀杰利印章
            Init(10050902, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Pururu, StampSlot.Three))
                    TakeItem(pc, 10050902, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //咕嚕印章
            Init(10050903, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Pururu, StampSlot.Four))
                    TakeItem(pc, 10050903, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //黃色皮露露印章
            Init(10050904, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Pururu, StampSlot.Five))
                    TakeItem(pc, 10050904, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //柏油杰利印章
            Init(10050905, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Pururu, StampSlot.Six))
                    TakeItem(pc, 10050905, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //綠色皮露露 印章
            Init(10050906, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Pururu, StampSlot.Seven))
                    TakeItem(pc, 10050906, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //魔法皮露露 印章
            Init(10050907, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Pururu, StampSlot.Eight))
                    TakeItem(pc, 10050907, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //紅色杰利印章
            Init(10050908, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Pururu, StampSlot.Nine))
                    TakeItem(pc, 10050908, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //迷路的金屬球印章
            Init(10050909, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Pururu, StampSlot.Ten))
                    TakeItem(pc, 10050909, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //海膽印章
            Init(10050910, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Field, StampSlot.One))
                    TakeItem(pc, 10050910, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //爬爬蟲印章
            Init(10050911, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Field, StampSlot.Two))
                    TakeItem(pc, 10050911, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //殺人蜂印章
            Init(10050912, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Field, StampSlot.Three))
                    TakeItem(pc, 10050912, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //咕咕印章
            Init(10050913, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Field, StampSlot.Four))
                    TakeItem(pc, 10050913, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //巴嗚印章
            Init(10050914, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Field, StampSlot.Five))
                    TakeItem(pc, 10050914, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //沙拉海膽印章
            Init(10050915, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Field, StampSlot.Six))
                    TakeItem(pc, 10050915, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //毛毛蟲印章
            Init(10050916, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Field, StampSlot.Seven))
                    TakeItem(pc, 10050916, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //土蜘蛛印章
            Init(10050917, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Field, StampSlot.Eight))
                    TakeItem(pc, 10050917, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //大型爬爬蟲印章
            Init(10050918, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Field, StampSlot.Nine))
                    TakeItem(pc, 10050918, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //熊印章
            Init(10050919, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Field, StampSlot.Ten))
                    TakeItem(pc, 10050919, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //緋紅巴嗚印章
            Init(10050920, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Coast, StampSlot.One))
                    TakeItem(pc, 10050920, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //古代咕咕雞印章
            Init(10050921, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Coast, StampSlot.Two))
                    TakeItem(pc, 10050921, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //耶爾德蠕蟲印章
            Init(10050922, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Coast, StampSlot.Three))
                    TakeItem(pc, 10050922, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //飛魚印章
            Init(10050923, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Coast, StampSlot.Four))
                    TakeItem(pc, 10050923, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //冰裂艾西印章
            Init(10050924, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Coast, StampSlot.Five))
                    TakeItem(pc, 10050924, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //白澎獸印章
            Init(10050925, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Coast, StampSlot.Six))
                    TakeItem(pc, 10050925, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //寶拉熊印章
            Init(10050926, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Coast, StampSlot.Seven))
                    TakeItem(pc, 10050926, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //獨角獸印章
            Init(10050927, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Coast, StampSlot.Eight))
                    TakeItem(pc, 10050927, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //莫潔株印章
            Init(10050928, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Coast, StampSlot.Nine))
                    TakeItem(pc, 10050928, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //水晶印章
            Init(10050929, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Coast, StampSlot.Ten))
                    TakeItem(pc, 10050929, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //沙礫爬爬蟲印章
            Init(10050930, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Wild, StampSlot.One))
                    TakeItem(pc, 10050930, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //油輪印章
            Init(10050931, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Wild, StampSlot.Two))
                    TakeItem(pc, 10050931, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //螞蜂印章
            Init(10050932, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Wild, StampSlot.Three))
                    TakeItem(pc, 10050932, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //路普印章
            Init(10050933, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Wild, StampSlot.Four))
                    TakeItem(pc, 10050933, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //木乃伊印章
            Init(10050934, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Wild, StampSlot.Five))
                    TakeItem(pc, 10050934, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //骷髏骨印章
            Init(10050935, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Wild, StampSlot.Six))
                    TakeItem(pc, 10050935, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //蜂后印章
            Init(10050936, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Wild, StampSlot.Seven))
                    TakeItem(pc, 10050936, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //毒蜥蜴印章
            Init(10050937, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Wild, StampSlot.Eight))
                    TakeItem(pc, 10050937, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //達卡爾印章
            Init(10050938, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Wild, StampSlot.Nine))
                    TakeItem(pc, 10050938, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //破壞 RX1 印章
            Init(10050939, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Wild, StampSlot.Ten))
                    TakeItem(pc, 10050939, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //鋯石I印章
            Init(10050940, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Cave, StampSlot.One))
                    TakeItem(pc, 10050940, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //德拉奇印章
            Init(10050941, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Cave, StampSlot.Two))
                    TakeItem(pc, 10050941, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //小幽浮印章
            Init(10050942, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Cave, StampSlot.Three))
                    TakeItem(pc, 10050942, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //山洞洛基印章
            Init(10050943, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Cave, StampSlot.Four))
                    TakeItem(pc, 10050943, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //魔吉兒印章
            Init(10050944, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Cave, StampSlot.Five))
                    TakeItem(pc, 10050944, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //乾屍印章
            Init(10050945, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Cave, StampSlot.Six))
                    TakeItem(pc, 10050945, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //洞穴飛魚印章
            Init(10050946, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Cave, StampSlot.Seven))
                    TakeItem(pc, 10050946, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //喪屍印章
            Init(10050947, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Cave, StampSlot.Eight))
                    TakeItem(pc, 10050947, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //雷光小幽浮印章
            Init(10050948, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Cave, StampSlot.Nine))
                    TakeItem(pc, 10050948, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //雷魚印章
            Init(10050949, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Cave, StampSlot.Ten))
                    TakeItem(pc, 10050949, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //埃齊印章
            Init(10050950, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Snow, StampSlot.One))
                    TakeItem(pc, 10050950, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //圓莉多印章
            Init(10050951, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Snow, StampSlot.Two))
                    TakeItem(pc, 10050951, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //企鵝印章
            Init(10050952, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Snow, StampSlot.Three))
                    TakeItem(pc, 10050952, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //雪花水晶印章
            Init(10050953, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Snow, StampSlot.Four))
                    TakeItem(pc, 10050953, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //使魔印章
            Init(10050954, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Snow, StampSlot.Five))
                    TakeItem(pc, 10050954, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //塞克洛斯印章
            Init(10050955, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Snow, StampSlot.Six))
                    TakeItem(pc, 10050955, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //黑暗羽毛印章
            Init(10050956, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Snow, StampSlot.Seven))
                    TakeItem(pc, 10050956, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //雷鳥印章
            Init(10050957, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Snow, StampSlot.Eight))
                    TakeItem(pc, 10050957, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //死神印章
            Init(10050958, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Snow, StampSlot.Nine))
                    TakeItem(pc, 10050958, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //得菩提印章
            Init(10050959, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Snow, StampSlot.Ten))
                    TakeItem(pc, 10050959, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //狼蛛印章
            Init(10050960, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Colliery, StampSlot.One))
                    TakeItem(pc, 10050960, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //克伯特印章
            Init(10050961, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Colliery, StampSlot.Two))
                    TakeItem(pc, 10050961, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //緋紅積巴印章
            Init(10050962, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Colliery, StampSlot.Three))
                    TakeItem(pc, 10050962, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //夢零星印章
            Init(10050963, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Colliery, StampSlot.Four))
                    TakeItem(pc, 10050963, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //金屬海膽印章
            Init(10050964, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Colliery, StampSlot.Five))
                    TakeItem(pc, 10050964, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //鋯石II印章
            Init(10050965, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Colliery, StampSlot.Six))
                    TakeItem(pc, 10050965, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //喀布爾印章
            Init(10050966, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Colliery, StampSlot.Seven))
                    TakeItem(pc, 10050966, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //殺人野獸印章
            Init(10050967, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Colliery, StampSlot.Eight))
                    TakeItem(pc, 10050967, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //金屬球印章
            Init(10050968, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Colliery, StampSlot.Nine))
                    TakeItem(pc, 10050968, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //黑熊印章
            Init(10050969, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Colliery, StampSlot.Ten))
                    TakeItem(pc, 10050969, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //迷你多印章
            Init(10050970, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Northan, StampSlot.One))
                    TakeItem(pc, 10050970, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //梅花艾卡納印章
            Init(10050971, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Northan, StampSlot.Two))
                    TakeItem(pc, 10050971, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //骷髏骨弓手印章
            Init(10050972, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Northan, StampSlot.Three))
                    TakeItem(pc, 10050972, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //方塊艾卡納印章
            Init(10050973, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Northan, StampSlot.Four))
                    TakeItem(pc, 10050973, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //紅心艾卡納印章
            Init(10050974, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Northan, StampSlot.Five))
                    TakeItem(pc, 10050974, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //黑桃艾卡納印章
            Init(10050975, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Northan, StampSlot.Six))
                    TakeItem(pc, 10050975, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //艾卡納J牌印章
            Init(10050976, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Northan, StampSlot.Seven))
                    TakeItem(pc, 10050976, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //阿歐哥印章
            Init(10050977, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Northan, StampSlot.Eight))
                    TakeItem(pc, 10050977, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //艾卡納王后印章
            Init(10050978, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Northan, StampSlot.Nine))
                    TakeItem(pc, 10050978, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //重裝兵印章
            Init(10050979, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Northan, StampSlot.Ten))
                    TakeItem(pc, 10050979, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //火岩皮露露印章
            Init(10050980, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.IronSouth, StampSlot.One))
                    TakeItem(pc, 10050980, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //南極企鵝印章
            Init(10050981, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.IronSouth, StampSlot.Two))
                    TakeItem(pc, 10050981, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //康康印章
            Init(10050982, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.IronSouth, StampSlot.Three))
                    TakeItem(pc, 10050982, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //熱帶飛魚印章
            Init(10050983, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.IronSouth, StampSlot.Four))
                    TakeItem(pc, 10050983, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //詩納摩印章
            Init(10050984, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.IronSouth, StampSlot.Five))
                    TakeItem(pc, 10050984, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //蘿伊特印章
            Init(10050985, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.IronSouth, StampSlot.Six))
                    TakeItem(pc, 10050985, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //普莉普莉印章
            Init(10050986, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.IronSouth, StampSlot.Seven))
                    TakeItem(pc, 10050986, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //唧古印章
            Init(10050987, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.IronSouth, StampSlot.Eight))
                    TakeItem(pc, 10050987, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //怒火小幽浮印章
            Init(10050988, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.IronSouth, StampSlot.Nine))
                    TakeItem(pc, 10050988, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //破壞 RX3 印章
            Init(10050989, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.IronSouth, StampSlot.Ten))
                    TakeItem(pc, 10050989, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //白色使魔印章
            Init(10050990, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.SouthDungeon, StampSlot.One))
                    TakeItem(pc, 10050990, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //迪貝斯特印章
            Init(10050991, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.SouthDungeon, StampSlot.Two))
                    TakeItem(pc, 10050991, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //詛咒者印章
            Init(10050992, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.SouthDungeon, StampSlot.Three))
                    TakeItem(pc, 10050992, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //基米拉印章
            Init(10050993, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.SouthDungeon, StampSlot.Four))
                    TakeItem(pc, 10050993, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //拉米阿印章
            Init(10050994, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.SouthDungeon, StampSlot.Five))
                    TakeItem(pc, 10050994, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //破壞坦克印章
            Init(10050995, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.SouthDungeon, StampSlot.Six))
                    TakeItem(pc, 10050995, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //皮洛仕印章
            Init(10050996, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.SouthDungeon, StampSlot.Seven))
                    TakeItem(pc, 10050996, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //道米尼背德者印章
            Init(10050997, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.SouthDungeon, StampSlot.Eight))
                    TakeItem(pc, 10050997, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //道米尼高位背德者印章
            Init(10050998, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.SouthDungeon, StampSlot.Nine))
                    TakeItem(pc, 10050998, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //鳳凰印章
            Init(10050999, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.SouthDungeon, StampSlot.Ten))
                    TakeItem(pc, 10050999, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //兜襠布印章
            Init(10051000, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Special, StampSlot.One))
                    TakeItem(pc, 10051000, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //破損的縫製玩偶印章
            Init(10051001, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Special, StampSlot.Two))
                    TakeItem(pc, 10051001, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //最強之魔獸印章
            Init(10051002, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Special, StampSlot.Three))
                    TakeItem(pc, 10051002, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //白熊印章
            Init(10051003, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Special, StampSlot.Four))
                    TakeItem(pc, 10051003, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //皇路普印章
            Init(10051004, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Special, StampSlot.Five))
                    TakeItem(pc, 10051004, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //巨大咕咕印章
            Init(10051005, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Special, StampSlot.Six))
                    TakeItem(pc, 10051005, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //雷王印章
            Init(10051006, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Special, StampSlot.Seven))
                    TakeItem(pc, 10051006, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
            //冥王印章
            Init(10051007, delegate(ActorPC pc)
            {
                if (UseStamp(pc, StampGenre.Special, StampSlot.Eight))
                    TakeItem(pc, 10051007, 1);
                else
                    Say(pc, 0, 131, "那個印章已經蓋了$R;");
            });
        }
    }
}