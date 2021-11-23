# --------------------------------------------------------
# Host:                         localhost
# Server version:               5.0.67-community-nt
# Server OS:                    Win32
# HeidiSQL version:             6.0.0.3603
# Date/time:                    2016-04-26 22:55:54
# --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

# Dumping structure for table sagaeco.ff
DROP TABLE IF EXISTS `ff`;
CREATE TABLE IF NOT EXISTS `ff` (
  `ff_id` int(10) unsigned NOT NULL default '0',
  `ring_id` int(10) unsigned NOT NULL default '0',
  `name` varchar(50) character set utf8 collate utf8_bin NOT NULL default '',
  `content` text character set utf8 collate utf8_bin NOT NULL,
  `level` int(10) unsigned NOT NULL default '0',
  PRIMARY KEY  (`ff_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

# Data exporting was unselected.
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
