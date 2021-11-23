using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class MaxHPDown : DefaultBuff
    {
        public MaxHPDown(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "MaxHPDown", lifetime)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                pc.CInt["死亡毒素层数"]++;
                pc.CInt["死亡毒素连续触发开关"] = 0;
                int hpdowm = (int)(pc.MaxHP * 0.01f * pc.CInt["死亡毒素层数"]);
                if (this.Variable.ContainsKey("死亡毒素"))
                    this.Variable.Remove("死亡毒素");
                this.Variable.Add("死亡毒素", hpdowm);
                pc.Status.hp_skill -= (short)this.Variable["死亡毒素"];
            }

        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                if (pc.CInt["死亡毒素连续触发开关"] == 0)
                    pc.CInt["死亡毒素层数"] = 0;
                pc.Status.hp_skill += (short)this.Variable["死亡毒素"];
            }
        }
    }
}
