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
        public 迎賓兔子()
        {

        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> mask = pc.CMask["Tinyis_Land_01"];

            if (pc.Account.GMLevel >= 100)
            {
                if (Select(pc, "去ECO城吗？", "", "去", "不去") == 1)
                {
                    PlaySound(pc, 2040, false, 100, 50); 
                    NPCMotion(pc, 11001894, 364, false, 10);
                    NPCMotion(pc, 11001895, 364, false, 10);
                    NPCMotion(pc, 11001896, 364, false, 10);
                    NPCMotion(pc, 11001897, 364, false, 10);
                    NPCMotion(pc, 11001898, 364, false, 10);
                    NPCMotion(pc, 11001899, 364, false, 10);
                    NPCMotion(pc, 11001900, 364, false, 10);
                    NPCMotion(pc, 11001901, 364, false, 10);
                    NPCMotion(pc, 11001902, 364, false, 10);
                    Wait(pc, 990);
                    ShowEffect(pc, 4011);
                    Wait(pc, 990);
                    Warp(pc, 11027000, 252, 7);
                }
                return;
            }

            Say(pc, 0, 131, "……、虹、钥匙……。$R;" + 
             "梦之…门……、打开、钥匙……。$R;", "迎宾兔子");

            if (CountItem(pc, 10022953) >= 1 && mask.Test(Tinyis_Land_01.去過ECO城))
            {
                if (Select(pc, "怎么办？", "", "什麼也不做", "「彩虹之钥」给他") == 2)
                {
                    TakeItem(pc, 10022953, 1);
                    Say(pc, 0, 0, "「彩虹之钥」给兔子了！$R;", " ");
                    PlaySound(pc, 2040, false, 100, 50);
                    NPCMotion(pc, 11001894, 364, false, 10);
                    NPCMotion(pc, 11001895, 364, false, 10);
                    NPCMotion(pc, 11001896, 364, false, 10);
                    NPCMotion(pc, 11001897, 364, false, 10);
                    NPCMotion(pc, 11001898, 364, false, 10);
                    NPCMotion(pc, 11001899, 364, false, 10);
                    NPCMotion(pc, 11001900, 364, false, 10);
                    NPCMotion(pc, 11001901, 364, false, 10);
                    NPCMotion(pc, 11001902, 364, false, 10);
                    Wait(pc, 990);
                    ShowEffect(pc, 4011);
                    Wait(pc, 990);
                    Warp(pc, 11027000, 252, 7);
                    return;
                }
            }
        }
    }
}
