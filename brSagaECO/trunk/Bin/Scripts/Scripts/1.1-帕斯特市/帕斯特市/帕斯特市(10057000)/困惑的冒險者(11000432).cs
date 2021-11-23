using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000432 : Event
    {
        public S11000432()
        {
            this.EventID = 11000432;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 190, "我們三個一起到處冒險時$R;" +
                "$R有一個人到這個森林裡後$R;" +
                "被困而不能出來$R;" +
                "處在進退兩難的地步$R;");
            Say(pc, 11000431, 190, "可以的話去咖啡館接受任務好嗎？$R;" +
                "$R拜金使會給您豐富的報酬的$R;");
            Say(pc, 190, "什麽?我給？$R;");
        }
    }
}