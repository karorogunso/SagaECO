using SagaDB.Actor;
using SagaMap.Mob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaMap.Skill.SkillDefinations.Guardian
{
    class LightOfTheDarkness : ISkill
    {
        public Dictionary<SagaMap.Skill.SkillHandler.ActorDirection, List<int>> range = new Dictionary<SkillHandler.ActorDirection, List<int>>();
        #region Init
        //public LightOfTheDarkness()
        //{
        //    //建立List
        //    for (int i = 0; i < 8; i++)
        //    {
        //        range.Add((SkillHandler.ActorDirection)i, new List<int>());
        //    }
        //    //塞入內容
        //    #region RangePos
        //    //North
        //    range[SkillHandler.ActorDirection.North].Add(SkillHandler.Instance.CalcPosHashCode(0, 1, 6));
        //    range[SkillHandler.ActorDirection.North].Add(SkillHandler.Instance.CalcPosHashCode(0, 2, 6));
        //    range[SkillHandler.ActorDirection.North].Add(SkillHandler.Instance.CalcPosHashCode(0, 3, 6));
        //    range[SkillHandler.ActorDirection.North].Add(SkillHandler.Instance.CalcPosHashCode(0, 4, 6));
        //    range[SkillHandler.ActorDirection.North].Add(SkillHandler.Instance.CalcPosHashCode(0, 5, 6));
        //    range[SkillHandler.ActorDirection.North].Add(SkillHandler.Instance.CalcPosHashCode(1, 1, 6));
        //    range[SkillHandler.ActorDirection.North].Add(SkillHandler.Instance.CalcPosHashCode(1, 2, 6));
        //    range[SkillHandler.ActorDirection.North].Add(SkillHandler.Instance.CalcPosHashCode(1, 3, 6));
        //    range[SkillHandler.ActorDirection.North].Add(SkillHandler.Instance.CalcPosHashCode(1, 4, 6));
        //    range[SkillHandler.ActorDirection.North].Add(SkillHandler.Instance.CalcPosHashCode(1, 5, 6));
        //    range[SkillHandler.ActorDirection.North].Add(SkillHandler.Instance.CalcPosHashCode(-1, 1, 6));
        //    range[SkillHandler.ActorDirection.North].Add(SkillHandler.Instance.CalcPosHashCode(-1, 2, 6));
        //    range[SkillHandler.ActorDirection.North].Add(SkillHandler.Instance.CalcPosHashCode(-1, 3, 6));
        //    range[SkillHandler.ActorDirection.North].Add(SkillHandler.Instance.CalcPosHashCode(-1, 4, 6));
        //    range[SkillHandler.ActorDirection.North].Add(SkillHandler.Instance.CalcPosHashCode(-1, 5, 6));
        //    //NorthEast
        //    range[SkillHandler.ActorDirection.NorthEast].Add(SkillHandler.Instance.CalcPosHashCode(0, 1, 6));
        //    range[SkillHandler.ActorDirection.NorthEast].Add(SkillHandler.Instance.CalcPosHashCode(1, 2, 6));
        //    range[SkillHandler.ActorDirection.NorthEast].Add(SkillHandler.Instance.CalcPosHashCode(2, 3, 6));
        //    range[SkillHandler.ActorDirection.NorthEast].Add(SkillHandler.Instance.CalcPosHashCode(3, 4, 6));
        //    range[SkillHandler.ActorDirection.NorthEast].Add(SkillHandler.Instance.CalcPosHashCode(4, 5, 6));
        //    range[SkillHandler.ActorDirection.NorthEast].Add(SkillHandler.Instance.CalcPosHashCode(1, 1, 6));
        //    range[SkillHandler.ActorDirection.NorthEast].Add(SkillHandler.Instance.CalcPosHashCode(2, 2, 6));
        //    range[SkillHandler.ActorDirection.NorthEast].Add(SkillHandler.Instance.CalcPosHashCode(3, 3, 6));
        //    range[SkillHandler.ActorDirection.NorthEast].Add(SkillHandler.Instance.CalcPosHashCode(4, 4, 6));
        //    range[SkillHandler.ActorDirection.NorthEast].Add(SkillHandler.Instance.CalcPosHashCode(5, 5, 6));
        //    range[SkillHandler.ActorDirection.NorthEast].Add(SkillHandler.Instance.CalcPosHashCode(1, 0, 6));
        //    range[SkillHandler.ActorDirection.NorthEast].Add(SkillHandler.Instance.CalcPosHashCode(2, 1, 6));
        //    range[SkillHandler.ActorDirection.NorthEast].Add(SkillHandler.Instance.CalcPosHashCode(3, 2, 6));
        //    range[SkillHandler.ActorDirection.NorthEast].Add(SkillHandler.Instance.CalcPosHashCode(4, 3, 6));
        //    range[SkillHandler.ActorDirection.NorthEast].Add(SkillHandler.Instance.CalcPosHashCode(5, 4, 6));
        //    //East
        //    range[SkillHandler.ActorDirection.East].Add(SkillHandler.Instance.CalcPosHashCode(1, 0, 6));
        //    range[SkillHandler.ActorDirection.East].Add(SkillHandler.Instance.CalcPosHashCode(2, 0, 6));
        //    range[SkillHandler.ActorDirection.East].Add(SkillHandler.Instance.CalcPosHashCode(3, 0, 6));
        //    range[SkillHandler.ActorDirection.East].Add(SkillHandler.Instance.CalcPosHashCode(4, 0, 6));
        //    range[SkillHandler.ActorDirection.East].Add(SkillHandler.Instance.CalcPosHashCode(5, 0, 6));
        //    range[SkillHandler.ActorDirection.East].Add(SkillHandler.Instance.CalcPosHashCode(1, 1, 6));
        //    range[SkillHandler.ActorDirection.East].Add(SkillHandler.Instance.CalcPosHashCode(2, 1, 6));
        //    range[SkillHandler.ActorDirection.East].Add(SkillHandler.Instance.CalcPosHashCode(3, 1, 6));
        //    range[SkillHandler.ActorDirection.East].Add(SkillHandler.Instance.CalcPosHashCode(4, 1, 6));
        //    range[SkillHandler.ActorDirection.East].Add(SkillHandler.Instance.CalcPosHashCode(5, 1, 6));
        //    range[SkillHandler.ActorDirection.East].Add(SkillHandler.Instance.CalcPosHashCode(1, -1, 6));
        //    range[SkillHandler.ActorDirection.East].Add(SkillHandler.Instance.CalcPosHashCode(2, -1, 6));
        //    range[SkillHandler.ActorDirection.East].Add(SkillHandler.Instance.CalcPosHashCode(3, -1, 6));
        //    range[SkillHandler.ActorDirection.East].Add(SkillHandler.Instance.CalcPosHashCode(4, -1, 6));
        //    range[SkillHandler.ActorDirection.East].Add(SkillHandler.Instance.CalcPosHashCode(5, -1, 6));
        //    //SouthEast
        //    range[SkillHandler.ActorDirection.SouthEast].Add(SkillHandler.Instance.CalcPosHashCode(1, 0, 6));
        //    range[SkillHandler.ActorDirection.SouthEast].Add(SkillHandler.Instance.CalcPosHashCode(2, -1, 6));
        //    range[SkillHandler.ActorDirection.SouthEast].Add(SkillHandler.Instance.CalcPosHashCode(3, -2, 6));
        //    range[SkillHandler.ActorDirection.SouthEast].Add(SkillHandler.Instance.CalcPosHashCode(4, -3, 6));
        //    range[SkillHandler.ActorDirection.SouthEast].Add(SkillHandler.Instance.CalcPosHashCode(5, -4, 6));
        //    range[SkillHandler.ActorDirection.SouthEast].Add(SkillHandler.Instance.CalcPosHashCode(1, -1, 6));
        //    range[SkillHandler.ActorDirection.SouthEast].Add(SkillHandler.Instance.CalcPosHashCode(2, -2, 6));
        //    range[SkillHandler.ActorDirection.SouthEast].Add(SkillHandler.Instance.CalcPosHashCode(3, -3, 6));
        //    range[SkillHandler.ActorDirection.SouthEast].Add(SkillHandler.Instance.CalcPosHashCode(4, -4, 6));
        //    range[SkillHandler.ActorDirection.SouthEast].Add(SkillHandler.Instance.CalcPosHashCode(4, -5, 6));
        //    range[SkillHandler.ActorDirection.SouthEast].Add(SkillHandler.Instance.CalcPosHashCode(0, -1, 6));
        //    range[SkillHandler.ActorDirection.SouthEast].Add(SkillHandler.Instance.CalcPosHashCode(1, -2, 6));
        //    range[SkillHandler.ActorDirection.SouthEast].Add(SkillHandler.Instance.CalcPosHashCode(2, -3, 6));
        //    range[SkillHandler.ActorDirection.SouthEast].Add(SkillHandler.Instance.CalcPosHashCode(3, -4, 6));
        //    range[SkillHandler.ActorDirection.SouthEast].Add(SkillHandler.Instance.CalcPosHashCode(4, -5, 6));
        //    //South
        //    range[SkillHandler.ActorDirection.South].Add(SkillHandler.Instance.CalcPosHashCode(0, -1, 6));
        //    range[SkillHandler.ActorDirection.South].Add(SkillHandler.Instance.CalcPosHashCode(0, -2, 6));
        //    range[SkillHandler.ActorDirection.South].Add(SkillHandler.Instance.CalcPosHashCode(0, -3, 6));
        //    range[SkillHandler.ActorDirection.South].Add(SkillHandler.Instance.CalcPosHashCode(0, -4, 6));
        //    range[SkillHandler.ActorDirection.South].Add(SkillHandler.Instance.CalcPosHashCode(0, -5, 6));
        //    range[SkillHandler.ActorDirection.South].Add(SkillHandler.Instance.CalcPosHashCode(1, -1, 6));
        //    range[SkillHandler.ActorDirection.South].Add(SkillHandler.Instance.CalcPosHashCode(1, -2, 6));
        //    range[SkillHandler.ActorDirection.South].Add(SkillHandler.Instance.CalcPosHashCode(1, -3, 6));
        //    range[SkillHandler.ActorDirection.South].Add(SkillHandler.Instance.CalcPosHashCode(1, -4, 6));
        //    range[SkillHandler.ActorDirection.South].Add(SkillHandler.Instance.CalcPosHashCode(1, -5, 6));
        //    range[SkillHandler.ActorDirection.South].Add(SkillHandler.Instance.CalcPosHashCode(-1, -1, 6));
        //    range[SkillHandler.ActorDirection.South].Add(SkillHandler.Instance.CalcPosHashCode(-1, -2, 6));
        //    range[SkillHandler.ActorDirection.South].Add(SkillHandler.Instance.CalcPosHashCode(-1, -3, 6));
        //    range[SkillHandler.ActorDirection.South].Add(SkillHandler.Instance.CalcPosHashCode(-1, -4, 6));
        //    range[SkillHandler.ActorDirection.South].Add(SkillHandler.Instance.CalcPosHashCode(-1, -5, 6));
        //    //SouthWest
        //    range[SkillHandler.ActorDirection.SouthWest].Add(SkillHandler.Instance.CalcPosHashCode(-1, 0, 6));
        //    range[SkillHandler.ActorDirection.SouthWest].Add(SkillHandler.Instance.CalcPosHashCode(-2, -1, 6));
        //    range[SkillHandler.ActorDirection.SouthWest].Add(SkillHandler.Instance.CalcPosHashCode(-3, -2, 6));
        //    range[SkillHandler.ActorDirection.SouthWest].Add(SkillHandler.Instance.CalcPosHashCode(-4, -3, 6));
        //    range[SkillHandler.ActorDirection.SouthWest].Add(SkillHandler.Instance.CalcPosHashCode(-5, -4, 6));
        //    range[SkillHandler.ActorDirection.SouthWest].Add(SkillHandler.Instance.CalcPosHashCode(-5, -5, 6));
        //    range[SkillHandler.ActorDirection.SouthWest].Add(SkillHandler.Instance.CalcPosHashCode(-1, -1, 6));
        //    range[SkillHandler.ActorDirection.SouthWest].Add(SkillHandler.Instance.CalcPosHashCode(-2, -2, 6));
        //    range[SkillHandler.ActorDirection.SouthWest].Add(SkillHandler.Instance.CalcPosHashCode(-3, -3, 6));
        //    range[SkillHandler.ActorDirection.SouthWest].Add(SkillHandler.Instance.CalcPosHashCode(-4, -4, 6));
        //    range[SkillHandler.ActorDirection.SouthWest].Add(SkillHandler.Instance.CalcPosHashCode(-5, -5, 6));
        //    range[SkillHandler.ActorDirection.SouthWest].Add(SkillHandler.Instance.CalcPosHashCode(0, -1, 6));
        //    range[SkillHandler.ActorDirection.SouthWest].Add(SkillHandler.Instance.CalcPosHashCode(-1, -2, 6));
        //    range[SkillHandler.ActorDirection.SouthWest].Add(SkillHandler.Instance.CalcPosHashCode(-2, -3, 6));
        //    range[SkillHandler.ActorDirection.SouthWest].Add(SkillHandler.Instance.CalcPosHashCode(-3, -4, 6));
        //    //West
        //    range[SkillHandler.ActorDirection.West].Add(SkillHandler.Instance.CalcPosHashCode(-1, 0, 6));
        //    range[SkillHandler.ActorDirection.West].Add(SkillHandler.Instance.CalcPosHashCode(-2, 0, 6));
        //    range[SkillHandler.ActorDirection.West].Add(SkillHandler.Instance.CalcPosHashCode(-3, 0, 6));
        //    range[SkillHandler.ActorDirection.West].Add(SkillHandler.Instance.CalcPosHashCode(-4, 0, 6));
        //    range[SkillHandler.ActorDirection.West].Add(SkillHandler.Instance.CalcPosHashCode(-5, 0, 6));
        //    range[SkillHandler.ActorDirection.West].Add(SkillHandler.Instance.CalcPosHashCode(-1, 1, 6));
        //    range[SkillHandler.ActorDirection.West].Add(SkillHandler.Instance.CalcPosHashCode(-2, 1, 6));
        //    range[SkillHandler.ActorDirection.West].Add(SkillHandler.Instance.CalcPosHashCode(-3, 1, 6));
        //    range[SkillHandler.ActorDirection.West].Add(SkillHandler.Instance.CalcPosHashCode(-4, 1, 6));
        //    range[SkillHandler.ActorDirection.West].Add(SkillHandler.Instance.CalcPosHashCode(-5, 1, 6));
        //    range[SkillHandler.ActorDirection.West].Add(SkillHandler.Instance.CalcPosHashCode(-1, -1, 6));
        //    range[SkillHandler.ActorDirection.West].Add(SkillHandler.Instance.CalcPosHashCode(-2, -1, 6));
        //    range[SkillHandler.ActorDirection.West].Add(SkillHandler.Instance.CalcPosHashCode(-3, -1, 6));
        //    range[SkillHandler.ActorDirection.West].Add(SkillHandler.Instance.CalcPosHashCode(-4, -1, 6));
        //    range[SkillHandler.ActorDirection.West].Add(SkillHandler.Instance.CalcPosHashCode(-5, -1, 6));
        //    //NorthWest
        //    range[SkillHandler.ActorDirection.NorthWest].Add(SkillHandler.Instance.CalcPosHashCode(-1, 0, 6));
        //    range[SkillHandler.ActorDirection.NorthWest].Add(SkillHandler.Instance.CalcPosHashCode(-2, 1, 6));
        //    range[SkillHandler.ActorDirection.NorthWest].Add(SkillHandler.Instance.CalcPosHashCode(-3, 2, 6));
        //    range[SkillHandler.ActorDirection.NorthWest].Add(SkillHandler.Instance.CalcPosHashCode(-4, 3, 6));
        //    range[SkillHandler.ActorDirection.NorthWest].Add(SkillHandler.Instance.CalcPosHashCode(-5, 4, 6));
        //    range[SkillHandler.ActorDirection.NorthWest].Add(SkillHandler.Instance.CalcPosHashCode(-5, 5, 6));
        //    range[SkillHandler.ActorDirection.NorthWest].Add(SkillHandler.Instance.CalcPosHashCode(-4, 5, 6));
        //    range[SkillHandler.ActorDirection.NorthWest].Add(SkillHandler.Instance.CalcPosHashCode(-4, 4, 6));
        //    range[SkillHandler.ActorDirection.NorthWest].Add(SkillHandler.Instance.CalcPosHashCode(-3, 3, 6));
        //    range[SkillHandler.ActorDirection.NorthWest].Add(SkillHandler.Instance.CalcPosHashCode(-2, 2, 6));
        //    range[SkillHandler.ActorDirection.NorthWest].Add(SkillHandler.Instance.CalcPosHashCode(-1, 1, 6));
        //    range[SkillHandler.ActorDirection.NorthWest].Add(SkillHandler.Instance.CalcPosHashCode(-3, 4, 6));
        //    range[SkillHandler.ActorDirection.NorthWest].Add(SkillHandler.Instance.CalcPosHashCode(-2, 3, 6));
        //    range[SkillHandler.ActorDirection.NorthWest].Add(SkillHandler.Instance.CalcPosHashCode(-1, 2, 6));
        //    range[SkillHandler.ActorDirection.NorthWest].Add(SkillHandler.Instance.CalcPosHashCode(0, 1, 6));
        //    #endregion
        //}
        #endregion

        //public List<Actor> GetAffectedActors(Actor sActor)
        //{
        //    ////需去掉不在範圍內的 - 完成
        //    //Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
        //    //List<Actor> affected = map.GetActorsArea(sActor, 600, false);
        //    //List<Actor> realAffected = new List<Actor>();
        //    //SkillHandler.ActorDirection dir = SkillHandler.Instance.GetDirection(sActor);

        //    //ActorPC tmpActor = (ActorPC)sActor;
        //    ///*
        //    //    South = 0,
        //    //    SouthEast = 7,
        //    //    East = 6,
        //    //    NorthEast = 5,
        //    //    North = 4,
        //    //    NorthWest = 3,
        //    //    West = 2,
        //    //    SouthWest = 1
        //    //*/
        //    //string[] face = { "西南", "西", "西北", "北", "東北", "東", "東南", "南" };
        //    //SagaMap.Network.Client.MapClient.FromActorPC(tmpActor).SendSystemMessage("你正面向："+ face[dir.GetHashCode()]);


        //    foreach (Actor act in affected)
        //    {
        //        /*
        //         * 
        //         * □□□□□□□□   □□□□□□□□   □□□□□□□□   □□□□□□□□   □□□□□□□□   □□□□□□□□   □□□□□□□□   □□□□□□□□
        //         * □□□□□□□□   □□■■■□□□   □□□□□□□□   □□□☆□□□□   □□□□□■■□   □□□□□■☆□   □■■□□□□□   □☆■□□□□□
        //         * □□□□□□□□   □□■■■□□□   □□□□□□□□   □□■■■□□□   □□□□■■■□   □□□□■■■□   □■■■□□□□   □■■■□□□□
        //         * □□□□□□□□   □□■■■□□□   □□□□□□□□   □□■■■□□□   □□□■■■□□   □□□■■■□□   □□■■■□□□   □□■■■□□□
        //         * □□■■■■■□　 □□■■■□□□   □■■■■■□□   □□■■■□□□   □□■■■□□□   □□■■■□□□   □□□■■■□□   □□□■■■□□
        //         * □☆■■■■■□   □□■■■□□□   □■■■■■☆□   □□■■■□□□　 □■■■□□□□   □■■■□□□□   □□□□■■■□　 □□□□■■■□
        //         * □□■■■■■□   □□□☆□□□□   □■■■■■□□   □□■■■□□□　 □☆■□□□□□   □■■□□□□□   □□□□□■☆□　 □□□□□■■□
        //         * □□□□□□□□   □□□□□□□□   □□□□□□□□   □□□□□□□□   □□□□□□□□   □□□□□□□□   □□□□□□□□   □□□□□□□□
        //         * 
        //         */
        //        if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
        //        {
        //            int XDiff, YDiff;
        //            SkillHandler.Instance.GetXYDiff(map, sActor, act, out XDiff, out YDiff);
        //            if (range[dir].Contains(SkillHandler.Instance.CalcPosHashCode(XDiff, YDiff, 6)))
        //            {
        //                realAffected.Add(act);
        //            }
        //        }
        //    }
        //    return realAffected;
        //}

        public int TryCast(SagaDB.Actor.ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            actor.MapID = sActor.MapID;
            actor.X = sActor.X;
            actor.Y = sActor.Y;
            Mob.MobAI ai = new SagaMap.Mob.MobAI(actor, true);
            List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(sActor.X, map.Width), SagaLib.Global.PosY16to8(sActor.Y, map.Height), args.x, args.y);
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
                list = map.GetActorsArea(pos2[0], pos2[1], 100);
                //筛选有效对象
                foreach (Actor i in list)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i) && !i.Buff.Dead)
                        affected.Add(i);
                }
                count++;
            }
            float[] factors = new float[] { 0.0f, 33.0f, 20.0f, 38.0f, 25.0f };
            float factor = factors[level];
            //Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //List<Actor> realAffected = GetAffectedActors(sActor);
            if (level % 2 == 1)
                SkillHandler.Instance.PhysicalAttack(sActor, affected, args, SagaLib.Elements.Holy, factor);
            else
                SkillHandler.Instance.PhysicalAttack(sActor, affected, args, SagaLib.Elements.Dark, factor);
        }
    }
}
