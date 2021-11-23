--
-- Table structure for table `apiitem`
--

DROP TABLE IF EXISTS `apiitem`;

CREATE TABLE `apiitem` (
  `apiitem_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `char_id` int(10) unsigned NOT NULL,
  `item_id` int(10) unsigned NOT NULL,
  `qty` smallint(5) unsigned NOT NULL DEFAULT '1',
  `request_time` datetime NOT NULL,
  `process_time` datetime DEFAULT NULL,
  `status` tinyint(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`apiitem_id`)
) ENGINE=MyISAM AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;