using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations.X
{
    class BlackHole : MobISkill
    {
        #region ISkill Members

        public void BeforeCast(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 900, false, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                    realAffected.Add(act);
            }
            foreach (Actor a in realAffected)
            {
                short[] pos  = new short[2]{sActor.X,sActor.Y};
                map.MoveActor(Map.MOVE_TYPE.START, a, pos, 1000, 1000, true, MoveType.QUICKEN);
                Skill.Additions.Global.MoveSpeedDown 钝足 = new MoveSpeedDown(args.skill, a, 4000);
                SkillHandler.ApplyAddition(a, 钝足);
            }
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 600, false, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                    realAffected.Add(act);
            }
            SkillHandler.Instance.MagicAttack(sActor,realAffected,args, Elements.Neutral,3f);


            
            //if (sActor.Slave.Count <= 9)
            {
                for (int i = 0; i < 3; i++)
                {
                    short[] xy = map.GetRandomPosAroundActor2(sActor);
                    sActor.Slave.Add(map.SpawnMob(82000000, xy[0], xy[1], 2500, sActor));
                }
                
            }
        }
        #endregion
    }
}
