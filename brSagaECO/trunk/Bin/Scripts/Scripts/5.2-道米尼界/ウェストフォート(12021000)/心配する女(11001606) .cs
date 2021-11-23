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
            Say(pc, 131, "……ニナ的父母是因從DEM的攻擊中$R;" +
            "把那個孩子保護下來$R;" +
            "而喪命的……。$R;" +
            "$R自此以後ニナ就一直，$R;" +
            "變成那樣的樣子……。$R;" +
            "$P……的確，我在這街上$R;" +
            "打聽到哥哥還在生的消息$R;" +
            "雖然我是被帶來這兒的……。$R;", "心配する女");
           }

           }
                        
                }
//
/*Say(pc, 131, "……あの子、ニナの両親は$R;" +
            "ＤＥＭの攻撃からあの子をかばって$R;" +
            "命を失ってしまったの……。$R;" +
            "$Rそれからニナはずっと$R;" +
            "あんな感じで……。$R;" +
            "$P……たしか、この街に$R;" +
            "お兄さんがいるって聞いて$R;" +
            "私が連れて来たんだけど……。$R;", "心配する女");*/

            
            
        
     
    
