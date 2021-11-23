using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M12002102
{
    public class S11001637 : Event
    {
        public S11001637()
        {
            this.EventID = 11001637;
        }
        public override void OnEvent(ActorPC pc)
        {
                Say(pc, 11000405, 131, "因為亂點了屬點?而要洗點嗎?$R;", "路人");
                switch (Select(pc, "洗點", "", "洗屬性點(10等級前可無限洗)", "洗技能點(10等級前可無限洗)", "什麼都不做"))
                {
                    case 1:
                        if (pc.Race == PC_RACE.DEM)
                        {
                        Say(pc, 131, "DEM不可以洗點啊$R;");
                        }
                        if (pc.Level <= 11)
                        {
                            ResetStatusPoint(pc);
                            //STATUSRESET
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 4000);
                            Say(pc, 131, "回到初始狀態囉$R;");
                            break;
                        }
                        if (pc.Gold <= 100000)
                        {
                            Say(pc, 131, "沒有足夠金錢喔$R;");
                            return;
                        }
                        ResetStatusPoint(pc);
                        //STATUSRESET
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 4000);
                        pc.Gold -= 100000;
                        Say(pc, 131, "回到初始狀態囉$R;");
                        break;
                    case 2:
                       if (pc.Race == PC_RACE.DEM)
                        {
                        Say(pc, 131, "DEM不可以洗點啊$R;");
                        }
                        if (pc.Skills.Count == 0 &&
                            pc.Skills2.Count == 0)
                        {
                            Say(pc, 131, "沒有持有技能喔$R;");
                            return;
                        }
                        if (pc.Level <= 11)
                        {
                            ResetSkill(pc, 1);
                            ResetSkill(pc, 2);
                            //SKILLRESET_ALL
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 4000);
                            Say(pc, 131, "技能進行重新設定了$R;");
                            break;
                        }
                        if (pc.Gold <= 100000)
                        {
                            Say(pc, 131, "沒有足夠金錢喔$R;");
                            return;
                        }
                        ResetSkill(pc, 1);
                        ResetSkill(pc, 2);
                        //SKILLRESET_ALL
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 4000);
                        pc.Gold -= 100000;
                        Say(pc, 131, "技能進行重新設定了$R;");
                        break;
                }
        }
    }
}
        
     
    