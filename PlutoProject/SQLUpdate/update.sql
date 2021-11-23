DROP TABLE IF EXISTS `partymember`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `partymember` (
  `party_id` int(10) unsigned NOT NULL,	
  `index` smallint(6) unsigned AUTO_INCREMENT,
  `char_id` int(10) unsigned NOT NULL,
  KEY `party_id` (`party_id`,`index`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

DROP TABLE IF EXISTS `stamp`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `stamp` (
  `char_id` int(10) unsigned NOT NULL DEFAULT '0',
  `stamp_id` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `value` smallint(6) NOT NULL DEFAULT '0',
  PRIMARY KEY (`char_id`,`stamp_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

DROP TABLE IF EXISTS `npcstates`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `npcstates` (
  `char_id` int(10) unsigned NOT NULL,
  `npc_id` int(10) unsigned NOT NULL,
  `state` TINYINT(1) NOT NULL default false,
  PRIMARY KEY (`char_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

DROP TABLE IF EXISTS `mobstates`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `mobstates` (
  `char_id` int(10) unsigned NOT NULL,
  `mob_id` int(10) unsigned NOT NULL,
  `state` TINYINT(1) NOT NULL default false,
  PRIMARY KEY (`char_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

DROP TABLE IF EXISTS `titlestates`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `titlestates` (
  `char_id` int(10) unsigned NOT NULL,
  `title_id` int(10) unsigned NOT NULL,
  `state` tinyint(3) NOT NULL default '0',
  PRIMARY KEY (`char_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

DROP TABLE IF EXISTS `titleprerequisites`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `titleprerequisites` (
  `char_id` int(10) unsigned NOT NULL,
  `prerequisite_id` int(10) unsigned NOT NULL,
  `progress` bigint(10) NOT NULL default '0',
  PRIMARY KEY (`char_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

ALTER TABLE `char` DROP COLUMN `dominionjlv`;
ALTER TABLE `char` DROP COLUMN `dstr`;
ALTER TABLE `char` DROP COLUMN `ddex`;
ALTER TABLE `char` DROP COLUMN `dintel`;
ALTER TABLE `char` DROP COLUMN `dvit`;
ALTER TABLE `char` DROP COLUMN `dagi`;
ALTER TABLE `char` DROP COLUMN `dmag`;
ALTER TABLE `char` DROP COLUMN `dstatpoint`;
ALTER TABLE `char` DROP COLUMN `dcexp`;
ALTER TABLE `char` DROP COLUMN `djexp`;
ALTER TABLE `char` DROP COLUMN `dreserve`;
ALTER TABLE `char` DROP COLUMN `depused`;
ALTER TABLE `char` DROP COLUMN `dcl`;
