using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018102) NPC基本信息:東方海角食品販賣(11001410) X:203 Y:98
namespace SagaScript.M10018102
{
    public class S11001410 : Event
    {
        public S11001410()
        {
            this.EventID = 11001410;
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

            switch (Select(pc, "想再听一遍吗?", "", "再听一遍", "不用了"))
            {
                case 1:
                    Say(pc, 11001410, 131, "这一次将简单的说明。$R;" +
                                           "$P想卖道具时，选择「卖东西」。$R;" +
                                           "$R就会显示交易视窗，$R;" +
                                           "把想卖的道具移到右边，$R;" +
                                           "点击「售卖」。$R;" +
                                           "$R这样就完成了!$R;" +
                                           "$P想买的时候，就选择「买东西」吧!$R;" +
                                           "$R会显示交易视窗，$R;" +
                                           "把想买的道具移到左边，$R;" +
                                           "点击「购买。」$R;" +
                                           "$R这样就可以买到想要的商品!$R;" +
                                           "$P把取得的道具卖掉，$R;" +
                                           "用那些钱买装备即可。$R;" +
                                           "$R那么好好加油吧!$R;", "东方海角食品贩卖");
                    break;
                    
                case 2:
                    break;
            }
        }

        void 尚未與埃米爾對話(ActorPC pc)
        {
            Say(pc, 11001410, 131, "还没有听过埃米尔的讲解吧?$R;" +
                                   "$R先听完后再来吧!$R;", "东方海角食品贩卖");
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

            Say(pc, 11001410, 131, "买东西也不难吧?$R;" +
                                   "$P交易这样就算结束了，可以做到吧?$R;" +
                                   "$P把取得的道具卖掉，就有钱啰，$R;" +
                                   "这样就可以购买其他装备了。$R;" +
                                   "$R那么加油吧!$R;", "东方海角食品贩卖");
        }   

        void NPC物品販賣教學(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.NPC物品買賣教學開始) &&
                !Beginner_01_mask.Test(Beginner_01.販賣巧克力完成))
            {
                Say(pc, 11001410, 131, "您好!$R;" +
                                       "$R这里是食品店，$R;" +
                                       "想知道买卖物品的方法吗?$R;", "东方海角食品贩卖");

                switch (Select(pc, "听一听买卖物品的方法，如何?", "", "我要听! 我要听!", "不用了"))
                {
                    case 1:
                        Beginner_01_mask.SetValue(Beginner_01.NPC物品買賣教學開始, true);

                        GiveItem(pc, 10009200, 1);

                        Say(pc, 0, 0, "得到『巧克力』!$R;", " ");

                        Say(pc, 11001410, 131, "先教您卖道具的方法吧!$R;" +
                                               "$R试试卖一下我给您的『巧克力』吧!$R;" +
                                               "$P把想卖的道具移到右边视窗，$R;" +
                                               "再点一下「卖」键。$R;" +
                                               "$R很简单吧?$R;", "东方海角食品贩卖");
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
                Say(pc, 11001410, 131, "卖东西不难吧?$R;" +
                                       "$R身上数量多的时候，会有视窗显示，$R;" +
                                       "问您想卖几个哦!$R;" +
                                       "$P嗯…那么现在换买道具看看吧?$R;" +
                                       "$R用刚才得到的钱，在我这里买$R;" +
                                       "『苹果』试试看吧。$R;" +
                                       "$P把想买的道具移到左边视窗，$R;" +
                                       "点击「购买」键即可。$R;" +
                                       "$R很简单吧?$R;", "东方海角食品贩卖");
            }

            OpenShopBuy(pc, 188);

            if (CountItem(pc, 10002800) > 0)
            {
                Beginner_01_mask.SetValue(Beginner_01.購買蘋果完成, true);
            }
        }
    }
}
