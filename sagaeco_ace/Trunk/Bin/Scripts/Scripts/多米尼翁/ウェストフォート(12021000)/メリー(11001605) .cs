using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001605 : Event
    {
        public S11001605()
        {
            this.EventID = 11001605;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 500, "クゥ～ン……。$R;", "メリー");

            Say(pc, 0, 131, "メリーは、ニナを$R;" +
            "心配そうに見つめている。$R;", " ");
           }

           }
                        
                }
            
            
        
     
    