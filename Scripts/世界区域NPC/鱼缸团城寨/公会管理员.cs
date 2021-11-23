
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using System.Text;
using System.Diagnostics;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.FF
{
    public class S80000007 : Event
    {
        public S80000007()
        {
            this.EventID = 80000007;
        }


        public override void OnEvent(ActorPC pc)
        {
            if (pc.Ring == null)
            {
                switch (Select(pc, "欢迎来到公会登记处，请问您想做什么?", "", "组织公会 (20万金币)", "什么也不做"))
                {
                    case 1:
                        创立新的公会(pc);
                        return;

                }
            }
        }
        void 创立新的公会(ActorPC pc)
        {
            string Ring_Name;

            if (pc.Gold >= 200000)
            {
                Ring_Name = InputBox(pc, "请输入公会名称", InputType.PetRename);

                if (Ring_Name != "")
                {
                    if (Ring_Name.Length > 24)
                    {
                        Say(pc, 11000250, 131, "不能使用这个名字呀!$R;" +
                                               "$R请确认字数以后，再来申请吧!$R;" +
                                               "（最多中文12个字/英文24个字）$R;", "公会管理员");

                        创立新的公会(pc);
                    }

                    if (CreateRing(pc, Ring_Name))
                    {
                        PlaySound(pc, 4006, false, 100, 50);
                        pc.Gold -= 1000000;

                        Say(pc, 11000250, 111, "成功创立新的公会了!!$R;" +
                                               "$P违现在开始，$R;" +
                                               "您就是这个公会的『公会总管』唷!$R;" +
                                               "$R一定要成为出色的公会喔，$R;" +
                                               "加油吧!!$R;", "公会管理员");
                    }
                    else
                    {
                        Say(pc, 11000250, 131, "抱歉，已经有同名的公会了。$R;", "公会管理员");

                        创立新的公会(pc);
                    }
                }
            }
            else
            {
                Say(pc, 11000250, 131, "想要组成公会的话，需要20万金币呀!$R;", "公会管理员");
            }
        }
    }
}

