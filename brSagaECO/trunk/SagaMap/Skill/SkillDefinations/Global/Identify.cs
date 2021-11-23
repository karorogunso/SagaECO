using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaMap.Network.Client;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Global
{
    public class Identify : SkillEvent
    {
        protected override void RunScript(SkillEvent.Parameter para)
        {
            Scripting.SkillEvent.Instance.Identify((ActorPC)para.sActor);
        }
    }
}
