using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
using System.Globalization;
using System.Net;
using System.Linq;

namespace SagaScript.M19230003
{
    public class S19230003 : Event
    {
        public S19230003()
        {
            this.EventID = 19230003;
        }
        public override void OnEvent(ActorPC pc)
        {
			

						
			Random Rand = new Random();
			int randVal = Rand.Next(1,10000);
			
			if(randVal <= 1000){
				GiveItem(pc,10087400,1);
			}else if(randVal <= 2000){
				GiveItem(pc,10087401,1);
			}else if(randVal <= 3000){
				GiveItem(pc,10087402,1);
			}else if(randVal <= 4000){
				GiveItem(pc,10087403,1);
			}else if(randVal <= 5000){
				GiveItem(pc,20050038,1);
			}else if(randVal <= 6000){
				GiveItem(pc,20050039,1);
			}else if(randVal <= 7000){
				GiveItem(pc,20050133,1);
			}else if(randVal <= 8000){
				GiveItem(pc,20050053,1);
			}else if(randVal <= 9000){
				GiveItem(pc,20050020,1);
			}else if(randVal <= 10000){
				GiveItem(pc,20050003,1);
			}
			
			
		   
			Say(pc,11000776,0,"你蓋滿了第10個印章了！","接待泰迪");
			
			
			
        }
    }
}