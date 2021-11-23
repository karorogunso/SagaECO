using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10001626
{
    public class S11001591 : Event
    {
        public S11001591()
        {
            this.EventID = 11001591;
        }

        public override void OnEvent(ActorPC pc)
        {
             switch (Select(pc, "怎么办？", "", "要回去", "不了"))
             {
                 case 1:
            Say(pc, 0, 0, "……摇晃。$R;" +
            "$R手抓住就好$R;" +
             "好好的抓紧。$R;", "飛空庭係員");
            PlaySound(pc, 2426, false, 100, 50);

            Say(pc, 0, 0, "……向通天塔出发了！$R;", "飛空庭係員");
            PlaySound(pc, 2438, false, 100, 50);
            ShowEffect(pc, 48, 158, 8066);
            Wait(pc, 1980);
            Warp(pc, 12058000, 128, 250);
            break;
        }
           }

           }
                        
                }
            
            
        
     
    