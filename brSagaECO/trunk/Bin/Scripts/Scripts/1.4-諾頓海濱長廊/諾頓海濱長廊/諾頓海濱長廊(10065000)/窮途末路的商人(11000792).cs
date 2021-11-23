using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10065000
{
    public class S11000792 : Event
    {
        public S11000792()
        {
            this.EventID = 11000792;
        }

        public override void OnEvent(ActorPC pc)
        {

            int selection;
            BitMask<NDFlags> mask = new BitMask<NDFlags>(pc.CMask["ND"]);
            Say(pc, 131, "太可怕了，我不知道要等到什麼時候$R;");
            if (!mask.Test(NDFlags.窮途末路的商人结束))
            {
                switch (Select(pc, "搭話嗎？", "", "怎麼了？", "不搭話"))
                {
                    case 1:
                        Say(pc, 131, "您是冒險者吧，什麼時候到這來的$R見過大商人古魯杜了嗎？$R;" +
                            "$P古魯杜是我們商人行會裡，$R交易額居冠的大商人唷$R;" +
                            "$P…$R;" +
                            "$P是嗎…好像不清楚…$R;" +
                            "$R哎呀，也不知道到底等到什麼時候$R;");
                        selection = Select(pc, "搭話嗎？", "", "為什麼坐立不安呢", "這是什麼動物？", "不搭話");
                        while (selection != 3)
                        {
                            switch (selection)
                            {
                                case 1:
                                    mask.SetValue(NDFlags.窮途末路的商人对话一, true);
                                    Say(pc, 131, "那是因為，我有點害怕監視這動物呀$R;" +
                                        "$P古魯杜把動物委託我監視後，$R自己去旅行了，讓我等他回來$R;" +
                                        "$P這傢伙平時很溫順，$R但有時會露出可怕的牙齒$R或吐出火來$R;" +
                                        "$R我擔心有天會被它咬，或變成碳阿$R;");
                                    break;
                                case 2:
                                    mask.SetValue(NDFlags.窮途末路的商人对话二, true);
                                    Say(pc, 131, "這是『火紅德拉古』是嗎$R;" +
                                        "$P是古魯杜根據機械時代的文獻記載，$R用傳說中的生物名字命名的$R;");
                                    break;
                                case 3:
                                    return;
                            }
                            if (mask.Test(NDFlags.窮途末路的商人对话一) && mask.Test(NDFlags.窮途末路的商人对话二))
                            {
                                Say(pc, 131, "跟您說話現在好多了$R;" +
                                    "$R說來話長，想不想聽呢？$R;");
                                switch (Select(pc, "怎麼辦呀？", "", "聽", "不聽"))
                                {
                                    case 1:
                                        mask.SetValue(NDFlags.窮途末路的商人结束, true);
                                        Say(pc, 131, "謝謝$R;" +
                                            "$R那件事情是在我們行會商人在$R諾頓北部遇難時發生的。$R;");
                                        Say(pc, 131, "那些商人為了找新的商團，$R正要到諾頓北部$R;" +
                                            "$R在『永遠的北方邊界』$R遭到了魔物的襲擊阿$R;" +
                                            "$P古魯杜、我、復活戰士救助隊$R接到消息後，就向遇難地點前進了$R;" +
                                            "$R幸好成功把所有人救出來了，$R但行李全部被魔物搶走了$R;");
                                        Say(pc, 131, "剩下的行李裡頭，$R有沒見過的幾個很大的蛋唷$R;" +
                                            "$R商人說，那些蛋是從凍僵的大地上，$R而且比機械時代更久遠的$R地層發現的。$R;" +
                                            "$P古魯杜拿著蛋，拿出桃子，$R說是發掘品什麼的？$R用超聲波注射劑$R;" +
                                            "$R注入那顆蛋裡$R;");
                                        Say(pc, 131, "幾天後從那顆蛋裡出來的$R就是『火紅德拉古』$R;" +
                                            "$R瞬時間變的很大，$R到現在變成這麼大了。$R;" +
                                            "$P古魯杜說去拿給別的蛋裡$R注射的桃子，就不見了$R;" +
                                            "$R遺傳基因、派生物…？$R這些是機械時代發掘藥品的主成分。$R我也不知道是什麼意思…$R;" +
                                            "$P讓我們在這裡監視德拉古…$R然後等他回來…$R太過份了吧$R;");
                                        Say(pc, 131, "說了這麼長時間。對不起$R;" +
                                            "$R所以我只能在這裡等著古魯杜回來$R;");
                                        break;
                                    case 2:
                                        Say(pc, 131, "是嗎？$R;" +
                                            "$R沒關係，是我硬要求的，不要介意$R;");
                                        break;
                                }
                            }
                            selection = Select(pc, "搭話嗎？", "", "為什麼坐立不安呢", "這是什麼動物？", "不搭話");
                        }
                        break;
                    case 2:
                        break;
                }
                return;
            }
        }
    }
}
