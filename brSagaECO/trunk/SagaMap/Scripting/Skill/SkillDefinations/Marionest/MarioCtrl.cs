
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Marionest
{
    /// <summary>
    /// 召喚活動木偶（マリオネット召喚）
    /// </summary>
    public class MarioCtrl : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 25000;
            uint[] MobID ={0,   26320000//寵物泰迪
                               ,26280000//寵物皮諾
                               ,26290000//寵物愛伊斯
                               ,26300000//寵物塔依
                               ,26310000//寵物虎姆拉
                             };
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorMob mob = map.SpawnMob(MobID[level], SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 2500, sActor);
            MarioCtrlBuff skill = new MarioCtrlBuff(args.skill, sActor,  lifetime,mob);
            SkillHandler.ApplyAddition(sActor, skill);
            if (!sActor.Status.Additions.ContainsKey("MarioCtrlMove"))
            {
                CannotMove cm = new CannotMove(args.skill, sActor, lifetime);
                SkillHandler.ApplyAddition(sActor, cm);
            }
        }
        public class MarioCtrlBuff : DefaultBuff
        {
            private ActorMob mob;
            public MarioCtrlBuff(SagaDB.Skill.Skill skill, Actor actor, int lifetime,ActorMob mob)
                : base(skill, actor, "MarioCtrl", lifetime)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.mob = mob;
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
            }
            void EndEvent(Actor actor, DefaultBuff skill)
            {
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                mob.ClearTaskAddition();
                map.DeleteActor(mob);
            }
        }
        #endregion
    }
}
