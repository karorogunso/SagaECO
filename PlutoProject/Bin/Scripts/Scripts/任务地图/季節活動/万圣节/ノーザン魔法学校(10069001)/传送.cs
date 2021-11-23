using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript
{


    public class P10001744 : Event
    {
        public P10001744()
        {
            this.EventID = 10001744;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 10069001, 119, 185);
        }
    }
    public class P10001745 : Event
    {
        public P10001745()
        {
            this.EventID = 10001745;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 10069001, 129, 155);
        }
    }
    public class P10001746 : Event
    {
        public P10001746()
        {
            this.EventID = 10001746;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 10069001, 125, 163);
        }
    }
    public class P10001747 : Event
    {
        public P10001747()
        {
            this.EventID = 10001747;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 10069001, 88, 161);
        }
    }
    public class P10001748 : Event
    {
        public P10001748()
        {
            this.EventID = 10001748;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 10069001, 98, 171);
        }
    }
    public class P10001749 : Event
    {
        public P10001749()
        {
            this.EventID = 10001749;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<wsj> wsj_mask = new BitMask<wsj>(pc.CMask["wsj"]);
            //int selection;
            if (wsj_mask.Test(wsj.项链))
            {
                Say(pc, 0, 0, "儀式は終わりました。$R;" +
                "現在清掃中ですので$R;" +
                "中に入ることは出来ません。$R;", "");
                return;
            }
            Warp(pc, 10069001, 127, 118);
        }
    }
    public class P10001750 : Event
    {
        public P10001750()
        {
            this.EventID = 10001750;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 10069001, 119, 144);
        }
    }
}