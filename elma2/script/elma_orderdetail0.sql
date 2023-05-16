CREATE DATABASE  IF NOT EXISTS `elma` /*!40100 DEFAULT CHARACTER SET utf8mb3 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `elma`;
-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: elma
-- ------------------------------------------------------
-- Server version	8.0.32

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `orderdetail`
--

DROP TABLE IF EXISTS `orderdetail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orderdetail` (
  `orderId` int NOT NULL,
  `lineNo` int NOT NULL DEFAULT '10000',
  `serviceId` int NOT NULL,
  `serviceStatus` varchar(50) NOT NULL DEFAULT 'Подготовлена',
  `userLogin` varchar(50) DEFAULT NULL,
  `serviceDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`orderId`,`lineNo`),
  KEY `orderDetail_idx` (`serviceStatus`),
  CONSTRAINT `orderDetail` FOREIGN KEY (`serviceStatus`) REFERENCES `servicestatuses` (`status`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orderdetail`
--

LOCK TABLES `orderdetail` WRITE;
/*!40000 ALTER TABLE `orderdetail` DISABLE KEYS */;
INSERT INTO `orderdetail` VALUES (3,10000,10,'Подготовлена','admin','2023-05-15 20:29:26'),(3,10001,10,'Подготовлена','admin','2023-05-15 20:29:26'),(3,10101,20,'Подготовлена','admin','2023-05-15 21:53:14'),(3,10201,20,'Подготовлена','admin','2023-05-15 21:53:21'),(3,10301,20,'Подготовлена','admin','2023-05-15 21:53:22'),(3,10401,20,'Подготовлена','admin','2023-05-15 21:53:23'),(3,10501,20,'Подготовлена','admin','2023-05-15 21:53:23'),(3,10601,20,'Подготовлена','admin','2023-05-15 21:53:23'),(3,10701,20,'Подготовлена','admin','2023-05-15 21:53:24'),(3,10801,10,'Подготовлена','admin','2023-05-16 10:24:54'),(3,10901,10,'Подготовлена','admin','2023-05-16 10:24:58'),(4,100,20,'Выполнена','petrov','2023-05-15 21:19:10'),(4,110,10,'Подготовлена','admin','2023-05-15 21:19:10'),(4,210,10,'Подготовлена','admin','2023-05-15 21:54:56'),(4,310,20,'Выполнена','petrov','2023-05-15 21:55:05'),(4,410,10,'Выполняется',NULL,'2023-05-16 12:57:13');
/*!40000 ALTER TABLE `orderdetail` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-05-16 13:12:41
