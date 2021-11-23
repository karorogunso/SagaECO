using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaMap.Skill.SkillDefinations;
using SagaMap.Network.Client;

namespace SagaMap.Skill.Additions.Global
{
    public class 食物效果 : ICDBuff
    {
        void ICDBuff.ApplyBuff(Actor actor, int lifetime)
        {
            string BuffName = "食物效果";
            SkillHandler.RemoveAddition(actor, "食物效果");
            StableAddition cd = new StableAddition(null, actor, BuffName, lifetime);
            cd.OnAdditionStart += 开始效果;
            cd.OnAdditionEnd += 结束效果;
            SkillHandler.ApplyAddition(actor, cd);
        }
        private void 开始效果(Actor actor, StableAddition skill)
        {
            ActorPC pc = actor as ActorPC;
            MapClient client = MapClient.FromActorPC(pc);
            SetFoodBuff(actor, true);
            CalcFoodStatus(pc);
            PC.StatusFactory.Instance.CalcStatus(pc);
            client.SendCharInfoUpdate();
            SkillHandler.Instance.ShowEffectOnActor(pc, 7142);
        }
        private void 结束效果(Actor actor, StableAddition skill)
        {
            ActorPC pc = actor as ActorPC;
            MapClient client = MapClient.FromActorPC(pc);
            SetFoodBuff(actor, false);
            PC.StatusFactory.Instance.ClearFoodStatus(pc);
            PC.StatusFactory.Instance.CalcStatus(pc);
            client.SendCharInfoUpdate();
        }
        void SetFoodBuff(Actor actor, bool set)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.食物效果 = set;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void CalcFoodStatus(ActorPC pc)
        {
            PC.StatusFactory.Instance.ClearFoodStatus(pc);
            pc.Status.str_food = (short)pc.CInt["F_STR提升"];
            pc.Status.mag_food = (short)pc.CInt["F_MAG提升"];
            pc.Status.vit_food = (short)pc.CInt["F_VIT提升"];
            pc.Status.int_food = (short)pc.CInt["F_INT提升"];
            pc.Status.agi_food = (short)pc.CInt["F_AGI提升"];
            pc.Status.dex_food = (short)pc.CInt["F_DEX提升"];
            pc.Status.hp_food = (short)pc.CInt["F_MaxHP提升"];
            //TODO:完善其他属性
            string text = "食物效果令你有了 ";
            foreach (var item in pc.CInt)
                if (item.Key.Substring(0, 2) == "F_" && pc.CInt[item.Key] > 0)
                    text += item.Key.Substring(2) + ":" + item.Value + " ";
            if (text != "食物效果令你有了" && text.Length > 0)
                MapClient.FromActorPC(pc).SendSystemMessage(text);
        }
    }
}
