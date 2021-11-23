using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using SagaLib;
using SagaDB.Actor;

namespace SagaDB.Quests
{
    public class QuestFactory : Factory<QuestFactory, QuestInfo>
    {
        public QuestFactory()
        {
            this.loadingTab = "Loading Quest database";
            this.loadedTab = " quests loaded.";
            this.databaseName = "quest";
            this.FactoryType = FactoryType.XML;
        }

        protected override uint GetKey(QuestInfo item)
        {
            return item.ID;
        }

        protected override void ParseCSV(QuestInfo item, string[] paras)
        {
            throw new NotImplementedException();
        }

        protected override void ParseXML(XmlElement root, XmlElement current, QuestInfo item)
        {
            switch (root.Name.ToLower())
            {
                case "questdb":
                    item.GroupID = uint.Parse(root.Attributes[0].InnerText);
                    break;
                case "quest":
                    switch (current.Name.ToLower())
                    {
                        case "id":
                            item.ID = uint.Parse(current.InnerText);
                            break;
                        case "name":
                            item.Name = current.InnerText;
                            break;
                        case "type":
                            item.QuestType = (QuestType)Enum.Parse(typeof(QuestType), current.InnerText);
                            break;
                        case "time":
                            item.TimeLimit = int.Parse(current.InnerText);
                            break;
                        case "rewarditem":
                            item.RewardItem = uint.Parse(current.InnerText);
                            break;
                        case "rewardcount":
                            item.RewardCount = byte.Parse(current.InnerText);
                            break;
                        case "requiredquestpoints":
                            item.RequiredQuestPoint = byte.Parse(current.InnerText);
                            break;
                        case "dungeonid":
                            item.DungeonID = uint.Parse(current.InnerText);
                            break;
                        case "minlv":
                            item.MinLevel = byte.Parse(current.InnerText);
                            break;
                        case "maxlv":
                            item.MaxLevel = byte.Parse(current.InnerText);
                            break;
                        case "jobtype":
                            item.JobType = (JobType)Enum.Parse(typeof(JobType), current.InnerText);
                            break;
                        case "job":
                            item.Job = (PC_JOB)Enum.Parse(typeof(PC_JOB), current.InnerText);
                            break;
                        case "race":
                            item.Race = (PC_RACE)Enum.Parse(typeof(PC_RACE), current.InnerText);
                            break;
                        case "gender":
                            item.Gender = (PC_GENDER)Enum.Parse(typeof(PC_GENDER), current.InnerText);
                            break;
                        case "exp":
                            item.EXP = uint.Parse(current.InnerText);
                            break;
                        case "jexp":
                            item.JEXP = uint.Parse(current.InnerText);
                            break;
                        case "gold":
                            item.Gold = uint.Parse(current.InnerText);
                            break;
                        case "cp":
                            item.CP = uint.Parse(current.InnerText);
                            break;
                        case "fame":
                            item.Fame = uint.Parse(current.InnerText);
                            break;
                        case "mapid1":
                            item.MapID1 = uint.Parse(current.InnerText);
                            break;
                        case "mapid2":
                            item.MapID2 = uint.Parse(current.InnerText);
                            break;
                        case "mapid3":
                            item.MapID3 = uint.Parse(current.InnerText);
                            break;
                        case "objectid1":
                            item.ObjectID1 = uint.Parse(current.InnerText);
                            break;
                        case "count1":
                            item.Count1 = int.Parse(current.InnerText);
                            break;
                        case "objectid2":
                            item.ObjectID2 = uint.Parse(current.InnerText);
                            break;
                        case "count2":
                            item.Count2 = int.Parse(current.InnerText);
                            break;
                        case "objectid3":
                            item.ObjectID3 = uint.Parse(current.InnerText);
                            break;
                        case "count3":
                            item.Count3 = int.Parse(current.InnerText);
                            break;
                        case "party":
                            item.Party = bool.Parse(current.InnerText);
                            break;
                        case "npcsource":
                            item.NPCSource = uint.Parse(current.InnerText);
                            break;
                        case "npcdestination":
                            item.NPCDestination = uint.Parse(current.InnerText);
                            break;
                        case "questcountername":
                            item.QuestCounterName = current.InnerText;
                            break;
                    }
                    break;
            }
        }
    }
}
