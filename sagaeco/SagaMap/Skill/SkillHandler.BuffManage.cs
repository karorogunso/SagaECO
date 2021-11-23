using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Item;

using SagaDB;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.SkillDefinations;
using SagaMap.Network.Client;
using SagaMap.Mob;
using SagaMap.Skill.Additions.Global;
using SagaMap.ActorEventHandlers;
namespace SagaMap.Skill
{
    public partial class SkillHandler : Singleton<SkillHandler>
    {
        public Dictionary<string, ICDBuff> StableBuffsHandler = new Dictionary<string, ICDBuff>();
        public void InitBuffs()
        {
            StableBuffsHandler.Add("无尽寒冬CD", new 无尽寒冬CD());
            StableBuffsHandler.Add("自杀CD", new 自杀CD());
            StableBuffsHandler.Add("食物效果", new 食物效果());
        }
    }
}
