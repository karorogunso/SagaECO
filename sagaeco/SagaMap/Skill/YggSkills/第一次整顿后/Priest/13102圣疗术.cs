using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 治愈术：单体大量治疗，并随机消除一个控制状态，如果消除了控制状态则再额外恢复少量hp
    /// </summary>
    public class S13102 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (dActor.type == ActorType.MOB)
            {
                ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)dActor.e;
                if (eh.AI.Mode.Symbol)
                    return -14;
            }
            return 0;
        }
        public void Proc(Actor sActor, Actor dActorS, SkillArg args, byte level)
        {
            if (dActorS.type == ActorType.PARTNER && level <= 3)
            {
                if (((ActorPartner)dActorS).Owner != null)
                    dActorS = ((ActorPartner)dActorS).Owner;
            }
            List<Actor> actors = new List<Actor>() { dActorS };
            if(level == 4)
            {
                Map map = Manager.MapManager.Instance.GetMap(dActorS.MapID);
                List<Actor> targets = map.GetActorsArea(dActorS, 300, true);
                actors = new List<Actor>();
                foreach (var item in targets)
                {
                    if(item.type == ActorType.PC && sActor.type == ActorType.PC)
                    {
                        ActorPC pc = (ActorPC)item;
                        if(pc.Mode == ((ActorPC)sActor).Mode && !pc.Buff.Dead)
                        {
                            actors.Add(pc);
                        }
                    }
                }

            }
            ActorPC Me = (ActorPC)sActor;
            if ((Me.CInt["圣疗术任务"] == 2 || Me.CInt["圣疗术任务"] == 3) && Me.CInt["圣疗术任务条件"] < 1500 && Me.MapID != 10054000)
            {
                Me.CInt["圣疗术任务条件"]++;
                Network.Client.MapClient.FromActorPC(Me).SendSystemMessage("进阶『圣疗术』修炼进度：" + Me.CInt["圣疗术任务条件"].ToString() + "/1500");
            }
            float factor = 1.6f + 0.4f * level;
            if (level == 4) factor = 2.8f;
            factor += factor * (sActor.BeliefLight / 5000f);
            SkillHandler.Instance.MagicAttack(sActor, actors, args, SkillHandler.DefType.IgnoreAll, Elements.Holy, -factor);
            foreach (var dActor in args.affectedActors)
            {
                //Logger.ShowWarning(actors.Count.ToString() + " " + args.affectedActors.Count.ToString());
                //Logger.ShowError(args.affectedActors.IndexOf(dActor).ToString()+" " + args.flag[args.affectedActors.IndexOf(dActor)]);
                if ((args.flag[args.affectedActors.IndexOf(dActor)] & AttackFlag.CRITICAL) != 0)
                {
                    //Logger.ShowInfo("暴击！");
                    SkillHandler.Instance.ShowEffectOnActor(dActor, 4084, sActor);
                    SkillHandler.Instance.ShowEffectOnActor(sActor, 4274);
                    if (sActor.Status.Additions.ContainsKey("祝福之声"))
                        SkillHandler.RemoveAddition(sActor, "祝福之声");
                    OtherAddition skill = new OtherAddition(null, sActor, "祝福之声", 60000);
                    SkillHandler.ApplyAddition(sActor, skill);
                }
                if (sActor.BeliefLight >= 2000)
                {
                    List<string> ss = new List<string>();
                    if (dActor.Status.Additions.ContainsKey("Confuse")) ss.Add("Confuse");
                    if (dActor.Status.Additions.ContainsKey("Frosen")) ss.Add("Frosen");
                    if (dActor.Status.Additions.ContainsKey("Paralyse")) ss.Add("Paralyse");
                    if (dActor.Status.Additions.ContainsKey("Silence")) ss.Add("Silence");
                    if (dActor.Status.Additions.ContainsKey("Sleep")) ss.Add("Sleep");
                    if (dActor.Status.Additions.ContainsKey("Stone")) ss.Add("Stone");
                    if (dActor.Status.Additions.ContainsKey("Stun")) ss.Add("Stun");
                    if (dActor.Status.Additions.ContainsKey("鈍足")) ss.Add("鈍足");
                    if (dActor.Status.Additions.ContainsKey("冰棍的冻结")) ss.Add("冰棍的冻结");
                    if (ss.Count > 1)
                        SkillHandler.RemoveAddition(dActor, ss[SagaLib.Global.Random.Next(0, ss.Count - 1)]);
                    else if (ss.Count == 1)
                        SkillHandler.RemoveAddition(dActor, ss[0]);
                }

                if (sActor.BeliefLight>=4000 && !dActor.Status.Additions.ContainsKey("圣疗持续恢复CD"))
                {
                    Activator timer = new Activator(sActor, dActor, args, level);
                    timer.Activate();
                    OtherAddition skill = new OtherAddition(null, dActor, "圣疗持续恢复CD", 10000);
                    SkillHandler.ApplyAddition(dActor, skill);
                }
            }
            if (!sActor.Status.Additions.ContainsKey("意志坚定"))
                sActor.EP += 300;
            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }

        private class Activator : MultiRunTask
        {
            Actor caster;
            Actor dactor;
            Map map;
            SkillArg args;
            float factor = 1f;
            int countMax = 10, count = 0;
            int damage = 0;
            public Activator(Actor caster, Actor dactor, SkillArg args, byte level)
            {
                this.dactor = dactor;
                this.caster = caster;
                this.args = args.Clone();
                map = Manager.MapManager.Instance.GetMap(dactor.MapID);
                period = 1000;
                DueTime = 3000;
                factor = 0.25f + level * 0.1f;
                damage = SkillHandler.Instance.CalcDamage(false, caster, dactor, this.args, SkillHandler.DefType.IgnoreAll, Elements.Holy, 50, -factor);
            }
            public override void CallBack()
            {
                try
                {
                    if (count < countMax && dactor.HP >= 1 && !dactor.Buff.Dead)
                    {
                        count++;
                        dactor.HP += (uint)-damage;
                        if (dactor.HP > dactor.MaxHP) dactor.HP = dactor.MaxHP;
                        SkillHandler.Instance.ShowVessel(dactor, (int)damage);
                    }
                    else
                    {
                        this.Deactivate();
                    }
                }
                catch (Exception ex)
                {
                    Deactivate();
                    Logger.ShowError(ex);
                }
            }
        }
    }
}

