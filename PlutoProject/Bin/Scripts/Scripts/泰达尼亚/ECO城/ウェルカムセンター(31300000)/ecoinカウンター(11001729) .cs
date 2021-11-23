using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31300000
{
    public class S11001729 : Event
    {
        public S11001729()
        {
            this.EventID = 11001729;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "何でもご相談下さい！", "", "別に用はない", "『ecoin』を購入する", "銀行にお金を預ける", "銀行からお金を引き出す"))
            {
                case 3:
                    BankDeposit(pc);
                    break;

                case 4:
                    BankWithdraw(pc);
                    break;
            }
            Say(pc, 11001724, 131, "では、引き続き$R;" +
            "ＥＣＯタウンをお楽しみ下さい！$R;", "ecoinカウンター");
        }


    }
}


