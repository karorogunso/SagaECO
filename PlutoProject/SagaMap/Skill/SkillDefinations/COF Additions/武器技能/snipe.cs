using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Mob;


namespace SagaMap.Skill.SkillDefinations.Swordman
{
    public class Snipe:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(pc, dActor))
            {
                return 0;
            }
            else
            {
                return -14;
            }
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            /*if (sActor.Status.Additions.ContainsKey("狙击模式"))
            {
                sActor.Status.Additions["狙击模式"].AdditionEnd();
                sActor.Status.Additions.Remove("狙击模式");
                return;
            }*/
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            actor.MapID = sActor.MapID;
            actor.X = sActor.X;
            actor.Y = sActor.Y;
            Mob.MobAI ai = new SagaMap.Mob.MobAI(actor, true);
            List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(sActor.X, map.Width), SagaLib.Global.PosY16to8(sActor.Y, map.Height),
                SagaLib.Global.PosX16to8(dActor.X, map.Width), SagaLib.Global.PosY16to8(dActor.Y, map.Height));
            if (path.Count >= 2)
            {
                //根据现有路径推算一步
                int deltaX = path[path.Count - 1].x - path[path.Count - 2].x;
                int deltaY = path[path.Count - 1].y - path[path.Count - 2].y;
                deltaX = path[path.Count - 1].x + deltaX;
                deltaY = path[path.Count - 1].y + deltaY;
                MapNode node = new MapNode();
                node.x = (byte)deltaX;
                node.y = (byte)deltaY;
                path.Add(node);
            }
            if (path.Count == 1)
            {
                //根据现有路径推算一步
                int deltaX = path[path.Count - 1].x - SagaLib.Global.PosX16to8(sActor.X, map.Width);
                int deltaY = path[path.Count - 1].y - SagaLib.Global.PosY16to8(sActor.Y, map.Height);
                deltaX = path[path.Count - 1].x + deltaX;
                deltaY = path[path.Count - 1].y + deltaY;
                MapNode node = new MapNode();
                node.x = (byte)deltaX;
                node.y = (byte)deltaY;
                path.Add(node);
            }
            short[] pos2 = new short[2];
            List<Actor> affected = new List<Actor>();
            List<Actor> list;
            int count = -1;
            while (path.Count > count + 1)
            {
                pos2[0] = SagaLib.Global.PosX8to16(path[count + 1].x, map.Width);
                pos2[1] = SagaLib.Global.PosY8to16(path[count + 1].y, map.Height);
                //取得当前格子内的Actor
                list = map.GetActorsArea(pos2[0], pos2[1], 50);
                //筛选有效对象
                foreach (Actor i in list)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                        affected.Add(i);
                }
                count++;
            }

            if(affected.Contains(dActor))
            affected.Remove(dActor);

            SkillArg arg2 = new SkillArg();
            arg2 = args.Clone();
            arg2.skill.BaseData.id = 100;
            SkillHandler.Instance.PhysicalAttack(sActor, affected, arg2, SagaLib.Elements.Neutral, 3f);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg2, sActor, true);
            arg2.skill.BaseData.id = 20014;

            List<Actor> da = new List<Actor>();
            da.Add(dActor);

            SkillHandler.Instance.PhysicalAttack(sActor, da, args, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, 0, 5f, false, 0, false, 100, 0);
        }

        #endregion
    }
}
