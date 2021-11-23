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
                Say(pc, 131, "最近南邊的古城和東邊樹林裡$R;" +
                    "魔物的活動頻繁起來了$R;" +
                    "$P為了守護這座城市的安全$R;" +
                    "我們綠色防備軍定期擊退魔物呢$R;" +
                    "$R您也為了這個街道的安全$R;" +
                    "做些什麼事情嗎？$R;");
            }
            else
            {
                Say(pc, 131, "歡迎光臨$R;" +
                    "$R其他人都説天氣好，出去散步了$R;" +
                    "反正這些傢伙真是能溜…$R;");
                Say(pc, 131, "什麽事？$R;");
            }
            switch (Select(pc, "想怎麼做？", "", "任務服務台", "什麽也不做"))
            {
                case 1:
                    break;
            }

        }
    }
}