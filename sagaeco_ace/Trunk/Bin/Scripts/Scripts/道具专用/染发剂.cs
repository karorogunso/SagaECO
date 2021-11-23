using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public class 头发颜色 : Item
    {
        public 头发颜色()
        {
            //头发颜色 – 樱桃紅
            Init(11000416, delegate(ActorPC pc)
            {

                TakeItem(pc, 10031301, 1);
                pc.HairColor = 1;
            });

            //头发颜色 – 紫
            Init(11000417, delegate(ActorPC pc)
            {

                TakeItem(pc, 10031302, 1);
                pc.HairColor = 2;
            });

            //头发颜色 – 蓝
            Init(11000418, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031303, 1);
                pc.HairColor = 3;
            });

            //头发颜色 – 冰蓝
            Init(11000419, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031304, 1);
                pc.HairColor = 4;
            });

            //头发颜色 – 绿
            Init(11000420, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031305, 1);
                pc.HairColor = 5;
            });

            //头发颜色 – 青绿
            Init(11000421, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031306, 1);
                pc.HairColor = 6;
            });

            //头发颜色 – 黄
            Init(11000422, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031307, 1);
                pc.HairColor = 7;
            });
            //头发颜色 – 橙
            Init(11000423, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031308, 1);
                pc.HairColor = 8;
            });

            //头发颜色 – 纯黑
            Init(11000424, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031309, 1);
                pc.HairColor = 9;
            });

            //头发颜色 – 灰黑
            Init(11000425, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031310, 1);
                pc.HairColor = 10;
            });

            //头发颜色 – 闪亮
            Init(11000426, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031311, 1);
                pc.HairColor = 11;
            });

            //头发颜色 – 银
            Init(11000427, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031312, 1);
                pc.HairColor = 12;
            });

            //头发颜色 – 埃米尔浅色
            Init(11000439, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031313, 1);
                pc.HairColor = 50;
            });

            //头发颜色 – 埃米尔中色
            Init(11000440, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031314, 1);
                pc.HairColor = 51;
            });

            //头发颜色 – 埃米尔深色
            Init(11000441, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031315, 1);
                pc.HairColor = 52;
            });

            //头发颜色 – 塔尼亚浅色
            Init(11000442, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031316, 1);
                pc.HairColor = 60;
            });

            //头发颜色 – 塔尼亚中色
            Init(11000443, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031317, 1);
                pc.HairColor = 61;
            });

            //头发颜色 – 塔尼亚深色
            Init(11000444, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031318, 1);
                pc.HairColor = 62;
            });

            //头发颜色 – 道米尼浅色
            Init(11000445, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031319, 1);
                pc.HairColor = 70;
            });

            //头发颜色 – 道米尼浅中色
            Init(11000446, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031320, 1);
                pc.HairColor = 71;
            });

            //头发颜色 – 道米尼浅深色
            Init(11000447, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031321, 1);
                pc.HairColor = 72;
            });

            //头发颜色 - 秋
            Init(90000052, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031322, 1);
                pc.HairColor = 18;
            });

            //头发颜色 - 冬
            Init(90000053, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031323, 1);
                pc.HairColor = 19;
            });

            //头发颜色 - 春
            Init(90000054, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031326, 1);
                pc.HairColor = 20;//82
            });

            //头发颜色 - 夏
            Init(90000055, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031325, 1);
                pc.HairColor = 21;//83
            });
            
            //ヘアカラー・ペールゴールド
            Init(90000330, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031327, 1);
                pc.HairColor = 22;
            });
            
            //ヘアカラー・セラミックホワイト
            Init(90000331, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031328, 1);
                pc.HairColor = 80;
            });
            
            //ヘアカラー・ダークブルー
            Init(90000332, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031329, 1);
                pc.HairColor = 81;
            });
            
            //ヘアカラー・ライトバイオレット
            Init(90000333, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031330, 1);
                pc.HairColor = 82;
            });

            //ヘアカラー・メタリックピンク
            Init(90000342, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031331, 1);
                pc.HairColor = 23;
            });

            //ヘアカラー・モスグリーン
            Init(90000392, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031332, 1);
                pc.HairColor = 24;
            });

            //ヘアカラー・エメラルドグリーン
            Init(90000393, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031333, 1);
                pc.HairColor = 26;
            });

            //ヘアカラー・バイオレット
            Init(90000420, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031364, 1);
                pc.HairColor = 25;
            });

            //ヘアカラー・アッシュブラウン
            Init(90000421, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031365, 1);
                pc.HairColor = 27;
            });

            //ヘアカラー・リリーホワイト
            Init(90000422, delegate(ActorPC pc)
            {
                TakeItem(pc, 10031366, 1);
                pc.HairColor = 28;
            });

        }
    }
}