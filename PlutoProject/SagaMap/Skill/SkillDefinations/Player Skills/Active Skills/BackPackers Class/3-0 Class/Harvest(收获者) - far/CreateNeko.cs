using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Harvest
{
    /// <summary>
    /// クリエイトネコグルミ
    /// </summary>
    public class CreateNeko : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            //SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("技能未实装");
            return -13;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            int lifetime = 15000 + 15000 * level;
            switch(level)
            {
                case 1:
                    
                    ActorMob mob = map.SpawnMob(90010042, SagaLib.Global.PosX8to16(args.x, map.Width)
                                                       , SagaLib.Global.PosY8to16(args.y, map.Height)
                                                       , 2500, sActor);

                    //mob.type = ActorType.ANOTHERMOB;
                    mob.type = ActorType.ANOTHERMOB;
                    mob.Owner = sActor;
                    mob.MaxHP = 3500 + sActor.MaxHP;
                    mob.Speed = 300;
                    ActorEventHandlers.MobEventHandler eE = (ActorEventHandlers.MobEventHandler)mob.e;
                    eE.AI.Master = sActor;
                    //修复
                    eE.AI.Mode = new Mob.AIMode(1);
                    //eE.AI.Mode.EventAttacking.Add(2116, 100);   //旋风剑
                    //eE.AI.Mode.EventAttacking.Add(2401, 100);   //斩击无双
                    eE.AI.Mode.EventAttackingSkillRate = 0;
                    StaffCtrlBuff skill = new StaffCtrlBuff(mob, args.skill, sActor, lifetime);
                    SkillHandler.ApplyAddition(sActor, skill);
                    break;
                case 2:
                    ActorMob mob2 = map.SpawnMob(90010043, SagaLib.Global.PosX8to16(args.x, map.Width)
                                                       , SagaLib.Global.PosY8to16(args.y, map.Height)
                                                       , 2500, sActor);

                    //mob.type = ActorType.ANOTHERMOB;
                    mob2.type = ActorType.ANOTHERMOB;
                    mob2.Owner = sActor;
                    mob2.MaxHP = 3500 + (uint)(sActor.MaxHP*1.1f);
                    mob2.Speed = 300;
                    ActorEventHandlers.MobEventHandler eE2 = (ActorEventHandlers.MobEventHandler)mob2.e;
                    eE2.AI.Master = sActor;
                    //修复
                    eE2.AI.Mode = new Mob.AIMode(1);
                    //eE2.AI.Mode.EventAttacking.Add(3308, 100);   //区域治愈
                    //eE2.AI.Mode.EventAttacking.Add(7758, 100);   //怒火燎原
                    eE2.AI.Mode.EventAttackingSkillRate = 0;
                    StaffCtrlBuff skill2 = new StaffCtrlBuff(mob2, args.skill, sActor, lifetime);
                    SkillHandler.ApplyAddition(sActor, skill2);
                    break;
                case 3:
                    ActorMob mob3 = map.SpawnMob(90010044, SagaLib.Global.PosX8to16(args.x, map.Width)
                                                       , SagaLib.Global.PosY8to16(args.y, map.Height)
                                                       , 2500, sActor);

                    //mob.type = ActorType.ANOTHERMOB;
                    mob3.type = ActorType.ANOTHERMOB;
                    mob3.Owner = sActor;
                    mob3.MaxHP = 3500 + (uint)(sActor.MaxHP * 1.1f);
                    mob3.Speed = 300;
                    ActorEventHandlers.MobEventHandler eE3 = (ActorEventHandlers.MobEventHandler)mob3.e;
                    eE3.AI.Master = sActor;
                    //修复
                    eE3.AI.Mode = new Mob.AIMode(1);
                    //辅助系有待完善
                    //eE3.AI.Mode.EventAttacking.Add(3308, 100);   //区域治愈
                    eE3.AI.Mode.EventAttackingSkillRate = 0;
                    StaffCtrlBuff skill3 = new StaffCtrlBuff(mob3, args.skill, sActor, lifetime);
                    SkillHandler.ApplyAddition(sActor, skill3);
                    break;
                    break;
            }
            
        }
        public class StaffCtrlBuff : DefaultBuff
        {
            private Actor mob;
            public StaffCtrlBuff(Actor mob, SagaDB.Skill.Skill skill, Actor actor, int lifetime)
                : base(skill, actor, "CreateNeko", lifetime)
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
                if (mob != null)
                {
                    mob.ClearTaskAddition();
                    map.DeleteActor(mob);
                }
            }
        }
        #endregion
    }
}



