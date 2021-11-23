using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Network.Client;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Global
{
    public class BlowOfFriendship : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 200, null);
            List<Actor> recoveraffected = new List<Actor>();
            List<Actor> damageaffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (!SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {

                    if (act.Buff.Dead != true)
                    {
                        recoveraffected.Add(act);
                    }
                    else if (act.Buff.TurningPurple != true && act.type == ActorType.PC)
                    {
                        ActorPC m = (ActorPC)act;
                        m.Buff.TurningPurple = true;
                        MapClient.FromActorPC(m).Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, m, true);
                        m.TInt["Revive"] = level;
                        MapClient.FromActorPC(m).EventActivate(0xF1000000);
                        m.TStr["Revive"] = sActor.Name;
                        MapClient.FromActorPC(m).SendSystemMessage(string.Format("玩家 {0} 正在请求你复活", sActor.Name));
                    }
                    if (!act.Status.Additions.ContainsKey("BlowOfFriendship") && !act.Buff.Undead)
                    {
                        DefaultBuff skill = new DefaultBuff(args.skill, act, "BlowOfFriendship", 120000);
                        skill.OnAdditionStart += this.StartEventHandler;
                        skill.OnAdditionEnd += this.EndEventHandler;
                        SkillHandler.ApplyAddition(act, skill);
                    }
                }
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.TurningRed = true;//找不到对应buff图标到底是哪个了，先用这个代替，反正也没有其他地方在用……
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.TurningRed = true;
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
