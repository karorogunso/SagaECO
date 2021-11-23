using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

using SagaDB.Quests;

namespace SagaScript.M10057000
{
    public class S11000701 : Event
    {
        public S11000701()
        {
            this.EventID = 11000701;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<PSTFlags> mask = pc.CMask["PST"];

            if (pc.Quest != null)
            {
                if (pc.Quest.ID == 10031200 && pc.Quest.Status == QuestStatus.COMPLETED)
                {
                    if (!mask.Test(PSTFlags.獲得技能點))//_5a19)
                    {
                        mask.SetValue(PSTFlags.獲得技能點, true);
                        //_5a19 = true;
                        Say(pc, 131, "啊！這是給您的特別奬勵！$R;" +
                            "$R因為是您用心幫我做啊$R;");
                        Wait(pc, 2000);
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 2000);
                        SkillPointBonus(pc, 1);
                        Say(pc, 131, "技能點數提升1！$R;");
                        return;
                    }
                    Say(pc, 131, "順利嗎？$R;" +
                        "報酬到咖啡館領取！$R;");
                    return;
                }
            }
            if (!mask.Test(PSTFlags.農村之泉水第一次對話))//_0c44)
            {
                mask.SetValue(PSTFlags.農村之泉水第一次對話, true);
                //_0c44 = true;
                Say(pc, 131, "想做『除草』兼職嗎？$R;" +
                    "$R人手不夠阿，如果您可以把朋友$R;" +
                    "也帶過來一起做就太好了$R;");
            }
            else
            {
                Say(pc, 131, "想做『除草』兼職嗎？$R;");
            }

            switch (Select(pc, "怎麼做？", "", "做", "什麼都不做"))
            {
                case 1:
                    if (pc.Quest == null)
                    {
                        Say(pc, 131, "没有任務點數，也可以做這個兼職$R;");

                        Say(pc, 131, "聽好了！在規定時間内$R;" +
                            "割回來50個『雜草』就成功！$R;" +
                            "$R時間比想像中要短，快認真做!$R;");
                        switch (Select(pc, "做嗎?", "", "當然！", "好煩！"))
                        {
                            case 1:
                                Say(pc, 131, "那麽拜託了！$R;" +
                                    "報酬到咖啡館領取！$R;");
                                HandleQuest(pc, 25);
                                break;
                        }

                        //Say(pc, 131, "没有可以做的任務$R;");
                        return;
                    }
                    Say(pc, 131, "那麽其他事結束之後$R;" +
                        "到我這裡來吧$R;");
                    break;
            }
        }
    }
}
