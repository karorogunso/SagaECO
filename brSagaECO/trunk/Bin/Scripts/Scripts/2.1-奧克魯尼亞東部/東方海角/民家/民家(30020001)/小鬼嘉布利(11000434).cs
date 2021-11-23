using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M30020001
{
    public class S11000434 : Event
    {
        public S11000434()
        {
            this.EventID = 11000434;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Account.GMLevel == 1)
            {
                if (pc.Gender == PC_GENDER.FEMALE)
                {
                    Say(pc, 131, "姐姐也喜歡寵物?$R;");
                }
                else
                {
                    Say(pc, 131, "哥哥也喜歡寵物?$R;");
                }
                switch (Select(pc, "喜歡寵物嗎?", "", "喜歡", "普通", "討厭"))
                {
                    case 1:
                        Say(pc, 131, "啊…是嗎?$R;" +
                            "那我帶你到好地方吧$R;" +
                            "$R但是場所是秘密，要矇住眼睛$R;");
                        //GOTO EVT10000244
                        Warp(pc, 30112000, 25, 13);
                        break;
                }
                Say(pc, 131, "什麽?不喜歡?$R;" +
                    "嗯…那樣的話沒辦法啊$R;");
                return;
            }
            Say(pc, 131, "媽媽允許養汪汪了!!$R;" +
                "太開心了!$R;");

        }
    }
}