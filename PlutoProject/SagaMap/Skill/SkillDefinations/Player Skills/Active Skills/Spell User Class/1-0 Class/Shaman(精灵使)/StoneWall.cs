using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Shaman
{
    /// <summary>
    /// 大地之壁
    /// </summary>
    public class StoneWall : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            #region OldMethod
            //Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //List<ActorSkill> actorSkill = new List<ActorSkill>();
            //int deltaX = 0; int deltaY = 0;
            //int argX = (int)args.x; int argY = (int)args.y;
            //int castorX = (int)SagaLib.Global.PosX16to8(sActor.X, map.Width);
            //int castorY = (int)SagaLib.Global.PosY16to8(sActor.Y, map.Height);
            //deltaX = argX - castorX; deltaY = argY - castorY;
            //Dictionary<int, int[]> pos = new Dictionary<int, int[]>();
            //int n = 0; int flag = 0;
            //for (int i = 0; i <= 4; i++)
            //{
            //    ActorSkill actorT = new ActorSkill(args.skill, sActor);
            //    actorSkill.Add(actorT);
            //    actorSkill[i].e = new ActorEventHandlers.NullEventHandler();
            //    actorSkill[i].MapID = sActor.MapID;
            //    int[] posT = new int[2]; posT[0] = 0; posT[1] = 0;
            //    pos.Add(i, posT);
            //}
            //if (deltaX != 0 || deltaY != 0)
            //{
            //    if (Math.Abs(deltaX) != Math.Abs(deltaY))
            //    {
            //        actorSkill.Remove(actorSkill[actorSkill.Count - 1]); actorSkill.Remove(actorSkill[actorSkill.Count - 1]);
            //        if (Math.Abs(deltaY) > Math.Abs(deltaX))
            //        {
            //            pos[0][0] = argX; pos[0][1] = argY;
            //            pos[1][0] = argX + 1; pos[1][1] = argY;
            //            pos[2][0] = argX - 1; pos[2][1] = argY;
            //            if (deltaY > 0)
            //                flag = 1;//Mark which kind of area was the wall setted.
            //            else
            //                flag = 2;
            //        }
            //        else
            //        {
            //            pos[0][0] = argX; pos[0][1] = argY;
            //            pos[1][0] = argX; pos[1][1] = argY + 1;
            //            pos[2][0] = argX; pos[2][1] = argY - 1;
            //            if (deltaX > 0)
            //                flag = 3;
            //            else
            //                flag = 4;
            //        }
            //    }
            //    else
            //    {
            //        if (deltaX * deltaY < 0)
            //        {
            //            pos[0][0] = argX; pos[0][1] = argY;
            //            pos[1][0] = argX + 1; pos[1][1] = argY + 1;
            //            pos[2][0] = argX - 1; pos[2][1] = argY - 1;
            //            if (argX > 0)
            //            {
            //                pos[3][0] = argX; pos[3][1] = argY + 1;
            //                pos[4][0] = argX - 1; pos[4][1] = argY;
            //                flag = 5;
            //            }
            //            else
            //            {
            //                pos[3][0] = argX; pos[3][1] = argY - 1;
            //                pos[4][0] = argX + 1; pos[4][1] = argY;
            //                flag = 6;
            //            }
            //        }
            //        else
            //        {
            //            pos[0][0] = argX; pos[0][1] = argY;
            //            pos[1][0] = argX + 1; pos[1][1] = argY - 1;
            //            pos[2][0] = argX - 1; pos[2][1] = argY + 1;
            //            if (argX > 0)
            //            {
            //                pos[3][0] = argX; pos[3][1] = argY - 1;
            //                pos[4][0] = argX - 1; pos[4][1] = argY;
            //                flag = 7;
            //            }
            //            else
            //            {
            //                pos[3][0] = argX; pos[3][1] = argY + 1;
            //                pos[4][0] = argX + 1; pos[4][1] = argY;
            //                flag = 8;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    pos[0][0] = argX; pos[0][1] = argY;
            //    pos[1][0] = argX; pos[1][1] = argY + 1;
            //    pos[2][0] = argX; pos[2][1] = argY - 1;
            //    flag = 9;
            //}

            //foreach (ActorSkill i in actorSkill)
            //{
            //    if (pos[n][0] != 0 && pos[n][1] != 0)
            //    {
            //        i.X = SagaLib.Global.PosX8to16((byte)pos[n][0], map.Width);
            //        i.Y = SagaLib.Global.PosY8to16((byte)pos[n][1], map.Height);
            //        map.RegisterActor(i);
            //        i.invisble = false;
            //        map.OnActorVisibilityChange(i);
            //    }
            //    n = n + 1;
            //}
            ////Activator timer = new Activator(sActor, args, level, flag, actorSkill);
            ////timer.Activate();
            #endregion
            int r = SagaLib.Global.Random.Next(0, 99);
            StoneWallBuff skill = new StoneWallBuff(args, sActor, 60000, r);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        public class StoneWallBuff : DefaultBuff
        {
            private SkillArg args;
            private List<ActorMob> MobLst = new List<ActorMob>();
            public StoneWallBuff(SkillArg skill, Actor actor, int lifetime, int rand)
                : base(skill.skill, actor, "StoneWall" + rand.ToString(), lifetime)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                args = skill.Clone();
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                uint MobID = (uint)(30470000 + skill.skill.Level - 1);
                byte[] posX = new byte[3];
                byte[] posY = new byte[3];

                SkillHandler.Instance.GetRelatedPos(actor, 0, 0, args.x, args.y, out posX[0], out posY[0]);
                switch (SkillHandler.Instance.GetDirection(actor))
                {
                    case SkillHandler.ActorDirection.North:
                    case SkillHandler.ActorDirection.South:
                        SkillHandler.Instance.GetRelatedPos(actor, 1, 0, args.x, args.y, out posX[1], out posY[1]);
                        SkillHandler.Instance.GetRelatedPos(actor, -1, 0, args.x, args.y, out posX[2], out posY[2]);
                        break;
                    case SkillHandler.ActorDirection.NorthEast:
                    case SkillHandler.ActorDirection.SouthWest:
                        SkillHandler.Instance.GetRelatedPos(actor, 1, -1, args.x, args.y, out posX[1], out posY[1]);
                        SkillHandler.Instance.GetRelatedPos(actor, -1, 1, args.x, args.y, out posX[2], out posY[2]);
                        break;
                    case SkillHandler.ActorDirection.SouthEast:
                    case SkillHandler.ActorDirection.NorthWest:
                        SkillHandler.Instance.GetRelatedPos(actor, -1, -1, args.x, args.y, out posX[1], out posY[1]);
                        SkillHandler.Instance.GetRelatedPos(actor, 1, 1, args.x, args.y, out posX[2], out posY[2]);
                        break;
                    case SkillHandler.ActorDirection.West:
                    case SkillHandler.ActorDirection.East:
                        SkillHandler.Instance.GetRelatedPos(actor, 0, -1, args.x, args.y, out posX[1], out posY[1]);
                        SkillHandler.Instance.GetRelatedPos(actor, 0, 1, args.x, args.y, out posX[2], out posY[2]);
                        break;
                }
                MobLst.Add(map.SpawnMob(MobID, SagaLib.Global.PosX8to16(posX[0], map.Width), SagaLib.Global.PosY8to16(posY[0], map.Height), 50, null));
                MobLst.Add(map.SpawnMob(MobID, SagaLib.Global.PosX8to16(posX[1], map.Width), SagaLib.Global.PosY8to16(posY[1], map.Height), 50, null));
                MobLst.Add(map.SpawnMob(MobID, SagaLib.Global.PosX8to16(posX[2], map.Width), SagaLib.Global.PosY8to16(posY[2], map.Height), 50, null));

            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                foreach (ActorMob m in MobLst)
                {
                    map.DeleteActor(m);
                }
            }
        }

        #endregion
    }
}
