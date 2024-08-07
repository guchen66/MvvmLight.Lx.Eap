/*
 Navicat Premium Data Transfer

 Source Server         : mysql
 Source Server Type    : MySQL
 Source Server Version : 80036
 Source Host           : localhost:3306
 Source Schema         : mvvmlighteap

 Target Server Type    : MySQL
 Target Server Version : 80036
 File Encoding         : 65001

 Date: 16/06/2024 20:13:25
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;


-- ----------------------------
-- Table structure for headerinfo
-- ----------------------------
DROP TABLE IF EXISTS `headerinfo`;
CREATE TABLE `headerinfo`  (
  `id` int(0) NOT NULL,
  `headername` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `navigattoview` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of headerinfo
-- ----------------------------
INSERT INTO `headerinfo` VALUES (1, 'RFID调试', 'MvvmLight.Lx.Controls.Shell.Views.RfidWindow');
INSERT INTO `headerinfo` VALUES (2, 'PLC调试', 'MvvmLight.Lx.Controls.Shell.Views.PlcDebugerWindow');
INSERT INTO `headerinfo` VALUES (3, '扫码枪调试', 'MvvmLight.Lx.Controls.Shell.Views.ScanCodeWindow');

-- ----------------------------
-- Table structure for userinfo
-- ----------------------------
DROP TABLE IF EXISTS `userinfo`;
CREATE TABLE `userinfo`  (
  `id` int(0) NOT NULL AUTO_INCREMENT,
  `name` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_german2_ci NULL DEFAULT NULL,
  `password` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_german2_ci NULL DEFAULT NULL,
  `email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_german2_ci NULL DEFAULT NULL,
  `createtime` datetime(0) NULL DEFAULT NULL,
  `updatetime` datetime(0) NULL DEFAULT NULL,
  `isdelete` tinyint(0) NULL DEFAULT NULL,
  `IsAdministrator` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_german2_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of userinfo
-- ----------------------------
INSERT INTO `userinfo` VALUES (1, '张三', '123', '156', '2024-06-04 22:45:24', '2024-06-07 10:11:46', 0, b'0');
INSERT INTO `userinfo` VALUES (2, '李四', '123456', '123@.com', '2024-06-07 09:10:03', '2024-06-07 10:11:50', 0, b'0');
INSERT INTO `userinfo` VALUES (3, '王五2', '21', NULL, '2024-06-07 15:31:03', '2024-06-07 15:31:10', 0, b'0');
INSERT INTO `userinfo` VALUES (4, '王五', '2', '2', NULL, NULL, 2, b'1');

SET FOREIGN_KEY_CHECKS = 1;
