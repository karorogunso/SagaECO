using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12106 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("箭雨CD"))
                return -30;
            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
            {
                SagaDB.Item.Item item = pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND];
                if ((item.BaseData.itemType == SagaDB.Item.ItemType.BOW || item.BaseData.itemType == SagaDB.Item.ItemType.RIFLE
                    || item.BaseData.itemType == SagaDB.Item.ItemType.GUN || item.BaseData.itemType == SagaDB.Item.ItemType.DUALGUN) && pc.TInt["斥候远程模式"] == 1)
                    return 0;
                else return -2;
            }
            return -2;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //创建设置型技能技能体
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //设定技能体位置
            actor.MapID = sActor.MapID;
            actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            //设定技能体的事件处理器，由于技能体不需要得到消息广播，因此创建个空处理器
            actor.e = new ActorEventHandlers.NullEventHandler();
            //在指定地图注册技能体Actor
            map.RegisterActor(actor);
            //设置Actor隐身属性为非
            actor.invisble = false;
            //广播隐身属性改变事件，以便让玩家看到技能体
            map.OnActorVisibilityChange(actor);
            //設置系
            actor.Stackable = false;
            //创建技能效果处理对象
            Activator timer = new Activator(sActor, actor, args, level);
            timer.Activate();

            //int cd = 5 * level * 400;
            OtherAddition cds = new OtherAddition(null, sActor, "箭雨CD", 60000);
            cds.OnAdditionEnd += (s, e) =>
            {

                SkillHandler.SendSystemMessage(sActor, "『流星坠』可以再次使用了。");
            };
            SkillHandler.ApplyAddition(sActor, cds);

            OtherAddition 流星坠 = new OtherAddition(null, sActor, "流星坠", 1200 + level * 400 * 3);
            SkillHandler.ApplyAddition(sActor, 流星坠);

            uint epheal = 10;
            sActor.EP += epheal;
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);

            if (level == 4)
            {
                ActorPC pc = (ActorPC)sActor;
                pc.TInt["爆裂次数"] = 5;
                SkillHandler.Instance.ShowEffectOnActor(sActor, 7918);
            }
        }

        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            float factor = 1.0f;
            int countMax = 4, count = 0;

            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                period = 400;
                dueTime = 0;
                countMax += 3 * level;
            }

            public override void CallBack()
            {
                try
                {
                    if (count < countMax)
                    {
                        List<Actor> actors = map.GetActorsArea(actor, 350, false);
                        List<Actor> affected = new List<Actor>();
                        skill.affectedActors.Clear();
                        foreach (Actor i in actors)
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                                affected.Add(i);

                        SkillHandler.Instance.PhysicalAttack(caster, affected, skill, Elements.Neutral, factor);

                        //广播技能效果
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);
                        count++;
                    }
                    else
                    {
                        this.Deactivate();
                        map.DeleteActor(actor);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    this.Deactivate();
                    map.DeleteActor(actor);
                }
            }
        }
    }
}
