
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.Manager;
using SagaDB.Mob;
namespace SagaScript.M30210000
{
    public class S50006 : Event
    {
        public S50006()
        {
            this.EventID = 50006;
        }

        public override void OnEvent(ActorPC pc)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
            if (pc.Account.GMLevel > 200)
            {
                List<Actor> ac = map.GetActorsArea(pc, 200, false);
                foreach (var item in ac)
                {
                    if (item.type == ActorType.PC)
                        SagaMap.Skill.SkillHandler.Instance.PushBack(pc, item, 9);
                }
                switch (Select(pc, "怎么办呢？", "", "调试", "离开", "调试3转外观", "超级蓝莓", "移动搭档", "解封账号", "解封mac"))
                {
                    case 1:
                        /*
                        ActorMob mob2 = null;
                        foreach (ActorMob item in MobFactory.Instance.BossList)
                        {
                            if (item.Tasks.ContainsKey("Respawn"))
                            {
                                if (item.Name == "萝蕾拉")
                                    mob2 = item;
                            }
                        }
                        if(mob2 != null)
                        {
                            Say(pc, 131, ((SagaMap.ActorEventHandlers.MobEventHandler)mob2.e).AI.Mode.SkillOfShort[31162].CD.ToString() +
    ((SagaMap.ActorEventHandlers.MobEventHandler)mob2.e).AI.Mode.SkillOfLong[31162].CD.ToString());
                            ((SagaMap.ActorEventHandlers.MobEventHandler)mob2.e).AI.Mode.SkillOfShort[31162].CD = 29;
                            ((SagaMap.ActorEventHandlers.MobEventHandler)mob2.e).AI.Mode.SkillOfLong[31162].CD = 29;
                            Say(pc, 131, ((SagaMap.ActorEventHandlers.MobEventHandler)mob2.e).AI.Mode.SkillOfShort[31162].CD.ToString() +
                                ((SagaMap.ActorEventHandlers.MobEventHandler)mob2.e).AI.Mode.SkillOfLong[31162].CD.ToString());
                        }*/
                        byte xs = Global.PosX16to8(pc.X, map.Width);
                        byte ys = Global.PosY16to8(pc.Y, map.Height);
                        ActorMob mob5 = map.SpawnCustomMob(10000000, pc.MapID, 16925100, 0, 0, xs, ys, 1, 1, 0, 活动怪物.调试怪Info(), 活动怪物.调试怪AI(), null, 0)[0];
                        break;
                    case 2:
                        //Warp(pc, 91000999, 20, 20);
                        break;
                    case 3:
                        byte C1 = byte.Parse(InputBox(pc, "头？", InputType.Bank));
                        byte C2 = byte.Parse(InputBox(pc, "样子？", InputType.Bank));
                        byte C3 = byte.Parse(InputBox(pc, "颜色？", InputType.Bank));
                        pc.TailStyle = C1;
                        pc.WingStyle = C2;
                        pc.WingColor = C3;
                        map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, pc, true);
                        break;
                    case 4:
                        byte x = Global.PosX16to8(pc.X, map.Width);
                        byte y = Global.PosY16to8(pc.Y, map.Height);
                        ActorMob mob = map.SpawnCustomMob(10000000, pc.MapID, 30550000, 0, 0, x, y, 1, 1, 0, 活动怪物.超级活动蓝莓Info(), 活动怪物.超级活动蓝莓AI(), null, 0)[0];
                        mob.TInt["playersize"] = 5000;
                        map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, mob, true);
                        break;
                    case 5:
                        List<Actor> acs = new List<Actor>();
                        foreach (var item in map.Actors.Values)
                        {
                            if (item.type == ActorType.PARTNER)
                            {
                                short[] pos = new short[2];
                                pos[0] = 0;
                                pos[1] = 0;
                                map.MoveActor(SagaMap.Map.MOVE_TYPE.START, item, pos, 0, 1000, false, MoveType.VANISH2);
                            }
                            if (item.type == ActorType.SKILL && item != null)
                            {
                                SagaMap.Skill.SkillHandler.Instance.ShowEffectOnActor(item, 5121);
                                acs.Add(item);

                            }
                        }
                        for (int i = 0; i < acs.Count; i++)
                        {
                            map.DeleteActor(acs[i]);
                        }
                        break;
                    case 6:
                        VariableHolderA<string, int> dailyban = ScriptManager.Instance.VariableHolder.Adict["多开当日限制登录的账号"];
                        string user = InputBox(pc, "请输入要解封的账号", InputType.PetRename);
                        if (dailyban.ContainsKey(user))
                        {
                            dailyban.Remove(user);
                            Say(pc, 0, "解封OK");
                        }
                        else
                            Say(pc, 0, "输入的账号不存在");
                        break;
                    case 7:
                        VariableHolderA<string, int> macban = ScriptManager.Instance.VariableHolder.Adict["多开MAC限制"];
                        string mac = InputBox(pc, "请输入要解封的MAC地址", InputType.PetRename);
                        if (macban.ContainsKey(mac))
                        {
                            macban.Remove(mac);
                            Say(pc, 0, "解封OK");
                        }
                        else
                            Say(pc, 0, "输入的账号不存在");
                        break;
                }
            }
        }
    }
}

