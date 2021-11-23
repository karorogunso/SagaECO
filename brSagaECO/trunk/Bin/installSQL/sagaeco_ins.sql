

SET FOREIGN_KEY_CHECKS=0;
DROP TABLE IF EXISTS `avar`;
CREATE TABLE `avar` (
  `account_id` int(10) unsigned NOT NULL,
  `values` blob NOT NULL,
  PRIMARY KEY  (`account_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `bbs`;
CREATE TABLE `bbs` (
  `postid` int(10) unsigned NOT NULL auto_increment,
  `bbsid` int(10) unsigned NOT NULL default '0',
  `postdate` datetime NOT NULL default '1970-01-01 00:00:00',
  `charid` int(10) unsigned NOT NULL default '0',
  `name` varchar(30) collate utf8_bin NOT NULL default ' ',
  `title` varchar(256) collate utf8_bin NOT NULL default ' ',
  `content` varchar(256) collate utf8_bin NOT NULL default ' ',
  PRIMARY KEY  (`postid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

DROP TABLE IF EXISTS `char`;
CREATE TABLE `char` (
  `char_id` int(10) unsigned NOT NULL auto_increment,
  `account_id` int(10) unsigned NOT NULL,
  `name` varchar(30) collate utf8_bin NOT NULL,
  `race` tinyint(4) unsigned NOT NULL,
  `gender` tinyint(3) unsigned NOT NULL,
  `hairStyle` tinyint(3) unsigned NOT NULL,
  `hairColor` tinyint(3) unsigned NOT NULL,
  `wig` smallint(3) unsigned NOT NULL,
  `face` smallint(3) unsigned NOT NULL,
  `job` tinyint(3) unsigned NOT NULL,
  `lv` tinyint(3) unsigned NOT NULL,
  `lv1` tinyint(3) unsigned NOT NULL default '0',
  `jointjlv` tinyint(3) unsigned NOT NULL default '1',
  `dreserve` tinyint(3) unsigned NOT NULL default '0',
  `jlv1` tinyint(3) unsigned NOT NULL,
  `jlv2x` tinyint(3) unsigned NOT NULL,
  `jlv2t` tinyint(3) unsigned NOT NULL,
  `jlv3` tinyint(3) unsigned NOT NULL default '0',
  `questRemaining` smallint(5) unsigned NOT NULL,
  `fame` int(10) unsigned NOT NULL default '0',
  `questresettime` datetime NOT NULL default '2000-01-01 00:00:00',
  `slot` tinyint(3) unsigned default NULL,
  `mapID` int(10) unsigned NOT NULL,
  `x` tinyint(3) unsigned NOT NULL,
  `y` tinyint(3) unsigned NOT NULL,
  `save_map` int(10) unsigned NOT NULL default '0',
  `save_x` tinyint(3) unsigned NOT NULL default '0',
  `save_y` tinyint(3) unsigned NOT NULL default '0',
  `dir` tinyint(3) unsigned NOT NULL,
  `hp` int(10) unsigned NOT NULL,
  `max_hp` int(10) unsigned NOT NULL,
  `mp` int(10) unsigned NOT NULL,
  `max_mp` int(10) unsigned NOT NULL,
  `sp` int(10) unsigned NOT NULL,
  `max_sp` int(10) unsigned NOT NULL,
  `ep` int(10) unsigned NOT NULL default '0',
  `eplogindate` datetime NOT NULL default '2000-01-01 00:00:00',
  `epgreetingdate` datetime NOT NULL default '2000-01-01 00:00:00',
  `epused` smallint(6) NOT NULL default '0',
  `depused` smallint(6) NOT NULL default '0',
  `tailStyle` tinyint(3) unsigned NOT NULL default '0',
  `wingStyle` tinyint(3) unsigned NOT NULL default '0',
  `wingColor` tinyint(3) unsigned NOT NULL default '0',
  `online` tinyint(4) NOT NULL default '0',
  `cl` smallint(6) NOT NULL default '9',
  `dcl` smallint(6) NOT NULL default '9',
  `str` smallint(5) unsigned NOT NULL,
  `dex` smallint(5) unsigned NOT NULL,
  `intel` smallint(5) unsigned NOT NULL,
  `vit` smallint(5) unsigned NOT NULL,
  `agi` smallint(6) unsigned NOT NULL,
  `mag` smallint(6) unsigned NOT NULL,
  `statspoint` smallint(5) unsigned NOT NULL default '0',
  `skillpoint` smallint(5) unsigned NOT NULL default '0',
  `skillpoint2x` smallint(5) unsigned NOT NULL default '0',
  `skillpoint2t` smallint(5) unsigned NOT NULL default '0',
  `skillpoint3` smallint(5) unsigned NOT NULL default '0',
  `explorerEXP` int(10) unsigned NOT NULL default '0',
  `gold` bigint(10) NOT NULL default '0',
  `cp` int(10) unsigned NOT NULL default '0',
  `ecoin` int(10) unsigned NOT NULL default '0',
  `cexp` bigint(10) unsigned NOT NULL default '0',
  `cexprate` float unsigned NOT NULL default '1',
  `jexp` bigint(10) unsigned NOT NULL default '0',
  `jexprate` float unsigned NOT NULL default '1',
  `jjexp` bigint(10) unsigned NOT NULL default '0',
  `wrp` int(11) NOT NULL default '0',
  `possession_target` int(10) unsigned NOT NULL default '0',
  `questid` int(10) unsigned NOT NULL default '0',
  `questendtime` datetime default NULL,
  `queststatus` tinyint(3) unsigned NOT NULL default '1',
  `questcurrentcount1` int(11) NOT NULL default '0',
  `questcurrentcount2` int(11) NOT NULL default '0',
  `questcurrentcount3` int(11) NOT NULL default '0',
  `party` int(10) unsigned NOT NULL default '0',
  `ring` int(10) unsigned NOT NULL default '0',
  `golem` int(10) unsigned NOT NULL default '0',
  `stamp1` smallint(6) NOT NULL default '0',
  `stamp2` smallint(6) NOT NULL default '0',
  `stamp3` smallint(6) NOT NULL default '0',
  `stamp4` smallint(6) NOT NULL default '0',
  `stamp5` smallint(6) NOT NULL default '0',
  `stamp6` smallint(6) NOT NULL default '0',
  `stamp7` smallint(6) NOT NULL default '0',
  `stamp8` smallint(6) NOT NULL default '0',
  `stamp9` smallint(6) NOT NULL default '0',
  `stamp10` smallint(6) NOT NULL default '0',
  `stamp11` smallint(6) NOT NULL default '0',
  `stamp12` smallint(6) NOT NULL default '0',
  `stamp13` smallint(6) NOT NULL default '0',
  `stamp14` smallint(6) NOT NULL default '0',
  `stamp15` smallint(6) NOT NULL default '0',
  `stamp16` smallint(6) NOT NULL default '0',
  `stamp17` smallint(6) NOT NULL default '0',
  `stamp18` smallint(6) NOT NULL default '0',
  `stamp19` smallint(6) NOT NULL default '0',
  `stamp20` smallint(6) NOT NULL default '0',
  `stamp21` smallint(6) NOT NULL default '0',
  `dailystamp` smallint(6) NOT NULL default '0',
  `dailystampdate` datetime NOT NULL default '2000-01-01 00:00:00',
  `title1` bigint(10) unsigned NOT NULL default '0',
  `title2` bigint(10) unsigned NOT NULL default '0',
  `title3` bigint(10) unsigned NOT NULL default '0',
  `title4` bigint(10) unsigned NOT NULL default '0',
  `title5` bigint(10) unsigned NOT NULL default '0',
  `title6` bigint(10) unsigned NOT NULL default '0',
  `title7` bigint(10) unsigned NOT NULL default '0',
  `title8` bigint(10) unsigned NOT NULL default '0',
  `title9` bigint(10) unsigned NOT NULL default '0',
  `title10` bigint(10) unsigned NOT NULL default '0',
  `newtitle1` bigint(10) unsigned NOT NULL default '0',
  `newtitle2` bigint(10) unsigned NOT NULL default '0',
  `newtitle3` bigint(10) unsigned NOT NULL default '0',
  `newtitle4` bigint(10) unsigned NOT NULL default '0',
  `newtitle5` bigint(10) unsigned NOT NULL default '0',
  `newtitle6` bigint(10) unsigned NOT NULL default '0',
  `newtitle7` bigint(10) unsigned NOT NULL default '0',
  `newtitle8` bigint(10) unsigned NOT NULL default '0',
  `newtitle9` bigint(10) unsigned NOT NULL default '0',
  `newtitle10` bigint(10) unsigned NOT NULL default '0',
  `WaitType` tinyint(3) unsigned NOT NULL default '0',
  `mainTitle` int(10) unsigned NOT NULL,
  `firstTitle` int(10) unsigned NOT NULL,
  `secondTitle` int(10) unsigned NOT NULL,
  `thirdTitle` int(10) unsigned NOT NULL,
  PRIMARY KEY  (`char_id`)
) ENGINE=MyISAM AUTO_INCREMENT=9 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

DROP TABLE IF EXISTS `cvar`;
CREATE TABLE `cvar` (
  `char_id` int(10) unsigned NOT NULL,
  `values` blob NOT NULL,
  PRIMARY KEY  (`char_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `ff`;
CREATE TABLE `ff` (
  `ff_id` int(10) unsigned NOT NULL auto_increment,
  `level` int(10) unsigned NOT NULL default '0',
  `name` varchar(50) character set utf8 collate utf8_bin NOT NULL,
  `content` varchar(50) character set utf8 collate utf8_bin NOT NULL,
  `ring_id` int(10) unsigned NOT NULL default '0',
  PRIMARY KEY  (`ff_id`)
) ENGINE=MyISAM AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `ff_copy`;
CREATE TABLE `ff_copy` (
  `ff_id` int(10) unsigned NOT NULL auto_increment,
  `level` int(10) unsigned NOT NULL default '0',
  `name` varchar(50) character set utf8 collate utf8_bin NOT NULL,
  `content` varchar(50) character set utf8 collate utf8_bin NOT NULL,
  `ring_id` int(10) unsigned NOT NULL default '0',
  PRIMARY KEY  (`ff_id`)
) ENGINE=MyISAM AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `ff_furniture`;
CREATE TABLE `ff_furniture` (
  `ff_id` int(10) unsigned NOT NULL default '0',
  `place` tinyint(3) unsigned NOT NULL default '0',
  `item_id` int(10) unsigned NOT NULL default '0',
  `pict_id` int(10) unsigned NOT NULL,
  `x` smallint(6) NOT NULL default '0',
  `y` smallint(6) NOT NULL default '0',
  `z` smallint(6) NOT NULL default '0',
  `xaxis` smallint(6) NOT NULL default '0',
  `yaxis` smallint(6) NOT NULL default '0',
  `zaxis` smallint(6) NOT NULL default '0',
  `motion` smallint(5) unsigned NOT NULL default '0',
  `name` varchar(50) character set utf8 collate utf8_bin NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `ff_furniture_copy`;
CREATE TABLE `ff_furniture_copy` (
  `ff_id` int(10) unsigned NOT NULL default '0',
  `place` tinyint(3) unsigned NOT NULL default '0',
  `item_id` int(10) unsigned NOT NULL default '0',
  `pict_id` int(10) unsigned NOT NULL,
  `x` smallint(6) NOT NULL default '0',
  `y` smallint(6) NOT NULL default '0',
  `z` smallint(6) NOT NULL default '0',
  `xaxis` smallint(6) NOT NULL default '0',
  `yaxis` smallint(6) NOT NULL default '0',
  `zaxis` smallint(6) NOT NULL default '0',
  `motion` smallint(5) unsigned NOT NULL default '0',
  `name` varchar(50) character set utf8 collate utf8_bin NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `fgarden`;
CREATE TABLE `fgarden` (
  `fgarden_id` int(10) unsigned NOT NULL auto_increment,
  `account_id` int(10) unsigned NOT NULL default '0',
  `part1` int(10) unsigned NOT NULL default '0',
  `part2` int(10) unsigned NOT NULL default '0',
  `part3` int(10) unsigned NOT NULL default '0',
  `part4` int(10) unsigned NOT NULL default '0',
  `part5` int(10) unsigned NOT NULL default '0',
  `part6` int(10) unsigned NOT NULL default '0',
  `part7` int(10) unsigned NOT NULL default '0',
  `part8` int(10) unsigned NOT NULL default '0',
  PRIMARY KEY  (`fgarden_id`),
  KEY `account_id` (`account_id`)
) ENGINE=MyISAM AUTO_INCREMENT=178 DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `fgarden_furniture`;
CREATE TABLE `fgarden_furniture` (
  `fgarden_id` int(10) unsigned NOT NULL default '0',
  `place` tinyint(3) unsigned NOT NULL default '0',
  `item_id` int(10) unsigned NOT NULL default '0',
  `pict_id` int(10) unsigned NOT NULL default '0',
  `x` smallint(6) NOT NULL default '0',
  `y` smallint(6) NOT NULL default '0',
  `z` smallint(6) NOT NULL default '0',
  `xaxis` smallint(6) NOT NULL default '0',
  `yaxis` smallint(6) NOT NULL default '0',
  `zaxis` smallint(6) NOT NULL default '0',
  `dir` smallint(5) unsigned NOT NULL default '0',
  `motion` smallint(5) unsigned NOT NULL default '0',
  `name` varchar(50) character set utf8 collate utf8_bin NOT NULL default ' ',
  KEY `fgarden_id` (`fgarden_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `friend`;
CREATE TABLE `friend` (
  `char_id` int(10) unsigned NOT NULL,
  `friend_char_id` int(10) unsigned NOT NULL,
  KEY `char_id` (`char_id`),
  KEY `friend_char_id` (`friend_char_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `inventory`;
CREATE TABLE `inventory` (
  `char_id` int(10) unsigned NOT NULL default '0',
  `data` blob,
  PRIMARY KEY  (`char_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

DROP TABLE IF EXISTS `levellimit`;
CREATE TABLE `levellimit` (
  `NowLevelLimit` int(10) unsigned NOT NULL default '0',
  `NextLevelLimit` int(10) unsigned NOT NULL default '0',
  `LastTimeLevelLimit` int(10) unsigned NOT NULL default '0',
  `SetNextUpLevel` int(10) unsigned NOT NULL default '0',
  `SetNextUpDays` int(10) unsigned NOT NULL default '0',
  `ReachTime` datetime NOT NULL default '1970-01-01 00:00:00',
  `NextTime` datetime NOT NULL default '1970-01-01 00:00:00',
  `FirstPlayer` int(10) unsigned NOT NULL default '0',
  `SecondPlayer` int(10) unsigned NOT NULL default '0',
  `ThirdPlayer` int(10) unsigned NOT NULL default '0',
  `FourthPlayer` int(10) unsigned NOT NULL default '0',
  `FifthPlayer` int(10) unsigned NOT NULL default '0',
  `IsLock` tinyint(3) unsigned NOT NULL default '0'
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `log`;
CREATE TABLE `log` (
  `eventType` varchar(20) collate utf8_bin NOT NULL,
  `eventTime` datetime NOT NULL,
  `src` varchar(50) collate utf8_bin NOT NULL,
  `dst` varchar(50) collate utf8_bin default NULL,
  `detail` varchar(1024) collate utf8_bin default NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

DROP TABLE IF EXISTS `login`;
CREATE TABLE `login` (
  `account_id` int(11) unsigned NOT NULL auto_increment,
  `username` varchar(30) collate utf8_bin NOT NULL,
  `password` varchar(32) collate utf8_bin NOT NULL,
  `deletepass` varchar(32) collate utf8_bin NOT NULL,
  `banned` tinyint(3) unsigned NOT NULL default '0',
  `gmlevel` tinyint(3) unsigned NOT NULL default '0',
  `bank` int(10) unsigned NOT NULL default '0',
  `vshop_points` int(10) unsigned NOT NULL default '0',
  `used_vshop_points` int(10) unsigned NOT NULL default '0',
  `lastip` varchar(20) collate utf8_bin default NULL,
  PRIMARY KEY  (`account_id`)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

DROP TABLE IF EXISTS `npcstates`;
CREATE TABLE `npcstates` (
  `char_id` int(10) unsigned NOT NULL,
  `data` blob NOT NULL,
  PRIMARY KEY  (`char_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `party`;
CREATE TABLE `party` (
  `party_id` int(10) unsigned NOT NULL auto_increment,
  `name` varchar(30) character set utf8 collate utf8_bin NOT NULL,
  `leader` int(10) unsigned NOT NULL default '0',
  `member1` int(10) unsigned NOT NULL default '0',
  `member2` int(10) unsigned NOT NULL default '0',
  `member3` int(10) unsigned NOT NULL default '0',
  `member4` int(10) unsigned NOT NULL default '0',
  `member5` int(10) unsigned NOT NULL default '0',
  `member6` int(10) unsigned NOT NULL default '0',
  `member7` int(10) unsigned NOT NULL default '0',
  `member8` int(10) unsigned NOT NULL default '0',
  PRIMARY KEY  (`party_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `questinfo`;
CREATE TABLE `questinfo` (
  `char_id` int(10) NOT NULL,
  `object_id` int(10) NOT NULL,
  `count` int(10) NOT NULL default '0',
  `totalcount` int(10) NOT NULL default '0',
  `infinish` tinyint(3) NOT NULL default '0',
  PRIMARY KEY  (`char_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `ring`;
CREATE TABLE `ring` (
  `ring_id` int(10) unsigned NOT NULL auto_increment,
  `name` varchar(50) collate utf8_bin NOT NULL default ' ',
  `leader` int(10) unsigned NOT NULL default '0',
  `fame` int(10) unsigned NOT NULL default '0',
  `emblem` blob,
  `emblem_date` datetime default NULL,
  PRIMARY KEY  (`ring_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

DROP TABLE IF EXISTS `ringmember`;
CREATE TABLE `ringmember` (
  `ring_id` int(10) unsigned NOT NULL,
  `char_id` int(10) unsigned NOT NULL,
  `right` int(10) unsigned NOT NULL,
  KEY `ring_id` (`ring_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `skill`;
CREATE TABLE `skill` (
  `char_id` int(10) unsigned NOT NULL,
  `skills` blob NOT NULL,
  `jobbasic` int(10) signed NOT NULL,

  `joblv` tinyint(3) unsigned NOT NULL default '0',
  `jobexp` bigint(10) unsigned NOT NULL default '0',
  `skillpoint` smallint(5) unsigned NOT NULL default '0',
  `skillpoint2x` smallint(5) unsigned NOT NULL default '0',
  `skillpoint2t` smallint(5) unsigned NOT NULL default '0',
  `skillpoint3` smallint(5) unsigned NOT NULL default '0',
  PRIMARY KEY  (`char_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `svar`;
CREATE TABLE `svar` (
  `name` varchar(25) character set utf8 collate utf8_bin NOT NULL,
  `type` tinyint(3) unsigned NOT NULL default '0',
  `content` varchar(25) character set utf8 collate utf8_bin NOT NULL,
  PRIMARY KEY  (`name`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `warehouse`;
CREATE TABLE `warehouse` (
  `account_id` int(10) unsigned NOT NULL default '0',
  `data` blob,
  PRIMARY KEY  (`account_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;