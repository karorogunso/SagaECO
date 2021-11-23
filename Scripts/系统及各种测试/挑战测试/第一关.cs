
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S50003001 : Event
    {
        public S50003001()
        {
            this.EventID = 50003001;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<挑战记录> 记录 = pc.CMask["挑战记录"];
            Say(pc, 0, "第一关$R$R最佳挑战者:$R时间:$R");
            if (!记录.Test(挑战记录.完成第一关))
            {
                if (pc.TInt["挑战标记"] == 1)//个人挑战
                {
                    switch(Select(pc,"个人挑战第一关","","开始挑战","还没有准备好"))
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                    }
                }
                else if (pc.TInt["挑战标记"] == 2)//团队挑战
                {
                }
                else
                {
                    Say(pc, 0, "……");
                }
            }
        }
    }
}

