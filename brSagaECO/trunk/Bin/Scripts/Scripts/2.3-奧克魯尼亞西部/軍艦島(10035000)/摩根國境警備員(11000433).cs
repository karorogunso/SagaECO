using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10035000
{
    public class S11000433 : Event
    {
        public S11000433()
        {
            this.EventID = 11000433;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Knights> Knights_mask = pc.CMask["Knights"];

            /*
            if (_7a88 && _7a87)
            {
                Say(pc, 131, "剩下的只是回到賢者首領那裡!$R;");
                return;
            }
            if (_7a87)
            {
                Say(pc, 131, "阿阿…這個$R;" +
                    "來到這麽遠的地方!$R辛苦了!$R;" +
                    "$R這個地區「熊」「山洞熊」「巴嗚」$R;" +
                    "這樣的魔物挺多的$R;" +
                    "很多時候會在一個地方同時出現$R;" +
                    "所以提起精神呢$R;" +
                    "被包圍的情況也挺多呢$R;" +
                    "$P很可惜的是到現在爲止$R;" +
                    "很多冒險者們都丟了性命$R;" +
                    "$R如果你也不想那樣的話$R;" +
                    "要注意才可以阿$R;" +
                    "$P我的話就到這裡了$R;");
                _7a88 = true;
                Say(pc, 131, "剩下的只是回到賢者首領那裡!$R;");
                return;
            }

            if (_6a53 || _6a58)
            {
                Say(pc, 131, "那希望乘坐飛空庭$R;");
                return;
            }//*/

            Say(pc, 131, "歡迎來到軍艦島的機場$R;" +
                "這裡是乘坐摩根飛空庭的地方$R;");
            switch (Select(pc, "歡迎來到軍艦島的機場", "", "辦理乘坐手續", "什麽都不作"))
            {
                case 1:
                    if (pc.Skills2.ContainsKey(706) || pc.SkillsReserve.ContainsKey(706))
                    {
                        Say(pc, 131, "哦哦!$R;" +
                            "是" + pc.Name + "嗎!！$R;" +
                            "$R乘坐手續就不需要了$R;" +
                            "那希望乘坐飛空庭$R;" +
                            "已經準備好了特等座$R;");
                        return;
                    }
                    if (Knights_mask.Test(Knights.加入西軍騎士團))
                    {
                        Say(pc, 131, "是屬於奧克魯尼亞混城騎士團$R;" +
                            "『西軍』的人啊$R;" +
                            "$R請出示『摩根騎士團證』$R;");
                        switch (Select(pc, "出示騎士團證嗎?", "", "出示", "不出示"))
                        {
                            case 1:
                                if (CountItem(pc, 10041400) >= 1)
                                {
                                    Knights_mask.SetValue(Knights.西國過境檢查完成, true);
                                    PlaySound(pc, 2040, false, 100, 50);
                                    Say(pc, 131, "好的，沒有問題$R;" +
                                        "$R以後不出示也可以通過$R;");
                                    return;
                                }
                                PlaySound(pc, 2041, false, 100, 50);
                                Say(pc, 131, "好像沒有騎士團證阿$R;");
                                break;
                            case 2:
                                break;
                        }
                    }
                    Say(pc, 131, "那請出示『摩根入國許可證』$R;");
                    switch (Select(pc, "出示入國許可證嗎?", "", "出示", "不出示"))
                    {
                        case 1:
                            if (CountItem(pc, 10041900) >= 1)
                            {
                                Knights_mask.SetValue(Knights.出示西國許可證, true);
                                PlaySound(pc, 2040, false, 100, 50);
                                TakeItem(pc, 10041900, 1);
                                Say(pc, 131, "好的，沒有問題$R;" +
                                    "$R那麽許可證在這裡回收了$R;" +
                                    "可以通過了$R;");
                                return;
                            }
                            break;
                        case 2:
                            break;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 131, "不出示入國許可證的話$R;" +
                        "無法乘坐飛空庭$R;" +
                        "$R請出示入國許可證$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}