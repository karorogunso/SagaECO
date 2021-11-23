using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations
{
    public partial class S21110 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (dActor.HP < dActor.MaxHP && !pc.Status.Additions.ContainsKey("P_瘴气兵装"))
            {
                SkillHandler.SendSystemMessage(pc, "目标未处于满血状态，无法使用『阴影之镰』。");
                return -150;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //显示特效
            SkillHandler.Instance.ShowEffectByActor(sActor, 7925);
            SkillHandler.Instance.ShowEffectOnActor(sActor, 7845);
            SkillHandler.Instance.ShowEffectOnActor(sActor, 7926);

            //瞬移至目标周围随机位置，并避免与目标位置重合
            Map map = SkillHandler.GetActorMap(sActor);
            short[] pos = map.GetRandomPosAroundPos(dActor.X, dActor.Y, 150);
            while (Math.Max(Math.Abs(pos[0] - dActor.X), Math.Abs(pos[1] - dActor.Y)) < 50)
                pos = map.GetRandomPosAroundPos(dActor.X, dActor.Y, 150);
            map.TeleportActor(sActor, pos[0], pos[1]);

            //面向目标
            ushort dir = map.CalcDir(sActor.X, sActor.Y, dActor.X, dActor.Y);
            map.MoveActor(Map.MOVE_TYPE.START, sActor, pos, dir, sActor.Speed, true, MoveType.CHANGE_DIR);

            //造成【混合】伤害
            float factor = 2f + level * 1f;
            SkillHandler.Instance.PhysicalAttack(sActor, new List<Actor> { dActor }, args, SkillHandler.DefType.Def, Elements.Dark, 0, factor, true);

            //附加DEBUFF
            OtherAddition skill = new OtherAddition(null, dActor, "魂之易伤", 10000);
            SkillHandler.ApplyBuffAutoRenew(dActor, skill);

            //增加魂之易伤的处理
            SkillHandler.OnCheckBuffList.Add("DEBUFF_魂之易伤", (s, t, d) =>
             {
                 if (t.Status.Additions.ContainsKey("魂之易伤"))
                 {
                     SkillHandler.Instance.ShowEffectOnActor(t, 8054);
                     d += (int)(d * 0.3f);
                 }
                 return d;
             });

            ////魄
            七魄.Show(sActor, dActor, "犬魄", 21900);
            //七魄.Show(sActor, dActor, "犬魄", 21191);
            //七魄.Show(sActor, dActor, "犬魄", 21192);
            //七魄.Show(sActor, dActor, "犬魄", 21193);
            //七魄.Show(sActor, dActor, "犬魄", 21194);
            //七魄.Show(sActor, dActor, "犬魄", 21195);
        }
    }
}
