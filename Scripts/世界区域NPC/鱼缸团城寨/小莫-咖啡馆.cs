
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
    public partial class S60000130 : Event
    {
        public S60000130()
        {
            this.EventID = 60000130;
        }

        public override void OnEvent(ActorPC pc)
        {
                switch (pc.CInt["东牢进入任务"])
                {
                    case 0:
                        东牢进入任务(pc);
                        return;
                    case 1:
                        Say(pc, 0, "事不宜迟，我们得赶快前往东方地牢才行。", "小莫");
                        return;
                    case 2:
                        Say(pc, 0, "嗯……？怎么了，你在怀疑我吗？", "小莫");
                        return;
                    case 3:
                        Say(pc, 0, "我只是回来看看情况，我没有恶意的……", "莫库草·阿鲁玛");
                        return;
                }
        }
    }
}