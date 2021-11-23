using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;


namespace SagaMap.Skill.SkillDefinations
{
    public class S31006 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 1500, false);
            List<Actor> dactors = new List<Actor>();
            foreach (var item in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    Damage task = new Damage(sActor, item);
                    task.Activate();
                    Stun stun = new Stun(null, item, 1000);
                    SkillHandler.Instance.ShowEffectOnActor(item, 7743);
                    SkillHandler.ApplyAddition(item, stun);
                    SkillHandler.SendSystemMessage(item, "你中了『暗影猎杀』！3秒后BOSS将对你造成伤害，伤害倍率取决于接下来3秒内团队对BOSS造成的伤害(每1000点提升10%)。");
                }
            }

            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "暗影猎杀", 3000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);


            if (sActor.type == ActorType.MOB)
                SkillHandler.Instance.ActorSpeak(sActor, "隐身蝙蝠是不会被打败的。感受我的速度吧，暗影猎杀！");
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.TInt["暗影猎杀"] = 0;
            SkillHandler.Instance.ShowEffectByActor(actor, 4381);
            actor.Buff.Transparent = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.Transparent = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }


        class Damage : MultiRunTask
        {
            Actor Caster;
            Actor Target;
            public Damage(Actor caster, Actor target)
            {
                Caster = caster;
                Target = target;
                dueTime = 3000;
            }

            public override void CallBack()
            {
                Deactivate();
                if (Caster.HP > 0)
                {
                    float factor = 1.5f;
                    factor += Caster.TInt["暗影猎杀"] / 1000f;
                    int damage = SkillHandler.Instance.CalcDamage(true, Caster, Target, null, SkillHandler.DefType.MDef, Elements.Neutral, 0, factor);
                    SkillHandler.Instance.CauseDamage(Caster, Target, damage);
                    SkillHandler.Instance.ShowVessel(Target, damage);
                    SkillHandler.Instance.ShowEffectOnActor(Target, 8077);
                }
            }
        }
    }
}
