DROP TABLE IF EXISTS `partner`;
CREATE TABLE `partner` (
  `apid` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `pid` int(10) unsigned NOT NULL,
  `lv` tinyint(3) unsigned NOT NULL,
  `tlv` tinyint(3) unsigned NOT NULL,
  `rb` tinyint(3) NOT NULL,
  `rank` tinyint(3) unsigned NOT NULL,
  `perkspoints` smallint(6) NOT NULL,
  `perk0` tinyint(3) unsigned NOT NULL,
  `perk1` tinyint(3) unsigned NOT NULL,
  `perk2` tinyint(3) unsigned NOT NULL,
  `perk3` tinyint(3) unsigned NOT NULL,
  `perk4` tinyint(3) unsigned NOT NULL,
  `perk5` tinyint(3) unsigned NOT NULL,
  `aimode` tinyint(3) unsigned NOT NULL,
  `basicai1` tinyint(3) unsigned NOT NULL,
  `basicai2` tinyint(3) unsigned NOT NULL,
  `hp` int(10) unsigned NOT NULL,
  `maxhp` int(10) unsigned NOT NULL,
  `mp` int(10) unsigned NOT NULL,
  `maxmp` int(10) unsigned NOT NULL,
  `sp` int(10) unsigned NOT NULL,
  `maxsp` int(10) unsigned NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `partnerai`;
CREATE TABLE `partnerai` (
  `apid` int(10) unsigned NOT NULL,
  `type` tinyint(3) unsigned DEFAULT NULL,
  `index` tinyint(3) unsigned DEFAULT NULL,
  `value` smallint(6) unsigned DEFAULT NULL,
  PRIMARY KEY (`apid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `partnerequip`;
CREATE TABLE `partnerequip` (
  `apid` int(10) unsigned NOT NULL,
  `type` tinyint(3) unsigned NOT NULL,
  `item_id` int(10) unsigned NOT NULL,
  `count` smallint(6) unsigned NOT NULL,
  PRIMARY KEY (`apid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `partnercube`;
CREATE TABLE `partnercube` (
  `apid` int(10) unsigned NOT NULL,
  `type` tinyint(3) unsigned NOT NULL,
  `unique_id` smallint(6) unsigned NOT NULL,
  PRIMARY KEY (`apid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
