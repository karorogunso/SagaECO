using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Mob;
using SagaMap.Scripting;
using SagaMap.ActorEventHandlers;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31031 : ISkill
    {
        List<ActorMob> actorMobs = new List<ActorMob>();
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            byte x = SagaLib.Global.PosX16to8(sActor.X, map.Width);
            byte y = SagaLib.Global.PosY16to8(sActor.Y, map.Height);
            ActorMob ca = (ActorMob)sActor;
            int count = ca.TInt["零件数"] / 20 + 1;
            ca.TInt["零件数"] = 0;
            for (int i = 0; i < count; i++)
            {
                ActorMob mob = map.SpawnCustomMob(10000000, sActor.MapID, 30330000,0,0, x, y, 15, 1, 0, 冰棍Info(), 冰棍AI(), Ondie, 1)[0];
                actorMobs.Add(mob);
                SkillHandler.Instance.ShowEffectByActor(mob, 4023);
            }
            Activator timer = new Activator(sActor, args, actorMobs);
            timer.Activate();
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;

            List<ActorMob> actorMobs;
            public Activator(Actor caster, SkillArg args, List<ActorMob> actorMobs)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 0;
                this.dueTime = 15000;
                this.actorMobs = actorMobs;
                SkillHandler.Instance.ActorSpeak(caster, "因为需要找到‘零件’才能进行作业，零件不足的话就无法补全身体了。");
            }
            public override void CallBack()
            {
                byte count = 0;
                foreach (var i in actorMobs)
                {
                    SkillHandler.Instance.ShowEffect(map, i, SagaLib.Global.PosX16to8(i.X, map.Width), SagaLib.Global.PosY16to8(i.Y, map.Height), 4086);
                    map.DeleteActor(i);
                    count++;
                }
                if (count == 0)
                    SkillHandler.Instance.ActorSpeak(caster, "没有零件可以修复身体了。我需要更多零件！");
                else
                    SkillHandler.Instance.ActorSpeak(caster, "机械之心，修理完毕！");
                if (caster.HP > 0)
                {
                    ActorMob ca = (ActorMob)caster;
                    ca.TInt["零件数"] += count * 20;
                    int heal = (int)(caster.MaxHP * 0.04f * count);
                    SkillHandler.Instance.CauseDamage(caster, caster, -heal);
                    SkillHandler.Instance.ShowVessel(caster, -heal,-ca.TInt["零件数"]);
                    if (caster.HP > caster.MaxHP)
                        caster.HP = caster.MaxHP;
                }
                actorMobs.Clear();
                this.Deactivate();
            }
        }
        void Ondie(MobEventHandler e, ActorPC pc)
        {
            actorMobs.Remove(e.mob);
        }
        #region 冰棍属性
        ActorMob.MobInfo 冰棍Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "维修零件";
            info.maxhp = 2000;
            info.speed = 500;
            info.atk_min = 1;
            info.atk_max = 1;
            info.matk_min = 1;
            info.matk_max = 1;
            info.def = 20;
            info.def_add = 20;
            info.mdef = 1;
            info.mdef_add = 1;
            info.hit_critical = 23;
            info.hit_magic = 118;
            info.hit_melee = 118;
            info.hit_ranged = 120;
            info.avoid_critical = 24;
            info.avoid_magic = 59;
            info.avoid_melee = 60;
            info.avoid_ranged = 60;
            info.Aspd = 540;
            info.Cspd = 540;
            info.elements[SagaLib.Elements.Neutral] = 0;
            info.elements[SagaLib.Elements.Fire] = 0;
            info.elements[SagaLib.Elements.Water] = 0;
            info.elements[SagaLib.Elements.Wind] = 0;
            info.elements[SagaLib.Elements.Earth] = 0;
            info.elements[SagaLib.Elements.Holy] = 40;
            info.elements[SagaLib.Elements.Dark] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 30;
            info.baseExp = 100;
            info.jobExp = 100;


            /*---------物理掉落---------*/
            /*newDrop.ItemID = 10009000;//掉落物品ID
            newDrop.Rate = 5000;//掉落幾率,10000是100%，5000是50%
            info.dropItems.Add(newDrop);

            newDrop.ItemID = 10000104;//掉落物品ID
            newDrop.Rate = 5000;//掉落幾率
            info.dropItems.Add(newDrop);*/
            /*---------物理掉落---------*/

            return info;
        }

        AIMode 冰棍AI()
        {
            AIMode ai = new AIMode();//1為主動，0為被動
            ai.AI = 6;
            ai.MobID = 30030001;//怪物ID
            ai.EventAttackingSkillRate = 0;
            ai.isNewAI = true;
            ai.Distance = 3;
            ai.ShortCD = 3;
            ai.LongCD = 3;
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();
            return ai;//*/
        }
        #endregion
    }
}
