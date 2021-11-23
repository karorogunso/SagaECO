using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001606 : Event
    {
        public S11001606()
        {
            this.EventID = 11001606;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "……あの子、ニナの両親は$R;" +
            "ＤＥＭの攻撃からあの子をかばって$R;" +
            "命を失ってしまったの……。$R;" +
            "$Rそれからニナはずっと$R;" +
            "あんな感じで……。$R;" +
            "$P……たしか、この街に$R;" +
            "お兄さんがいるって聞いて$R;" +
            "私が連れて来たんだけど……。$R;", "心配する女");
           }

           }
                        
                }
            
            
        
     
    