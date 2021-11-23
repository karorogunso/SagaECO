using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 光炮：原3转光炮
    /// </summary>
    public class S43002 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 5.5f;
            if (sActor.Status.Additions.ContainsKey("属性契约"))
            {
                if (((OtherAddition)(sActor.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Holy)
                {
                    factor = 7.5f;
                    sActor.EP += 300;
                    if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
                    Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, Elements.Holy, factor);
            if (sActor.Status.holymortar_combo_iris > 0)
            {
                if (SagaLib.Global.Random.Next(1, 100) <= 20)
                {
                    Activator a = new Activator(sActor, dActor, factor, args, sActor.Status.holymortar_combo_iris);
                    a.Activate();
                }
            }
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            Actor da;
            SkillArg args;
            float factor;
            int maxcount = 0, count = 0;
            public Activator(Actor caster, Actor dactor, float factor, SkillArg arg,int maxcount)
            {
                this.factor = factor;
                this.caster = caster;
                da = dactor;
                args = arg.Clone();
                period = 200;
                dueTime = 1000;
                this.maxcount = maxcount;
                SkillHandler.Instance.ShowEffectOnActor(caster, 5360);
            }
            public override void CallBack()
            {
                try
                {
                    if (caster.MP > args.skill.MP && da.HP > 1 && !da.Buff.Dead && count < maxcount)
                    {
                        factor += 0.5f;
                        caster.MP -= (uint)args.skill.MP;
                        SkillHandler.Instance.DoDamage(false, caster, da, args, SkillHandler.DefType.MDef, Elements.Holy, 50, factor);
                        caster.EP += 300;
                        SkillHandler.Instance.ShowEffectOnActor(da, 4362);
                        if (caster.EP > caster.MaxEP) caster.EP = caster.MaxEP;
                        Map map = Manager.MapManager.Instance.GetMap(caster.MapID);
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, caster, true);
                        count++;
                    }
                    else
                        this.Deactivate();
                }
                catch(Exception ex)
                {
                    Logger.ShowError(ex);
                    this.Deactivate();
                }
            }
        }
    }
}
