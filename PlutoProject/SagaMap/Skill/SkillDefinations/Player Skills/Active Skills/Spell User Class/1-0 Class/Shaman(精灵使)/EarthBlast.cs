﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Shaman
{
    public class EarthBlast : ISkill
    {
        bool MobUse;
        public EarthBlast()
        {
            this.MobUse = false;
        }
        public EarthBlast(bool MobUse)
        {
            this.MobUse = MobUse;
        }
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            if (MobUse)
                level = 3;
            switch (level)
            {
                case 1:
                    factor = 1.1f;
                    break;
                case 2:
                    factor = 1.8f;
                    break;
                case 3:
                    factor = 2.5f;
                    break;
                case 4:
                    factor = 3.3f;
                    break;
                case 5:
                    factor = 4.0f;
                    break;
            }

            List<Actor> actors = Manager.MapManager.Instance.GetMap(dActor.MapID).GetActorsArea(dActor, 100, true);
            List<Actor> affected = new List<Actor>();
            //取得有效Actor（即怪物）
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected.Add(i);
            }
            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Earth, factor / affected.Count);
        }
        #endregion
    }
}
