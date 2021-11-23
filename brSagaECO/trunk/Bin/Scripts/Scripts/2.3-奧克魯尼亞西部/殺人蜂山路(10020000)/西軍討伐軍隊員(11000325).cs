using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10020000
{
    public class S11000325 : Event
    {
        public S11000325()
        {
            this.EventID = 11000325;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<SRFSLFlags> mask = new BitMask<SRFSLFlags>(pc.CMask["SRFSL"]);
            int selection;
            if (pc.Level > 15)
            {
                Say(pc, 131, "往裡走是『蜂之故鄉』$R;" +
                    "$R像你那樣經驗多的冒險家來説$R;" +
                    "會有點無聊的$R;" +
                    "這裡就交給年輕的冒險家吧!$R;");
                return;
            }
            if (!mask.Test(SRFSLFlags.西軍討伐軍隊員第一次對話))
            {
                mask.SetValue(SRFSLFlags.西軍討伐軍隊員第一次對話, true);
                Say(pc, 131, "往裡走是『蜂之故鄉』$R;" +
                    "從地面的裂縫中出來了蜜蜂$R;" +
                    "$P派調查團查明是$R;" +
                    "巨大蜂蜜『螫針蜂』的$R;" +
                    "$R決定擊退了…給我下命令…$R;" +
                    "$P雖然進去就能知道$R;" +
                    "但是裡面有我討厭的魔物…$R;" +
                    "雖然不是不能做$R;" +
                    "但是我有蟲子恐怖症…$R;" +
                    "$R你幫我擊退可以嗎?$R;");
            }
            selection = Select(pc, "做什麽呢?", "", "討伐蜂之故鄉!", "聽螫針蜂攻略法", "什麽都不作");
            while (selection == 2)
            {
                switch (selection)
                {
                    case 1:
                        if (pc.Quest == null)
                        {
                            if (pc.QuestRemaining < 3)
                            {
                                Say(pc, 131, "這個任務是消耗任務點數『3』$R;" +
                                    "$R怎麽看都覺得任務點數不足啊$R;" +
                                    "不好意思下次再挑戰吧!$R;");
                                return;
                            }
                            Say(pc, 131, "這個任務是消耗任務點數『3』$R;");
                            switch (Select(pc, "接受任務?", "", "當然接受了", "還是放棄吧!"))
                            {
                                case 1:
                                    HandleQuest(pc, 20);
                                    break;
                                case 2:
                                    break;
                            }
                            return;
                        }
                        Say(pc, 131, "你在執行其他任務中阿$R;" +
                            "$R等結束現在的任務後$R;" +
                            "可以再過來嗎?$R;");
                        break;
                    case 2:
                        Say(pc, 131, "螫針蜂的周邊有叫『螞蜂』的$R;" +
                            "像血一樣紅的蜜蜂$R;" +
                            "$P想要同時擊退螫針蜂和螞蜂的話$R;" +
                            "會馬上全滅的$R;" +
                            "$R首先想辦法把魔物們拉開$R;" +
                            "$P我們通常使用的作戰是$R;" +
                            "『馬拉松大作戰!』$R;" +
                            "$R選一個有體力的家伙後$R;" +
                            "在那些魔物們周圍全力奔馳引起注意$R;" +
                            "$P趁那個機會藏起來的其他隊員$R;" +
                            "一個一個殺掉螞蜂$R;" +
                            "$R因爲螞蜂的迴避率較高$R;" +
                            "所以比較難攻擊$R;" +
                            "可以魔法攻擊的話會比較容易$R;" +
                            "$P殺掉螞蜂後全體人員攻擊螫針蜂$R;" +
                            "會很容易擊退的$R;");
                        break;
                    case 3:
                        break;
                }
                selection = Select(pc, "做什麽呢?", "", "討伐蜂之故鄉!", "聽螫針蜂攻略法", "什麽都不作");
            }
        }
    }
}
