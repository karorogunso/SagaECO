using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Manager;

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

            foreach (Actor i in dActor)
            {
                uint itemhp, itemsp, itemmp;
                if (arg.item.BaseData.isRate)
                {
                    itemhp = (uint)((i.MaxHP * arg.item.BaseData.hp) / 100);
                    itemsp = (uint)((i.MaxSP * arg.item.BaseData.sp) / 100);
                    itemmp = (uint)((i.MaxMP * arg.item.BaseData.mp) / 100);
                }
                else
                {
                    itemhp = (uint)(arg.item.BaseData.hp);
                    itemsp = (uint)(arg.item.BaseData.sp);
                    itemmp = (uint)(arg.item.BaseData.mp);
                }
                i.HP = (uint)(i.HP + itemhp);
                i.SP = (uint)(i.SP + itemsp);
                i.MP = (uint)(i.MP + itemmp);
                if (i.HP > i.MaxHP)
                    i.HP = i.MaxHP;
                if (i.SP > i.MaxSP)
                    i.SP = i.MaxSP;
                if (i.MP > i.MaxMP)
                    i.MP = i.MaxMP;

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
            arg.delay = arg.item.BaseData.delay;
        }
    }
}
