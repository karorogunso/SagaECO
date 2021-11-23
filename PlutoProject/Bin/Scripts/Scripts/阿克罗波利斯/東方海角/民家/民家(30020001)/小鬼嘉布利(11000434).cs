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
                    Say(pc, 131, "姐姐也喜欢宠物?$R;");
                }
                else
                {
                    Say(pc, 131, "哥哥也喜欢宠物?$R;");
                }
                switch (Select(pc, "喜欢宠物吗?", "", "喜欢", "普通", "讨厌"))
                {
                    case 1:
                        Say(pc, 131, "啊…是吗?$R;" +
                            "那我带你到好地方吧$R;" +
                            "$R但是场所是秘密，要蒙住眼睛$R;");
                        //GOTO EVT10000244
                        Warp(pc, 30112000, 25, 13);
                        break;
                }
                Say(pc, 131, "什么?不喜欢?$R;" +
                    "嗯…那样的话没办法啊$R;");
                return;
            }
            Say(pc, 131, "妈妈允许养汪汪了!!$R;" +
                "太开心了!$R;");

        }
    }
}