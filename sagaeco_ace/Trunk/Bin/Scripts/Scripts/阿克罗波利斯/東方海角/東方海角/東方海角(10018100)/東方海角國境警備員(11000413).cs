using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10018100
{
    public class S11000413 : Event
    {
        public S11000413()
        {
            this.EventID = 11000413;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Knights> Knights_mask = pc.CMask["Knights"];

            if (pc.Skills2.ContainsKey(706) || pc.SkillsReserve.ContainsKey(706))
            {
                Say(pc, 131, "哦哦!$R;" +
                    "不是" + pc.Name + "STR1$R;" +
                    "$R来…通过也可以$R;");
                return;
            }
            if (!Knights_mask.Test(Knights.東國過境檢查完成))
            {
                if (Knights_mask.Test(Knights.加入東軍騎士團))
                {
                    Knights_mask.SetValue(Knights.東國過境檢查完成, true);
                    Say(pc, 131, "嗯?你属于东军?$R;" +
                        "那样的话审核通过!$R;" +
                        "$R可以通过了~!$R;");
                    return;
                }
                if (CountItem(pc, 10041800) >= 1)
                {
                    Knights_mask.SetValue(Knights.東國過境檢查完成, true);
                    TakeItem(pc, 10041800, 1);
                    Say(pc, 131, "啊…莫非那个是$R;" +
                        "『法伊斯特入国许可证』?$R;" +
                        "$R那样的话审核通过!$R;" +
                        "可以通过了~!$R;");
                    return;
                }
                Say(pc, 131, "你想去法伊斯特?$R;" +
                    "$R想去的话$R;" +
                    "要有『法伊斯特入国许可证』才可以$R;");
                if (pc.Job > (PC_JOB)80)
                {
                    Say(pc, 131, "在生产系行会里有卖$R;" +
                        "所以去买过来吧$R;");
                    return;
                }
                Say(pc, 131, "在生产系行会里有卖$R;" +
                    "拜托朋友购买吧$R;");
                return;
            }
            Say(pc, 131, "好的…可以通过了$R;");
        }
    }
}