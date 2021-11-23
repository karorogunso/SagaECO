using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001062 : Event
    {
        public S11001062()
        {
            this.EventID = 11001062;
        }

        public override void OnEvent(ActorPC pc)
        {
            int a = Global.Random.Next(1, 2);
            if (a == 1)
            {
                Say(pc, 131, "因為丈夫工作關係，我們從$R;" +
                    "摩根搬到這裡來。$R;" +
                    "$R是一個位於摩根西部，$R有豐富礦產的國家，$R;" +
                    "可以容易找到用於飛空庭燃料的$R『摩根炭』！$R;");
            }
            else
            {
                Say(pc, 131, "到唐卡來的最好的事情是，$R;" +
                    "天氣好，衣服乾得快，$R;" +
                    "$R摩根總是陰天，衣服得晾在屋裡$R;" +
                    "就會有味兒。$R;");
            }
        }
    }
}