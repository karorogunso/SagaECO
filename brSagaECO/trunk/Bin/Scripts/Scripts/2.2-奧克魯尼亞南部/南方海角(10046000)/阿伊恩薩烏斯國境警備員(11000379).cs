using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10046000
{
    public class S11000379 : Event
    {
        public S11000379()
        {
            this.EventID = 11000379;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Knights> Knights_mask = pc.CMask["Knights"];

            if (pc.Skills2.ContainsKey(706) || pc.SkillsReserve.ContainsKey(706))
            {
                Say(pc, 131, "哦哦!$R;" +
                    pc.Name + "不是嗎!$R;" +
                    "$R快請進吧$R;");
                return;
            }
            if (Knights_mask.Test(Knights.加入南軍騎士團))
            {
                if (Knights_mask.Test(Knights.南國過境檢查完成))
                {
                    Say(pc, 131, "辛苦了!$R;" +
                        "可以通過了$R;");
                    return;
                }
                Say(pc, 131, "是屬於奧克魯尼亞混城騎士團$R;" +
                    "『南軍』的人啊$R;" +
                    "$R請出示『阿伊恩薩烏斯騎士團證』$R;");
                switch (Select(pc, "出示騎士團證嗎?", "", "出示", "不出示"))
                {
                    case 1:
                        if (CountItem(pc, 10041500) >= 1)
                        {
                            Knights_mask.SetValue(Knights.南國過境檢查完成, true);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "好的，沒問題$R;" +
                                "$R以後不出示也可以通過了$R;");
                            Warp(pc, 10061000, 145, 3);
                            return;
                        }
                        Say(pc, 131, "好像沒有騎士團證阿$R;");
                        break;
                    case 2:
                        break;
                }
            }
            //EVT1100037903
            if (CountItem(pc, 10042000) >= 1)
            {
                Say(pc, 131, "請把『阿伊恩薩烏斯入國許可證』$R;" +
                    "$R出示一下吧$R;");
                switch (Select(pc, "出示入國許可證嗎?", "", "出示", "不出示"))
                {
                    case 1:
                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10042000, 1);
                        Say(pc, 131, "好的，沒問題$R;" +
                            "$R那麽許可證就在這裡回收了$R;" +
                            "可以通過了$R;");
                        Warp(pc, 10061000, 145, 3);
                        break;
                    case 2:
                        PlaySound(pc, 2041, false, 100, 50);
                        Say(pc, 131, "如果不出示入國許可證的話$R;" +
                            "就無法通過此地了$R;" +
                            "$R請出示入國許可證$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "歡迎來到阿伊恩薩烏斯聯邦國!$R;" +
                "$R阿伊恩薩烏斯聯邦國$R;" +
                "只有屬於奧克魯尼亞混成騎士團南軍$R;" +
                "或持『阿伊恩薩烏斯許可證』的人$R;" +
                "$R才可以入境$R;");
        }
    }
}
