
--
-- Table structure for table `party`
--

DROP TABLE IF EXISTS `team`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `team` (
  `team_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(30) NOT NULL,
  `leader` int(10) unsigned NOT NULL DEFAULT '0',
  `member1` int(10) unsigned NOT NULL DEFAULT '0',
  `member2` int(10) unsigned NOT NULL DEFAULT '0',
  `member3` int(10) unsigned NOT NULL DEFAULT '0',
  `member4` int(10) unsigned NOT NULL DEFAULT '0',
  `member5` int(10) unsigned NOT NULL DEFAULT '0',
  `member6` int(10) unsigned NOT NULL DEFAULT '0',
  `member7` int(10) unsigned NOT NULL DEFAULT '0',
  `member8` int(10) unsigned NOT NULL DEFAULT '0',
  `member9` int(10) unsigned NOT NULL DEFAULT '0',
  `member10` int(10) unsigned NOT NULL DEFAULT '0',
  `member11` int(10) unsigned NOT NULL DEFAULT '0',
  `member12` int(10) unsigned NOT NULL DEFAULT '0',
  `member13` int(10) unsigned NOT NULL DEFAULT '0',
  `member14` int(10) unsigned NOT NULL DEFAULT '0',
  `member15` int(10) unsigned NOT NULL DEFAULT '0',
  `member16` int(10) unsigned NOT NULL DEFAULT '0',
  `member17` int(10) unsigned NOT NULL DEFAULT '0',
  `member18` int(10) unsigned NOT NULL DEFAULT '0',
  `member19` int(10) unsigned NOT NULL DEFAULT '0',
  `member20` int(10) unsigned NOT NULL DEFAULT '0',
  `member21` int(10) unsigned NOT NULL DEFAULT '0',
  `member22` int(10) unsigned NOT NULL DEFAULT '0',
  `member23` int(10) unsigned NOT NULL DEFAULT '0',
  `member24` int(10) unsigned NOT NULL DEFAULT '0',
  `member25` int(10) unsigned NOT NULL DEFAULT '0',
  `member26` int(10) unsigned NOT NULL DEFAULT '0',
  `member27` int(10) unsigned NOT NULL DEFAULT '0',
  `member28` int(10) unsigned NOT NULL DEFAULT '0',
  `member29` int(10) unsigned NOT NULL DEFAULT '0',
  `member30` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`team_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;