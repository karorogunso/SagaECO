using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31107 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.ActorSpeak(sActor, "弱小的灵魂，就像泡沫一样脆弱，不堪一击…");
            int count = 1;//先定好攻击次数，默认次数为1
            List<string> ss = new List<string>();//定一个列表，用来装可以移除的BUFF
            if (dActor.Status.Additions.ContainsKey("STRDOWN")) ss.Add("STRDOWN");
            if (dActor.Status.Additions.ContainsKey("MAGDOWN")) ss.Add("MAGDOWN");
            if (dActor.Status.Additions.ContainsKey("AGIDOWN")) ss.Add("AGIDOWN");
            if (dActor.Status.Additions.ContainsKey("INTDOWN")) ss.Add("INTDOWN");
            if (dActor.Status.Additions.ContainsKey("VITDOWN")) ss.Add("VITDOWN");
            if (dActor.Status.Additions.ContainsKey("DEXDOWN")) ss.Add("DEXDOWN");//如果目标存在这6个BUFF，则添加到ss里
            if(ss.Count >= 1)//如果ss的个数大于等于1了
            {
                count += ss.Count;//攻击次数加上ss的个数
                for (int i = 0; i < ss.Count; i++)//遍历ss
                {
                    if (dActor.Status.Additions.ContainsKey(ss[i]))//移除BUFF前重复检查
                        SkillHandler.RemoveAddition(dActor, ss[i]);//移除这个BUFF
                }
            }
            List<Actor> dactor = new List<Actor>();//定一个目标列表，用来装目标会被攻击多少次
            for (int i = 0; i < count; i++)
                dactor.Add(dActor);//每次循环都把目标装进list一次，攻击次数是多少就装多少次

            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.BLOW;
            args.delayRate = 1f;

            SkillHandler.Instance.ShowEffectOnActor(dActor, 5111);

            SkillHandler.Instance.PhysicalAttack(sActor, dactor, args, Elements.Dark, 2f);//造成伤害，每次300%

            if (dActor.HP <= 0)//如果造成伤害后目标死亡
            {
                if (!sActor.Status.Additions.ContainsKey("瘴气兵装"))
                {
                    瘴气兵装 buff = new 瘴气兵装(null, sActor, 15000);
                    SkillHandler.ApplyAddition(sActor, buff);
                }
            }
        }
    }
}
