using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.X
{
    public class Iceroad : MobISkill
    {
        #region ISkill Members

        public void BeforeCast(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //向右的判定矩型
            short ox1 = 0;
            short oy1 = 100;
            short ox2 = 0;
            short oy2 = -100;
            short ox3 = 700;
            short oy3 = -100;
            short ox4 = 700;
            short oy4 = 100;
            //矩阵旋转
            double angel = map.DirChange(sActor.Dir) * Math.PI / 180;
            //double angel=sActor.Dir * Math.PI / 180;
            //Logger.ShowError(angel.ToString());
            short x1 = (short)(ox1 * Math.Cos(angel) - oy1 * Math.Sin(angel));
            short y1 = (short)(ox1 * Math.Sin(angel) + oy1 * Math.Cos(angel));
            short x2 = (short)(ox2 * Math.Cos(angel) - oy2 * Math.Sin(angel));
            short y2 = (short)(ox2 * Math.Sin(angel) + oy2 * Math.Cos(angel));
            short x3 = (short)(ox3 * Math.Cos(angel) - oy3 * Math.Sin(angel));
            short y3 = (short)(ox3 * Math.Sin(angel) + oy3 * Math.Cos(angel));
            short x4 = (short)(ox4 * Math.Cos(angel) - oy4 * Math.Sin(angel));
            short y4 = (short)(ox4 * Math.Sin(angel) + oy4 * Math.Cos(angel));
            //Logger.ShowError(x1.ToString() + "," + y1.ToString() + " " + 
            //  x2.ToString() + "," + y2.ToString() + " " + 
            //x3.ToString() + "," + y3.ToString() + " " + 
            //x4.ToString() + "," + y4.ToString());
            List<Actor> actors = map.GetRectAreaActors(
                (short)(x1 + sActor.X), (short)(y1 + sActor.Y),
                (short)(x2 + sActor.X), (short)(y2 + sActor.Y),
                (short)(x3 + sActor.X), (short)(y3 + sActor.Y),
                (short)(x4 + sActor.X), (short)(y4 + sActor.Y));

            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected.Add(i);
            }
            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Water, 3f);

            //SkillHandler.Instance.FixAttack(sActor, affected, args, SagaLib.Elements.Water, 980);

            foreach (Actor item in affected)
            {
                Skill.Additions.Global.Freeze freeze = new Freeze(args.skill, item, 5000);
                SkillHandler.ApplyAddition(item, freeze);
            }



            for (int i = 0; i < 2; i++)
            {
                short[] xy = map.GetRandomPosAroundActor2(sActor);
                xy[0] = SagaLib.Global.PosX8to16(16, map.Width);
                xy[1] = SagaLib.Global.PosY8to16(79, map.Height);
                if (sActor.Slave.Count < 2)
                    sActor.Slave.Add(map.SpawnMob(82000003, xy[0], xy[1], 2500, sActor));
                else if (sActor.Slave.Count < 4)
                    sActor.Slave.Add(map.SpawnMob(82000004, xy[0], xy[1], 2500, sActor));
                else if (sActor.Slave[0].Buff.Dead)
                    sActor.Slave[0] = map.SpawnMob(82000003, xy[0], xy[1], 2500, sActor);
                else if (sActor.Slave[1].Buff.Dead)
                    sActor.Slave[1] = map.SpawnMob(82000003, xy[0], xy[1], 2500, sActor);
                else if (sActor.Slave[2].Buff.Dead)
                    sActor.Slave[2] = map.SpawnMob(82000004, xy[0], xy[1], 2500, sActor);
                else if (sActor.Slave[3].Buff.Dead)
                    sActor.Slave[3] = map.SpawnMob(82000004, xy[0], xy[1], 2500, sActor);
            }
        }
        #endregion
    }
}
