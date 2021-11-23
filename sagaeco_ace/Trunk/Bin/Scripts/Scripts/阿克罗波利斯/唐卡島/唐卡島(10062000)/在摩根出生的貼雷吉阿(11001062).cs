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
                Say(pc, 131, "因为丈夫工作关系，我们从$R;" +
                    "摩戈搬到这里来。$R;" +
                    "$R是一个位于摩戈西部，$R有丰富矿产的国家，$R;" +
                    "可以容易找到用于飞空庭燃料的$R『摩戈炭』！$R;");
            }
            else
            {
                Say(pc, 131, "到唐卡来的最好的事情是，$R;" +
                    "天气好，衣服干得快，$R;" +
                    "$R摩戈总是阴天，衣服得晾在屋里$R;" +
                    "就会有味儿。$R;");
            }
        }
    }
}