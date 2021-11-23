using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 坚强些：给目标戴上绿帽子并让目标的发色变为绿色，为其施加圣盾加护效果。
    /// </summary>
    public class S9300 : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (dActor.type != ActorType.PC)
                return -14;

            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            if (dActor.type == ActorType.PC)
            {
                ActorPC Tgt = (ActorPC)dActor;
                sActor.TInt["水属性魔法释放"] = 0;
                sActor.TInt["火属性魔法释放"] = 0;

                Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
                Tgt.appearance = new Appearance();
                Tgt.appearance.HairColor = 6;
                Tgt.appearance.Equips.Add(SagaDB.Item.EnumEquipSlot.HEAD, SagaDB.Item.ItemFactory.Instance.GetItem(50142203).Clone());
                Tgt.appearance.Equips.Add(SagaDB.Item.EnumEquipSlot.HEAD_ACCE, null);
                Network.Client.MapClient.FromActorPC(Tgt).SendCharInfoUpdate();

                if (!Tgt.Status.Additions.ContainsKey("圣盾加护CD"))
                {
                    SkillHandler.Instance.ShowEffectOnActor(Tgt, 4254);
                    Tgt.SHIELDHP = Tgt.MaxHP;
                    OtherAddition skill = new OtherAddition(null, Tgt, "圣盾加护", 60000);
                    skill.OnAdditionStart += (s, e) =>
                    {
                        if (Tgt.type == ActorType.PC)
                            Network.Client.MapClient.FromActorPC((ActorPC)Tgt).SendSystemMessage("从 " + sActor.Name + " 获得了护盾值： " + Tgt.MaxHP + " 的『圣盾加护』效果。");
                        Tgt.Buff.三转DEF增强 = true;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, Tgt, true);
                    };
                    skill.OnAdditionEnd += (s, e) =>
                    {
                        Tgt.SHIELDHP = 0;
                        Tgt.Buff.三转DEF增强 = false;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, Tgt, true);
                    };
                    SkillHandler.ApplyAddition(Tgt, skill);

                    DefaultBuff skill2 = new DefaultBuff(args.skill, Tgt, "圣盾加护CD", 60000);
                    skill2.OnAdditionEnd += (s, e) =>
                    {
                        SkillHandler.Instance.ShowEffect(map, Tgt, 4267);
                        Network.Client.MapClient.FromActorPC((ActorPC)Tgt).SendSystemMessage("『圣盾加护』冷却结束！");
                    };
                    SkillHandler.ApplyAddition(Tgt, skill2);
                }
                if (sActor.Status.Additions.ContainsKey("坚强些"))
                    SkillHandler.RemoveAddition(sActor, "坚强些");
                OtherAddition 坚强些 = new OtherAddition(null, sActor, "坚强些", 10000);
                坚强些.OnAdditionEnd += (s, e) =>
                {
                    //Tgt.appearance.HairColor = 0;
                    Tgt.appearance = new Appearance();
                    Network.Client.MapClient.FromActorPC(Tgt).SendCharInfoUpdate();
                };
                SkillHandler.ApplyAddition(sActor, 坚强些);

            }

        }

    }
}
