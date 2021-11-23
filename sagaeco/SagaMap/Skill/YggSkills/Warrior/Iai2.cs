using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaDB.Item;
namespace SagaMap.Skill.SkillDefinations.Swordman
{
    /// <summary>
    /// 居和
    /// </summary>
    public class Iai2 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }


        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            dActor = map.GetActor((uint)sActor.TInt["targetID"]);
            ushort dir = map.CalcDir(sActor.X, sActor.Y, dActor.X, dActor.Y);
            map.MoveActor(Map.MOVE_TYPE.STOP, sActor, new short[2] { sActor.X, sActor.Y }, map.CalcDir(sActor.X, sActor.Y, dActor.X, dActor.Y), sActor.Speed, true);

            bool cri = calucri(sActor);
            Elements ele = getele(sActor);
            uint effectid = geteffect(sActor, cri);
            uint sactoreffect = getsactoreffect(sActor, cri);

            float factor = 4.5f;
            if (cri)
                factor = 8f;

            if (ele != Elements.Holy)
            {
                factor = 6.5f;
                if (cri)
                    factor = 11f;
            }
            if (sActor.type == ActorType.MOB)
                factor = factor * 0.6f;
            args.argType = SkillArg.ArgType.Active;
            args.delayRate = (1000 - sActor.Status.aspd * 1.2f) * 0.001f;

            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, ele, factor);
            SkillHandler.Instance.ShowEffectOnActor(dActor, effectid);
            SkillHandler.Instance.ShowEffectOnActor(sActor, sactoreffect);


            sActor.SP += (uint)(sActor.MaxSP * 0.01f);
            if (sActor.SP > sActor.MaxSP)
                sActor.SP = sActor.MaxSP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
            if (ele == Elements.Holy)
            {
                List<Actor> targets = map.GetActorsArea(dActor, 200, false);
                List<Actor> dactors = new List<Actor>();
                foreach (var item in targets)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                    {
                        dactors.Add(item);
                    }
                }
                if (dactors.Count < 1) return;
                foreach (var item in dactors)
                {
                    SkillHandler.Instance.DoDamage(true, sActor, item, args, SkillHandler.DefType.Def, ele, 50, factor);
                    SkillHandler.Instance.ShowEffectOnActor(item, effectid);
                }
            }
        }
        bool calucri(Actor actor)
        {
            float crirate = 20f;
            crirate = SkillHandler.Instance.CalcRawCriRate(actor.Status.hit_critical, 10) / 2;
            if (crirate < 15f) crirate = 15f;
            if (crirate > 100f) crirate = 100f;
            if (SagaLib.Global.Random.Next(0, 100) < crirate) return true;//技能暴击
            return false;
        }
        Elements getele(Actor actor)
        {
            Elements ele = Elements.Fire;
            if (actor.Status.Additions.ContainsKey("旋风斩")) ele = Elements.Wind;
            if (actor.Status.Additions.ContainsKey("地裂斩")) ele = Elements.Earth;
            if (actor.Status.Additions.ContainsKey("炎龙斩")) ele = Elements.Fire;
            if (actor.Status.Additions.ContainsKey("寒冰斩")) ele = Elements.Water;
            if (actor.Status.Additions.ContainsKey("圣光之矛")) ele = Elements.Holy;
            return ele;
        }
        uint geteffect(Actor actor, bool iscri)
        {
            uint id = 8006;
            if (!iscri)
            {
                if (actor.Status.Additions.ContainsKey("炎龙斩")) id = 8031;
                if (actor.Status.Additions.ContainsKey("寒冰斩")) id = 5609;
                if (actor.Status.Additions.ContainsKey("旋风斩")) id = 8057;
                if (actor.Status.Additions.ContainsKey("地裂斩")) id = 4094;
                if (actor.Status.Additions.ContainsKey("圣光之矛")) id = 8050;
            }
            else
            {
                if (actor.Status.Additions.ContainsKey("炎龙斩")) id = 5205;
                if (actor.Status.Additions.ContainsKey("寒冰斩")) id = 5284;
                if (actor.Status.Additions.ContainsKey("旋风斩")) id = 5213;
                if (actor.Status.Additions.ContainsKey("地裂斩")) id = 5144;
                if (actor.Status.Additions.ContainsKey("圣光之矛")) id = 5288;
            }
            return id;
        }
        uint getsactoreffect(Actor actor, bool iscri)
        {
            uint id = 4012;
            if (actor.Status.Additions.ContainsKey("炎龙斩")) id = 0;
            if (actor.Status.Additions.ContainsKey("寒冰斩")) id = 0;
            if (actor.Status.Additions.ContainsKey("旋风斩")) id = 0;
            if (actor.Status.Additions.ContainsKey("地裂斩")) id = 0;
            if (actor.Status.Additions.ContainsKey("圣光之矛")) id = 0;
            return id;
        }
        #endregion
    }
}
