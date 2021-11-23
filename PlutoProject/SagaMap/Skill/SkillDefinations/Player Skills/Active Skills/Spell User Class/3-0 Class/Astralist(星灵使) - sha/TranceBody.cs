using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Astralist
{
    /// <summary>
    /// トランスボディ
    /// </summary>
    public class TranceBody : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public SkillArg arg = new SkillArg();
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Actor realdActor = SkillHandler.Instance.GetPossesionedActor((ActorPC)sActor);
            int lifetime = 150000 + 30000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, realdActor, "TranceBody", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(realdActor, skill);

        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.三转元素身体属性赋予 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.三转元素身体属性赋予 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        //public void Passive(Actor sActor, Actor dActor, Elements element, int elementValue, int damage)
        //{
        //    if (sActor.type == ActorType.PC)
        //    {
        //        ActorPC pc = (ActorPC)sActor;
        //        //这里取副职的3377等级
        //        var duallv = 0;
        //        if (pc.DualJobSkill.Exists(x => x.ID == 3377))
        //            duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 3377).Level;

        //        //这里取主职的3337等级
        //        var mainlv = 0;
        //        if (pc.Skills3.ContainsKey(3377))
        //            mainlv = pc.Skills3[3377].Level;

        //        //这里取等级最高的3377等级用来参与运算
        //        int level3337 = Math.Max(duallv, mainlv);
        //        SagaDB.Skill.Skill skill2 = SagaDB.Skill.SkillFactory.Instance.GetSkill(3377, (byte)level3337);
        //        SkillArg arg = new SkillArg();
        //        arg.sActor = sActor.ActorID;
        //        arg.dActor = dActor.ActorID;
        //        arg.skill = skill2;
        //        arg.x = 0;
        //        arg.y = 0;
        //        arg.argType = SkillArg.ArgType.Cast;


        //        int lifetime = 150000 + 30000 * pc.Skills3[3377].BaseData.lv;
        //        if (element == Elements.Earth)
        //        {
        //            DefaultBuff skill = new DefaultBuff(arg.skill, sActor, "TranceBody_Earth", lifetime);
        //            skill.OnAdditionStart += this.StartEventHandlerEarth;
        //            skill.OnAdditionEnd += this.EndEventHandlerEarth;
        //            SkillHandler.ApplyAddition(sActor, skill);
        //        }
        //        if (element == Elements.Water)
        //        {
        //            DefaultBuff skill = new DefaultBuff(arg.skill, sActor, "TranceBody_Water", lifetime);
        //            skill.OnAdditionStart += this.StartEventHandlerWater;
        //            skill.OnAdditionEnd += this.EndEventHandlerWater;
        //            SkillHandler.ApplyAddition(sActor, skill);
        //        }
        //        if (element == Elements.Fire)
        //        {
        //            DefaultBuff skill = new DefaultBuff(null, sActor, "TranceBody_Fire", lifetime);
        //            skill.OnAdditionStart += this.StartEventHandlerFire;
        //            skill.OnAdditionEnd += this.EndEventHandlerFire;
        //            SkillHandler.ApplyAddition(sActor, null);
        //        }
        //        if (element == Elements.Holy)
        //        {
        //            DefaultBuff skill = new DefaultBuff(arg.skill, sActor, "TranceBody_Wind", lifetime);
        //            skill.OnAdditionStart += this.StartEventHandlerWind;
        //            skill.OnAdditionEnd += this.EndEventHandlerWind;
        //            SkillHandler.ApplyAddition(sActor, skill);
        //        }
        //    }
        //}

        //void StartEventHandlerEarth(Actor actor, DefaultBuff skill)
        //{
        //    ActorPC pc = (ActorPC)actor;
        //    //这里取副职的3377等级
        //    var duallv = 0;
        //    if (pc.DualJobSkill.Exists(x => x.ID == 3377))
        //        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 3377).Level;

        //    //这里取主职的3377等级
        //    var mainlv = 0;
        //    if (pc.Skills3.ContainsKey(3377))
        //        mainlv = pc.Skills3[3377].Level;

        //    //这里取等级最高的3377等级用来参与运算
        //    int level3377 = Math.Max(duallv, mainlv);
        //    int element_up_e = 15 + 5 * (byte)level3377
        //        ;
        //    //不管是主职还是副职
        //    if (pc.Skills.ContainsKey(3042) || pc.DualJobSkill.Exists(x => x.ID == 3042))
        //    {

        //        //这里取副职的3042等级
        //        var duallv3042 = 0;
        //        if (pc.DualJobSkill.Exists(x => x.ID == 3042))
        //            duallv3042 = pc.DualJobSkill.FirstOrDefault(x => x.ID == 3042).Level;

        //        //这里取主职的3042等级
        //        var mainlv3042 = 0;
        //        if (pc.Skills.ContainsKey(3042))
        //            mainlv3042 = pc.Skills[3042].Level;

        //        //这里取等级最高的3042等级用来参与运算
        //        element_up_e += 5 * Math.Max(duallv3042, mainlv3042);
        //    }
        //    if (skill.Variable.ContainsKey("TranceBody_Earth_Add"))
        //        skill.Variable.Remove("TranceBody_Earth_Add");
        //    skill.Variable.Add("TranceBody_Earth_Add", element_up_e);
        //    actor.Status.elements_item[Elements.Earth] += element_up_e;
        //}
        //void EndEventHandlerEarth(Actor actor, DefaultBuff skill)
        //{
        //    actor.Status.elements_item[Elements.Earth] -= skill.Variable["TranceBody_Earth_Add"];
        //}

        //void StartEventHandlerWater(Actor actor, DefaultBuff skill)
        //{
        //    ActorPC pc = (ActorPC)actor;
        //    //这里取副职的3377等级
        //    var duallv = 0;
        //    if (pc.DualJobSkill.Exists(x => x.ID == 3377))
        //        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 3377).Level;

        //    //这里取主职的3377等级
        //    var mainlv = 0;
        //    if (pc.Skills3.ContainsKey(3377))
        //        mainlv = pc.Skills3[3377].Level;

        //    //这里取等级最高的3337等级用来参与运算
        //    int level3377 = Math.Max(duallv, mainlv);
        //    int element_up_w = 15 + 5 * (byte)level3377;
        //    //int element_up_w = 15 + 5 * pc.Skills3[3377].BaseData.lv;

        //    //不管是主职还是副职
        //    if (pc.Skills.ContainsKey(3030) || pc.DualJobSkill.Exists(x => x.ID == 3030))
        //    {
        //        //这里取副职的3030等级
        //        var duallv3030 = 0;
        //        if (pc.DualJobSkill.Exists(x => x.ID == 3030))
        //            duallv3030 = pc.DualJobSkill.FirstOrDefault(x => x.ID == 3030).Level;

        //        //这里取主职的3030等级
        //        var mainlv3030 = 0;
        //        if (pc.Skills.ContainsKey(3030))
        //            mainlv3030 = pc.Skills[3030].Level;

        //        //这里取等级最高的3030等级用来参与运算
        //        element_up_w += 5 * Math.Max(duallv3030, mainlv3030);
        //        //element_up_w += 5 * pc.Skills[3030].BaseData.lv;
        //    }
        //    if (skill.Variable.ContainsKey("TranceBody_Water_Add"))
        //        skill.Variable.Remove("TranceBody_Water_Add");
        //    skill.Variable.Add("TranceBody_Water_Add", element_up_w);
        //    actor.Status.elements_item[Elements.Water] += element_up_w;
        //}
        //void EndEventHandlerWater(Actor actor, DefaultBuff skill)
        //{
        //    actor.Status.elements_item[Elements.Water] -= skill.Variable["TranceBody_Water_Add"];
        //}

        //void StartEventHandlerFire(Actor actor, DefaultBuff skill)
        //{
        //    ActorPC pc = (ActorPC)actor;
        //    //这里取副职的3377等级
        //    var duallv = 0;
        //    if (pc.DualJobSkill.Exists(x => x.ID == 3377))
        //        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 3377).Level;

        //    //这里取主职的3377等级
        //    var mainlv = 0;
        //    if (pc.Skills3.ContainsKey(3377))
        //        mainlv = pc.Skills3[3377].Level;

        //    //这里取等级最高的3337等级用来参与运算
        //    int level3377 = Math.Max(duallv, mainlv);
        //    int element_up_f = 15 + 5 * level3377;
        //    //不管是主职还是副职
        //    if (pc.Skills.ContainsKey(3007) || pc.DualJobSkill.Exists(x => x.ID == 3007))
        //    {
        //        //这里取副职的3007等级
        //        var duallv3007 = 0;
        //        if (pc.DualJobSkill.Exists(x => x.ID == 3007))
        //            duallv3007 = pc.DualJobSkill.FirstOrDefault(x => x.ID == 3007).Level;

        //        //这里取主职的3030等级
        //        var mainlv3007 = 0;
        //        if (pc.Skills.ContainsKey(3007))
        //            mainlv3007 = pc.Skills[3007].Level;

        //        //这里取等级最高的3030等级用来参与运算
        //        element_up_f += 5 * Math.Max(duallv3007, mainlv3007);
        //        //element_up_f += 5 * pc.Skills[3007].BaseData.lv;
        //    }
        //    if (skill.Variable.ContainsKey("TranceBody_Fire_Add"))
        //        skill.Variable.Remove("TranceBody_Fire_Add");
        //    skill.Variable.Add("TranceBody_Fire_Add", element_up_f);
        //    actor.Status.elements_item[Elements.Fire] += element_up_f;
        //}
        //void EndEventHandlerFire(Actor actor, DefaultBuff skill)
        //{
        //    actor.Status.elements_item[Elements.Fire] -= skill.Variable["TranceBody_Fire_Add"];
        //}

        //void StartEventHandlerWind(Actor actor, DefaultBuff skill)
        //{
        //    ActorPC pc = (ActorPC)actor;
        //    //这里取副职的3377等级
        //    var duallv = 0;
        //    if (pc.DualJobSkill.Exists(x => x.ID == 3377))
        //        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 3377).Level;

        //    //这里取主职的3377等级
        //    var mainlv = 0;
        //    if (pc.Skills3.ContainsKey(3377))
        //        mainlv = pc.Skills3[3377].Level;

        //    //这里取等级最高的3337等级用来参与运算
        //    int level3377 = Math.Max(duallv, mainlv);
        //    int element_up_h = 15 + 5 * level3377;
        //    //不管是主职还是副职
        //    if (pc.Skills.ContainsKey(3018) || pc.DualJobSkill.Exists(x => x.ID == 3018))
        //    {
        //        //这里取副职的3018等级
        //        var duallv3018 = 0;
        //        if (pc.DualJobSkill.Exists(x => x.ID == 3018))
        //            duallv3018 = pc.DualJobSkill.FirstOrDefault(x => x.ID == 3018).Level;

        //        //这里取主职的3018等级
        //        var mainlv3018 = 0;
        //        if (pc.Skills.ContainsKey(3018))
        //            mainlv3018 = pc.Skills[3018].Level;

        //        //这里取等级最高的3030等级用来参与运算
        //        element_up_h += 5 * Math.Max(duallv3018, mainlv3018);
        //        //element_up_h += 5 * pc.Skills[3018].BaseData.lv;
        //    }
        //    if (skill.Variable.ContainsKey("TranceBody_Wind_Add"))
        //        skill.Variable.Remove("TranceBody_Wind_Add");
        //    skill.Variable.Add("TranceBody_Wind_Add", element_up_h);
        //    actor.Status.elements_item[Elements.Wind] += element_up_h;
        //}
        //void EndEventHandlerWind(Actor actor, DefaultBuff skill)
        //{
        //    actor.Status.elements_item[Elements.Wind] -= skill.Variable["TranceBody_Wind_Add"];
        //}
    }
}
