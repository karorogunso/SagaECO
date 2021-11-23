using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S14030 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            int tpcooldown = 20000;     //冷却时间
            if (pc.Skills.ContainsKey(14053) && pc.Skills[14053].Level >= 2)
                tpcooldown = 15000;

            if (pc.Status.Additions.ContainsKey("电报CD"))
                if (!pc.Skills.ContainsKey(14053)) //根本没学被动的
                    return -30;
                else if (((OtherAddition)pc.Status.Additions["电报CD"]).RestLifeTime > tpcooldown) //学了但是已经用过2次，也可以用ContainsKey("电报CD2")判断
                    return -30;


            if (pc.AInt["接受了搬运任务"] == 1)
            {
                SkillHandler.SendSystemMessage(pc, "由于接受了搬运任务，你无法使用该技能。");
                return -13;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //SkillHandler.Instance.ShowEffect((ActorPC)sActor, SagaLib.Global.PosX16to8(sActor.X, map.Width), SagaLib.Global.PosY16to8(sActor.Y, map.Height), 5211);
            ActorPC Me = (ActorPC)sActor;

            if (!sActor.Status.Additions.ContainsKey("疾风斩移动速度UP"))
            {
                OtherAddition 疾风斩移动速度UP = new OtherAddition(null, sActor, "疾风斩移动速度UP", 5000);
                疾风斩移动速度UP.OnAdditionStart += (x, e) =>
                {
                    sActor.Speed = 1100;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SPEED_UPDATE, null, sActor, true);
                    sActor.Buff.移動力上昇 = true;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
                };
                疾风斩移动速度UP.OnAdditionEnd += (x, e) =>
                {
                    sActor.Buff.移動力上昇 = false;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
                };
                SkillHandler.ApplyAddition(sActor, 疾风斩移动速度UP);
            }
            else
            {
                Addition 疾风斩移动速度UP = sActor.Status.Additions["疾风斩移动速度UP"];
                TimeSpan time = new TimeSpan(0, 0, 0, 5);
                ((OtherAddition)疾风斩移动速度UP).endTime = DateTime.Now + time;
            }

            int range = level * 3 + 2;//移动距离
            short[] posI = new short[2] { 0, 0 };
            for (int i = 1; i < range; i++)
            {
                short[] pos = SkillHandler.Instance.GetNewPoint(sActor.Dir, sActor.X, sActor.Y, i);
                byte x = SagaLib.Global.PosX16to8(pos[0], map.Width);
                byte y = SagaLib.Global.PosY16to8(pos[1], map.Height);
                if (map.Info.walkable[x, y] != 2)
                    break;
                else
                {
                    posI[0] = pos[0];
                    posI[1] = pos[1];
                }
            }
            if (posI[0] == 0 && posI[1] == 0) return;

            map.TeleportActor(sActor, posI[0], posI[1]);

            if (Me.Status.Additions.ContainsKey("魔法少女") && Me.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) && Me.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RAPIER)
            {
                SkillHandler.Instance.ShowEffectOnActor(sActor, 4132);
                int hpheal = (int)(Me.MaxHP*0.2f);
                SkillHandler.Instance.ShowVessel(Me, -hpheal);
                Me.HP += (uint)hpheal;
                if (Me.HP > Me.MaxHP)
                    Me.HP = Me.MaxHP;
            }

            if (Me.Skills.ContainsKey(14007))
            {
                byte lv = Me.Skills[14007].Level;
                float factor = 7f + 3f * lv;
                SkillHandler.Instance.ShowEffect((ActorPC)sActor, SagaLib.Global.PosX16to8(posI[0], map.Width), SagaLib.Global.PosY16to8(posI[1], map.Height), 5211);
                List<Actor> actors = map.GetActorsArea(posI[0], posI[1], 200, false);
                List<Actor> targets = new List<Actor>();
                foreach (var item in actors)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                    {
                        targets.Add(item);
                        if (!item.Status.Additions.ContainsKey("空间震"))
                        {
                            OtherAddition sk = new OtherAddition(null, item, "空间震", 4000);
                            SkillHandler.ApplyAddition(item, sk);
                            SkillHandler.Instance.ShowEffectOnActor(item, 5280, sActor);
                        }
                    }
                }
                if (factor > 16f) factor = 16f;
                SkillHandler.Instance.MagicAttack(sActor, targets, args, Elements.Neutral, factor);
            }
            else
                SkillHandler.Instance.ShowEffect((ActorPC)sActor, SagaLib.Global.PosX16to8(posI[0], map.Width), SagaLib.Global.PosY16to8(posI[1], map.Height), 5115);
            //SkillHandler.Instance.ShowEffectByActor(sActor, 5211);

            int tpcooldown = 20000;
            if (Me.Skills.ContainsKey(14053) && Me.Skills[14053].Level >= 2)
                tpcooldown = 15000;

            //地雷：在使用电报后1秒内受到致命伤害而死亡 1次
            Me.TTime["电报时间"] = DateTime.Now;

            if (Me.Status.Additions.ContainsKey("电报CD")) //已经有一次CD，trycast中已经判定了2次电报的合法性，这里就不判断了。
            {
                TimeSpan span = new TimeSpan(0, 0, 0, 0, tpcooldown);
                
                //实际上这里可以再给自己一个RestLifeTime毫秒的独立状态用于提示电报充能完成一次。
                //并没有必要性，不要也可以
                OtherAddition skill = new OtherAddition(args.skill, dActor, "电报CD2", ((OtherAddition)dActor.Status.Additions["电报CD"]).RestLifeTime);
                skill.OnAdditionEnd += (s, e) =>
                {
                    Network.Client.MapClient.FromActorPC(Me).SendSystemMessage("『电报』可以使用了");
                };
                SkillHandler.ApplyAddition(dActor, skill);
                ((OtherAddition)Me.Status.Additions["电报CD"]).endTime += span;
            }
            else
            {
                OtherAddition skill = new OtherAddition(args.skill, dActor, "电报CD", tpcooldown);
                skill.OnAdditionEnd += (s, e) =>
                {
                    Network.Client.MapClient.FromActorPC(Me).SendSystemMessage("『电报』达到了最大充能次数。");
                };
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }

        #endregion
    }
}
