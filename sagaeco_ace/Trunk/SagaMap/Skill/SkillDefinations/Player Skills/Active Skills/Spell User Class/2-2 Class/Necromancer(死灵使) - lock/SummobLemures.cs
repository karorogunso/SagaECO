
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    /// <summary>
    /// 死靈召喚（死霊召喚）
    /// </summary>
    public class SummobLemures : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SummobLemuresBuff skill = new SummobLemuresBuff(args.skill, sActor, args.x, args.y);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        public class SummobLemuresBuff : DefaultBuff
        {
            uint[] MobID = { 0, 10200400, 10690002, 10180101, 10350101, 10420901 };
            uint[] MobHP = { 0, 450, 500, 600, 700, 1000 };
            public ActorMob mob;
            short x, y;
            public SummobLemuresBuff(SagaDB.Skill.Skill skill, Actor actor, byte x, byte y)
                : base(skill, actor, "SummobLemures", int.MaxValue)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.x = SagaLib.Global.PosX8to16(x, map.Width);
                this.y = SagaLib.Global.PosY8to16(y, map.Height);
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                mob = map.SpawnMob(MobID[skill.skill.Level], x, y, 2500, actor);
                uint HP = MobHP[skill.skill.Level];
                
                #region PassiveSkill Detection
                if (actor.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)actor;

                    //提升召喚對象的HP（召喚対象HP上昇）
                    uint LemuresHpUp_SkillID = 961;
                    if (pc.Skills2.ContainsKey(LemuresHpUp_SkillID))
                    {
                        HP += (uint)(pc.Skills2[LemuresHpUp_SkillID].Level * 50);
                    }
                    else if (pc.SkillsReserve.ContainsKey(LemuresHpUp_SkillID))
                    {
                        HP +=  (uint)(pc.SkillsReserve[LemuresHpUp_SkillID].Level * 50);
                    }

                    //提升召喚對象的魔法攻擊（召喚対象魔法系上昇）
                    uint LemuresMatkUp_SkillID = 963;
                    if (pc.Skills2.ContainsKey(LemuresMatkUp_SkillID))
                    {
                        mob.Status.max_matk_skill += (short)(pc.Skills2[LemuresMatkUp_SkillID].Level * 0.3f * actor.Status.max_matk);
                        mob.Status.min_matk_skill += (short)(pc.Skills2[LemuresMatkUp_SkillID].Level * 0.3f * actor.Status.min_matk);
                    }
                    else if (pc.SkillsReserve.ContainsKey(LemuresMatkUp_SkillID))
                    {
                        mob.Status.max_matk_skill += (short)(pc.SkillsReserve[LemuresMatkUp_SkillID].Level * 0.3f * actor.Status.max_matk);
                        mob.Status.min_matk_skill += (short)(pc.SkillsReserve[LemuresMatkUp_SkillID].Level * 0.3f * actor.Status.min_matk);
                    }
                    //提升召喚對象的物理攻擊（召喚対象物理系上昇）
                    uint LemuresAtkUp_SkillID = 962;
                    if (pc.Skills2.ContainsKey(LemuresAtkUp_SkillID))
                    {
                        //最大攻擊
                        mob.Status.max_atk1_skill += (short)(actor.Status.max_atk_ori * pc.Skills2[LemuresAtkUp_SkillID].Level * 0.3f);
                        mob.Status.max_atk2_skill += (short)(actor.Status.max_atk_ori * pc.Skills2[LemuresAtkUp_SkillID].Level * 0.3f);
                        mob.Status.max_atk3_skill += (short)(actor.Status.max_atk_ori * pc.Skills2[LemuresAtkUp_SkillID].Level * 0.3f);
                        //最小攻擊
                        mob.Status.min_atk1_skill += (short)(actor.Status.min_atk_ori * pc.Skills2[LemuresAtkUp_SkillID].Level * 0.3f);
                        mob.Status.min_atk2_skill += (short)(actor.Status.min_atk_ori * pc.Skills2[LemuresAtkUp_SkillID].Level * 0.3f);
                        mob.Status.min_atk3_skill += (short)(actor.Status.min_atk_ori * pc.Skills2[LemuresAtkUp_SkillID].Level * 0.3f);
                    }
                    else if (pc.SkillsReserve.ContainsKey(LemuresAtkUp_SkillID))
                    {
                        //最大攻擊
                        mob.Status.max_atk1_skill += (short)(actor.Status.max_atk_ori * pc.SkillsReserve[LemuresAtkUp_SkillID].Level * 0.3f);
                        mob.Status.max_atk2_skill += (short)(actor.Status.max_atk_ori * pc.SkillsReserve[LemuresAtkUp_SkillID].Level * 0.3f);
                        mob.Status.max_atk3_skill += (short)(actor.Status.max_atk_ori * pc.SkillsReserve[LemuresAtkUp_SkillID].Level * 0.3f);
                        //最小攻擊
                        mob.Status.min_atk1_skill += (short)(actor.Status.min_atk_ori * pc.SkillsReserve[LemuresAtkUp_SkillID].Level * 0.3f);
                        mob.Status.min_atk2_skill += (short)(actor.Status.min_atk_ori * pc.SkillsReserve[LemuresAtkUp_SkillID].Level * 0.3f);
                        mob.Status.min_atk3_skill += (short)(actor.Status.min_atk_ori * pc.SkillsReserve[LemuresAtkUp_SkillID].Level * 0.3f);
                    }

                }
                #endregion

                mob.MaxHP = HP;
                mob.HP = mob.MaxHP;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, mob, true);
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATUS, null, mob, true);
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