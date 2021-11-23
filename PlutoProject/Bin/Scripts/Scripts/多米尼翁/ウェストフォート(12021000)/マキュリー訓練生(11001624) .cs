using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001624 : Event
    {
        public S11001624()
        {
            this.EventID = 11001624;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "……。$R;" +
            "$P…………。$R;" +
            "$P………………。$R;" +
            "（……今、訓練中なの。$R;" +
            "話しかけないで貰えるかしら？）$R;", "マキュリー訓練生");
           }

           }
                        
                }
            
            
        
     
    