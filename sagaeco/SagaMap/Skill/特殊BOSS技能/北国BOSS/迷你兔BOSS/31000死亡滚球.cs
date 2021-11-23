using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Mob;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31000 : ISkill
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
            var mobs = map.SpawnCustomMob(10111300, sActor.MapID, x, y, 5, 5, 0, Info(), AI());
            foreach (ActorMob itme in mobs)
            {
                List<Actor> actors = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(itme, 1000);
                if (actors.Count > 0)
                {
                    double len = 999999;
                    ActorPC da = null;
                    foreach (ActorPC a in actors)
                    {
                        double len1 = MobAI.GetLengthD(itme.X, itme.Y, a.X, a.Y);
                        if (len1 < len)
                        {
                            len = len1;
                            da = a;
                        }
                    }
                    if (da != null)
                    {
                        ((ActorEventHandlers.MobEventHandler)itme.e).AI.Hate.Add(da.ActorID,99999);
                    }

                }
                //((ActorEventHandlers.MobEventHandler)itme.e).AI.Start();
            }//*/
        }
        ActorMob.MobInfo Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "逆袭的肉桂兔";
            info.maxhp = 30500;
            info.speed = 920;
            info.atk_min = 120;
            info.atk_max = 242;
            info.matk_min = 11700;
            info.matk_max = 23700;
            info.def = 21;
            info.def_add = 121;
            info.mdef = 21;
            info.mdef_add = 118;
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



            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            newDrop.ItemID = 10009000;//掉落物品ID
            newDrop.Rate = 5000;//掉落幾率,10000是100%，5000是50%
            info.dropItems.Add(newDrop);

            newDrop.ItemID = 10000104;//掉落物品ID
            newDrop.Rate = 5000;//掉落幾率
            info.dropItems.Add(newDrop);
            /*---------物理掉落---------*/

            return info;
        }

        AIMode AI()
        {
            AIMode ai = new AIMode();//1為主動，0為被動
            ai.AI = 1;
            ai.MobID = 10111302;//怪物ID
            ai.isNewAI = true;
            ai.Distance = 1;
            ai.ShortCD = 3;
            ai.LongCD = 3;
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------自爆---------*/
            skillinfo.CD = 3;//技能CD
            skillinfo.Rate = 100;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31005, skillinfo);//將這個技能加進進程技能表
            return ai;
        }
    }
}
