--
-- Table structure for table `tamairelending`
--

DROP TABLE IF EXISTS `tamairelending`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tamairelending` (
  `char_id` int(10) unsigned NOT NULL DEFAULT '0',
  `postdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `comment` varchar(256) NOT NULL DEFAULT ' ',
  `renter1` int(10) unsigned NOT NULL DEFAULT '0',
  `renter2` int(10) unsigned NOT NULL DEFAULT '0',
  `renter3` int(10) unsigned NOT NULL DEFAULT '0',
  `renter4` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`char_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tamairerental`
--

DROP TABLE IF EXISTS `tamairerental`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tamairerental` (
  `char_id` int(10) unsigned NOT NULL DEFAULT '0',
  `rentdate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `lender` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`char_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;