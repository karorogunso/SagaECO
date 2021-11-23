using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10001000
{
    public class S11000057 : Event
    {
        public S11000057()
        {
            this.EventID = 11000057;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Knights> Knights_mask = pc.CMask["Knights"];

            if (pc.Skills2.ContainsKey(706) || pc.SkillsReserve.ContainsKey(706))
            {
                Say(pc, 131, "哦哦!$R;" +
                    "這不是" + pc.Name + "嗎!$R;" +
                    "$R快請進$R;");
                return;
            }
            if (Knights_mask.Test(Knights.加入北軍騎士團))
            {
                if (Knights_mask.Test(Knights.北國過境檢查完成))
                {
                    Say(pc, 131, "辛苦了!您可以通過了$R;");
                    return;
                }
                Say(pc, 131, "您是隸屬奧克魯尼亞混城騎士團$R;" +
                    "的『北軍』吧？$R;" +
                    "$R請出示您的『諾頓騎士團證』$R;");
                switch (Select(pc, "是否出示騎士團證?", "", "出示", "不出示"))
                {
                    case 1:
                        if (CountItem(pc, 10041600) >= 1)
                        {
                            Knights_mask.SetValue(Knights.北國過境檢查完成, true);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "好的，沒問題了！！$R;" +
                                "$R以後不需要出示證件就可以通過了$R;");
                            Warp(pc, 10049000, 147, 252);
                            return;
                        }
                        Say(pc, 131, "您好像沒有騎士團証啊$R;");
                        break;
                    case 2:
                        break;
                }
            }
            if (CountItem(pc, 10042100) >= 1)
            {
                Say(pc, 131, "請出示您的『諾頓騎士團證』$R;");
                switch (Select(pc, "出示入國許可證?", "", "出示", "不出示"))
                {
                    case 1:
                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10042100, 1);
                        Say(pc, 131, "好的，沒問題了！！$R;" +
                            "$R我們將收回您的入國許可證$R;" +
                            "您可以通過了$R;");
                        Warp(pc, 10049000, 147, 252);
                        break;
                    case 2:
                        PlaySound(pc, 2041, false, 100, 50);
                        Say(pc, 131, "不出示許可證就無法進入國境$R;" +
                            "$R所以請您出示一下許可證$R;");
                        break;
                }
                return;
            }
            if (CountItem(pc, 10042101) >= 1)
            {
                Say(pc, 131, "請出示您的『諾頓騎士團證』$R;");
                switch (Select(pc, "出示僞造入國許可證?", "", "出示", "不出示"))
                {
                    case 1:
                        Say(pc, 131, "這個證件是假的??$R;" +
                            "印刷的顏色好像比較深啊！$R;" +
                            "$R該不會是偽造的吧?$R;");
                        switch (Select(pc, "是真的嗎?", "", "不是", "是的"))
                        {
                            case 1:
                                TakeItem(pc, 10042101, 1);
                                Say(pc, 131, "什麼！？證件是假的！！$R;" +
                                    "你想做非法的事情嗎！？$R;" +
                                    "$P哼！這次我就放你一馬！！$R;" +
                                    "如果再有下次，我絕對不會放過你！$R;" +
                                    "$R這張假許可證我就沒收了$R;" +
                                    "有甚麼不滿嗎？去投訴我吧！$R;");
                                PlaySound(pc, 2041, false, 100, 50);
                                Say(pc, 0, 131, "失去了僞造諾頓入國許可證$R;");
                                break;
                            case 2:
                                TakeItem(pc, 10042101, 1);
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 131, "啊…我好像失禮了$R;" +
                                    "我太過敏感了，真是抱歉啊$R;" +
                                    "$R我們將收回您的入國許可證$R;" +
                                    "您可以通過了$R;");
                                Warp(pc, 10049000, 147, 252);
                                break;
                        }
                        break;
                    case 2:
                        PlaySound(pc, 2041, false, 100, 50);
                        Say(pc, 131, "不出示許可證就無法進入國境$R;" +
                            "$R所以請您出示一下許可證$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "如果不是奧克魯尼亞$R;" +
                "混城騎士團北軍的團員$R;" +
                "或持有『諾頓入國許可證』的冒險者$R;" +
                "不能進入諾頓王國唷$R;" +
                "$P請您回去吧$R;");
        }
    }
}
