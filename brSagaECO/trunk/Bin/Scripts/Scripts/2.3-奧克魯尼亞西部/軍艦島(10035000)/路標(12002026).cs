using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10035000
{
    public class S12002026 : Event
    {
        public S12002026()
        {
            this.EventID = 12002026;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 131, "西邊荒廢礦村$R;");
            switch (Select(pc, "後面寫了些什麽…", "", "看看吧！", "不看！"))
            {
                case 1:
                    Say(pc, 131, "攪拌黃麥粉、鹽和砂糖$R;" +
                        "$P把混合物放在水裡再攪勻$R;" +
                        "$P變得滑滑的，放在室溫20分鐘$R;" +
                        "$P用平底鍋煎到硝味四溢就完成$R;" +
                        "$R非常香甜可口的$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}