using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public abstract class 鑑定師 : Event
    {
        public 鑑定師()
        {

        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            if (!World_01_mask.Test(World_01.已經與鑑定師進行第一次對話))
            {
                初次與鑑定師進行對話(pc);
                return;
            }

            Identify(pc);
        }

        void 初次與鑑定師進行對話(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            World_01_mask.SetValue(World_01.已經與鑑定師進行第一次對話, true);

            Say(pc, 131, "您好! 我是「鉴定师」。$R;" +
                         "$R有句话叫老马识途。$R;" +
                         "$P我就使用我长时间的人生经验，$R;" +
                         "来「鉴定」未鉴定的道具。$R;" +
                         "$P发现未鉴定的道具的时候，$R;" +
                         "就带过来找我吧。$R;", "鉴定师");
        }
    }
}
