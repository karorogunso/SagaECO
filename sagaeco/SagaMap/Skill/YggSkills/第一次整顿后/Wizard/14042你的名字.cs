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
    /// 你的名字：与一名选定的队友交换位置（并短暂地交换身体）。
    /// </summary>
    public class S14042 : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {

            if (pc.Status.Additions.ContainsKey("你的名字CD"))
                return -30;

            if (pc.AInt["接受了搬运任务"] == 1)
            {
                SkillHandler.SendSystemMessage(pc, "由于接受了搬运任务，你无法使用该技能。");
                return -13;
            }
            if (dActor.type != ActorType.PC)
                return -14;
            else
            {
                ActorPC target = (ActorPC)dActor;
                if (target.ActorID==pc.ActorID)
                {
                    SkillHandler.SendSystemMessage(pc, "不能对自己使用这个技能");
                    return -14;
                }
                if (pc.Party == null || target.Party == null||pc.Party!= target.Party)
                {
                    SkillHandler.SendSystemMessage(pc, "对方不是你的队伍成员");
                    return -14;
                }
                if (target.AInt["接受了搬运任务"] == 1)
                {
                    SkillHandler.SendSystemMessage(pc, "由于对方接受了搬运任务，你无法对其使用该技能。");
                    return -14;
                }

            }
           
                return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            if (sActor.type == ActorType.PC && dActor.type == ActorType.PC  )
            {
                ActorPC Me = (ActorPC)sActor;
                ActorPC Tgt = (ActorPC)dActor;
                Me.TInt["水属性魔法释放"] = 0;
                Me.TInt["火属性魔法释放"] = 0;

                int cooldown = 60000;

                Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);

                OtherAddition cd = new OtherAddition(null, sActor, "你的名字CD", cooldown);
                cd.OnAdditionEnd += (s, e) =>
                {
                    Network.Client.MapClient.FromActorPC(Me).SendSystemMessage("『你的名字』冷却完毕。");
                };
                SkillHandler.ApplyAddition(sActor, cd);
                
                short[] posI = new short[2] { sActor.X, sActor.Y };
                map.TeleportActor(sActor, dActor.X, dActor.Y);
                map.TeleportActor(dActor, posI[0], posI[1]);

                //魔力负载随距离提升的部分废案了。因为听说服务端已经有距离判定了！

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
                if (Tgt.PictID != 0) Me.IllusionPictID = Tgt.PictID;
                Tgt.appearance = new Appearance()
                {
                    Race = Me.Race,
                    Gender = Me.Gender,
                    Form = Me.Form,
                    TailStyle = Me.TailStyle,
                    WingStyle = Me.WingStyle,
                    WingColor = Me.WingColor,
                    HairStyle = Me.HairStyle,
                    HairColor = Me.HairColor,
                    Wig = Me.Wig,
                    Face = Me.Face,
                    MarionettePictID = Me.Marionette == null ? 0 : Me.Marionette.PictID
                };
                if (Me.PictID != 0) Tgt.IllusionPictID = Me.PictID;
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

                    if (Me.Inventory.Equipments.ContainsKey((SagaDB.Item.EnumEquipSlot)j))
                        Tgt.appearance.Equips.Add((SagaDB.Item.EnumEquipSlot)j, Me.Inventory.Equipments[(SagaDB.Item.EnumEquipSlot)j].Clone());
                    else
                        Tgt.appearance.Equips.Add((SagaDB.Item.EnumEquipSlot)j, null);

                }

                Network.Client.MapClient.FromActorPC(Me).SendCharInfoUpdate();
                Network.Client.MapClient.FromActorPC(Tgt).SendCharInfoUpdate();

                OtherAddition 幻化1 = new OtherAddition(null, sActor, "你的名字幻化CD", 4000);
                幻化1.OnAdditionEnd += (s, e) =>
                {
                    Me.appearance = new Appearance();
                    Me.IllusionPictID = 0;
                    Network.Client.MapClient.FromActorPC(Me).SendCharInfoUpdate();
                };
                SkillHandler.ApplyAddition(sActor, 幻化1);
                OtherAddition 幻化2 = new OtherAddition(null, dActor, "你的名字幻化CD", 4000);
                幻化2.OnAdditionEnd += (s, e) =>
                {
                    Tgt.appearance = new Appearance();
                    Tgt.IllusionPictID = 0;
                    Network.Client.MapClient.FromActorPC(Tgt).SendCharInfoUpdate();
                };
                SkillHandler.ApplyAddition(dActor, 幻化2);
                dActor.TTime["被你名时间"] = DateTime.Now;
            }

        }

    }
}
