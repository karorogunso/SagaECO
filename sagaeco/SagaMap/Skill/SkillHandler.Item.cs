using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Manager;
using SagaDB.Item;
using SagaDB.Skill;

namespace SagaMap.Skill
{
    public partial class SkillHandler
    {
        public void ItemUse(Actor sActor, Actor dActor, SkillArg arg)
        {
            List<Actor> list = new List<Actor>();
            list.Add(dActor);
            ItemUse(sActor, list, arg);
        }

        public void ItemUse(Actor sActor, List<Actor> dActor, SkillArg arg)
        {            

            int counter = 0;
            arg.affectedActors = dActor;
            arg.Init();

            if (arg.item.BaseData.duration == 0)
            {
                foreach (Actor i in dActor)
                {
                    uint itemhp, itemsp, itemmp, itemep;
                    if (arg.item.BaseData.isRate)
                    {
                        itemhp = (uint)((i.MaxHP * arg.item.BaseData.hp) / 100);
                        itemsp = (uint)((i.MaxSP * arg.item.BaseData.sp) / 100);
                        itemmp = (uint)((i.MaxMP * arg.item.BaseData.mp) / 100);
                        itemep = (uint)(arg.item.BaseData.delay / 100);
                    }
                    else
                    {
                        itemhp = (uint)(arg.item.BaseData.hp);
                        itemsp = (uint)(arg.item.BaseData.sp);
                        itemmp = (uint)(arg.item.BaseData.mp);
                        itemep = (uint)(arg.item.BaseData.delay);
                    }
                    if (i.type == ActorType.PC)
                    {
                        ActorPC pc = (ActorPC)i;
                        if (pc.Job == PC_JOB.HAWKEYE)
                            itemmp = 0;
                    }
                    i.HP = (i.HP + itemhp);
                    i.SP = (i.SP + itemsp);
                    i.MP = (i.MP + itemmp);
                    i.EP = (i.EP + itemep);

                    if (i.HP > i.MaxHP)
                        i.HP = i.MaxHP;
                    if (i.SP > i.MaxSP)
                        i.SP = i.MaxSP;
                    if (i.MP > i.MaxMP)
                        i.MP = i.MaxMP;
                    if (i.EP > i.MaxEP)
                        i.EP = i.MaxEP;

                    if (arg.item.BaseData.hp > 0)
                    {
                        arg.flag[counter] |= AttackFlag.HP_HEAL;
                        arg.hp[counter] = (int)(-itemhp);
                    }
                    else if (arg.item.BaseData.hp < 0)
                    {
                        arg.flag[counter] |= AttackFlag.HP_DAMAGE;
                        arg.hp[counter] = (int)(-itemhp);
                    }
                    if (arg.item.BaseData.sp > 0)
                    {
                        arg.flag[counter] |= AttackFlag.SP_HEAL;
                        arg.sp[counter] = (int)(-itemsp);
                    }
                    else if (arg.item.BaseData.sp < 0)
                    {
                        arg.flag[counter] |= AttackFlag.SP_DAMAGE;
                        arg.sp[counter] = (int)(-itemsp);
                    }
                    if (arg.item.BaseData.mp > 0)
                    {
                        arg.flag[counter] |= AttackFlag.MP_HEAL;
                        arg.mp[counter] = (int)(-itemmp);
                    }
                    else if (arg.item.BaseData.mp < 0)
                    {
                        arg.flag[counter] |= AttackFlag.MP_DAMAGE;
                        arg.mp[counter] = (int)(-itemmp);
                    }
                    counter++;
                    Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, i, true);
                }
            }
            else if (arg.item.BaseData.duration > 0)
            {
                foreach (Actor i in dActor)
                {
                    if (arg.item.BaseData.hp > 0)
                    {
                        i.Status.hp_medicine = arg.item.BaseData.hp;
                        Additions.Global.Medicine1 skill1 = new SagaMap.Skill.Additions.Global.Medicine1(null, i, (int)60000);
                        SkillHandler.ApplyAddition(i, skill1);
                    }
                    /*if (arg.item.BaseData.mp > 0)
                    {
                        i.Status.mp_medicine = arg.item.BaseData.mp;
                        Additions.Global.Medicine2 skill2 = new SagaMap.Skill.Additions.Global.Medicine2(null, i, (int)arg.item.BaseData.duration);
                        SkillHandler.ApplyAddition(i, skill2);
                    }
                    if (arg.item.BaseData.sp > 0)
                    {
                        i.Status.sp_medicine = arg.item.BaseData.sp;
                        Additions.Global.Medicine3 skill3 = new SagaMap.Skill.Additions.Global.Medicine3(null, i, (int)arg.item.BaseData.duration);
                        SkillHandler.ApplyAddition(i, skill3);
                    }*/
                }               
            }
            //arg.delay = 5000;
        }
    }
}
