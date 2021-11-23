using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Network.Client;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Global
{
    public class Synthese : SkillEvent
    {
        protected override void RunScript(SkillEvent.Parameter para)
        {
            Scripting.SkillEvent.Instance.Synthese((ActorPC)para.sActor, (ushort)para.args.skill.ID, para.level, true);
        }
    }
}
