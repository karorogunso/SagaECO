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
                        Say(pc, 131, "啊！这是给您的特别奖励！$R;" +
                            "$R因为是您用心在帮我做啊$R;");
                        Wait(pc, 2000);
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 2000);
                        SkillPointBonus(pc, 1);
                        Say(pc, 131, "技能点数提升1！$R;");
                        return;
                    }
                    Say(pc, 131, "顺利吗？$R;" +
                        "报酬到酒馆领取！$R;");
                    return;
                }
            }
            if (!mask.Test(PSTFlags.農村之泉水第一次對話))//_0c44)
            {
                mask.SetValue(PSTFlags.農村之泉水第一次對話, true);
                //_0c44 = true;
                Say(pc, 131, "想做『除草』兼职吗？$R;" +
                    "$R人手不够啊，如果您可以把朋友$R;" +
                    "也带过来一起做就太好了$R;");
            }
            else
            {
                Say(pc, 131, "想做『除草』兼职吗？$R;");
            }

            switch (Select(pc, "怎么做？", "", "做", "什么都不做"))
            {
                case 1:
                    if (pc.Quest == null)
                    {
                        Say(pc, 131, "没有任务点数，也可以做这个兼职$R;");

                        Say(pc, 131, "听好了！在规定时间内$R;" +
                            "割回来50个『杂草』就成功！$R;" +
                            "$R时间比想像中要短，快认真做!$R;");
                        switch (Select(pc, "做吗?", "", "当然！", "好烦！"))
                        {
                            case 1:
                                Say(pc, 131, "那么拜托了！$R;" +
                                    "报酬到酒馆领取！$R;");
                                HandleQuest(pc, 25);
                                break;
                        }

                        //Say(pc, 131, "没有可以做的任务$R;");
                        return;
                    }
                    Say(pc, 131, "那么其他事结束之后$R;" +
                        "到我这里来吧$R;");
                    break;
            }
        }
    }
}
