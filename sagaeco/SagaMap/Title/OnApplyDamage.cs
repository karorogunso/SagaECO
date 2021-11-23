using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Linq;
using System.Text;
using SagaDB.Item;
using Microsoft.CSharp;
using System.IO;

using SagaDB;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.SkillDefinations;
using System.Reflection;
using SagaMap.Skill;

namespace SagaMap.Titles
{
    public partial class TitleEventManager : Singleton<TitleEventManager>
    {
        SkillHandler s = SkillHandler.Instance;

        public void OnApplyDamage(Actor sActor, Actor dActor, int damage)
        {
            if(damage>0)
            {
                //残暴与不屈
                s.TitleProccess(sActor, 23, (uint)damage);
                s.TitleProccess(dActor, 24, (uint)damage);

                if (sActor.type == ActorType.PC)
                {
                    ActorPC pc = sActor as ActorPC;

                    if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                    {
                        //女王：累积用鞭子造成1E伤害
                        if (pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType == ItemType.ROPE)
                            s.TitleProccess(pc, 79, (uint)damage);
                        //金刚狼：累积用爪子造成1E伤害
                        if (pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType == ItemType.CLAW)
                            s.TitleProccess(pc, 112, (uint)damage);
                    }
                    if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                    {
                        //鹰眼：累积用弓造成1E伤害
                        if (pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType == ItemType.BOW && pc.TInt["斥候远程模式"] == 1)
                            s.TitleProccess(pc, 113, (uint)damage);
                    }
                    //搞事：幻视之形的仇恨误导效果结束前对敌人造成合计50w以上的伤害。
                    if (sActor.Status.Additions.ContainsKey("误导"))
                        s.TitleProccess(pc, 96, (uint)damage);
                }
                if (dActor.type == ActorType.PC)
                {
                    ActorPC pc = dActor as ActorPC;

                    if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND) && damage > 0)
                    {
                        //美国队长：累积用盾受到5000W伤害
                        if (pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType == ItemType.SHIELD)
                            s.TitleProccess(pc, 114, (uint)damage);
                    }
                }
            }
            else
            {
                //圣母：累积治疗
                s.TitleProccess(sActor, 115, (uint)-damage);
            }

            //目标死亡
            if (dActor.HP == 0)
            {
                //地雷：在使用电报后0.1秒内受到致命伤害而死亡 1次。
                if (sActor.type != ActorType.PC && dActor.TTime["电报时间"] + new TimeSpan(0, 0, 0, 0, 100) > DateTime.Now)
                    s.TitleProccess(dActor, 94, 1, true);

                //替我挡着：使用你的名字交换位置的目标在幻化效果结束之前受到致命伤害而死亡 1次。
                if (sActor.type != ActorType.PC && dActor.TTime["被你名时间"] + new TimeSpan(0, 0, 4) > DateTime.Now)
                    s.TitleProccess(dActor, 98, 1, true);

                //推倒
                if (sActor.type == ActorType.PC && dActor.type == ActorType.PC && sActor != dActor)
                {
                    if (sActor.type == ActorType.PC)
                    {
                        ActorPC pc = (ActorPC)sActor;
                        if (pc.Mode == PlayerMode.NORMAL)
                            s.TitleProccess(pc, 10, 1);
                    }
                }
            }
        }
    }
}

