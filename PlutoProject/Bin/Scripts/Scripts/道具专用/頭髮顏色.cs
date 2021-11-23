using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public class 頭髮顏色 : Item
    {
        public 頭髮顏色()
        {
            //頭髮顏色 – 櫻桃紅
            Init(11000416, delegate(ActorPC pc)
            {
                pc.HairColor = 1;
            });

            //頭髮顏色 – 紫
            Init(11000417, delegate(ActorPC pc)
            {
                pc.HairColor = 2;
            });

            //頭髮顏色 – 藍
            Init(11000418, delegate(ActorPC pc)
            {
                pc.HairColor = 3;
            });

            //頭髮顏色 – 冰藍
            Init(11000419, delegate(ActorPC pc)
            {
                pc.HairColor = 4;
            });

            //頭髮顏色 – 綠
            Init(11000420, delegate(ActorPC pc)
            {
                pc.HairColor = 5;
            });

            //頭髮顏色 – 青綠
            Init(11000421, delegate(ActorPC pc)
            {
                pc.HairColor = 6;
            });

            //頭髮顏色 – 黃
            Init(11000422, delegate(ActorPC pc)
            {
                pc.HairColor = 7;
            });
            //頭髮顏色 – 橙
            Init(11000423, delegate(ActorPC pc)
            {
                pc.HairColor = 8;
            });

            //頭髮顏色 – 純黑
            Init(11000424, delegate(ActorPC pc)
            {
                pc.HairColor = 9;
            });

            //頭髮顏色 – 灰黑
            Init(11000425, delegate(ActorPC pc)
            {
                pc.HairColor = 10;
            });

            //頭髮顏色 – 閃亮
            Init(11000426, delegate(ActorPC pc)
            {
                pc.HairColor = 11;
            });

            //頭髮顏色 – 銀
            Init(11000427, delegate(ActorPC pc)
            {
                pc.HairColor = 12;
            });

            //頭髮顏色 – 埃米爾淺色
            Init(11000439, delegate(ActorPC pc)
            {
                pc.HairColor = 50;
            });

            //頭髮顏色 – 埃米爾中色
            Init(11000440, delegate(ActorPC pc)
            {
                pc.HairColor = 51;
            });

            //頭髮顏色 – 埃米爾深色
            Init(11000441, delegate(ActorPC pc)
            {
                pc.HairColor = 52;
            });

            //頭髮顏色 – 塔妮亞淺色
            Init(11000442, delegate(ActorPC pc)
            {
                pc.HairColor = 60;
            });

            //頭髮顏色 – 塔妮亞中色
            Init(11000443, delegate(ActorPC pc)
            {
                pc.HairColor = 61;
            });

            //頭髮顏色 – 塔妮亞深色
            Init(11000444, delegate(ActorPC pc)
            {
                pc.HairColor = 62;
            });

            //頭髮顏色 – 道米尼淺色
            Init(11000445, delegate(ActorPC pc)
            {
                pc.HairColor = 70;
            });

            //頭髮顏色 – 道米尼淺中色
            Init(11000446, delegate(ActorPC pc)
            {
                pc.HairColor = 71;
            });

            //頭髮顏色 – 道米尼淺深色
            Init(11000447, delegate(ActorPC pc)
            {
                pc.HairColor = 72;
            });

            //頭髮顏色 - 秋
            Init(90000052, delegate(ActorPC pc)
            {
                pc.HairColor = 18;
            });

            //頭髮顏色 - 冬
            Init(90000053, delegate(ActorPC pc)
            {
                pc.HairColor = 19;
            });

            //頭髮顏色 - 春
            Init(90000054, delegate(ActorPC pc)
            {
                pc.HairColor = 20;//82
            });

            //頭髮顏色 - 夏
            Init(90000055, delegate(ActorPC pc)
            {
                pc.HairColor = 21;//83
            });
        }
    }
}