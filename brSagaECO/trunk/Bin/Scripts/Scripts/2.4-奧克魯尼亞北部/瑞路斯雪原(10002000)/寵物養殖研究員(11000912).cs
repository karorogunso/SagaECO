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
                Say(pc, 131, "您好!$R今天有點涼~$R;" +
                    "$R我想養殖高寶，所以到這裡來…$R這裡人也挺多的呀$R;" +
                    "$P那麼！去寵物養殖場…$R;" +
                    "$R日前發現有個地方適合養殖寵物唷$R;" +
                    "$R想不想去一趟呢？$R;");
                mask.SetValue(RLSXYFlags.寵物養殖研究員第一次對話, true);
            }
            else
            {
                Say(pc, 131, "您好！$R;" +
                    "$R是不是想去寵物養殖場了？$R;");
            }
            selection = Select(pc, "去寵物養殖場嗎？", "", "去", "不去", "什麼叫寵物養殖場？");
            while (selection == 3)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 131, "那麼我給您帶路吧$R;");
                        Say(pc, 131, "未實裝$R;");
                        break;
                    case 2:
                        break;
                    case 3:
                        Say(pc, 131, "寵物養殖場$R顧名思義$R是養殖自己的寵物的地方$R;" +
                            "$R是個可以隨心所欲地$R養殖寵物的地方$R;" +
                            "$P北部也有發現，可以去看看唷$R;" +
                            "$R還有沙漠地帶$R也聽說有很好的地方……$R;");
                        break;
                }
                selection = Select(pc, "去寵物養殖場嗎？", "", "去", "不去", "什麼叫寵物養殖場？");
            }
        }
    }
}
