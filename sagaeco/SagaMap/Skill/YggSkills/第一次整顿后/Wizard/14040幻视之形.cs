using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 幻视之形：选择一名角色，令你变成那名角色的样子。
    /// </summary>
    public class S14040 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (dActor.type != ActorType.PC)
                    return -14;
            return 0;
        }
        public void Proc(Actor sActor, Actor dActorS, SkillArg args, byte level)
        {
            const int misguideTime = 10000;
            //if (dActorS.type != ActorType.PC) //只能幻化玩家！！！！
            //    return;

            ActorPC Me = (ActorPC)sActor;
            ActorPC Tgt = (ActorPC)dActorS;
            if (Me.CharID == Tgt.CharID) //对自己使用则取消幻视
            {
                Me.appearance = new Appearance();
                Me.IllusionPictID = 0;
                //现在设置是解除幻化也不会解除误导，也就是误导依然会持续到10秒结束。也可以在这里重置误导目标。
                //Me.appearance.Equips.Add(SagaDB.Item.EnumEquipSlot.UPPER_BODY, null);
                Network.Client.MapClient.FromActorPC(Me).SendCharInfoUpdate();
                
            }
            else 
            {
                if(!Tgt.appearance.Illusion()) //目标没有任何幻化
                {
                    //只有幻化时会重置元素协调，解除幻化不会重置元素协调。
                    Me.TInt["水属性魔法释放"] = 0;
                    Me.TInt["火属性魔法释放"] = 0;

                    //误导状态下的角色造成的仇恨全部转移至幻化的目标角色
                    //出于平衡性角度考虑，也可以检测只有Tgt是队友才触发误导。否则可能被用来引怪害人。

                    sActor.TInt["误导"] = (int)Tgt.ActorID; //误导这个状态以后可能别的技能也能用到，所以单独提出来了，不然可以和幻化共用变量。
                    OtherAddition 误导 = new OtherAddition(args.skill, sActor, "误导", misguideTime);
                    误导.OnAdditionEnd += (s, e) =>
                    {
                        sActor.TInt["误导"] = 0;
                        SkillHandler.Instance.SetTitleProccess(sActor, 96,0);
                        Network.Client.MapClient.FromActorPC(Me).SendSystemMessage("误导效果结束了。");
                    };
                    SkillHandler.ApplyAddition(sActor, 误导);

                    Me.appearance = new Appearance()
                    {
                        Race = Tgt.Race,
                        Gender = Tgt.Gender,
                        Form = Tgt.Form,
                        TailStyle = Tgt.TailStyle,
                        WingStyle = Tgt.WingStyle,
                        WingColor = Tgt.WingColor,
                        HairStyle = Tgt.HairStyle,
                        HairColor = Tgt.HairColor,
                        Wig = Tgt.Wig,
                        Face = Tgt.Face,
                        MarionettePictID = Tgt.Marionette == null ? 0 : Tgt.Marionette.PictID
                    };
                    for (int j = 0; j < 15; j++)
                    {
                        if ((SagaDB.Item.EnumEquipSlot)j == SagaDB.Item.EnumEquipSlot.LEFT_HAND ||
                            (SagaDB.Item.EnumEquipSlot)j == SagaDB.Item.EnumEquipSlot.RIGHT_HAND ||
                            (SagaDB.Item.EnumEquipSlot)j == SagaDB.Item.EnumEquipSlot.PET)
                            continue;
                        if (Tgt.Inventory.Equipments.ContainsKey((SagaDB.Item.EnumEquipSlot)j))
                            Me.appearance.Equips.Add((SagaDB.Item.EnumEquipSlot)j, Tgt.Inventory.Equipments[(SagaDB.Item.EnumEquipSlot)j].Clone());
                        else
                            Me.appearance.Equips.Add((SagaDB.Item.EnumEquipSlot)j, null);
                    }
                    if (Tgt.PictID != 0) Me.IllusionPictID = Tgt.PictID;
                    //Me.appearance.Equips = new Dictionary<SagaDB.Item.EnumEquipSlot, SagaDB.Item.Item>(Tgt.Inventory.Equipments);

                    //Me.appearance.Equips.Remove(SagaDB.Item.EnumEquipSlot.LEFT_HAND);
                    //Me.appearance.Equips.Remove(SagaDB.Item.EnumEquipSlot.RIGHT_HAND);
                    //Me.appearance.Equips.Remove(SagaDB.Item.EnumEquipSlot.PET);

                    Network.Client.MapClient.FromActorPC(Me).SendCharInfoUpdate();
                }
                else
                {
                    Network.Client.MapClient.FromActorPC(Me).SendSystemMessage("不能对已经处于幻化状态下的目标使用幻视之形");
                }
            }
            //Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
    }
}

