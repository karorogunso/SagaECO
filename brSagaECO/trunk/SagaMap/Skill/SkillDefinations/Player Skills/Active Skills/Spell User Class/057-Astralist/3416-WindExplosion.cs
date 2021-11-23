using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.Astralist
{
    class WindExplosion : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            int TotalLv = 0;
            switch (level)
            {
                case 1:
                    factor = 3.0f;
                    break;
                case 2:
                    factor = 6.0f;
                    break;
                case 3:
                    factor = 9.0f;
                    break;
                case 4:
                    factor = 12.0f;
                    break;
                case 5:
                    factor = 15.0f;
                    break;
            }
            ActorPC Me = (ActorPC)sActor;
            if (Me.Skills2.ContainsKey(3261))//Caculate the factor according to skill FireStorm.
            {
                TotalLv = Me.Skills2[3261].BaseData.lv;
                switch (level)
                {
                    case 1:
                        factor += 0.5f;
                        break;
                    case 2:
                        factor += 1.0f;
                        break;
                    case 3:
                        factor += 1.5f;
                        break;
                    case 4:
                        factor += 2.0f;
                        break;
                    case 5:
                        factor += 2.5f;
                        break;

                }
            }
            if (Me.SkillsReserve.ContainsKey(3261))//Caculate the factor according to skill FireStorm.
            {
                TotalLv = Me.SkillsReserve[3261].BaseData.lv;
                switch (level)
                {
                    case 1:
                        factor += 0.5f;
                        break;
                    case 2:
                        factor += 1.0f;
                        break;
                    case 3:
                        factor += 1.5f;
                        break;
                    case 4:
                        factor += 2.0f;
                        break;
                    case 5:
                        factor += 2.5f;
                        break;

                }
            }
            if (Me.Skills2.ContainsKey(3264))//Caculate the factor according to skill FireStorm.
            {
                TotalLv = Me.Skills2[3264].BaseData.lv;
                switch (level)
                {
                    case 1:
                        factor += 0.5f;
                        break;
                    case 2:
                        factor += 1.0f;
                        break;
                    case 3:
                        factor += 1.5f;
                        break;
                    case 4:
                        factor += 2.0f;
                        break;
                    case 5:
                        factor += 2.5f;
                        break;

                }
            }
            if (Me.SkillsReserve.ContainsKey(3264))//Caculate the factor according to skill FireStorm.
            {
                TotalLv = Me.SkillsReserve[3264].BaseData.lv;
                switch (level)
                {
                    case 1:
                        factor += 0.5f;
                        break;
                    case 2:
                        factor += 1.0f;
                        break;
                    case 3:
                        factor += 1.5f;
                        break;
                    case 4:
                        factor += 2.0f;
                        break;
                    case 5:
                        factor += 2.5f;
                        break;

                }
            }
            ActorSkill actorS = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 350, null);
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected.Add(i);
            }
            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Wind, factor);
        }
        #endregion
    }
}
