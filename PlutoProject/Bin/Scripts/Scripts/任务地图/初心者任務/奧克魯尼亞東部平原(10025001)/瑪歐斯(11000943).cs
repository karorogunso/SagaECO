using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:瑪歐斯(11000943) X:171 Y:96
namespace SagaScript.M10025001
{
    public class S11000943 : Event
    {
        public S11000943()
        {
            this.EventID = 11000943;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            if (!Beginner_02_mask.Test(Beginner_02.向瑪莎第一次詢問瑪歐斯的情報))
            {
                初次向瑪莎詢問瑪歐斯的情報(pc);
                return;
            }

            Say(pc, 11000941, 131, "$P那个是「石像鱼人」喔!$R;" +
                                   "是「活动木偶」的一种…$R;" +
                                   "$R嗯， 说明起来会比较复杂啊!$R;" +
                                   "$P在「阿克罗波利斯」的「上城」，$R;" +
                                   "有一个叫埃琳娜的孩子，$R;" +
                                   "会给您简单地介绍哦!$R;" +
                                   "$R去看看她吧!$R;" +
                                   "$P她现在还在看店呢。$R;" +
                                   "$P等您得到活动木偶后，$R;" +
                                   "去见见「人偶师总管」吧!$R;" +
                                   "$R他是活动木偶的专家，$R;" +
                                   "会详细地告诉您相关情报呀!$R;", "玛莎");
        }

        void 初次向瑪莎詢問瑪歐斯的情報(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            Beginner_02_mask.SetValue(Beginner_02.向瑪莎第一次詢問瑪歐斯的情報, true);

            Say(pc, 11000941, 131, "想知道这尾鱼啊?$R;" +
                                   "$P那个是「石像鱼人」喔!$R;" +
                                   "是「活动木偶」的一种…$R;" +
                                   "$R嗯， 说明起来会比较复杂啊!$R;" +
                                   "$P在阿克罗波利斯上城，$R;" +
                                   "有一个叫埃琳娜的孩子，$R;" +
                                   "会给您简单地介绍哦!$R;" +
                                   "$R去看看她吧!$R;" +
                                   "$P她现在还在看店呢。$R;" +
                                   "$R登出ECO世界后，$R;" +
                                   "也可以利用石像开店或收集道具!$R;" +
                                   "$P等您得到活动木偶后，$R;" +
                                   "去见见「人偶师总管」吧!$R;" +
                                   "$R他是活动木偶的专家，$R;" +
                                   "会详细地告诉您相关情报呀!$R;" +
                                   "$P「人偶师总管」就在「上城」的$R;" +
                                   "「行会宫殿」裡，$R;" +
                                   "$R「行会宫殿」就在$R;" +
                                   "城市北边的巨大建筑物，$R;" +
                                   "很容易找到的。$R;" +
                                   "$P对了! 先给您这个吧!$R;" +
                                   "这个是『石像目录』阿$R;" +
                                   "$R用这工具，可以看到同一地区里，$R;" +
                                   "所有玩家的商店!!$R;" +
                                   "真的很方便哦~!$R;" +
                                   "$P只要双击目录，就可以使用了。$R;" +
                                   "$R到「下城」的话，试试看吧!$R;", "玛莎");

            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 10029600, 1);
            Say(pc, 0, 0, "得到『石像目录』!$R;", " ");
        } 
    }
}
