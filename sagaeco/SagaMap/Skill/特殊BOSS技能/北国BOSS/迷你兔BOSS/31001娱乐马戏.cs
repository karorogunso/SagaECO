using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;
using SagaMap.Scripting;
using SagaMap.ActorEventHandlers;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31001 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            byte x = SagaLib.Global.PosX16to8(sActor.X, map.Width);
            byte y = SagaLib.Global.PosY16to8(sActor.Y, map.Height);

            //计算并刷出3种共20只萝卜
            int id1 =0, id2 = 0, id3 = 0;
            for (int i = 0; i < 40; i++)
            {
                switch (SagaLib.Global.Random.Next(1, 3))
                {
                    case 1:
                        id1++;
                        break;
                    case 2:
                        id2++;
                        break;
                    case 3:
                        id3++;
                        break;
                }
            }
            actorMobs = map.SpawnCustomMob(10580500, sActor.MapID, x, y, 20, id1, 0, Info(), AI(), (MobCallback)Ondie, 1);
            actorMobs.AddRange(map.SpawnCustomMob(10580002, sActor.MapID, x, y, 30, id2, 0, Info(), AI(), (MobCallback)Ondie, 1));
            actorMobs.AddRange(map.SpawnCustomMob(10580003, sActor.MapID, x, y, 20, id3, 0, Info(), AI(), (MobCallback)Ondie, 1));

            //Manager.ScriptManager.Instance.VariableHolder.AInt["胡萝卜计数"] = 0;

            Activator timer = new Activator(sActor, args, actorMobs);
            timer.Activate();
        }
        List<ActorMob> actorMobs;
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
                this.dueTime = 10000;
                this.actorMobs = actorMobs;
            }
            public override void CallBack()
            {
                foreach (var i in actorMobs)
                {
                    try
                    {
                        if (i.HP > 0)
                        {
                            SkillHandler.Instance.ShowEffect(map, i, SagaLib.Global.PosX16to8(i.X, map.Width), SagaLib.Global.PosY16to8(i.Y, map.Height), 4086);
                            map.DeleteActor(i);
                        }
                    }
                    catch { }
                }
                List<Actor> actors = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(caster, 2000);
                foreach (var item in actors)
                {
                    float f = 0;
                    //if (Manager.ScriptManager.Instance.VariableHolder.AInt["胡萝卜计数"] >= 20)
                    if (!item.Status.Additions.ContainsKey("娱乐马戏BUFF"))
                        f = 1000f;
                    else
                    {
                        f = 0f;
                        SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)item).SendSystemMessage("肚子里的胡萝卜为你抵挡了致命一击！");
                    }

                    int damage = SkillHandler.Instance.CalcDamage(false, caster, item, skill, SkillHandler.DefType.MDef, SagaLib.Elements.Dark, 0, f);
                    SkillHandler.Instance.CauseDamage(caster, item, damage);
                    SkillHandler.Instance.ShowVessel(item, damage);
                    SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(map.ID), item, 5003);
                }
                this.Deactivate();
            }
        }
        #region 萝卜死亡事件，会给玩家加buff
        void Ondie(MobEventHandler e, ActorPC pc)
        {
            actorMobs.Remove(e.mob);
            Actor sActor = e.mob;

            if (!pc.Status.Additions.ContainsKey("娱乐马戏BUFF"))
            {
                DefaultBuff buff = new DefaultBuff(null, pc, "娱乐马戏BUFF", 15000);
                SkillHandler.ApplyAddition(pc, buff);
                Network.Client.MapClient.FromActorPC((ActorPC)pc).SendSystemMessage("一口吃下了胡萝卜，你感受到了胡萝卜的刺激，攻击力上升了！");
                if (!pc.Status.Additions.ContainsKey("AtkUp"))
                {
                    AtkUp au = new AtkUp(null, pc, 15000, 800);
                    SkillHandler.ApplyAddition(pc, au);
                }
            }
            else
            {
                /*SkillHandler.RemoveAddition(pc, "娱乐马戏BUFF");
                SkillHandler.RemoveAddition(pc, "AtkUp");
                SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)pc).SendSystemMessage("又是一口吃下了胡萝卜，两只胡萝卜在肚子里打架，BUFF被吃掉了！");*/
            }
        }

        #endregion
        #region 萝卜属性
        ActorMob.MobInfo Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "我是胡萝卜";
            info.maxhp = 200;
            info.speed = 340;
            info.atk_min = 100;
            info.atk_max = 300;
            info.matk_min = 1;
            info.matk_max = 1;
            info.def = 1;
            info.def_add = 1;
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
            info.elements[SagaLib.Elements.Fire] = 50;
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

        AIMode AI()
        {
            //return Mob.MobAIFactory.Instance.Items[10001000];
            //*
            AIMode ai = new AIMode();//1為主動，0為被動
            ai.AI = 8;
            ai.MobID = 10000000;//怪物ID
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
