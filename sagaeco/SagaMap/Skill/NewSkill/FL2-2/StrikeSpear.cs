
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;

using SagaLib;

namespace SagaMap.Skill.SkillDefinations.Knight
{
    /// <summary>
    /// 比卡利之槍（ストライクスピア）
    /// </summary>
    public class StrikeSpear : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 2.0f+0.4f*level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //向右的判定矩型
            short ox1 = 0;
            short oy1 = 100;
            short ox2 = 0;
            short oy2 = -100;
            short ox3 = 400;
            short oy3 = -100;
            short ox4 = 400;
            short oy4 = 100;
            //矩阵旋转
            double angel = map.DirChange(sActor.Dir) * Math.PI / 180;
            short x1 = (short)(ox1 * Math.Cos(angel) - oy1 * Math.Sin(angel));
            short y1 = (short)(ox1 * Math.Sin(angel) + oy1 * Math.Cos(angel));
            short x2 = (short)(ox2 * Math.Cos(angel) - oy2 * Math.Sin(angel));
            short y2 = (short)(ox2 * Math.Sin(angel) + oy2 * Math.Cos(angel));
            short x3 = (short)(ox3 * Math.Cos(angel) - oy3 * Math.Sin(angel));
            short y3 = (short)(ox3 * Math.Sin(angel) + oy3 * Math.Cos(angel));
            short x4 = (short)(ox4 * Math.Cos(angel) - oy4 * Math.Sin(angel));
            short y4 = (short)(ox4 * Math.Sin(angel) + oy4 * Math.Cos(angel));

            List<Actor> actors = map.GetRectAreaActors(
                (short)(x1 + sActor.X), (short)(y1 + sActor.Y),
                (short)(x2 + sActor.X), (short)(y2 + sActor.Y),
                (short)(x3 + sActor.X), (short)(y3 + sActor.Y),
                (short)(x4 + sActor.X), (short)(y4 + sActor.Y));
            //Logger.ShowError(actors.Count.ToString());
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected.Add(i);
            }

            
            SkillHandler.Instance.PhysicalAttack(sActor, affected, args, SagaLib.Elements.Neutral, factor);

        }
    }
}