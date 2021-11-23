using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 各種精靈的憤怒 [接續技能]
    /// </summary>
    public class MobElementRandcircleSeq : ISkill
    {
        private Elements e;
        public MobElementRandcircleSeq(Elements element)
        {
            e = element;
        }
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 3.0f;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            short[] xy = { (short)SagaLib.Global.Random.Next(sActor.X -200, sActor.X + 200), (short)SagaLib.Global.Random.Next(sActor.Y - 200, sActor.Y + 200) };
            List<Actor> actors = map.GetActorsArea(xy[0],xy[1], 100, null);
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    affected.Add(i);
                }
            }

            SkillHandler.Instance.MagicAttack(sActor, affected, args, e, factor);
            args.dActor = 0xffffffff;
            args.x = SagaLib.Global.PosX16to8(xy[0],map.Width);
            args.y = SagaLib.Global.PosY16to8(xy[1],map.Height);
        }
        #endregion
    }
}