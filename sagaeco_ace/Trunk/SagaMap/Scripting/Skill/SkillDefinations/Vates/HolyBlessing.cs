using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Vates
{
    public class HolyBlessing : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("HerosProtectionCD"))
            {
                Network.Client.MapClient.FromActorPC(pc).SendSystemMessage(string.Format("该技能正在单独冷却中，剩余时间：{0}毫秒", pc.Status.Additions["HerosProtectionCD"].RestLifeTime));
                return -99;
            }
            else
                return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int life = 0;
            life = 36000;
            DefaultBuff skill2 = new DefaultBuff(args.skill, sActor, "HerosProtectionCD", 30000);
            skill2.OnAdditionStart += this.StartEventHandler;
            skill2.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill2);
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "HolyBlessing", life);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            short maxatk1 = (short)(actor.Status.max_atk1 * (float)(0.02f + (skill.skill.Level * 0.02f)));
            short maxatk2 = (short)(actor.Status.max_atk2 * (float)(0.02f + (skill.skill.Level * 0.02f)));
            short maxatk3 = (short)(actor.Status.max_atk3 * (float)(0.02f + (skill.skill.Level * 0.02f)));
            short minatk1 = (short)(actor.Status.min_atk1 * (float)(0.02f + (skill.skill.Level * 0.02f)));
            short minatk2 = (short)(actor.Status.min_atk2 * (float)(0.02f + (skill.skill.Level * 0.02f)));
            short minatk3 = (short)(actor.Status.min_atk3 * (float)(0.02f + (skill.skill.Level * 0.02f)));
            short maxmatk = (short)(actor.Status.max_matk * (float)(0.02f + (skill.skill.Level * 0.02f)));
            short minmatk = (short)(actor.Status.min_matk * (float)(0.02f + (skill.skill.Level * 0.02f)));

            if (skill.Variable.ContainsKey("HolyBlessingMaxAtk1"))
                skill.Variable.Remove("HolyBlessingMaxAtk1");
            skill.Variable.Add("HolyBlessingMaxAtk1", maxatk1);
            actor.Status.max_atk1_skill += (short)maxatk1;

            if (skill.Variable.ContainsKey("HolyBlessingMaxAtk2"))
                skill.Variable.Remove("HolyBlessingMaxAtk2");
            skill.Variable.Add("HolyBlessingMaxAtk2", maxatk2);
            actor.Status.max_atk2_skill += (short)maxatk2;

            if (skill.Variable.ContainsKey("HolyBlessingMaxAtk3"))
                skill.Variable.Remove("HolyBlessingMaxAtk3");
            skill.Variable.Add("HolyBlessingMaxAtk3", maxatk3);
            actor.Status.max_atk3_skill += (short)maxatk3;

            if (skill.Variable.ContainsKey("HolyBlessingMinAtk1"))
                skill.Variable.Remove("HolyBlessingMinAtk1");
            skill.Variable.Add("HolyBlessingMinAtk1", minatk1);
            actor.Status.min_atk1_skill += (short)minatk1;

            if (skill.Variable.ContainsKey("HolyBlessingMinAtk2"))
                skill.Variable.Remove("HolyBlessingMinAtk2");
            skill.Variable.Add("HolyBlessingMinAtk2", minatk2);
            actor.Status.min_atk2_skill += (short)minatk2;

            if (skill.Variable.ContainsKey("HolyBlessingMinAtk3"))
                skill.Variable.Remove("HolyBlessingMinAtk3");
            skill.Variable.Add("HolyBlessingMinAtk3", minatk3);
            actor.Status.min_atk3_skill += (short)minatk3;

            if (skill.Variable.ContainsKey("HolyBlessingMinMAtk"))
                skill.Variable.Remove("HolyBlessingMinMAtk");
            skill.Variable.Add("HolyBlessingMinMAtk", minmatk);
            actor.Status.min_matk_skill += (short)minmatk;

            if (skill.Variable.ContainsKey("HolyBlessingMaxMAtk"))
                skill.Variable.Remove("HolyBlessingMaxMAtk");
            skill.Variable.Add("HolyBlessingMaxMAtk", maxmatk);
            actor.Status.max_matk_skill += (short)maxmatk;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.max_atk1_skill -= (short)skill.Variable["HolyBlessingMaxAtk1"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["HolyBlessingMaxAtk2"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["HolyBlessingMaxAtk3"];
            actor.Status.min_atk1_skill -= (short)skill.Variable["HolyBlessingMaxAtk1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["HolyBlessingMinAtk2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["HolyBlessingMinAtk3"];
            actor.Status.min_matk_skill -= (short)skill.Variable["HolyBlessingMinMAtk"];
            actor.Status.max_matk_skill -= (short)skill.Variable["HolyBlessingMaxMAtk"];
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        public void RemoveAddition(Actor actor, String additionName)
        {
            if (actor.Status.Additions.ContainsKey(additionName))
            {
                Addition addition = actor.Status.Additions[additionName];
                actor.Status.Additions.Remove(additionName);
                if (addition.Activated)
                {
                    addition.AdditionEnd();
                }
                addition.Activated = false;
            }
        }
     
    }
}