using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001611 : Event
    {
        public S11001611()
        {
            this.EventID = 11001611;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 135, "ＤＥＭのヤロー。$R;" +
            "俺の大切なコレクションを$R;" +
            "こんなにボロボロにしやがって……。$R;" +
            "$Rいつかスクラップにしてやるぜ。$R;", "スミス");
           }

           }
                        
                }
            
            
        
     
    