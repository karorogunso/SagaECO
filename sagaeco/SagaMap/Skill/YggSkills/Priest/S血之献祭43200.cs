using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 血之献祭：持续30秒，自己每3秒扣3%hp，获得1点魂
    /// </summary>
    public class S43200 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("BloodSacrifice")) return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.type == ActorType.PC)
            {
                SkillHandler.Instance.ShowEffectOnActor(sActor, 5368);
                OtherAddition buff = new OtherAddition(args.skill, sActor, "BloodSacrifice", 30000);
                SkillHandler.ApplyAddition(sActor, buff);
                Activator timer = new Activator(sActor, args);
                timer.Activate();
            }

        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            int countMax = 10, count = 0;
            public Activator(Actor caster, SkillArg args)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 3000;
                this.dueTime = 0;

                ActorPC Me = (ActorPC)caster;
            }
            public override void CallBack()
            {
                try
                {
                    ActorPC pc = (ActorPC)caster;
                    if (count == 0)
                    { 
                        Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("进入『血之献祭』状态");
                    }
                    SkillHandler.Instance.ShowEffectOnActor(pc, 5366);
                    uint damage = (uint)(pc.MaxHP * 0.17f);
                    SkillHandler.Instance.ShowVessel(pc, (int)damage);
                    if (pc.HP > damage) pc.HP -= damage;
                    else pc.HP = 1;
                    caster.EP += 300;
                    if (caster.EP > caster.MaxEP) caster.EP = caster.MaxEP;
                    Map map = Manager.MapManager.Instance.GetMap(caster.MapID);
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, caster, true);
                    count++;
                    if (count >= countMax || caster.HP < caster.MaxHP * 0.2f)
                    {
                        this.Deactivate();
                        Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("『血之献祭』结束");
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }
    }
}