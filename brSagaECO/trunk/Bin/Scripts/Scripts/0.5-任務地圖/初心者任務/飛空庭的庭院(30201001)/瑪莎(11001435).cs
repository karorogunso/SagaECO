using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:飛空庭的庭院(30201001) NPC基本信息:瑪莎(11001435) X:9 Y:10
namespace SagaScript.M30201001
{
    public class S11001435 : Event
    {
        public S11001435()
        {
            this.EventID = 11001435;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.已經與瑪莎進行第一次對話))
            {
                與瑪莎進行第一次對話(pc);
                return;
            }
            else
            {
                與瑪莎進行第二次對話(pc);
                return;
            }
        }

        void 與瑪莎進行第一次對話(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            Beginner_01_mask.SetValue(Beginner_01.已經與瑪莎進行第一次對話, true);

            Say(pc, 11001435, 131, "歡迎來到飛空庭唷!$R;" +
                                   "$P飛空庭是飛在空中的家和庭園喔!$R;" +
                                   "$R在ECO世界，$R;" +
                                   "您可以擁有自己的房子和庭園。$R;" +
                                   "$P飛空庭是由「唐卡」的工匠製作的。$R;" +
                                   "$R收集部件有點困難呀…$R;" +
                                   "$P對了!$R;" +
                                   "$R有個村落裡有個工匠，找找看吧，$R;" +
                                   "他人挺好的，$R;" +
                                   "每星期都會免費分發一些部件唷!$R;" +
                                   "$P聽說好像在南部的什麼地方…$R;" +
                                   "$P飛空庭啊!$R;" +
                                   "$R是在ECO世界裡，$R;" +
                                   "非常重要的交通工具唷!$R;" +
                                   "$P要到「摩根島」$R;" +
                                   "一定要搭乘飛空庭呀!$R;" +
                                   "$R航道是由軍隊和行會管理的。$R;" +
                                   "$P一般人擁有的飛空庭$R;" +
                                   "只能當做房子和庭園呀!$R;" +
                                   "$R房子蓋好了，可以照喜好隨意裝修喔!$R;" +
                                   "您可以讓朋友進入，$R;" +
                                   "或指定一些人進入，$R;" +
                                   "也可以選擇不開放。$R;" +
                                   "$P每個人隨著自己的愛好。$R;" +
                                   "可以呈現出獨一無二的風格唷!$R;" +
                                   "$R還有，$R;" +
                                   "飛空庭只能在指定地點歸還呀!$R;" +
                                   "$R只能在有飛空庭專用的「機場」，$R;" +
                                   "才能歸還呢!$R;" +
                                   "$P其實這裡是不允許歸還的。$R;" +
                                   "$R為了給您看看，才破例的喔!$R;" +
                                   "$P進房子裡看一看吧?$R;" +
                                   "$R如果您想到下一階段的話，$R;" +
                                   "跟我說一聲就行了!$R;", "瑪莎");
        }

        void 與瑪莎進行第二次對話(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);
            BitMask<Beginner_02> Beginner_02_mask = pc.CMask["Beginner_02"];

            byte x, y;

            if (!Beginner_02_mask.Test(Beginner_02.得到巧克力碎餅乾和果汁))
            {
                Beginner_02_mask.SetValue(Beginner_02.得到巧克力碎餅乾和果汁, true);
                Say(pc, 11001435, 131, "這裡準備了餅乾!$R;" +
                                       "一邊吃，一邊休息吧!$R;", "瑪莎");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 10009450, 1);
                GiveItem(pc, 10001600, 1);
                Say(pc, 0, 131, "得到『巧克力碎餅乾』和『果汁』!$R;", " ");
            }
            if (Beginner_01_mask.Test(Beginner_01.發現床底下的東西))
            {
                ShowEffect(pc, 11000938, 4520);
                Wait(pc, 990);

                Say(pc, 11001435, 131, "啊啊啊啊!!$R;" +
                                       "$R這是「合成失敗物」??$R;" +
                                       "這是哪來的呀?$R;" +
                                       "$R什麼? 飛天鼠發現的?$R;" +
                                       "$P唉…$R;" +
                                       "$P被發現就沒有辦法了。$R;" +
                                       "$R烹調或合成，也有失敗的時候吧!$R;" +
                                       "那時產生的…就是 『合成失敗物』啦!$R;", "瑪莎");

                ShowEffect(pc, 11000938, 4507);
                Wait(pc, 990);

                Say(pc, 11001435, 131, "不是經常失敗的，$R;" +
                                       "只是偶~~~爾吧!!$R;", "瑪莎");

                Say(pc, 11001435, 134, "嗚嗚…$R;" +
                                       "$R本來想以後自己拿到古董商店那裡的。$R;" +
                                       "$P現在去不去下一階段呢?$R;", "瑪莎");
            }

            switch (Select(pc, "去下一階段嗎?", "", "去下一階段", "再告訴我一次關於飛空庭的情報", "再休息一會兒"))
            {
                case 1:
                    Say(pc, 11001435, 131, "那麼現在走吧!$R;" +
                                           "$P不過……$R;" +
                                           "$R從剛才那個地方到城市，$R;" +
                                           "需要一些時間喔!$R;" +
                                           "$P所以我用飛空庭$R;" +
                                           "送您到那裡吧!$R;" +
                                           "$P其實是不准使用飛空庭的。$R;" +
                                           "$R但是幫助初心者，應該沒問題啦!$R;" +
                                           "$P那麼出發吧!$R;", "瑪莎");

                    PlaySound(pc, 2438, false, 100, 50);
                    Wait(pc, 1980);

                    x = (byte)Global.Random.Next(171, 174);
                    y = (byte)Global.Random.Next(100, 103);

                    Warp(pc, 10025001, x, y);
                    break;

                case 2:
                    Say(pc, 11001435, 131, "飛空庭是由「唐卡」的工匠製作的唷!$R;" +
                                           "$R收集部件有點困難呀…$R;" +
                                           "$P對了!$R;" +
                                           "$R有個村落裡有個工匠，找找看吧，$R;" +
                                           "他人挺好的，$R;" +
                                           "每星期都會免費分發一些部件唷!$R;" +
                                           "$P聽說好像在南部的什麼地方…$R;" +
                                           "$P飛空庭啊!$R;" +
                                           "$R是在ECO世界裡，$R;" +
                                           "非常重要的交通工具唷!$R;" +
                                           "$P要到「摩根島」$R;" +
                                           "一定要搭乘飛空庭呀!$R;" +
                                           "$R航道是由軍隊和行會管理的。$R;" +
                                           "$P一般人擁有的飛空庭$R;" +
                                           "只能當做房子和庭園呀!$R;" +
                                           "$R房子蓋好了，可以照喜好隨意裝修喔!$R;" +
                                           "您可以讓朋友進入，$R;" +
                                           "或指定一些人進入，$R;" +
                                           "也可以選擇不開放。$R;" +
                                           "$P每個人隨著自己的愛好。$R;" +
                                           "可以呈現出獨一無二的風格唷!$R;" +
                                           "$R還有，$R;" +
                                           "飛空庭只能在指定地點歸還呀!$R;" +
                                           "$R只能在有飛空庭專用的「機場」，$R;" +
                                           "才能歸還呢!$R;" +
                                           "$P其實這裡是不允許歸還的。$R;" +
                                           "$R為了給您看看，才破例的喔!$R;" +
                                           "$P進房子裡看一看吧?$R;" +
                                           "$R如果您想到下一階段的話，$R;" +
                                           "跟我說一聲就行了!$R;", "瑪莎");
                    break;

                case 3:
                    break;
            }
        }
    }
}
