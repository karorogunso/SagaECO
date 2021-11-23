
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public class S60000127 : Event
    {
        public S60000127()
        {
            this.EventID = 60000127;
        }

        public override void OnEvent(ActorPC pc)
        {
            ChangeMessageBox(pc);
            Say(pc, 0, "依兰..我的挚爱...$R我果然还是忘不掉你...$R$R如果有机会，我也好想到你的那个世界去...", "樱花树下的男孩");
            Select(pc, "　", "", "奇怪的人……");
        }
    }
}