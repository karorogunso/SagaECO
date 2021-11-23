using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001617 : Event
    {
        public S11001617()
        {
            this.EventID = 11001617;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "門の中で、何か扉を$R;" +
            "破壊するような音がしたんだ！$R;", "羅城門門兵");
           }

           }
                        
                }
            
            
        
     
    