using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:奧克魯尼亞東部平原(10025000) NPC基本信息:弗朗西斯小姐(11000168) X:110 Y:135
namespace SagaScript.M10025000
{
    public class S11000168 : Event
    {
        public S11000168()
        {
            this.EventID = 11000168;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            if (!World_01_mask.Test(World_01.翅膀裝飾合成任務完成))
            {
                翅膀裝飾合成任務(pc);
                return;
            }

            Say(pc, 11000168, 131, "我很可愛吧?$R;" +
                                   "$R什麼都可以問我喔!$R;", "弗朗西斯小姐");

            switch (Select(pc, "想問什麼呢?", "", "關於「武器」", "關於「盾牌」", "關於「頭髮裝飾」", "關於「衣服」", "關於「鞋子」", "沒什麼想問的"))
            {
                case 1:
                    Say(pc, 11000168, 131, "啊? 這個是『尖鐵棍棒』。$R;" +
                                           "$R怎麼樣? 還好吧??$R;" +
                                           "看起來非常強悍啊!$R;" +
                                           "$P從商人大叔那裡購買的『棍棒』，$R;" +
                                           "$R再釘上『螺絲』和 『布』。$R;" +
                                           "$P可是因為無法自己完成$R;" +
                                           "這麼困難的工作。$R;" +
                                           "$R所以只好拜託在「下城」的$R;" +
                                           "「武器製作所」幫忙製作。$R;" +
                                           "$P『螺絲』在「下城」就有賣了，$R;" +
                                           "『布』是用『棉花』精製而成的。$R;" +
                                           "$R雖然收集材料是困難的事情，$R;" +
                                           "但是可以跟別人炫耀，$R;" +
                                           "所以也會很滿足吧。$R;", "弗朗西斯小姐");
                    break;

                case 2:
                    Say(pc, 11000168, 131, "啊? 這個是『木盾牌』。$R;" +
                                           "$R怎麼了呢?$R;" +
                                           "$P吊橋上的商人有在賣呢。$R;", "弗朗西斯小姐");
                    break;

                case 3:
                    Say(pc, 11000168, 131, "用「咕咕的翅膀」可以合成很多道具喔!$R;", "弗朗西斯小姐");
                    break;

                case 4:
                    Say(pc, 11000168, 131, "啊? 這個是『工作褲』。$R;" +
                                           "$R怎麼樣?$R;" +
                                           "這是帕斯特那邊的商人在賣的。$R;" +
                                           "$P聽說是在「帕斯特」很流行的工作服呢?$R;", "弗朗西斯小姐");
                    break;

                case 5:
                    Say(pc, 11000168, 131, "『涼鞋』你不也是一開始就穿了嗎?$R;" +
                                           "$R問其他能炫耀的東西吧!$R;", "弗朗西斯小姐");
                    break;

                case 6:
                    break;
            }
        }

        void 翅膀裝飾合成任務(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            if (World_01_mask.Test(World_01.翅膀裝飾合成任務開始))
            {
                製作翅膀裝飾(pc);
                return;
            }

            Say(pc, 11000168, 131, "這個『翅膀裝飾』不錯吧?$R;" +
                                   "這是用合成做成的~!$R;" +
                                   "$R你想不想挑戰看看呀?$R;", "弗朗西斯小姐");

            switch (Select(pc, "想要挑戰看看嗎?", "", "好啊! 挑戰!", "討厭……"))
            {
                case 1:
                    World_01_mask.SetValue(World_01.翅膀裝飾合成任務開始, true);

                    Say(pc, 11000168, 131, "要製作『翅膀裝飾』的話，$R;" +
                                           "首先要弄到『咕咕的翅膀』才可以!$R;" +
                                           "$P沿著這條路直走，就能找到「咕咕」了。$R;" +
                                           "$R等拿到羽毛之後，$R;" +
                                           "再回來找我說話好嗎?$R;", "弗朗西斯小姐");
                    break;

                case 2:
                    break;
            }
        }

        void 製作翅膀裝飾(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            if (!World_01_mask.Test(World_01.翅膀裝飾合成任務完成))
            {
                if (CountItem(pc, 10018208) > 0)
                {
                    Say(pc, 11000168, 131, "得到『咕咕的翅膀』後，$R;" +
                                           "要再去「寶石商」那裡。$R;" +
                                           "$R「寶石商店」在$R;" +
                                           "「阿高普路斯市」的「上城」那裡。$R;" +
                                           "$P委託他們幫你「飾品製作」的話，$R;" +
                                           "他們就會馬上幫你處理的!$R;", "弗朗西斯小姐");
                }
                else
                {
                    Say(pc, 11000168, 131, "要製作『翅膀裝飾』的話，$R;" +
                                           "首先要弄到『咕咕的翅膀』才可以!$R;" +
                                           "$P沿著這條路直走，就能找到「咕咕」了。$R;" +
                                           "$R等拿到羽毛之後，$R;" +
                                           "再回來找我說話好嗎?$R;", "弗朗西斯小姐");
                }
            }

            if (World_01_mask.Test(World_01.翅膀裝飾合成任務完成) &&
                CountItem(pc, 50031400) > 0)
            {
                World_01_mask.SetValue(World_01.翅膀裝飾合成任務完成, true);

                Say(pc, 11000168, 131, "嘿嘿~♪完成了~!R;" +
                                       "跟我的一模一樣啊!$R;" +
                                       "$R其他的道具也想製作嗎?$R;" +
                                       "那我以後就多教你幾種吧!$R;", "弗朗西斯小姐");
            }
        }
    }
}
