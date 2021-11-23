using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S12002097 : Event
    {
        public S12002097()
        {
            this.EventID = 12002097;
        }

        public override void OnEvent(ActorPC pc)
        {
            PlaySound(pc, 2559, false, 100, 50);
            Select(pc, "イラッシャイマセ", "", "アイテムチケット交換", "利用方法を読む", "何もしない");
           }

           }
                        
                }
            
            
        
     
    