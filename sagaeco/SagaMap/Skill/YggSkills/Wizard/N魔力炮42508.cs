using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Mob;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 魔力脉冲：单体多段无属性魔法攻击 16连
    /// </summary>
    class S42508 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("浮游炮CD")) return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            byte X = 0;
            byte Y = 0;

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            SkillHandler.Instance.GetTRoundPos(map, sActor, out X, out Y, 1);
            ActorSkill skill = new ActorSkill(args.skill, sActor);
            //ActorShadow skill = new ActorShadow((ActorPC)sActor);
            skill.MapID = sActor.MapID;
            skill.X = SagaLib.Global.PosX8to16(X, map.Width);
            skill.Y = SagaLib.Global.PosY8to16(Y, map.Height);
            skill.e = new ActorEventHandlers.NullEventHandler();
            skill.Speed = 300;
            skill.Name = "NOT_SHOW_DISAPPEAR";

            map.RegisterActor(skill);
            skill.invisble = false;
            map.OnActorVisibilityChange(skill);
            ActivatorAB timer = new ActivatorAB(skill, sActor, dActor, args, level);
            timer.Activate();


            sActor.EP += 500;
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
            if (sActor.type == ActorType.PC)
            {
                DefaultBuff skill2 = new DefaultBuff(args.skill, sActor, "浮游炮CD", 10000);
                skill2.OnAdditionStart += (s, e) =>
                {
                    s.Buff.三转鬼人斩 = true;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, s, true);
                    ((ActorPC)s).TInt["浮游炮暴击"] = 0;
                };
                skill2.OnAdditionEnd += (s, e) =>
                {
                    s.Buff.三转鬼人斩 = false;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, s, true);
                    ((ActorPC)s).TInt["浮游炮暴击"] = 0;
                };
                SkillHandler.ApplyAddition(sActor, skill2);
            }
        }
        #endregion

    }
    class ActivatorAB : MultiRunTask
    {
        ActorSkill BollSkill;
        Actor sActor;
        Actor dActor;
        Map map;
        SkillArg arg;
        bool isphy = false;
        int count = 0;
        int countMax = 20;
        float factor = 3f;
        public ActivatorAB(ActorSkill skill, Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            this.BollSkill = skill;
            this.sActor = sActor;
            this.dActor = dActor;
            this.arg = args.Clone();
            map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            this.period = 410;
            this.dueTime = 1200;
            if(sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.TInt["无属性状态"] == 1) isphy = true;
            }
            //if (sActor.Status.Additions.ContainsKey("属性契约")) factor = 1.5f;
            if (dActor.type == ActorType.PC) factor = 0.4f;
        }
        public override void CallBack()
        {
            ClientManager.EnterCriticalArea();
            try
            {
                if (count <= countMax && sActor !=null && sActor.MapID == BollSkill.MapID)
                {
                    MobAI ai = new MobAI(BollSkill, true);
                    List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(BollSkill.X, map.Width), SagaLib.Global.PosY16to8(BollSkill.Y, map.Height),
                        SagaLib.Global.PosX16to8(sActor.X, map.Width), SagaLib.Global.PosY16to8(sActor.Y, map.Height));
                    if (path.Count > 1)
                    {
                        int deltaX = path[0].x;
                        int deltaY = path[0].y;
                        if (path.Count == 1)
                        {
                            deltaX = SagaLib.Global.PosX16to8(sActor.X, map.Width);
                            deltaY = SagaLib.Global.PosY16to8(sActor.Y, map.Height);
                        }
                        MapNode node = new MapNode();
                        node.x = (byte)deltaX;
                        node.y = (byte)deltaY;
                        path.Add(node);
                        short[] pos = new short[2];
                        pos[0] = SagaLib.Global.PosX8to16(path[0].x, map.Width);
                        pos[1] = SagaLib.Global.PosY8to16(path[0].y, map.Height);
                        map.MoveActor(Map.MOVE_TYPE.START, BollSkill, pos, 0, 200);
                        //取得当前格子内的Actor
                    }

                    short DistanceA = Map.Distance(BollSkill, dActor);
                    if (DistanceA <= 900)//If mob is out the range that FireBolt can cast, skip out.
                    {
                        arg.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(3001, 1);
                        arg.argType = SkillArg.ArgType.Active;//Configure the skillarg of firebolt, the caster is the skillactor of subsituted groove.
                        arg.sActor = BollSkill.ActorID;
                        arg.dActor = dActor.ActorID;
                        arg.x = SagaLib.Global.PosX16to8(BollSkill.X, map.Width);
                        arg.y = SagaLib.Global.PosY16to8(BollSkill.Y, map.Height);

                        if (isphy) SkillHandler.Instance.MagicAttack(sActor, dActor, arg, SagaMap.Skill.SkillHandler.DefType.Def, Elements.Neutral, factor);
                        else SkillHandler.Instance.MagicAttack(sActor, dActor, arg, Elements.Neutral, factor);

                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg, BollSkill, true);
                        if (arg.flag.Contains(AttackFlag.DIE | AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT))//If mob died,terminate the proccess.
                        {
                            map.DeleteActor(BollSkill);
                            this.Deactivate();
                        }
                        if (sActor.type == ActorType.PC)
                        {
                            ((ActorPC)sActor).TInt["浮游炮暴击"] += 5;
                            SkillHandler.Instance.ShowVessel(sActor, 0, 0, ((ActorPC)sActor).TInt["浮游炮暴击"]);
                        }
                    }
                    count++;
                }
                else
                {
                    map.DeleteActor(BollSkill);
                    this.Deactivate();
                }
            }
            catch (Exception ex)
            {
                map.DeleteActor(BollSkill);
                this.Deactivate();
                Logger.ShowError(ex);
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
