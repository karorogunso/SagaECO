using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public abstract class 迎賓兔子 : Event
    {
        public override void OnEvent(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> mask = pc.CMask["Tinyis_Land_01"];

            if (pc.Account.GMLevel >= 100)
            {
                if (Select(pc, "去ECO城嗎？", "", "去", "不去") == 1)
                {
                    PlaySound(pc, 2040, false, 100, 50); 
                    NPCMotion(pc, 11001894, 364, false, true);
                    NPCMotion(pc, 11001895, 364, false, true);
                    NPCMotion(pc, 11001896, 364, false, true);
                    NPCMotion(pc, 11001897, 364, false, true);
                    NPCMotion(pc, 11001898, 364, false, true);
                    NPCMotion(pc, 11001899, 364, false, true);
                    NPCMotion(pc, 11001900, 364, false, true);
                    NPCMotion(pc, 11001901, 364, false, true);
                    NPCMotion(pc, 11001902, 364, false, true);
                    Wait(pc, 990);
                    ShowEffect(pc, 4011);
                    Wait(pc, 990);
                    Warp(pc, 11027000, 252, 7);
                }
                return;
            }


            if (CountItem(pc, 10022953) >= 1 && mask.Test(Tinyis_Land_01.去過ECO城))
            {
                if (Select(pc, "怎麼辦？", "", "什麼也不做", "「虹色の鍵」給他") == 2)
                {
                    TakeItem(pc, 10022953, 1);
                    Say(pc, 0, 0, "「虹色の鍵」給兔子了！$R;", " ");
                    PlaySound(pc, 2040, false, 100, 50);
                    NPCMotion(pc, 11001894, 364, false, true);
                    NPCMotion(pc, 11001895, 364, false, true);
                    NPCMotion(pc, 11001896, 364, false, true);
                    NPCMotion(pc, 11001897, 364, false, true);
                    NPCMotion(pc, 11001898, 364, false, true);
                    NPCMotion(pc, 11001899, 364, false, true);
                    NPCMotion(pc, 11001900, 364, false, true);
                    NPCMotion(pc, 11001901, 364, false, true);
                    NPCMotion(pc, 11001902, 364, false, true);
                    Wait(pc, 990);
                    ShowEffect(pc, 4011);
                    Wait(pc, 990);
                    Warp(pc, 11027000, 252, 7);
                    return;
                }
            }
            else
                Say(pc, 0, 131, "……、虹、鑰匙……。$R;" +
                    "夢之…門……、打開、鑰匙……。$R;", "迎賓兔子");
        }
    }
}
