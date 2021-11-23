using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50002000
{
    public class S11000885 : Event
    {
        public S11000885()
        {
            this.EventID = 11000885;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_04> Neko_04_amask = pc.AMask["Neko_04"];
            BitMask<Neko_04> Neko_04_cmask = pc.CMask["Neko_04"];
            if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.被詢問犯人的事) && 
                !Neko_04_cmask.Test(Neko_04.被告知去找機器人))
            {
                Say(pc, 11000885, 131, "受委託的物品被偷的事情$R要告訴商人嗎？$R;" +
                    "$R雖然我也不知道$R裡面有什麼東西$R;");
                return;
            }
            Say(pc, 11000885, 131, "我也暈了$R為甚麼這樣不講道理…$R;" +
                "$R不知該說什麼好…$R;");
        }
    }
}