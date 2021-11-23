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
    public class LuminaryNova:ISkill 
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 3.5f + 1.0f * level;
            int rate = 20 + 10 * level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 300, null);
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    if (SagaLib.Global.Random.Next(0, 99) < rate && !SkillHandler.Instance.isBossMob(i))
                    {
                        Additions.Global.DefaultBuff skill = new Additions.Global.DefaultBuff(args.skill, i, "LuminaryNova", 20000, 2000);
                        skill.OnAdditionStart += this.StartEventHandler;
                        skill.OnAdditionEnd += this.EndEventHandler;
                        skill.OnUpdate += this.UpdateTimeHandler;
                        SkillHandler.ApplyAddition(i, skill);
                    }
                    affected.Add(i);
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Neutral, factor);
            
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
        }
        void UpdateTimeHandler(Actor actor, DefaultBuff skill)
        {
            if (actor.HP > 0 && !actor.Buff.Dead)
            {
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                int demage = (int)(actor.HP * 0.02f);
                if (actor.HP > demage)
                    actor.HP = (uint)(actor.HP - demage);
                else
                    actor.HP = 1;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
                //Skill事件的arg不能为null
                //map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, null, actor, false);

            }
        }
        #endregion
    }
}
