using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001613 : Event
    {
        public S11001613()
        {
            this.EventID = 11001613;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 209, "空に浮ぶ太陽と月を見ていると$R;" +
            "なんだか不安になってくるんだ。$R;" +
            "$Rなんでだろう？$R;" +
            "今までこんなことなかったのに……。$R;", "空を見上げる少年");
           }

           }
                        
                }
            
            
        
     
    