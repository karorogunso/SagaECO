using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S14022 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("无尽寒冬CD")) return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int freezetime = level * 2000 + 3000;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> targets = map.GetActorsArea(sActor, 500, false);
            //List<Actor> dest = new List<Actor>();
            foreach (var item in targets)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    //dest.Add(item);
                    Freeze fz = new Freeze(null, item, freezetime);
                    SkillHandler.ApplyAddition(item, fz);
                }
            }

            //纯特效部分
            List<short> X = new List<short>();
            List<short> Y = new List<short>();
            for (int i = 0; i < 30; i++)
            {
                short[] effectPos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 500);
                while (X.Contains(effectPos[0]) && Y.Contains(effectPos[1]))//防止重复位置特效
                    effectPos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 500);
                X.Add(effectPos[0]);
                Y.Add(effectPos[1]);
                if (i == 0)
                {
                    effectPos[0] = sActor.X;
                    effectPos[1] = sActor.Y;
                }

                byte x = SagaLib.Global.PosX16to8(effectPos[0], map.Width);
                byte y = SagaLib.Global.PosY16to8(effectPos[1], map.Height);
                map.SendEffect(sActor, x, y, 5104);
                //map.SendEffect(sActor, x, y, 5078);
                Activator a = new Activator(sActor, x, y);
                a.Activate();
            }


            //纯特效部分

            ActorPC Me = (ActorPC)sActor;
            if (Me.SP < Me.MaxSP)
            {
                Me.SP = Me.MaxSP;
                SkillHandler.Instance.ShowEffectOnActor(Me, 2507, sActor);
                Me.TInt["续命恢复"] = 0;
            }
            int cdtime = 240000;
            switch (Me.TInt["降温"])
            {
                case 1:
                    cdtime = 192000;
                    break;
                case 2:
                    cdtime = 156000;
                    break;
                case 3:
                    cdtime = 120000;
                    break;
            }
            //SkillHandler.Instance.StableBuffsHandler["无尽寒冬CD"].ApplyBuff(sActor, cdtime);
            OtherAddition skill = new OtherAddition(args.skill, sActor, "无尽寒冬CD", cdtime);
            skill.OnAdditionEnd += (s, e) =>
            {
                SkillHandler.Instance.ShowEffectOnActor(sActor, 5084);
                Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("[幻界·无尽寒冬] 可以再次使用了");
            };
            SkillHandler.ApplyAddition(sActor, skill);
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            byte x, y;
            Map map;
            public Activator(Actor caster,byte x,byte y)
            {
                this.caster = caster;
                this.x = x;
                this.y = y;
                dueTime = SagaLib.Global.Random.Next(0, 2000);
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
            }
            public override void CallBack()
            {
                map.SendEffect(caster, x, y, 5050);
                Deactivate();
            }
        }
    }
}
