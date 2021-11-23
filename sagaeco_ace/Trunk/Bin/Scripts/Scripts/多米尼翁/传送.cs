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


    public class P10001646 : Event
    {
        public P10001646()
        {
            this.EventID = 10001646;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<emojie_00> emojie_00_mask = new BitMask<emojie_00>(pc.CMask["emojie_00"]);
            if (emojie_00_mask.Test(emojie_00.开关))//去夜
            {
                NPCShow(pc, 11001578);//让NPC出现
                NPCHide(pc, 11001633);//隐藏NPC
                NPCHide(pc, 11001577);//隐藏NPC
                Warp(pc, 32200001, 2, 5);
            }
            else
            {
                NPCHide(pc, 11001578);//隐藏NPC
                NPCShow(pc, 11001633);//让NPC出现
                NPCShow(pc, 11001577);//让NPC出现
                Warp(pc, 32200001, 2, 5);
            }

        }

    }
    public class P10001620 : Event
    {
        public P10001620()
        {
            this.EventID = 10001620;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<emojie_00> emojie_00_mask = new BitMask<emojie_00>(pc.CMask["emojie_00"]);
            if (emojie_00_mask.Test(emojie_00.开关))//去夜
            {
                Warp(pc, 12020001, 108, 77);
            }
            else
            {
                Warp(pc, 12020000, 110, 75);
            }
        }
    }
    public class P10001638 : Event
    {
        public P10001638()
        {
            this.EventID = 10001638;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 22004002, 105, 101);
        }
    }
    public class P10001639 : Event
    {
        public P10001639()
        {
            this.EventID = 10001639;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<emojie_00> emojie_00_mask = new BitMask<emojie_00>(pc.CMask["emojie_00"]);
            if (emojie_00_mask.Test(emojie_00.开关))//去夜
            {
                Warp(pc, 12020001, 182, 49);
            }
            else
            {
                Warp(pc, 12020000, 184, 49);
            }
        }
    }
    //-------------------(saga10)-------------------//
    public class P10001766 : Event
    {
        public P10001766()
        {
            this.EventID = 10001766;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 22198000, 97, 127);
        }
    }
    public class P10001765 : Event
    {
        public P10001765()
        {
            this.EventID = 10001765;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 22197000, 32, 16);
        }
    }




}