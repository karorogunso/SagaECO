using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10034000
{
    public class S11000910 : Event
    {
        public S11000910()
        {
            this.EventID = 11000910;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DHAFlags> mark = new BitMask<DHAFlags>(pc.CMask["DHA"]);
            int selection;
            if (!mark.Test(DHAFlags.寵物養殖研究員第一次對話))
            {
                mark.SetValue(DHAFlags.寵物養殖研究員第一次對話, true);
                Say(pc, 255, "您好$R;" +
                    "$R去寵物養殖場嗎?$R;");
            }
            else
            {
                Say(pc, 255, "您好$R真是風和日麗呀$R;" +
                    "$R為了養牛牛才來到了這裡$R不過人還不少呀~$R;" +
                    "$P其實附近有一處好地方唷$R;" +
                    "$R我們研究員稱它為寵物養殖場$R;" +
                    "$R一起去看看吧?$R;");
            }
            selection = Select(pc, "去寵物養殖場嗎?", "", "去", "不去", "什麼叫寵物養殖場?");
            while (selection == 3)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 255, "那麼我給您帶路吧!$R;");
                        Say(pc, 255, "未实装$R;");
                        break;
                    case 3:
                        Say(pc, 255, "寵物養殖場$R顧名思義$R是養殖自己的寵物的地方$R;" +
                            "$R是個可以隨心所欲地$R養殖寵物的地方$R;" +
                            "$P北部也有發現，可以去看看唷$R;" +
                            "$R還有沙漠地帶$R也聽說有很好的地方……$R;");
                        break;
                }
                selection = Select(pc, "去寵物養殖場嗎?", "", "去", "不去", "什麼叫寵物養殖場?");
            }
        }
    }
}
