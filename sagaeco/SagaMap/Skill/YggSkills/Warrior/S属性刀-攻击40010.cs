using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S40010 : ISkill
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
            uint effectid = geteffect(args);
            uint sactoreffect = getsactoreffect(args);

            args.argType = SkillArg.ArgType.Active;
            args.delayRate = (1000 - sActor.Status.aspd * 1.2f) * 0.001f;

            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, ele, 4.5f);
            SkillHandler.Instance.ShowEffectOnActor(dActor, effectid);
            SkillHandler.Instance.ShowEffectOnActor(sActor, sactoreffect);

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
                SkillHandler.Instance.DoDamage(true, sActor, item, args, SkillHandler.DefType.Def, ele, 7, 4.5f);
                SkillHandler.Instance.ShowEffectOnActor(item, effectid);
            }

            sActor.SP += (uint)(sActor.MaxSP * 0.01f);
            if (sActor.SP > sActor.MaxSP)
                sActor.SP = sActor.MaxSP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }

        Elements getele(SkillArg args)
        {
            Elements ele = Elements.Fire;
            if (args.skill.ID == 18010)
                ele = Elements.Fire;
            if (args.skill.ID == 18011)
                ele = Elements.Water;
            if (args.skill.ID == 18012)
                ele = Elements.Wind;
            if (args.skill.ID == 18013)
                ele = Elements.Earth;
            if (args.skill.ID == 18212)
                ele = Elements.Holy;
            return ele;
        }
        uint geteffect(SkillArg args)
        {
            uint id = 5284;
            if (args.skill.ID == 18010)
                id = 8031;
            if (args.skill.ID == 18011)
                id = 5609;
            if (args.skill.ID == 18012)
                id = 8057;
            if (args.skill.ID == 18013)
                id = 4094;
            if (args.skill.ID == 18212)
                id = 8050;
            return id;
        }
        uint getsactoreffect(SkillArg args)
        {
            uint id = 5284;
            if (args.skill.ID == 18010)
                id = 0;
            if (args.skill.ID == 18011)
                id = 0;
            if (args.skill.ID == 18012)
                id = 4098;
            if (args.skill.ID == 18013)
                id = 7962;
            if (args.skill.ID == 18212)
                id = 0;
            return id;
        }
        #endregion
    }
}
