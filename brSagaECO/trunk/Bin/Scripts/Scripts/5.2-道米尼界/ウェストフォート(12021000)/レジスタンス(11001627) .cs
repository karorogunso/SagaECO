using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001627 : Event
    {
        public S11001627()
        {
            this.EventID = 11001627;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 347, "這兒！！$R;", "レジスタンス");

            Say(pc, 11001628, 408, "好味道！$R;", "エイダ");
           }

           }
                        
                }

/*public override void OnEvent(ActorPC pc)
        {
            Say(pc, 347, "でやぁっ！！$R;", "レジスタンス");

            Say(pc, 11001628, 408, "甘いっ！$R;", "エイダ");
           }*/
     
    
