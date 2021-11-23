using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Sage
{
    /// <summary>
    /// 恆星磒落（ルミナリィノヴァ）
    /// </summary>
    public class LuminaryNova : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 3.2f + 1.0f * level;
            int rate = 20 + 10 * level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 300, null);
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    if (SkillHandler.Instance.CanAdditionApply(sActor, i, "LuminaryNova", 40) && !SkillHandler.Instance.isBossMob(i))
                    {
                        LuminaryNovaBuff skill = new LuminaryNovaBuff(args, sActor, i, 20000, 2000);
                        SkillHandler.ApplyAddition(i, skill);
                    }
                    affected.Add(i);
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Neutral, factor);

        }
        public class LuminaryNovaBuff : DefaultBuff
        {
            SkillArg args;
            Actor sActor;
            public LuminaryNovaBuff(SkillArg args, Actor sActor, Actor actor, int lifetime, int period)
                : base(args.skill, actor, "LuminaryNova", lifetime, period)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.OnUpdate += this.UpdateTimeHandler;
                this.args = args.Clone();
                this.sActor = sActor;
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {
            }
            void UpdateTimeHandler(Actor actor, DefaultBuff skill)
            {
                if (actor.HP > 0 && !actor.Buff.Dead)
                {
                    Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                    int demage = Math.Min((int)(actor.MaxHP * (0.015f + 0.002f * skill.skill.Level)), 3000);
                    EffectArg arg2 = new EffectArg();
                    arg2.effectID = 5059;
                    arg2.actorID = actor.ActorID;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg2, actor, true);
                    SkillHandler.Instance.FixAttack(sActor, actor, args, SagaLib.Elements.Neutral, demage);
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, args, actor, true);

                }
                else
                {
                    skill.AdditionEnd();
                }
            }
        }
        #endregion
    }
}
