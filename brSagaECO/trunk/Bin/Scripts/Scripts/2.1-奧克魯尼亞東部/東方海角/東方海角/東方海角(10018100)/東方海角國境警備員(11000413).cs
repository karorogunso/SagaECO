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
                    "$R來…通過也可以$R;");
                return;
            }
            if (!Knights_mask.Test(Knights.東國過境檢查完成))
            {
                if (Knights_mask.Test(Knights.加入東軍騎士團))
                {
                    Knights_mask.SetValue(Knights.東國過境檢查完成, true);
                    Say(pc, 131, "嗯?你屬於東軍?$R;" +
                        "那樣的話審核通過!$R;" +
                        "$R可以通過了~!$R;");
                    return;
                }
                if (CountItem(pc, 10041800) >= 1)
                {
                    Knights_mask.SetValue(Knights.東國過境檢查完成, true);
                    TakeItem(pc, 10041800, 1);
                    Say(pc, 131, "阿…莫非那個是$R;" +
                        "『帕斯特入國許可證』?$R;" +
                        "$R那樣的話審核通過!$R;" +
                        "可以通過了~!$R;");
                    return;
                }
                Say(pc, 131, "你想去帕斯特?$R;" +
                    "$R想去帕斯特的話$R;" +
                    "要有『帕斯特入國許可證』才可以$R;");
                if (pc.Job > (PC_JOB)80)
                {
                    Say(pc, 131, "在生產系行會裡有賣$R;" +
                        "所以去買過來吧$R;");
                    return;
                }
                Say(pc, 131, "在生產系行會裡有賣$R;" +
                    "拜託朋友購買吧$R;");
                return;
            }
            Say(pc, 131, "好的…可以通過了$R;");
        }
    }
}