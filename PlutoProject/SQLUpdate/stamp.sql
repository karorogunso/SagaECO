
--
-- Table structure for table `stamp`
--

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