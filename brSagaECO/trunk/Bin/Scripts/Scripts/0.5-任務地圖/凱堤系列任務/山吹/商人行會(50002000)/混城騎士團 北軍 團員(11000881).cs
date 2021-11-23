using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50002000
{
    public class S11000881 : Event
    {
        public S11000881()
        {
            this.EventID = 11000881;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_04> Neko_04_amask = pc.AMask["Neko_04"];
            BitMask<Neko_04> Neko_04_cmask = pc.CMask["Neko_04"];

            if (!Neko_04_cmask.Test(Neko_04.收到商人的傳達品))
            {
                Say(pc, 11000881, 131, "現在正調查犯罪現場$R不要妨礙調查$R;");
                return;
            }
            if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.被詢問犯人的事) &&
                !Neko_04_cmask.Test(Neko_04.被告知去找機器人))
            {
                Say(pc, 11000881, 131, "差一點出大事了…$R現在可以走了$R;");
                return;
            }
            Say(pc, 11000881, 131, "現在正調查犯罪現場$R結束之前不能回去$R;");
        }
    }
}