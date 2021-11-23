using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30131000
{
    public class S11000655 : Event
    {
        public S11000655()
        {
            this.EventID = 11000655;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<PSTFlags> mask = pc.CMask["PST"];
            if (!mask.Test(PSTFlags.綠盾軍兵士第一次對話))//_0c49)
            {
                mask.SetValue(PSTFlags.綠盾軍兵士第一次對話, true);
                //_0c49 = true;
                Say(pc, 131, "最近南边的古城和东边树林里$R;" +
                    "魔物的活动频繁起来了$R;" +
                    "$P为了守护这座城市的安全$R;" +
                    "我们绿盾军定期击退魔物呢$R;" +
                    "$R您也要为了这个街道的安全$R;" +
                    "做些什么事情吗？$R;");
            }
            else
            {
                Say(pc, 131, "欢迎光临$R;" +
                    "$R其他人都说天气好，出去散步了$R;" +
                    "反正这些家伙真是能溜…$R;");
                Say(pc, 131, "什么事？$R;");
            }
            switch (Select(pc, "想怎么做？", "", "任务服务台", "什么也不做"))
            {
                case 1:
                    break;
            }

        }
    }
}