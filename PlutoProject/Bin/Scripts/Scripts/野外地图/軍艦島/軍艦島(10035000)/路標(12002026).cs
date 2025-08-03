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

            Say(pc, 131, "西边废炭矿$R;");
            switch (Select(pc, "后面写了些什么…", "", "看看吧！", "不看！"))
            {
                case 1:
                    Say(pc, 131, "搅拌黄麦粉、盐和砂糖$R;" +
                        "$P把混合物放在水里再搅匀$R;" +
                        "$P变得滑滑的，放在室温20分钟$R;" +
                        "$P用平底锅煎到香味四溢就完成$R;" +
                        "$R非常香甜可口的$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}