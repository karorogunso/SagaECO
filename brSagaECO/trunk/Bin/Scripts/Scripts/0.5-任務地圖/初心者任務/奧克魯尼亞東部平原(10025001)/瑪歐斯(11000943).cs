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

            Say(pc, 11000941, 131, "$P那個是「石像瑪歐斯」喔!$R;" +
                                   "是「活動木偶」的一種…$R;" +
                                   "$R嗯， 說明起來會比較複雜啊!$R;" +
                                   "$P在「阿高普路斯市」的「上城」，$R;" +
                                   "有一個叫埃琳娜的孩子，$R;" +
                                   "會給您簡單地介紹唷!$R;" +
                                   "$R去看看她吧!$R;" +
                                   "$P她現在還在看店呢。$R;" +
                                   "$P等您得到活動木偶後，$R;" +
                                   "去見見「木偶使總管」吧!$R;" +
                                   "$R他是活動木偶的專家，$R;" +
                                   "會詳細地告訴您相關情報呀!$R;", "瑪莎");
        }

        void 初次向瑪莎詢問瑪歐斯的情報(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            Beginner_02_mask.SetValue(Beginner_02.向瑪莎第一次詢問瑪歐斯的情報, true);

            Say(pc, 11000941, 131, "想知道這尾魚啊?$R;" +
                                   "$P那個是「石像瑪歐斯」喔!$R;" +
                                   "是「活動木偶」的一種…$R;" +
                                   "$R嗯， 說明起來會比較複雜啊!$R;" +
                                   "$P在阿高普路斯市上城，$R;" +
                                   "有一個叫埃琳娜的孩子，$R;" +
                                   "會給您簡單地介紹唷!$R;" +
                                   "$R去看看她吧!$R;" +
                                   "$P她現在還在看店呢。$R;" +
                                   "$R登出ECO世界後，$R;" +
                                   "也可以利用石像開店或收集道具唷!$R;" +
                                   "$P等您得到活動木偶後，$R;" +
                                   "去見見「木偶使總管」吧!$R;" +
                                   "$R他是活動木偶的專家，$R;" +
                                   "會詳細地告訴您相關情報呀!$R;" +
                                   "$P「木偶使總管」就在「上城」的$R;" +
                                   "「行會宮殿」裡，$R;" +
                                   "$R「行會宮殿」就在$R;" +
                                   "城市北邊的巨大建築物，$R;" +
                                   "很容易找到的。$R;" +
                                   "$P對了! 先給您這個吧!$R;" +
                                   "這個是『石像目錄』阿$R;" +
                                   "$R用這工具，可以看到同一地區裡，$R;" +
                                   "所有玩家的商店!!$R;" +
                                   "真的很方便唷~!$R;" +
                                   "$P只要雙擊目錄，就可以使用了。$R;" +
                                   "$R到「下城」的話，試試看吧!$R;", "瑪莎");

            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 10029600, 1);
            Say(pc, 0, 0, "得到『石像目錄』!$R;", " ");
        } 
    }
}
