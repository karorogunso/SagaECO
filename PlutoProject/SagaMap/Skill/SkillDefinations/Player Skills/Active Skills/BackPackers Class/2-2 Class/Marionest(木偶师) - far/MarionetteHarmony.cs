
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations.Marionest
{
    /// <summary>
    /// 木偶融和（マリオネットハーモニー）
    /// </summary>
    public class MarionetteHarmony : ISkill
    {
        #region ISkill Members
        Elements ele = Elements.Neutral;
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Marionette != null)
                {
                    if (pc.Marionette.ID == 10050700 && args.skill.Level == 1)//零式泰迪.无属性
                        return 0;
                    else if (pc.Marionette.ID == 10021700 && args.skill.Level == 5)//火焰凤凰.火属性
                        return 0;
                    else if (pc.Marionette.ID == 10019300 && args.skill.Level == 3)//冰精灵.水属性
                        return 0;
                    else if (pc.Marionette.ID == 10030001 && args.skill.Level == 4)//电路机械.风属性
                        return 0;
                    else if (pc.Marionette.ID == 10027000 && args.skill.Level == 2)//皮诺.地属性
                        return 0;
                    else
                        return -12;
                }

            }
            return -12;

        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 180000;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "MarionetteHarmony", lifetime, 1000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            skill.OnUpdate += this.UpDate;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void UpDate(Actor actor, DefaultBuff skill)
        {
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                if (pc.Marionette != null)
                {
                    if (pc.Marionette.ID != 10050700 &&//零式泰迪.无属性
                        pc.Marionette.ID != 10021700 &&//火焰凤凰.火属性
                        pc.Marionette.ID != 10019300 &&//冰精灵.水属性
                        pc.Marionette.ID != 10030001 &&//电路机械.风属性
                        pc.Marionette.ID != 10027000)//皮诺.地属性
                    {
                        pc.Status.Additions["MarionetteHarmony"].OnTimerEnd();
                    }
                }
                else
                {
                    pc.Status.Additions["MarionetteHarmony"].OnTimerEnd();
                }

            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            
            int MarionetteStr = 0;
            int MarionetteDex = 0;
            int MarionetteInt = 0;
            int MarionetteVit = 0;
            int MarionetteAgi = 0;
            int MarionetteMag = 0;
            int MarionetteHp = 0;
            int MarionetteMp = 0;
            int MarionetteSp = 0;

            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                switch(pc.Marionette.ID)
                {
                    case 10050700:
                        MarionetteInt = 8;
                        MarionetteVit = -8;
                        MarionetteMag = 16;
                        MarionetteMp = 300;
                        MarionetteSp = 125;
                        break;
                    case 10021700:
                        ele = Elements.Fire;
                        MarionetteStr = 18;
                        MarionetteDex = 5;
                        MarionetteInt = -10;
                        MarionetteVit = 25;
                        MarionetteAgi = -9;
                        MarionetteMag = -16;
                        MarionetteHp = 300;
                        MarionetteMp = 400;
                        MarionetteSp = 150;
                        break;
                    case 10019300:
                        ele = Elements.Water;
                        MarionetteInt = 18;
                        MarionetteVit = -5;
                        MarionetteMag = 16;
                        MarionetteHp = 200;
                        MarionetteMp = 450;
                        MarionetteSp = 125;
                        break;
                    case 10030001:
                        ele = Elements.Wind;
                        MarionetteStr = 18;
                        MarionetteDex = 5;
                        MarionetteInt = -10;
                        MarionetteVit = 25;
                        MarionetteAgi = -9;
                        MarionetteMag = -16;
                        MarionetteMp = 500;
                        MarionetteSp = 150;
                        break;
                    case 10027000:
                        ele = Elements.Earth;
                        MarionetteStr = 9;
                        MarionetteDex = 10;
                        MarionetteVit = -12;
                        MarionetteAgi = 18;
                        MarionetteMag = 5;
                        MarionetteHp = 200;
                        MarionetteMp = 300;
                        MarionetteSp = 100;
                        break;
                }
                //        pc.Marionette.ID == 10050700 ||//零式泰迪.无属性
                //        pc.Marionette.ID == 10021700 ||//火焰凤凰.火属性
                //        pc.Marionette.ID == 10019300 ||//冰精灵.水属性
                //        pc.Marionette.ID == 10030001 ||//电路机械.风属性
                //        pc.Marionette.ID == 10027000)//皮诺.地属性

                actor.Status.elements_skill[ele] += 50;

                if (skill.Variable.ContainsKey("MarionetteStr"))
                    skill.Variable.Remove("MarionetteStr");
                skill.Variable.Add("MarionetteStr", MarionetteStr);
                actor.Status.str_skill += (short)MarionetteStr;

                if (skill.Variable.ContainsKey("MarionetteAgi"))
                    skill.Variable.Remove("MarionetteAgi");
                skill.Variable.Add("MarionetteAgi", MarionetteAgi);
                actor.Status.agi_skill += (short)MarionetteAgi;

                if (skill.Variable.ContainsKey("MarionetteVit"))
                    skill.Variable.Remove("MarionetteVit");
                skill.Variable.Add("MarionetteVit", MarionetteVit);
                actor.Status.vit_skill += (short)MarionetteVit;

                if (skill.Variable.ContainsKey("MarionetteInt"))
                    skill.Variable.Remove("MarionetteInt");
                skill.Variable.Add("MarionetteInt", MarionetteInt);
                actor.Status.int_skill += (short)MarionetteInt;

                if (skill.Variable.ContainsKey("MarionetteDex"))
                    skill.Variable.Remove("MarionetteDex");
                skill.Variable.Add("MarionetteDex", MarionetteDex);
                actor.Status.dex_skill += (short)MarionetteDex;

                if (skill.Variable.ContainsKey("MarionetteMag"))
                    skill.Variable.Remove("MarionetteMag");
                skill.Variable.Add("MarionetteMag", MarionetteMag);
                actor.Status.mag_skill += (short)MarionetteMag;

                if (skill.Variable.ContainsKey("MarionetteHp"))
                    skill.Variable.Remove("MarionetteHp");
                skill.Variable.Add("MarionetteHp", MarionetteHp);
                actor.Status.hp_skill += (short)MarionetteHp;

                if (skill.Variable.ContainsKey("MarionetteMp"))
                    skill.Variable.Remove("MarionetteMp");
                skill.Variable.Add("MarionetteMp", MarionetteMp);
                actor.Status.mp_skill += (short)MarionetteMp;

                if (skill.Variable.ContainsKey("MarionetteSp"))
                    skill.Variable.Remove("MarionetteSp");
                skill.Variable.Add("MarionetteSp", MarionetteSp);
                actor.Status.sp_skill += (short)MarionetteSp;
            }


        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.elements_skill[ele] -= 50;
            actor.Status.str_skill -= (short)skill.Variable["MarionetteStr"];
            actor.Status.agi_skill -= (short)skill.Variable["MarionetteAgi"];
            actor.Status.vit_skill -= (short)skill.Variable["MarionetteVit"];
            actor.Status.int_skill -= (short)skill.Variable["MarionetteInt"];
            actor.Status.dex_skill -= (short)skill.Variable["MarionetteDex"];
            actor.Status.mag_skill -= (short)skill.Variable["MarionetteMag"];
            actor.Status.hp_skill -= (short)skill.Variable["MarionetteHp"];
            actor.Status.mp_skill -= (short)skill.Variable["MarionetteMp"];
            actor.Status.sp_skill -= (short)skill.Variable["MarionetteSp"];
        }
        #endregion
    }
}
