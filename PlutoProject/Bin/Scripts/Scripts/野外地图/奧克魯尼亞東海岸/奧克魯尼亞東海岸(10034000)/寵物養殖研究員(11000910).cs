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
                    "$R去宠物养殖场吗?$R;");
            }
            else
            {
                Say(pc, 255, "您好$R真是风和日丽呀$R;" +
                    "$R为了养牛牛才来到了这里$R不过人还不少呀~$R;" +
                    "$P其实附近有一处好地方哦$R;" +
                    "$R我们研究员称它为宠物养殖场$R;" +
                    "$R一起去看看吧?$R;");
            }
            selection = Select(pc, "去宠物养殖场吗?", "", "去", "不去", "什么叫宠物养殖场?");
            while (selection == 3)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 255, "那么我给您带路吧!$R;");
                        Say(pc, 255, "未实装$R;");
                        break;
                    case 3:
                        Say(pc, 255, "宠物养殖场$R顾名思义$R是养殖自己的宠物的地方$R;" +
                            "$R是个可以随心所欲地$R养殖宠物的地方$R;" +
                            "$P北部也有发现，可以去看看哦$R;" +
                            "$R还有沙漠地带$R也听说有很好的地方……$R;");
                        break;
                }
                selection = Select(pc, "去宠物养殖场吗?", "", "去", "不去", "什么叫宠物养殖场?");
            }
        }
    }
}
