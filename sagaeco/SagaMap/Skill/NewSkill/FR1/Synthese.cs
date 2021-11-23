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
            ActorPC pc = (ActorPC)para.sActor;
            MapClient client = MapClient.FromActorPC(pc);
            List<Actor> actors = client.map.GetActorsArea(pc, 300, false);

            bool canuse = false;
            foreach (var item in actors)
            {
                if (item.type == ActorType.SKILL && item.Name == "营火")
                    canuse = true;
            }

            if (canuse)
                Scripting.SkillEvent.Instance.Synthese((ActorPC)para.sActor, (ushort)para.args.skill.ID, para.level, true);
            else
                client.SendSystemMessage("必须在营火周围才可以使用『料理』。");
        }
    }
}
