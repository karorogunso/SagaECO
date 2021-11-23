
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using System.Text;
using System.Diagnostics;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.FF
{
    public class S80000014 : Event
    {
        public S80000014()
        {
            this.EventID = 80000014;
        }

        class IrisTake
        {
            public uint score;
        }
        public override void OnEvent(ActorPC Character)
        {
            byte level = 1;
            if (Character.CInt["料理EXP"] > 5000)
                level = 2;
            if (Character.CInt["料理EXP"] > 30000)
                level = 3;
            if (Character.CInt["料理EXP"] > 150000)
                level = 4;
            if (Character.CInt["料理EXP"] > 500000)
                level = 5;
            SkillEvent.Instance.Synthese(Character, 2040, level);
            SagaMap.Network.Client.MapClient.FromActorPC(Character).SendSystemMessage("当前料理等级：" +level.ToString() + " 经验：" + Character.CInt["料理EXP"].ToString());
            /*if (Character.CInt["料理EXP"] >= 3000 && Character.AInt["美食家领取"] != 1)
            {
                Character.AInt["美食家领取"] = 1;
                GiveItem(Character, 130000028, 1);
                SagaMap.Network.Client.MapClient.FromActorPC(Character).SendSystemMessage("料理经验达到3000 ，获得【美食家】称号！");
            }*/
        }
    }
}