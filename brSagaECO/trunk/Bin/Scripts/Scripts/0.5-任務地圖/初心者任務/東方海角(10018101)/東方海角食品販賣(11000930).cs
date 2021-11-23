using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018101) NPC基本信息:東方海角食品販賣(11000930) X:203 Y:98
namespace SagaScript.M10018101
{
    public class S11000930 : Event
    {
        public S11000930()
        {
            this.EventID = 11000930;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.已經與埃米爾進行第一次對話))
            {
                尚未與埃米爾對話(pc);
                return;
            }

            if (!Beginner_01_mask.Test(Beginner_01.NPC物品買賣教學完成))
            {
                NPC物品買賣教學(pc);
                return;
            }

            switch (Select(pc, "想再聽一遍嗎?", "", "再聽一遍", "不用了"))
            {
                case 1:
                    Say(pc, 11000930, 131, "這一次將簡單的說明。$R;" +
                                           "$P想賣道具時，選擇「賣東西」。$R;" +
                                           "$R就會顯示交易視窗，$R;" +
                                           "把想賣的道具移到右邊，$R;" +
                                           "點擊「售賣」。$R;" +
                                           "$R這樣就完成了!$R;" +
                                           "$P想買的時候，就選擇「買東西」吧!$R;" +
                                           "$R會顯示交易視窗，$R;" +
                                           "把想買的道具移到左邊，$R;" +
                                           "點擊「購買。」$R;" +
                                           "$R這樣就可以買到想要的商品唷!$R;" +
                                           "$P把取得的道具賣掉，$R;" +
                                           "用那些錢買裝備即可。$R;" +
                                           "$R那麼好好加油吧!$R;", "東方海角食品販賣");
                    break;
                    
                case 2:
                    break;
            }
        }

        void 尚未與埃米爾對話(ActorPC pc)
        {
            Say(pc, 11000930, 131, "還沒有聽過埃米爾的講解吧?$R;" +
                                   "$R先聽完後再來吧!$R;", "東方海角食品販賣");
        }

        void NPC物品買賣教學(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.販賣巧克力完成))
            {
                NPC物品販賣教學(pc);
                return;
            }

            if (!Beginner_01_mask.Test(Beginner_01.購買蘋果完成))
            {
                NPC物品購買教學(pc);
                return;
            }

            Beginner_01_mask.SetValue(Beginner_01.NPC物品買賣教學完成, true);

            Say(pc, 11000930, 131, "買東西也不難吧?$R;" +
                                   "$P交易這樣就算結束了，可以做到吧?$R;" +
                                   "$P把取得的道具賣掉，就有錢囉，$R;" +
                                   "這樣就可以購買其他裝備了。$R;" +
                                   "$R那麼加油吧!$R;", "東方海角食品販賣");
        }   

        void NPC物品販賣教學(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.NPC物品買賣教學開始) &&
                !Beginner_01_mask.Test(Beginner_01.販賣巧克力完成))
            {
                Say(pc, 11000930, 131, "您好!$R;" +
                                       "$R這裡是食品店，$R;" +
                                       "想知道買賣物品的方法嗎?$R;", "東方海角食品販賣");

                switch (Select(pc, "聽一聽買賣物品的方法，如何?", "", "我要聽! 我要聽!", "不用了"))
                {
                    case 1:
                        Beginner_01_mask.SetValue(Beginner_01.NPC物品買賣教學開始, true);

                        GiveItem(pc, 10009200, 1);

                        Say(pc, 0, 0, "得到『巧克力』!$R;", " ");

                        Say(pc, 11000930, 131, "先教您賣道具的方法吧!$R;" +
                                               "$R試試賣一下我給您的『巧克力』吧!$R;" +
                                               "$P把想賣的道具移到右邊視窗，$R;" +
                                               "再點一下「賣」鍵。$R;" +
                                               "$R很簡單吧?$R;", "東方海角食品販賣");
                        break;

                    case 2:
                        return;
                }
            }

            OpenShopSell(pc, 188);

            if (CountItem(pc, 10009200) == 0)
            {
                Beginner_01_mask.SetValue(Beginner_01.販賣巧克力完成, true);
            }
        }

        void NPC物品購買教學(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.購買蘋果完成) &&
                !Beginner_01_mask.Test(Beginner_01.NPC物品買賣教學完成))
            {
                Say(pc, 11000930, 131, "賣東西不難吧?$R;" +
                                       "$R身上數量多的時候，會有視窗顯示，$R;" +
                                       "問您想賣幾個唷!$R;" +
                                       "$P嗯…那麼現在換買道具看看吧?$R;" +
                                       "$R用剛才得到的錢，在我這裡買$R;" +
                                       "『蘋果』試試看吧。$R;" +
                                       "$P把想買的道具移到左邊視窗，$R;" +
                                       "點擊「購買」鍵即可。$R;" +
                                       "$R很簡單吧?$R;", "東方海角食品販賣");
            }

            OpenShopBuy(pc, 188);

            if (CountItem(pc, 10002800) > 0)
            {
                Beginner_01_mask.SetValue(Beginner_01.購買蘋果完成, true);
            }
        }
    }
}
