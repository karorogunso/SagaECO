using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Astralist
{
    /// <summary>
    /// トランスボディ
    /// </summary>
    public class TranceBody : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public SkillArg arg = new SkillArg();
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 150000 + 30000 *level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "TranceBody", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);

        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.三转元素身体属性赋予 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.三转元素身体属性赋予 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        public void Passive(Actor sActor, Actor dActor, Elements element, int elementValue, int damage)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                SagaDB.Skill.Skill skill2 = SagaDB.Skill.SkillFactory.Instance.GetSkill(3377, pc.Skills3[3377].BaseData.lv);
                SkillArg arg = new SkillArg();
                arg.sActor = sActor.ActorID;
                arg.dActor = dActor.ActorID;
                arg.skill = skill2;
                arg.x = 0;
                arg.y = 0;
                arg.argType = SkillArg.ArgType.Cast;

                
                int lifetime = 150000 + 30000 * pc.Skills3[3377].BaseData.lv;
                if (element == Elements.Earth)
                {
                    DefaultBuff skill = new DefaultBuff(arg.skill, sActor, "TranceBody_Earth", lifetime);
                    skill.OnAdditionStart += this.StartEventHandlerEarth;
                    skill.OnAdditionEnd += this.EndEventHandlerEarth;
                    SkillHandler.ApplyAddition(sActor, skill);
                }
                if (element == Elements.Water)
                {
                    DefaultBuff skill = new DefaultBuff(arg.skill, sActor, "TranceBody_Water", lifetime);
                    skill.OnAdditionStart += this.StartEventHandlerWater;
                    skill.OnAdditionEnd += this.EndEventHandlerWater;
                    SkillHandler.ApplyAddition(sActor, skill);
                }
                if (element == Elements.Fire)
                {
                    DefaultBuff skill = new DefaultBuff(null, sActor, "TranceBody_Fire", lifetime);
                    skill.OnAdditionStart += this.StartEventHandlerFire;
                    skill.OnAdditionEnd += this.EndEventHandlerFire;
                    SkillHandler.ApplyAddition(sActor, null);
                }
                if (element == Elements.Holy)
                {
                    DefaultBuff skill = new DefaultBuff(arg.skill, sActor, "TranceBody_Holy", lifetime);
                    skill.OnAdditionStart += this.StartEventHandlerHoly;
                    skill.OnAdditionEnd += this.EndEventHandlerHoly;
                    SkillHandler.ApplyAddition(sActor, skill);
                }
            }
        }
        void StartEventHandlerEarth(Actor actor, DefaultBuff skill)
        {
            ActorPC pc = (ActorPC)actor;
            int element_up_e = 15 + 5 * pc.Skills3[3377].BaseData.lv;
            if (pc.Skills.ContainsKey(3042))
            {
                element_up_e += 5 * pc.Skills[3042].BaseData.lv;
            }
            if (skill.Variable.ContainsKey("TranceBody_Earth_Add"))
                skill.Variable.Remove("TranceBody_Earth_Add");
            skill.Variable.Add("TranceBody_Earth_Add", element_up_e);
            actor.Status.elements_skill[Elements.Earth] += element_up_e;
        }
        void EndEventHandlerEarth(Actor actor, DefaultBuff skill)
        {
            actor.Status.elements_skill[Elements.Earth] -= skill.Variable["TranceBody_Earth_Add"];
        }

        void StartEventHandlerWater(Actor actor, DefaultBuff skill)
        {
            ActorPC pc = (ActorPC)actor;
            int element_up_w = 15 + 5 * pc.Skills3[3377].BaseData.lv;
            if (pc.Skills.ContainsKey(3030))
            {
                element_up_w += 5 * pc.Skills[3030].BaseData.lv;
            }
            if (skill.Variable.ContainsKey("TranceBody_Water_Add"))
                skill.Variable.Remove("TranceBody_Water_Add");
            skill.Variable.Add("TranceBody_Water_Add", element_up_w);
            actor.Status.elements_skill[Elements.Water] += element_up_w;
        }
        void EndEventHandlerWater(Actor actor, DefaultBuff skill)
        {
            actor.Status.elements_skill[Elements.Water] -= skill.Variable["TranceBody_Water_Add"];
        }

        void StartEventHandlerFire(Actor actor, DefaultBuff skill)
        {
            ActorPC pc = (ActorPC)actor;
            int element_up_f = 15 + 5 * pc.Skills3[3377].BaseData.lv;
            if (pc.Skills.ContainsKey(3007))
            {
                element_up_f += 5 * pc.Skills[3007].BaseData.lv;
            }
            if (skill.Variable.ContainsKey("TranceBody_Fire_Add"))
                skill.Variable.Remove("TranceBody_Fire_Add");
            skill.Variable.Add("TranceBody_Fire_Add", element_up_f);
            actor.Status.elements_skill[Elements.Fire] += element_up_f;
        }
        void EndEventHandlerFire(Actor actor, DefaultBuff skill)
        {
            actor.Status.elements_skill[Elements.Fire] -= skill.Variable["TranceBody_Fire_Add"];
        }

        void StartEventHandlerHoly(Actor actor, DefaultBuff skill)
        {
            ActorPC pc = (ActorPC)actor;
            int element_up_h = 15 + 5 * pc.Skills3[3377].BaseData.lv;
            if (pc.Skills.ContainsKey(3018))
            {
                element_up_h += 5 * pc.Skills[3018].BaseData.lv;
            }
            if (skill.Variable.ContainsKey("TranceBody_Holy_Add"))
                skill.Variable.Remove("TranceBody_Holy_Add");
            skill.Variable.Add("TranceBody_Holy_Add", element_up_h);
            actor.Status.elements_skill[Elements.Holy] += element_up_h;
        }
        void EndEventHandlerHoly(Actor actor, DefaultBuff skill)
        {
            actor.Status.elements_skill[Elements.Holy] -= skill.Variable["TranceBody_Holy_Add"];
        }
    }
}
