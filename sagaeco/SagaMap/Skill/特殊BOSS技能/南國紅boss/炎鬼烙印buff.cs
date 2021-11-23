using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class 炎鬼烙印 : DefaultBuff
    {
        public 炎鬼烙印(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int damage)
            : base(skill, sActor, dActor, "炎鬼烙印", lifetime, 1000, damage)
        {
            
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.OnUpdate2 += this.TimerUpdate;

        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                if (this.Variable.ContainsKey("炎鬼缠身STR"))
                    this.Variable.Remove("炎鬼缠身STR");
                this.Variable["炎鬼缠身STR"] = (int)(pc.Str * 0.1f);
                if (this.Variable.ContainsKey("炎鬼缠身AGI"))
                    this.Variable.Remove("炎鬼缠身AGI");
                this.Variable["炎鬼缠身AGI"] = (int)(pc.Agi * 0.1f);
                if (this.Variable.ContainsKey("炎鬼缠身INT"))
                    this.Variable.Remove("炎鬼缠身INT");
                this.Variable["炎鬼缠身INT"] = (int)(pc.Int * 0.1f);
                if (this.Variable.ContainsKey("炎鬼缠身MAG"))
                    this.Variable.Remove("炎鬼缠身MAG");
                this.Variable["炎鬼缠身MAG"] = (int)(pc.Mag * 0.1f);
                if (this.Variable.ContainsKey("炎鬼缠身VIT"))
                    this.Variable.Remove("炎鬼缠身VIT");
                this.Variable["炎鬼缠身VIT"] = (int)(pc.Vit * 0.1f);
                if (this.Variable.ContainsKey("炎鬼缠身DEX"))
                    this.Variable.Remove("炎鬼缠身DEX");
                this.Variable["炎鬼缠身DEX"] = (int)(pc.Dex * 0.1f);

                pc.Status.str_skill -= (short)this.Variable["炎鬼缠身STR"];
                pc.Status.agi_skill -= (short)this.Variable["炎鬼缠身AGI"];
                pc.Status.int_skill -= (short)this.Variable["炎鬼缠身INT"];
                pc.Status.mag_skill -= (short)this.Variable["炎鬼缠身MAG"];
                pc.Status.vit_skill -= (short)this.Variable["炎鬼缠身VIT"];
                pc.Status.dex_skill -= (short)this.Variable["炎鬼缠身DEX"];
            }
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                pc.Status.str_skill += (short)this.Variable["炎鬼缠身STR"];
                pc.Status.agi_skill += (short)this.Variable["炎鬼缠身AGI"];
                pc.Status.int_skill += (short)this.Variable["炎鬼缠身INT"];
                pc.Status.mag_skill += (short)this.Variable["炎鬼缠身MAG"];
                pc.Status.vit_skill += (short)this.Variable["炎鬼缠身VIT"];
                pc.Status.dex_skill += (short)this.Variable["炎鬼缠身DEX"];
            }
        }

        void TimerUpdate(Actor sActor,Actor dActor, DefaultBuff skill, SkillArg arg, int damage)
        {
        }
    }
}
