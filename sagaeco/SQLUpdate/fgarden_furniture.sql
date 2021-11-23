/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50162
Source Host           : localhost:3306
Source Database       : cofeco

Target Server Type    : MYSQL
Target Server Version : 50162
File Encoding         : 65001

Date: 2013-08-01 15:33:51
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for `fgarden_furniture`
-- ----------------------------
DROP TABLE IF EXISTS `fgarden_furniture`;
CREATE TABLE `fgarden_furniture` (
  `fgarden_id` int(10) unsigned NOT NULL DEFAULT '0',
  `place` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `item_id` int(10) unsigned NOT NULL DEFAULT '0',
  `pict_id` int(10) unsigned NOT NULL DEFAULT '0',
  `x` smallint(6) NOT NULL DEFAULT '0',
  `y` smallint(6) NOT NULL DEFAULT '0',
  `z` smallint(6) NOT NULL DEFAULT '0',
  `xaxis` smallint(6) NOT NULL DEFAULT '0',
  `yaxis` smallint(6) NOT NULL DEFAULT '0',
  `zaxis` smallint(6) NOT NULL DEFAULT '0',
  `dir` smallint(5) unsigned NOT NULL DEFAULT '0',
  `motion` smallint(5) unsigned NOT NULL DEFAULT '0',
  `name` varchar(50) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL DEFAULT ' ',
  KEY `fgarden_id` (`fgarden_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of fgarden_furniture
-- ----------------------------
INSERT INTO `fgarden_furniture` VALUES ('4', '0', '31118700', '0', '69', '0', '110', '0', '0', '0', '0', '111', 'ヴァルハラ');
INSERT INTO `fgarden_furniture` VALUES ('5', '0', '31060700', '0', '234', '0', '-109', '0', '0', '0', '0', '111', 'キューブ１×１');
INSERT INTO `fgarden_furniture` VALUES ('5', '0', '31060700', '0', '105', '0', '57', '0', '0', '0', '0', '111', 'キューブ１×１');
INSERT INTO `fgarden_furniture` VALUES ('5', '0', '31060700', '0', '-313', '50', '-127', '0', '0', '0', '0', '111', 'キューブ１×１');
INSERT INTO `fgarden_furniture` VALUES ('5', '0', '31060700', '0', '-114', '0', '121', '0', '0', '0', '0', '111', 'キューブ１×１');
INSERT INTO `fgarden_furniture` VALUES ('5', '0', '31060700', '0', '-36', '0', '-143', '0', '0', '0', '0', '111', 'キューブ１×１');
