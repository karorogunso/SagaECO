using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10002000
{
    public class S11000912 : Event
    {
        public S11000912()
        {
            this.EventID = 11000912;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            BitMask<RLSXYFlags> mask = new BitMask<RLSXYFlags>(pc.CMask["RLSXY"]);
            if (!mask.Test(RLSXYFlags.寵物養殖研究員第一次對話))
            {
                Say(pc, 131, "您好!$R今天有点凉~$R;" +
                    "$R我想养殖克罗，所以到这里来…$R这里人也挺多的呀$R;" +
                    "$P那么！去宠物养殖场…$R;" +
                    "$R日前发现有个地方适合养殖宠物哦$R;" +
                    "$R想不想去一趟呢？$R;");
                mask.SetValue(RLSXYFlags.寵物養殖研究員第一次對話, true);
            }
            else
            {
                Say(pc, 131, "您好！$R;" +
                    "$R是不是想去宠物养殖场了？$R;");
            }
            selection = Select(pc, "去宠物养殖场吗？", "", "去", "不去", "什么叫宠物养殖场？");
            while (selection == 3)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 131, "那么我给您带路吧$R;");
                        Say(pc, 131, "未实装$R;");
                        break;
                    case 2:
                        break;
                    case 3:
                        Say(pc, 131, "宠物养殖场$R顾名思义$R是养殖自己的宠物的地方$R;" +
                            "$R是个可以随心所欲地$R养殖宠物的地方$R;" +
                            "$P东南部也有发现，可以去看看哦$R;" +
                            "$R还有沙漠地带$R也听说有很好的地方……$R;");
                        break;
                }
                selection = Select(pc, "去宠物养殖场吗？", "", "去", "不去", "什么叫宠物养殖场？");
            }
        }
    }
}
