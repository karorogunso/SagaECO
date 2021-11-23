using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S40020 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
                return 0;
            return -5;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Elements ele = getele(args);
            uint effectid = getsactoreffectcri(args);
            uint sactoreffectcri = getsactoreffect(args);

            args.argType = SkillArg.ArgType.Active;
            args.delayRate = (1000 - sActor.Status.aspd*1.2f) * 0.001f;

            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, ele, 7f);
            SkillHandler.Instance.ShowEffectOnActor(dActor, effectid);
            SkillHandler.Instance.ShowEffectOnActor(sActor, sactoreffectcri);

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> targets = map.GetActorsArea(dActor, 200, false);
            List<Actor> dactors = new List<Actor>();
            foreach (var item in targets)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    dactors.Add(item);
                }
            }
            if (dactors.Count < 1) return;
            foreach (var item in dactors)
            {
                SkillHandler.Instance.DoDamage(true, sActor, item, args, SkillHandler.DefType.Def, ele, 7, 7f);
                SkillHandler.Instance.ShowEffectOnActor(item, sactoreffectcri);
            }

            sActor.SP += (uint)(sActor.MaxSP * 0.01f);
            if (sActor.SP > sActor.MaxSP)
                sActor.SP = sActor.MaxSP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
        Elements getele(SkillArg args)
        {
            Elements ele = Elements.Fire;
            if (args.skill.ID == 18020)
                ele = Elements.Fire;
            if (args.skill.ID == 18021)
                ele = Elements.Water;
            if (args.skill.ID == 18022)
                ele = Elements.Wind;
            if (args.skill.ID == 18023)
                ele = Elements.Earth;
            if (args.skill.ID == 18222)
                ele = Elements.Holy;
            return ele;
        }
        uint getsactoreffectcri(SkillArg args)
        {
            uint id = 5284;
            if (args.skill.ID == 18020)
                id = 5205;
            if (args.skill.ID == 18021)
                id = 5284;
            if (args.skill.ID == 18022)
                id = 5213;
            if (args.skill.ID == 18023)
                id = 5144;
            if (args.skill.ID == 18222)
                id = 5288;
            return id;
        }
        uint getsactoreffect(SkillArg args)
        {
            uint id = 5284;
            if (args.skill.ID == 18020)
                id = 0;
            if (args.skill.ID == 18021)
                id = 0;
            if (args.skill.ID == 18022)
                id = 4098;
            if (args.skill.ID == 18023)
                id = 7962;
            if (args.skill.ID == 18222)
                id = 0;
            return id;
        }
        #endregion
    }
}
