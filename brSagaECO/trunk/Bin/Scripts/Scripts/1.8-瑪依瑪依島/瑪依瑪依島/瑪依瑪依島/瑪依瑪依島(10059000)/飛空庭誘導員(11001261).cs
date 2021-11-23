using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10059000
{
    public class S11001261 : Event
    {
        public S11001261()
        {
            this.EventID = 11001261;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001261, 131, "トンカ島行き、まもなく出発です。$R;");
            if (pc.Skills2.ContainsKey(706) || pc.SkillsReserve.ContainsKey(706))
            {
                Say(pc, 11001261, 131, pc.Name + "さんですよね？$R;" +
                    "マーチャントギルドから$R;" +
                    "便宜を図ってくれと頼まれましたの。$R;" +
                    "$Rどうぞ、スペシャルシートを$R;" +
                    "ご用意させていただいております。$R;");
                switch (Select(pc, "どうする？", "", "乗らない", "乗る"))
                {
                    case 1:
                        break;
                    case 2:
                        PlaySound(pc, 2426, false, 100, 50);
                        Say(pc, 0, 131, "トンカ島へ出発しまーす！$R;");
                        PlaySound(pc, 2438, false, 100, 50);
                        ShowEffect(pc, 95, 147, 8066);
                        Wait(pc, 2000);
                        Warp(pc, 10062000, 198, 48);
                        break;
                } 
            }
            switch (Select(pc, "どうする？", "", "乗らない", "乗る（200ゴールド）"))
            {
                case 1:
                    break;
                case 2:
                    if (pc.Gold > 199)
                    {
                        pc.Gold -= 200;
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 0, 131, "200ゴールド払った。$R;");
                        Say(pc, 11001261, 131, "では、飛空庭へどうぞ。$R;");
                        PlaySound(pc, 2426, false, 100, 50);
                        Say(pc, 0, 131, "トンカ島へ出発しまーす！$R;");
                        PlaySound(pc, 2438, false, 100, 50);
                        ShowEffect(pc, 95, 147, 8066);
                        Wait(pc, 2000);
                        Warp(pc, 10062000, 198, 48);
                        return;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 0, 131, "お金が足りません。$R;");
                    break;
            }
        }
    }
}