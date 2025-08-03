
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Striker
{
    public class DogHpUp : ISkill
    {
        private float[] HP_AddRate = { 0f, 6f, 6, 8f, 8f, 10f };
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            ActorPet pet = SkillHandler.Instance.GetPet(sActor);
            if (pet != null)
            {
                if (SkillHandler.Instance.CheckMobType(pet, "ANIMAL"))
                {
                    active = true;
                }
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, pet, "DogHpUp", active);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(pet, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);

            //MaxHP
            int MaxHP = (int)(actor.MaxHP);
            if (skill.Variable.ContainsKey("DogHpUp_MaxHP"))
                skill.Variable.Remove("DogHpUp_MaxHP");
            skill.Variable.Add("DogHpUp_MaxHP", MaxHP);
            actor.MaxHP = (uint)(actor.MaxHP * (1 + HP_AddRate[skill.skill.Level]));
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.MaxHP -= (uint)skill.Variable["DogHpUp_MaxHP"];
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
        }
        #endregion
    }
}

