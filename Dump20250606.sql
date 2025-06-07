-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: bank_aplication
-- ------------------------------------------------------
-- Server version	8.0.41

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
-- Table structure for table `accounts`
--

DROP TABLE IF EXISTS `accounts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `accounts` (
  `AccountID` int NOT NULL AUTO_INCREMENT,
  `CustomerID` int DEFAULT NULL,
  `AccountType` varchar(50) DEFAULT NULL,
  `Balance` decimal(15,2) DEFAULT NULL,
  `Currency` varchar(10) DEFAULT NULL,
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  `LinkedDebitAccountID` int DEFAULT NULL,
  `MonthlyTransferAmount` decimal(10,2) DEFAULT NULL,
  `LastTransferDate` date DEFAULT NULL,
  `CreditLimit` decimal(10,2) DEFAULT '0.00',
  `PaymentDueDate` date DEFAULT NULL,
  `MinimumPaymentDue` decimal(10,2) DEFAULT '0.00',
  PRIMARY KEY (`AccountID`),
  KEY `CustomerID` (`CustomerID`),
  CONSTRAINT `accounts_ibfk_1` FOREIGN KEY (`CustomerID`) REFERENCES `customers` (`CustomerID`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `accounts`
--

LOCK TABLES `accounts` WRITE;
/*!40000 ALTER TABLE `accounts` DISABLE KEYS */;
INSERT INTO `accounts` VALUES (1,NULL,'fhiw',362.00,'mofs','2025-05-18 07:21:55',NULL,NULL,NULL,0.00,NULL,0.00),(2,NULL,'dgr',1512.00,'23e2','2025-05-18 07:31:39',NULL,NULL,NULL,0.00,NULL,0.00),(3,NULL,'oytfu543w56r8y80i-0i9y7654e6ryu0--',2516.00,'jeoij','2025-05-19 05:12:28',NULL,NULL,NULL,0.00,NULL,0.00),(4,7,'fnowea[n',1707.00,'veg','2025-05-20 06:15:01',NULL,NULL,NULL,0.00,NULL,0.00),(5,9,'debit',7535.00,'bg levs','2025-05-21 06:43:32',NULL,NULL,NULL,0.00,NULL,0.00),(6,10,'idk',6319.00,'beden','2025-05-24 06:36:25',NULL,NULL,NULL,0.00,NULL,0.00),(8,12,'geb',1315.00,'euro','2025-05-29 06:02:30',NULL,NULL,NULL,0.00,NULL,0.00),(9,13,'d',6658.00,'e','2025-05-30 05:22:33',NULL,NULL,NULL,0.00,NULL,0.00),(10,14,'ewc',8839.00,'bg','2025-05-30 05:45:33',NULL,NULL,NULL,0.00,NULL,0.00),(11,16,'debit card',6960.00,' levs ','2025-05-30 06:02:00',NULL,NULL,NULL,0.00,NULL,0.00),(12,17,'saving',3668.00,'Euro','2025-05-31 07:10:07',5,NULL,'2025-06-03',0.00,NULL,0.00),(13,18,'credit ',0.00,'Euro','2025-06-05 06:01:16',NULL,NULL,NULL,0.00,NULL,0.00);
/*!40000 ALTER TABLE `accounts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customers`
--

DROP TABLE IF EXISTS `customers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `customers` (
  `CustomerID` int NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `DOB` date DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `PhoneNumber` varchar(20) DEFAULT NULL,
  `Address` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`CustomerID`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customers`
--

LOCK TABLES `customers` WRITE;
/*!40000 ALTER TABLE `customers` DISABLE KEYS */;
INSERT INTO `customers` VALUES (1,'Martin','Petkov','2007-05-21','gichkopikoto123@gmail.com','0098232323','lulin9'),(2,'Petur','Kolev','2007-08-05','pichokricho89@gmail.com','009876543','mladost1'),(3,'','','2025-05-18','','',''),(5,'fuiwh','wvmk','2025-05-07','f2','2543','fewe'),(6,'kjb;iu','hoi','2001-03-02','m\'j\'ko','55duer','ippip\''),(7,'petur','kolev','2025-05-20','gfd','btyj','ddd'),(9,'Martin','Ivanov','2007-11-06','marto111@gmail.com','0098765432','lulin10'),(10,'Hristo','Maimunski','2007-07-09','hristo7@gmail.com','12083294709','v guza na geografiqta'),(12,'Pavel','Penev','2007-06-07','mogjo[','43560','karlukovo'),(13,'Gosho','Pektot','2025-05-30','afgdf','23089','pod mosta'),(14,'Dimitur','Geylord','2007-08-12','mitko12@gmail.com','9845792','Karlukovo'),(16,'Kristqn','Ivanov','2025-05-30','kris12@gmail.com','+359 0887345234','lulin3'),(17,'Martin','Ivanov','2025-05-31','marto12@gmail.com','+3590766776','lulin5'),(18,'Martin','Kalchev','2025-06-05','malti12@gmail.com','+3598876543','htre');
/*!40000 ALTER TABLE `customers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `goals`
--

DROP TABLE IF EXISTS `goals`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `goals` (
  `GoalID` int NOT NULL AUTO_INCREMENT,
  `CustomerID` int NOT NULL,
  `Category` varchar(50) DEFAULT NULL,
  `TargetAmount` decimal(10,2) DEFAULT NULL,
  `Description` text,
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`GoalID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `goals`
--

LOCK TABLES `goals` WRITE;
/*!40000 ALTER TABLE `goals` DISABLE KEYS */;
INSERT INTO `goals` VALUES (1,9,'Food',300.00,'i wanna spend less than taht','2025-05-26 07:09:16'),(3,9,'Transport',100.00,'don\'t forget u fucking card','2025-05-27 05:48:24');
/*!40000 ALTER TABLE `goals` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `logincredentials`
--

DROP TABLE IF EXISTS `logincredentials`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `logincredentials` (
  `LoginID` int NOT NULL AUTO_INCREMENT,
  `CustomerID` int DEFAULT NULL,
  `Username` varchar(50) DEFAULT NULL,
  `PasswordHash` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`LoginID`),
  KEY `CustomerID` (`CustomerID`),
  CONSTRAINT `logincredentials_ibfk_1` FOREIGN KEY (`CustomerID`) REFERENCES `customers` (`CustomerID`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `logincredentials`
--

LOCK TABLES `logincredentials` WRITE;
/*!40000 ALTER TABLE `logincredentials` DISABLE KEYS */;
INSERT INTO `logincredentials` VALUES (1,NULL,'pichokricho','mikati3ba'),(2,NULL,'',''),(3,NULL,'t45egv','r34rf'),(4,NULL,'43qkt[','-0]o0'),(5,7,'petur','1234'),(6,9,'martobg','123456789'),(7,10,'prosqk','1234567'),(9,12,'pafel','Qwe12345!'),(10,13,'Teko Mecheto','Qqqqq123!'),(11,14,'Dermist','Kitty12!'),(12,16,'Krisko','Something12!'),(13,17,'martobgSavings','Qwe1209poi!'),(14,18,'Gicho','Zxc1209mnb!');
/*!40000 ALTER TABLE `logincredentials` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `transactions`
--

DROP TABLE IF EXISTS `transactions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `transactions` (
  `TransactionID` int NOT NULL AUTO_INCREMENT,
  `AccountID` int DEFAULT NULL,
  `Type` varchar(50) DEFAULT NULL,
  `Amount` decimal(15,2) DEFAULT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `Timestamp` datetime DEFAULT CURRENT_TIMESTAMP,
  `BalanceAfter` decimal(15,2) DEFAULT NULL,
  `Category` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`TransactionID`),
  KEY `AccountID` (`AccountID`),
  CONSTRAINT `transactions_ibfk_1` FOREIGN KEY (`AccountID`) REFERENCES `accounts` (`AccountID`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `transactions`
--

LOCK TABLES `transactions` WRITE;
/*!40000 ALTER TABLE `transactions` DISABLE KEYS */;
INSERT INTO `transactions` VALUES (1,5,'Withdrawal',64.00,'User withdrawal','2025-05-21 07:12:13',8000.00,NULL),(2,5,'Withdrawal',10.00,'xl duner','2025-05-22 05:24:24',7990.00,NULL),(7,6,'Withdrawal',229.00,'pomosht kum bednite','2025-05-24 07:04:42',6070.00,NULL),(8,5,'Deposit',229.00,'Transfer from user','2025-05-24 07:04:42',8219.00,NULL),(9,5,'Withdrawal',219.00,'e taka','2025-05-24 07:10:49',8000.00,NULL),(10,6,'Deposit',219.00,'Transfer from martobg','2025-05-24 07:10:49',6289.00,NULL),(11,5,'Withdrawal',10.00,'duner','2025-05-25 07:11:40',7990.00,'Food'),(12,5,'Withdrawal',10.00,'owed the monkey','2025-05-25 07:12:34',7980.00,'Other'),(13,6,'Deposit',10.00,'Transfer from martobg','2025-05-25 07:12:34',6299.00,'Transfer'),(14,5,'Withdrawal',10.00,'biliard','2025-05-25 07:32:23',7970.00,'Entertainment'),(15,5,'Withdrawal',20.00,'helping the poor','2025-05-25 07:33:01',7950.00,'Other'),(16,6,'Deposit',20.00,'Transfer from martobg','2025-05-25 07:33:01',6319.00,'Transfer'),(17,5,'Withdrawal',100.00,'headphones','2025-05-25 07:33:37',7850.00,'Utilities'),(18,5,'Withdrawal',50.00,'kfc is goated im sorry','2025-05-28 06:32:50',7800.00,'Food'),(19,5,'Withdrawal',150.00,'kfc is the goat','2025-05-28 07:31:17',7650.00,'Food'),(20,5,'Withdrawal',15.00,'card','2025-05-28 07:50:17',7635.00,'Transport'),(22,5,'Transfer Out',50.00,'Auto-transfer to savings','2025-06-03 06:21:17',7535.00,'Savings Transfer'),(23,12,'Transfer In',50.00,'Auto-transfer from debit','2025-06-03 06:21:17',3668.00,'Savings Transfer');
/*!40000 ALTER TABLE `transactions` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-06-06  5:36:01
