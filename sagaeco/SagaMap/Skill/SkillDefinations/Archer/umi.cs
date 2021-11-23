using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Archer
{
    /// <summary>
    /// 惡鬼
    /// </summary>
    public class umi : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
                return 0;
            return -5;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            Map map = SagaMap.Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 700, false);

            umis u = new umis(args.skill, sActor, sActor, actors.Count * 200, 0);
            SkillHandler.ApplyAddition(sActor, u);
        }
        class umis : DefaultBuff
        {
            int i = 0;
            public umis(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int damage)
                : base(skill, sActor, dActor, "umi", lifetime, 200, damage)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.OnUpdate2 += this.TimerUpdate;
                i = lifetime / 200;
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {

            }
            Actor LastHit = null;
            void TimerUpdate(Actor sActor, Actor dActor, DefaultBuff skill, SkillArg args, int damage)
            {
                try
                {
                    Map map = SagaMap.Manager.MapManager.Instance.GetMap(sActor.MapID);
                    List<Actor> actors = map.GetActorsArea(sactor, 700, false);
                    Actor target = actors[i - 1];
                    i--;
                    byte lv = 1;
                    if (sActor.type == ActorType.PC)
                    {
                        lv = ((ActorPC)sActor).Skills[3083].Level;
                    }
                    else
                    {
                        lv = 5;
                    }
                    if (target.HP > 0 && !target.Buff.Dead)
                    {
                        SkillArg arg = new SkillArg();
                        arg.Init();
                        arg.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(3083, lv);
                        arg.dActor = target.ActorID;
                        arg.x = 255;
                        arg.y = 255;
                        arg.sActor = sActor.ActorID;
                        arg.argType = SkillArg.ArgType.Cast;
                        arg.delay = 100;
                        arg.result = 0;
                        arg.useMPSP = false;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg, sActor, true);

                        Tasks.PC.SkillCast task = new SagaMap.Tasks.PC.SkillCast(SagaMap.Network.Client.MapClient.FromActorPC(((ActorPC)sActor)), arg);
                        if (sActor.Tasks.ContainsKey("SkillCast"))
                            sActor.Tasks.Add("SkillCast", task);
                        task.Activate();
                    }
                }
                catch(Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                }
            }
        }
        #endregion
    }
}
