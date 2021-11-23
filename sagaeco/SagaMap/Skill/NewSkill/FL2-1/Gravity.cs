using SagaLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    /// <summary>
    /// 重力波（グラヴィティ）
    /// </summary>
    public class Gravity : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 400, false, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                    realAffected.Add(act);
            }
            foreach (Actor a in realAffected)
            {
                short[] pos = new short[2];
                byte X = 0;
                byte Y = 0;
                SkillHandler.Instance.GetTRoundPos(map, sActor, out X, out Y, 1);
                pos[0] = SagaLib.Global.PosX8to16(X, map.Width);
                pos[1] = SagaLib.Global.PosY8to16(Y, map.Height);
                map.MoveActor(Map.MOVE_TYPE.START, a, pos, 1000, 1000, true, MoveType.QUICKEN);
                Skill.Additions.Global.硬直 硬直 = new Skill.Additions.Global.硬直(args.skill, a, 2000);
                SkillHandler.ApplyAddition(a, 硬直);
            }
            ActivatorA ab = new ActivatorA(sActor, realAffected, args, level);
            ab.Activate();
            //4213
        }
        #endregion

        class ActivatorA : MultiRunTask
        {
            Actor sActor;
            List<Actor> dActors;
            Map map;
            SkillArg arg;
            byte lv;
            public ActivatorA(Actor sActor, List<Actor> dActors, SkillArg args, byte level)
            {
                this.sActor = sActor;
                this.dActors = dActors;
                this.arg = args;
                map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                this.period = 1000;
                this.dueTime = 300;
                lv = level;
            }
            public override void CallBack()
            {
                try
                {
                    foreach (Actor a in dActors)
                    {
                        int damage = SkillHandler.Instance.CalcDamage(true, sActor, a, arg, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, 0, 1.8f + 0.3f * lv);
                        SkillHandler.Instance.PushBack(sActor, a, 2);
                        SkillHandler.Instance.CauseDamage(sActor, a, damage);
                        SkillHandler.Instance.ShowVessel(a, damage);
                        //SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(a.MapID), a, 8057);
                    }
                    SkillHandler.Instance.ShowEffect(map, sActor, 4213);
                    this.Deactivate();
                }
                catch (Exception ex)
                { Logger.ShowError(ex); this.Deactivate(); }
            }
        }
    }
}