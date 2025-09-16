CREATE DATABASE  IF NOT EXISTS `celulares` /*!40100 DEFAULT CHARACTER SET utf8mb3 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `celulares`;
-- MySQL dump 10.13  Distrib 8.0.42, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: celulares
-- ------------------------------------------------------
-- Server version	8.0.43

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
-- Table structure for table `caja`
--

DROP TABLE IF EXISTS `caja`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `caja` (
  `ID_caja` int NOT NULL AUTO_INCREMENT,
  `dia_hoy` date NOT NULL,
  `dinero inicial` decimal(12,2) NOT NULL,
  `dinero_final` decimal(12,2) DEFAULT NULL,
  `dia_cierre` date DEFAULT NULL,
  `empleado_apertura` int NOT NULL,
  `empleado_cierre` int DEFAULT NULL,
  PRIMARY KEY (`ID_caja`),
  KEY `empleado_apertura_idx` (`empleado_apertura`),
  KEY `empleado_cierre_idx` (`empleado_cierre`),
  CONSTRAINT `empleado_apertura` FOREIGN KEY (`empleado_apertura`) REFERENCES `empleados` (`ID_empleados`),
  CONSTRAINT `empleado_cierre` FOREIGN KEY (`empleado_cierre`) REFERENCES `empleados` (`ID_empleados`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `caja`
--

LOCK TABLES `caja` WRITE;
/*!40000 ALTER TABLE `caja` DISABLE KEYS */;
/*!40000 ALTER TABLE `caja` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clientes`
--

DROP TABLE IF EXISTS `clientes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clientes` (
  `ID_clientes` int NOT NULL AUTO_INCREMENT,
  `DNI` varchar(45) DEFAULT NULL,
  `ID_persona` int NOT NULL,
  PRIMARY KEY (`ID_clientes`),
  KEY `ID_persona_idx` (`ID_persona`),
  CONSTRAINT `IDpersona` FOREIGN KEY (`ID_persona`) REFERENCES `personas` (`ID_persona`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clientes`
--

LOCK TABLES `clientes` WRITE;
/*!40000 ALTER TABLE `clientes` DISABLE KEYS */;
INSERT INTO `clientes` VALUES (1,'40123456',21);
/*!40000 ALTER TABLE `clientes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `detalles_venta`
--

DROP TABLE IF EXISTS `detalles_venta`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `detalles_venta` (
  `ID_detalle` int NOT NULL AUTO_INCREMENT,
  `cantidad` int NOT NULL,
  `precio_unitario` int NOT NULL,
  `subtotal` int NOT NULL,
  `pagado` tinyint NOT NULL,
  `producto` int NOT NULL,
  `ID_venta` int NOT NULL,
  PRIMARY KEY (`ID_detalle`),
  KEY `ID_venta_idx` (`ID_venta`),
  KEY `IDproducto_idx` (`producto`),
  CONSTRAINT `IDproducto` FOREIGN KEY (`producto`) REFERENCES `productos` (`ID_productos`),
  CONSTRAINT `IDventa` FOREIGN KEY (`ID_venta`) REFERENCES `ventas` (`ID_ventas`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `detalles_venta`
--

LOCK TABLES `detalles_venta` WRITE;
/*!40000 ALTER TABLE `detalles_venta` DISABLE KEYS */;
/*!40000 ALTER TABLE `detalles_venta` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `empleados`
--

DROP TABLE IF EXISTS `empleados`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `empleados` (
  `ID_empleados` int NOT NULL AUTO_INCREMENT,
  `rol` int NOT NULL,
  `ID_persona` int NOT NULL,
  `contraseña` varchar(16) NOT NULL,
  PRIMARY KEY (`ID_empleados`),
  KEY `ID_rol_idx` (`rol`),
  KEY `ID_persona_idx` (`ID_persona`),
  CONSTRAINT `ID_persona` FOREIGN KEY (`ID_persona`) REFERENCES `personas` (`ID_persona`),
  CONSTRAINT `ID_rol` FOREIGN KEY (`rol`) REFERENCES `roles` (`ID_roles`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `empleados`
--

LOCK TABLES `empleados` WRITE;
/*!40000 ALTER TABLE `empleados` DISABLE KEYS */;
INSERT INTO `empleados` VALUES (1,3,1,'12345678'),(2,3,2,'Contr4señ4'),(3,3,3,''),(4,3,7,'1'),(5,3,8,'123'),(6,3,9,'1234'),(7,3,13,'1234'),(8,2,14,'1'),(9,1,15,'lucad'),(10,2,16,'asdafdsfaf'),(11,3,17,'2'),(12,3,18,'2sssdd');
/*!40000 ALTER TABLE `empleados` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `estados`
--

DROP TABLE IF EXISTS `estados`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `estados` (
  `ID_estados` int NOT NULL AUTO_INCREMENT,
  `nombre_estado` varchar(25) NOT NULL,
  PRIMARY KEY (`ID_estados`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `estados`
--

LOCK TABLES `estados` WRITE;
/*!40000 ALTER TABLE `estados` DISABLE KEYS */;
/*!40000 ALTER TABLE `estados` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `factura`
--

DROP TABLE IF EXISTS `factura`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `factura` (
  `num_factura` int NOT NULL AUTO_INCREMENT,
  `venta_individual` int NOT NULL,
  `venta_total` int NOT NULL,
  `pagado` tinyint NOT NULL,
  `fecha_venta` datetime NOT NULL,
  `tipo_factura` varchar(1) NOT NULL,
  PRIMARY KEY (`num_factura`),
  KEY `ventatotal_idx` (`venta_total`),
  KEY `ventaind_idx` (`venta_individual`),
  CONSTRAINT `ventaind` FOREIGN KEY (`venta_individual`) REFERENCES `detalles_venta` (`ID_detalle`),
  CONSTRAINT `ventatotal` FOREIGN KEY (`venta_total`) REFERENCES `ventas` (`ID_ventas`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `factura`
--

LOCK TABLES `factura` WRITE;
/*!40000 ALTER TABLE `factura` DISABLE KEYS */;
/*!40000 ALTER TABLE `factura` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `marcas`
--

DROP TABLE IF EXISTS `marcas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `marcas` (
  `ID_marcas` int NOT NULL AUTO_INCREMENT,
  `nombre_marca` varchar(45) NOT NULL,
  `modelo` varchar(45) NOT NULL,
  PRIMARY KEY (`ID_marcas`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `marcas`
--

LOCK TABLES `marcas` WRITE;
/*!40000 ALTER TABLE `marcas` DISABLE KEYS */;
/*!40000 ALTER TABLE `marcas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `movimientos`
--

DROP TABLE IF EXISTS `movimientos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `movimientos` (
  `ID_movimientos` int NOT NULL AUTO_INCREMENT,
  `fecha_mov` date NOT NULL,
  `ID_caja` int NOT NULL,
  `ID_venta` int NOT NULL,
  PRIMARY KEY (`ID_movimientos`),
  KEY `ID_caja_idx` (`ID_caja`),
  KEY `ID_venta_idx` (`ID_venta`),
  CONSTRAINT `IDcaja` FOREIGN KEY (`ID_caja`) REFERENCES `caja` (`ID_caja`),
  CONSTRAINT `IDventas` FOREIGN KEY (`ID_venta`) REFERENCES `ventas` (`ID_ventas`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `movimientos`
--

LOCK TABLES `movimientos` WRITE;
/*!40000 ALTER TABLE `movimientos` DISABLE KEYS */;
/*!40000 ALTER TABLE `movimientos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pagos`
--

DROP TABLE IF EXISTS `pagos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pagos` (
  `ID_pagos` int NOT NULL AUTO_INCREMENT,
  `metodo_pago` varchar(45) NOT NULL,
  PRIMARY KEY (`ID_pagos`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pagos`
--

LOCK TABLES `pagos` WRITE;
/*!40000 ALTER TABLE `pagos` DISABLE KEYS */;
INSERT INTO `pagos` VALUES (1,'Efectivo'),(2,'Transferencia'),(3,'Tarjeta de Crédito'),(4,'Tarjeta de Débito o Prepaga');
/*!40000 ALTER TABLE `pagos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `personas`
--

DROP TABLE IF EXISTS `personas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `personas` (
  `ID_persona` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(45) NOT NULL,
  `mail` varchar(45) NOT NULL,
  `telefono` varchar(10) NOT NULL,
  `apellido` varchar(45) DEFAULT NULL,
  `domicilio` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`ID_persona`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `personas`
--

LOCK TABLES `personas` WRITE;
/*!40000 ALTER TABLE `personas` DISABLE KEYS */;
INSERT INTO `personas` VALUES (1,'Luca','luca11maxi@gmail.com','3644223841',NULL,NULL),(2,'usuario','correo@mail.com','12',NULL,NULL),(3,'','','312',NULL,NULL),(4,'','','2',NULL,NULL),(5,'','','2213',NULL,NULL),(6,'','','23',NULL,NULL),(7,'12','1','1',NULL,NULL),(8,'a','1312','423',NULL,NULL),(9,'sos','123','5678',NULL,NULL),(10,'a','a','1',NULL,NULL),(11,'Usuario1','correo@gmail.com','3644',NULL,NULL),(12,'Usuario1','correo@gmail.com','3644',NULL,NULL),(13,'Usuario1','correo@gmail.com','3644',NULL,NULL),(14,'asdd','1','1',NULL,NULL),(15,'asd','oasd@asd.c','123',NULL,NULL),(16,'Luca222','asdasdasdasad@gmail.com','1231231',NULL,NULL),(17,'luca11','a@g.c','3',NULL,NULL),(18,'luca','asdasd@gmasd.com','123123',NULL,NULL),(20,'Provetech','provetech@gmail.com','3644123456',NULL,'Lopez y Planes 174'),(21,'Cliente1','correo@mail.com','3644888765',NULL,NULL);
/*!40000 ALTER TABLE `personas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `piezas_reparacion`
--

DROP TABLE IF EXISTS `piezas_reparacion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `piezas_reparacion` (
  `ID_reparacion` int NOT NULL,
  `ID_productos` int NOT NULL,
  `cantidad_usada` int NOT NULL,
  KEY `ID_productos_idx` (`ID_productos`),
  KEY `ID_reparacion_idx` (`ID_reparacion`),
  CONSTRAINT `IDproductos` FOREIGN KEY (`ID_productos`) REFERENCES `productos` (`ID_productos`),
  CONSTRAINT `IDreparacion` FOREIGN KEY (`ID_reparacion`) REFERENCES `reparaciones` (`ID_reparaciones`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `piezas_reparacion`
--

LOCK TABLES `piezas_reparacion` WRITE;
/*!40000 ALTER TABLE `piezas_reparacion` DISABLE KEYS */;
/*!40000 ALTER TABLE `piezas_reparacion` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `productos`
--

DROP TABLE IF EXISTS `productos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `productos` (
  `ID_productos` int NOT NULL AUTO_INCREMENT,
  `precio_costo` decimal(10,2) NOT NULL,
  `precio_venta` decimal(12,2) NOT NULL,
  `cantidad_stock` int NOT NULL,
  `proveedor` int NOT NULL,
  `marca` int NOT NULL,
  `tipo_producto` int NOT NULL,
  PRIMARY KEY (`ID_productos`),
  KEY `proveedor_pieza_idx` (`proveedor`),
  KEY `ID_marca_idx` (`marca`),
  KEY `ID_tipo_producto_idx` (`tipo_producto`),
  CONSTRAINT `ID_marca` FOREIGN KEY (`marca`) REFERENCES `marcas` (`ID_marcas`),
  CONSTRAINT `ID_tipo_producto` FOREIGN KEY (`tipo_producto`) REFERENCES `tipos_productos` (`ID_tipos_productos`),
  CONSTRAINT `proveedor_pieza` FOREIGN KEY (`proveedor`) REFERENCES `proveedores` (`ID_proveedores`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productos`
--

LOCK TABLES `productos` WRITE;
/*!40000 ALTER TABLE `productos` DISABLE KEYS */;
/*!40000 ALTER TABLE `productos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `proveedores`
--

DROP TABLE IF EXISTS `proveedores`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `proveedores` (
  `ID_proveedores` int NOT NULL AUTO_INCREMENT,
  `pagina` varchar(100) DEFAULT NULL,
  `ID_persona` int NOT NULL,
  PRIMARY KEY (`ID_proveedores`),
  KEY `ID_persona_idx` (`ID_persona`),
  CONSTRAINT `IDpersonas` FOREIGN KEY (`ID_persona`) REFERENCES `personas` (`ID_persona`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `proveedores`
--

LOCK TABLES `proveedores` WRITE;
/*!40000 ALTER TABLE `proveedores` DISABLE KEYS */;
INSERT INTO `proveedores` VALUES (1,'provetech.com.ar',20);
/*!40000 ALTER TABLE `proveedores` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reparaciones`
--

DROP TABLE IF EXISTS `reparaciones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reparaciones` (
  `ID_reparaciones` int NOT NULL AUTO_INCREMENT,
  `cliente` int NOT NULL,
  `tecnico_cargo` int NOT NULL,
  `dispotivo` int NOT NULL,
  `estado` int NOT NULL,
  `servicio` int NOT NULL,
  `fecha_ingreso` date NOT NULL,
  `fecha_entrega_real` date NOT NULL,
  `fecha_entrega_estimada` date DEFAULT NULL,
  `accesorios_entregados` varchar(250) DEFAULT NULL,
  `descripcion_estado` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`ID_reparaciones`),
  KEY `cliente_idx` (`cliente`),
  KEY `tecnico_cargo_idx` (`tecnico_cargo`),
  KEY `estado_idx` (`estado`),
  KEY `servicio_idx` (`servicio`),
  KEY `dispositivo_idx` (`dispotivo`),
  CONSTRAINT `cliente` FOREIGN KEY (`cliente`) REFERENCES `clientes` (`ID_clientes`),
  CONSTRAINT `dispositivo` FOREIGN KEY (`dispotivo`) REFERENCES `productos` (`ID_productos`),
  CONSTRAINT `estado` FOREIGN KEY (`estado`) REFERENCES `estados` (`ID_estados`),
  CONSTRAINT `servicio` FOREIGN KEY (`servicio`) REFERENCES `servicios` (`ID_servicios`),
  CONSTRAINT `tecnico_cargo` FOREIGN KEY (`tecnico_cargo`) REFERENCES `empleados` (`ID_empleados`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reparaciones`
--

LOCK TABLES `reparaciones` WRITE;
/*!40000 ALTER TABLE `reparaciones` DISABLE KEYS */;
/*!40000 ALTER TABLE `reparaciones` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roles` (
  `ID_roles` int NOT NULL AUTO_INCREMENT,
  `nombre_rol` varchar(20) NOT NULL,
  PRIMARY KEY (`ID_roles`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roles`
--

LOCK TABLES `roles` WRITE;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` VALUES (1,'administrador'),(2,'técnico'),(3,'recepcionista');
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `servicios`
--

DROP TABLE IF EXISTS `servicios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `servicios` (
  `ID_servicios` int NOT NULL AUTO_INCREMENT,
  `descripcion` longtext NOT NULL,
  `precio` decimal(10,2) NOT NULL,
  PRIMARY KEY (`ID_servicios`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `servicios`
--

LOCK TABLES `servicios` WRITE;
/*!40000 ALTER TABLE `servicios` DISABLE KEYS */;
/*!40000 ALTER TABLE `servicios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tipos_productos`
--

DROP TABLE IF EXISTS `tipos_productos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tipos_productos` (
  `ID_tipos_productos` int NOT NULL AUTO_INCREMENT,
  `nombre_producto` varchar(45) NOT NULL,
  PRIMARY KEY (`ID_tipos_productos`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tipos_productos`
--

LOCK TABLES `tipos_productos` WRITE;
/*!40000 ALTER TABLE `tipos_productos` DISABLE KEYS */;
INSERT INTO `tipos_productos` VALUES (1,'repuesto'),(2,'accesorio'),(3,'dispositivo');
/*!40000 ALTER TABLE `tipos_productos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ventas`
--

DROP TABLE IF EXISTS `ventas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ventas` (
  `ID_ventas` int NOT NULL AUTO_INCREMENT,
  `fecha_venta` date NOT NULL,
  `costo_total` decimal(12,2) NOT NULL,
  `costo_pagado` decimal(12,2) NOT NULL,
  `ID_cliente` int NOT NULL,
  `ID_empleado` int NOT NULL,
  `ID_caja` int NOT NULL,
  `tipo_pago` int NOT NULL,
  PRIMARY KEY (`ID_ventas`),
  KEY `ID_cliente_idx` (`ID_cliente`),
  KEY `ID_empleado_idx` (`ID_empleado`),
  KEY `ID_caja_idx` (`ID_caja`),
  KEY `tipo_pago_idx` (`tipo_pago`),
  CONSTRAINT `ID_caja` FOREIGN KEY (`ID_caja`) REFERENCES `caja` (`ID_caja`),
  CONSTRAINT `ID_cliente` FOREIGN KEY (`ID_cliente`) REFERENCES `clientes` (`ID_clientes`),
  CONSTRAINT `ID_empleado` FOREIGN KEY (`ID_empleado`) REFERENCES `empleados` (`ID_empleados`),
  CONSTRAINT `tipo_pago` FOREIGN KEY (`tipo_pago`) REFERENCES `pagos` (`ID_pagos`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ventas`
--

LOCK TABLES `ventas` WRITE;
/*!40000 ALTER TABLE `ventas` DISABLE KEYS */;
/*!40000 ALTER TABLE `ventas` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-09-16 18:42:42
