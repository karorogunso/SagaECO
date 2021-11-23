using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public abstract class 道具精製師 : Event
    {
        public 道具精製師()
        {

        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            string free;
            int money;

            if (!World_01_mask.Test(World_01.護髮劑合成任務完成))
            {
                護髮劑合成任務(pc);
                return;
            }

            if (!World_01_mask.Test(World_01.已經與道具精製師進行第一次對話))
            {
                初次與道具精製師進行對話(pc);
                return;
            }

            if (pc.Level < 16)
            {
                free = "免費";
                money = 0;
            }
            else
            {
                free = "100金幣";
                money = 100;
            }

            switch (Select(pc, "做什麼好呢?", "", "打開寶物箱(" + free + ")", "精製道具", "藥品合成", "木材加工", "購買維修工具箱", "放棄"))
            {
                case 1:
                    if (pc.Gold >= money)
                    {
                        pc.Gold -= money;

                        OpenTreasureBox(pc);
                    }
                    else
                    {
                        Say(pc, 131, "資金不足喔!$R;");
                    }
                    break;

                case 2:
                    Synthese(pc, 2009, 4);
                    break;

                case 3:
                    Synthese(pc, 2022, 5);
                    break;

                case 4:
                    Synthese(pc, 2020, 3);
                    break;

                case 5:
                    OpenShopBuy(pc, 6);
                    break;

                case 6:
                    break;
            }
        }

        void 初次與道具精製師進行對話(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            int selection;

            World_01_mask.SetValue(World_01.已經與道具精製師進行第一次對話, true);

            Say(pc, 131, "我是「阿高普路斯市」的$R;" +
                         "「行會評議會」派遣的$R;" +
                         "「道具精製師」。$R;" +
                         "$P負責處理道具的「精製」和「合成」，$R;" +
                         "除此之外，$R;" +
                         "還有負責「寶物箱」的開啟。$R;" +
                         "$R萬一有打不開的「寶物箱」，$R;" +
                         "就拿到我這裡來吧!$R;", "道具精製師");

            selection = Select(pc, "有什麼要問的嗎?", "", "「精製」是什麼?", "「合成」是什麼?", "委託「精製」、「合成」的方法", "「寶物箱」是什麼?", "沒什麼想問的");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 131, "所謂的「精製」，$R;" +
                                     "是將道具中的將雜質去掉，$R;" +
                                     "變成更佳有用到材料。$R;" +
                                     "$P如果你是「生產系職業」，$R;" +
                                     "總有一天，$R;" +
                                     "自己就能做出來的。$R;" +
                                     "$P此外，$R;" +
                                     "同時也有使「重量」和「體積」$R;" +
                                     "變小的效果，所以想要搬運道具的時候，$R;" +
                                     "學會「精製」的話會比較方便喔!$R;", "道具精製師");
                        break;

                    case 2:
                        Say(pc, 131, "所謂的「合成」，$R;" +
                                     "是使用2個以上的道具來$R;" +
                                     "製作出新的道具的意思。$R;" +
                                     "$R我所負責的合成是$R;" +
                                     "「藥品合成」和「木材加工」。$R;" +
                                     "$P「藥品合成」是把有藥效的$R;" +
                                     "粉末和液體混合在一起，$R;" +
                                     "製作出更好效果的藥品的方法。$R;" +
                                     "$R「木材加工」是把木頭的原料，$R;" +
                                     "加工成其他形態的部件的方法。$R;" +
                                     "$P如果你是「生產系職業」的話，$R;" +
                                     "使用不同技能，$R;" +
                                     "就能進行不同的合成，$R;" +
                                     "靠自己也可以完成的。$R;", "道具精製師");
                        break;

                    case 3:
                        Say(pc, 131, "委託的方法很簡單!$R;" +
                                     "$R拿著製做材料的狀態下，$R;" +
                                     "跟我說話就可以了。$R;" +
                                     "$P用你拿著的材料，$R;" +
                                     "我會告訴你可以$R;" +
                                     "「精製」或「合成」的秘方。$R;" +
                                     "$R然後自己決定做什麼跟做幾個後，$R;" +
                                     "再按「ok」就可以了。$R;" +
                                     "$P道具不足的時候，$R;" +
                                     "會告訴你什麼不足。$R;" +
                                     "$R所以把那個當作參考就可以!$R;" +
                                     "$P「精製」和「合成」$R;" +
                                     "都是有可能會失敗的。$R;" +
                                     "$R為了防止失敗，$R;" +
                                     "多準備點預備材料的話，$R;" +
                                     "會比較安心的。$R;", "道具精製師");
                        break;

                    case 4:
                        Say(pc, 131, "所謂的「寶物箱」，$R;" +
                                     "是掉在田野或地牢裡，$R;" +
                                     "裝有道具的箱子。$R;" +
                                     "$P裡面裝有各種道具，$R;" +
                                     "但是不打開的話，$R;" +
                                     "誰都不知道裡面有什麼道具。$R;" +
                                     "$R啊! 不打開也可以賣唷!$R;" +
                                     "$P「寶物箱」有三個種類，$R;" +
                                     "『木箱』、『寶物箱』、『集裝箱』。$R;" +
                                     "$R特別是『集裝箱』，$R;" +
                                     "是屬於機械時代的產物。$R;" +
                                     "$R裡面裝有叫『發掘品』的貴重道具唷!$R;" +
                                     "$P打開1個「寶物箱」需要「100金幣」，$R;" +
                                     "但是針對初心者有特別的獎勵!$R;" +
                                     "$R到「LV16」為止前全部免費，$R;" +
                                     "所以儘管拿來吧!$R;", "道具精製師");
                        break;
                }

                selection = Select(pc, "有什麼要問的嗎?", "", "「精製」是什麼?", "「合成」是什麼?", "委託「精製」、「合成」的方法", "「寶物箱」是什麼?", "沒什麼想問的");
            }
        }

        void 護髮劑合成任務(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            if (!World_01_mask.Test(World_01.護髮劑合成任務開始))
            {
                Say(pc, 131, "你好，我是道具精製師。$R;" +
                             "$R現在有為初心者舉辦$R;" +
                             "發送道具的贈品活動。$R;" +
                             "$P想不想進行一次合成看看呢?$R;", "道具精製師");

                switch (Select(pc, "怎麼做呢?", "", "做做看", "不做"))
                {
                    case 1:
                        if (CheckInventory(pc, 10000604, 1) &&
                            CheckInventory(pc, 10001905, 1) &&
                            CheckInventory(pc, 10035400, 1))
                        {
                            World_01_mask.SetValue(World_01.護髮劑合成任務開始, true);

                            PlaySound(pc, 2040, false, 100, 50);
                            GiveItem(pc, 10000604, 1);
                            GiveItem(pc, 10001905, 1);
                            GiveItem(pc, 10035400, 1);
                            Say(pc, 0, 65535, "得到『蜂蜜』、$R;" +
                                              "『樹皮精華素』、$R;" +
                                              "『復活藥水』!$R;", " ");

                            Say(pc, 131, "這次要做的是$R;" +
                                         "使頭髮長快速生長的神秘藥水$R;" +
                                         "『護髮劑』。$R;" +
                                         "$P殺人蜂掉的保濕效果良好的$R;" +
                                         "『蜂蜜』!$R;" +
                                         "$R長在小樹上，$R;" +
                                         "讓頭髮變得柔順有光澤的$R;" +
                                         "『樹皮精華素』!$R;" +
                                         "$P還有讓死去的毛孔活起來的$R;" +
                                         "『復活藥水』!$R;" +
                                         "$P把這些道具用「藥品合成」技能，$R;" +
                                         "合成看看吧?$R;", "道具精製師");

                            switch (Select(pc, "怎麼做呢？", "", "做做看", "只收道具"))
                            {
                                case 1:
                                    Synthese(pc, 2022, 5);
                                    break;

                                case 2:
                                    World_01_mask.SetValue(World_01.護髮劑合成任務完成, true);

                                    pc.Fame = pc.Fame - 1;

                                    Say(pc, 131, "太過分了…!$R;", "道具精製師");
                                    break;
                            }
                        }
                        else
                        {
                            Say(pc, 131, "嗯?$R;" +
                                         "$R無法給您道具啊!$R;" +
                                         "把身上的東西減少後再來吧!$R;", "道具精製師");
                        }
                        break;
                        
                    case 2:
                        World_01_mask.SetValue(World_01.護髮劑合成任務完成, true);
                        break;
                }
            }
            else
            {
                World_01_mask.SetValue(World_01.護髮劑合成任務完成, true);

                Say(pc, 131, "都好了，要看一下嗎?$R;" +
                             "$R以這個方式合成的話，$R;" +
                             "可以製作出有意思的東西。$R;" +
                             "$R你也體驗一下各種合成吧!$R;", "道具精製師");
            }
        }
    }
}