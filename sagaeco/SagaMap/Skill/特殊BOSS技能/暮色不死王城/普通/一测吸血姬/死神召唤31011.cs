using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaDB.Mob;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31011 : ISkill
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
            List<Actor> targets = map.GetActorsArea(sActor, 250, true);
            List<Actor> dactors = new List<Actor>();
            foreach (var i in targets)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    dactors.Add(i);
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, dactors, args, Elements.Holy, 3f);

            int count = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, 3000).Count;
            if (count < 3) count = 3;
            if (count > 8) count = 8;

            List<ActorMob> mobs = map.SpawnCustomMob(10000000, map.ID, 10250100, 0, 0, x, y, count, 7, 0, Info(sActor), AI(), null, 0);
            for (int i = 0; i < mobs.Count; i++)
            {
                sActor.Slave.Add(mobs[i]);
                ActorEventHandlers.MobEventHandler eh = new ActorEventHandlers.MobEventHandler(mobs[i]);
                eh.AI.Master = sActor;
            }

            if (sActor.type == ActorType.MOB)
                SkillHandler.Instance.ActorSpeak(sActor, "你们有两下子嘛，出来吧！本小姐的仆从们呀！");
        }
        ActorMob.MobInfo Info(Actor boss)
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.maxhp = (ushort)(boss.MaxHP * 0.1f);//血量
            info.name = "死亡守卫";
            info.speed = 820;//移動速度
            info.atk_min = (ushort)(boss.Status.max_atk1 * 2);//最低物理攻擊
            info.atk_max = (ushort)(boss.Status.max_atk1 * 3); ;//最高物理攻擊
            info.matk_min = 100;//最低魔法攻擊
            info.matk_max = 300;//最高物理攻擊
            info.def = 0;//物理左防
            info.mdef = 0;//魔法左防
            info.def_add = 0;//物理右防
            info.mdef_add = 0;//魔法右防
            info.hit_critical = 50;//暴擊值
            info.hit_magic = 100;//魔法命中值（目前沒用
            info.hit_melee = 300;//近戰命中值
            info.hit_ranged = 300;//遠程命中值
            info.avoid_critical = 20;//暴擊閃避值
            info.avoid_magic = 0;//魔法閃避值
            info.avoid_melee = 20;//近戰閃避值
            info.avoid_ranged = 30;//遠程閃避值
            info.Aspd = 900;//攻速
            info.Cspd = 100;//唱速
            info.AttackType = SagaDB.Actor.ATTACK_TYPE.BLOW;//攻擊類型，打 刺 斬，一般可以不管
            info.elements[SagaLib.Elements.Fire] = 0;//火屬性
            info.elements[SagaLib.Elements.Earth] = 0;//地屬性
            info.elements[SagaLib.Elements.Dark] = 20;//暗屬性
            info.elements[SagaLib.Elements.Holy] = 0;//光屬性
            info.elements[SagaLib.Elements.Neutral] = 0;//無屬性
            info.elements[SagaLib.Elements.Water] = 0;//水屬性
            info.elements[SagaLib.Elements.Wind] = 30;//風屬性
            info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 0;//混亂抗性
            info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 0;//冰抗性
            info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 0;//麻痺
            info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 0;//毒抗
            info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 0;//沉默抗
            info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 0;//睡抗
            info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 0;//石抗
            info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 0;//暈抗
            info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 0;//頓足抗
            info.baseExp = 1;//基礎經驗值
            info.jobExp = 1;//職業經驗值
            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            /*---------物理掉落---------*/

            return info;
        }
        AIMode AI()
        {
            AIMode ai = new AIMode(1); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10000000;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 20;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 20;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------居合1---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 8;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(2115, skillinfo);//將這個技能加進進程技能表

            /*---------旋风剑---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 8;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(2116, skillinfo);//將這個技能加進進程技能表

            return ai;
        }
    }
}
