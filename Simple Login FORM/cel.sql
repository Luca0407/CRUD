-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: celulares
-- ------------------------------------------------------
-- Server version	8.0.39

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
  `empleado_apertura` int DEFAULT NULL,
  `empleado_cierre` int DEFAULT NULL,
  PRIMARY KEY (`ID_caja`),
  KEY `empleado_apertura_idx` (`empleado_apertura`),
  KEY `empleado_cierre_idx` (`empleado_cierre`),
  CONSTRAINT `empleado_apertura` FOREIGN KEY (`empleado_apertura`) REFERENCES `empleados` (`ID_empleados`) ON DELETE SET NULL,
  CONSTRAINT `empleado_cierre` FOREIGN KEY (`empleado_cierre`) REFERENCES `empleados` (`ID_empleados`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `caja`
--

LOCK TABLES `caja` WRITE;
/*!40000 ALTER TABLE `caja` DISABLE KEYS */;
INSERT INTO `caja` VALUES (1,'2025-11-04',0.00,NULL,NULL,1,NULL);
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
  `ID_persona` int DEFAULT NULL,
  `cuil` varchar(11) DEFAULT NULL,
  PRIMARY KEY (`ID_clientes`),
  KEY `ID_persona_idx` (`ID_persona`),
  CONSTRAINT `IDpersona` FOREIGN KEY (`ID_persona`) REFERENCES `personas` (`ID_persona`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=145 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clientes`
--

LOCK TABLES `clientes` WRITE;
/*!40000 ALTER TABLE `clientes` DISABLE KEYS */;
INSERT INTO `clientes` VALUES (109,'1235456789',26,NULL),(124,'12345567',104,'20123123434'),(126,'12312312',106,'20123123120'),(138,'123',99,NULL),(141,'45678911',119,NULL),(143,'10666616',122,NULL),(144,'11223124',123,NULL);
/*!40000 ALTER TABLE `clientes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `compras`
--

DROP TABLE IF EXISTS `compras`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `compras` (
  `idcompras` int NOT NULL AUTO_INCREMENT,
  `fecha` date DEFAULT NULL,
  `provedor` int DEFAULT NULL,
  `producto` int DEFAULT NULL,
  `cantidad` int DEFAULT NULL,
  `sub_total` float DEFAULT NULL,
  PRIMARY KEY (`idcompras`),
  KEY `provedor_idx` (`provedor`),
  KEY `producto_idx` (`producto`),
  CONSTRAINT `producto` FOREIGN KEY (`producto`) REFERENCES `productos_genericos` (`ID_nombre_productos`) ON DELETE SET NULL,
  CONSTRAINT `provedor` FOREIGN KEY (`provedor`) REFERENCES `proveedores` (`ID_proveedores`) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `compras`
--

LOCK TABLES `compras` WRITE;
/*!40000 ALTER TABLE `compras` DISABLE KEYS */;
/*!40000 ALTER TABLE `compras` ENABLE KEYS */;
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
  `producto` int DEFAULT NULL,
  `ID_venta` int DEFAULT NULL,
  `num_factura` int DEFAULT NULL,
  PRIMARY KEY (`ID_detalle`),
  KEY `ID_venta_idx` (`ID_venta`),
  KEY `IDproducto_idx` (`producto`),
  KEY `num_factura_idx` (`num_factura`),
  CONSTRAINT `IDproducto` FOREIGN KEY (`producto`) REFERENCES `productos` (`ID_productos`) ON DELETE SET NULL,
  CONSTRAINT `IDventa` FOREIGN KEY (`ID_venta`) REFERENCES `ventas` (`ID_ventas`) ON DELETE SET NULL,
  CONSTRAINT `num_factura` FOREIGN KEY (`num_factura`) REFERENCES `facturas` (`num_factura`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `detalles_venta`
--

LOCK TABLES `detalles_venta` WRITE;
/*!40000 ALTER TABLE `detalles_venta` DISABLE KEYS */;
INSERT INTO `detalles_venta` VALUES (1,1,500000,500000,1,1,1,NULL),(2,1,4000,4000,1,NULL,2,NULL),(3,1,300000,300000,1,2,3,NULL),(4,1,500000,500000,1,1,4,NULL),(5,1,500000,500000,1,1,5,NULL),(6,1,2500,2500,1,NULL,6,NULL),(7,2,3000,6000,1,12,7,NULL),(8,1,2500,2500,1,NULL,8,NULL);
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
  `rol` int DEFAULT NULL,
  `ID_persona` int DEFAULT NULL,
  `contraseña` varchar(16) NOT NULL,
  `activo` tinyint NOT NULL,
  PRIMARY KEY (`ID_empleados`),
  KEY `ID_rol_idx` (`rol`),
  KEY `ID_persona_idx` (`ID_persona`),
  CONSTRAINT `ID_persona` FOREIGN KEY (`ID_persona`) REFERENCES `personas` (`ID_persona`) ON DELETE SET NULL,
  CONSTRAINT `ID_rol` FOREIGN KEY (`rol`) REFERENCES `roles` (`ID_roles`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `empleados`
--

LOCK TABLES `empleados` WRITE;
/*!40000 ALTER TABLE `empleados` DISABLE KEYS */;
INSERT INTO `empleados` VALUES (1,1,1,'12345678',1),(13,2,124,'12341234',1),(14,3,125,'56785678',1);
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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `estados`
--

LOCK TABLES `estados` WRITE;
/*!40000 ALTER TABLE `estados` DISABLE KEYS */;
INSERT INTO `estados` VALUES (1,'en espera'),(2,'en proceso'),(3,'terminado');
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
  `venta_total` int NOT NULL,
  `pagado` tinyint NOT NULL,
  `fecha_venta` datetime NOT NULL,
  `tipo_factura` varchar(1) NOT NULL,
  PRIMARY KEY (`num_factura`)
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
-- Table structure for table `facturas`
--

DROP TABLE IF EXISTS `facturas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `facturas` (
  `num_factura` int NOT NULL AUTO_INCREMENT,
  `venta_total` int NOT NULL,
  `pagado` tinyint NOT NULL,
  `fecha_venta` datetime NOT NULL,
  `tipo_factura` varchar(1) NOT NULL,
  PRIMARY KEY (`num_factura`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `facturas`
--

LOCK TABLES `facturas` WRITE;
/*!40000 ALTER TABLE `facturas` DISABLE KEYS */;
/*!40000 ALTER TABLE `facturas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ingreso_reparacion`
--

DROP TABLE IF EXISTS `ingreso_reparacion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ingreso_reparacion` (
  `idingreso_reparacion` int NOT NULL AUTO_INCREMENT,
  `descripcion` varchar(250) DEFAULT NULL,
  `producto` int DEFAULT NULL,
  `cantidad` int DEFAULT NULL,
  `cliente` int DEFAULT NULL,
  PRIMARY KEY (`idingreso_reparacion`),
  KEY `Productos_idx` (`producto`),
  KEY `client_idx` (`cliente`),
  CONSTRAINT `client` FOREIGN KEY (`cliente`) REFERENCES `clientes` (`ID_clientes`) ON DELETE SET NULL,
  CONSTRAINT `Productos` FOREIGN KEY (`producto`) REFERENCES `productos` (`ID_productos`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ingreso_reparacion`
--

LOCK TABLES `ingreso_reparacion` WRITE;
/*!40000 ALTER TABLE `ingreso_reparacion` DISABLE KEYS */;
INSERT INTO `ingreso_reparacion` VALUES (1,'no funciona el pin ',25,1,143),(2,'flex roto',27,1,109),(3,'jajksdkasdkad',16,1,138);
/*!40000 ALTER TABLE `ingreso_reparacion` ENABLE KEYS */;
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
  PRIMARY KEY (`ID_marcas`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `marcas`
--

LOCK TABLES `marcas` WRITE;
/*!40000 ALTER TABLE `marcas` DISABLE KEYS */;
INSERT INTO `marcas` VALUES (1,'Iphone'),(2,'LG'),(3,'Samsung'),(4,'Motorola'),(5,'Xiaomi'),(6,'Huawei'),(7,'Nokia'),(8,'Ipad'),(9,'Universal');
/*!40000 ALTER TABLE `marcas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `modelos`
--

DROP TABLE IF EXISTS `modelos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `modelos` (
  `ID_modelos` int NOT NULL AUTO_INCREMENT,
  `nombre_modelo` varchar(35) NOT NULL,
  PRIMARY KEY (`ID_modelos`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `modelos`
--

LOCK TABLES `modelos` WRITE;
/*!40000 ALTER TABLE `modelos` DISABLE KEYS */;
INSERT INTO `modelos` VALUES (1,'X'),(2,'K50S'),(3,'Redmi Note 12'),(4,'P30 Lite'),(5,'Galaxy A54'),(6,'G13'),(7,'S10'),(8,'Universal');
/*!40000 ALTER TABLE `modelos` ENABLE KEYS */;
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
  `ID_caja` int DEFAULT NULL,
  `ID_venta` int DEFAULT NULL,
  PRIMARY KEY (`ID_movimientos`),
  KEY `ID_caja_idx` (`ID_caja`),
  KEY `ID_venta_idx` (`ID_venta`),
  CONSTRAINT `IDcaja` FOREIGN KEY (`ID_caja`) REFERENCES `caja` (`ID_caja`) ON DELETE SET NULL,
  CONSTRAINT `IDventas` FOREIGN KEY (`ID_venta`) REFERENCES `ventas` (`ID_ventas`) ON DELETE SET NULL
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
  `nombre` varchar(20) NOT NULL,
  `apellido` varchar(20) DEFAULT NULL,
  `mail` varchar(45) NOT NULL,
  `telefono` varchar(10) NOT NULL,
  `domicilio` varchar(45) DEFAULT NULL,
  `tipo` varchar(1) NOT NULL,
  PRIMARY KEY (`ID_persona`)
) ENGINE=InnoDB AUTO_INCREMENT=127 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `personas`
--

LOCK TABLES `personas` WRITE;
/*!40000 ALTER TABLE `personas` DISABLE KEYS */;
INSERT INTO `personas` VALUES (1,'Luca','Elizondo','luca11maxi@gmail.com','3644223841','San Juan S/N','e'),(20,'Provetech',NULL,'provetech@gmail.com','3644123456','Lopez y Planes 174','p'),(26,'Juan','Aurelio','mail@gmail.com','2345677889','Manzana 53, Parcela 18, Barrio Rucci','c'),(32,'TecnoSur','Distribuciones','contacto@tecnosur.com.ar','3415551111','Av. Rivadavia 1234','p'),(33,'InsumosGlobal','Mayorista','ventas@insumosglobal.com.ar','3415552222','Córdoba 567','p'),(34,'SoftArg','Proveedores','info@softarg.com.ar','3415553333','San Martín 890','p'),(99,'Martín','Fernández','martin.fernandez@mail.com','2352345233','Urquiza 30312','c'),(104,'Pedro','Pica piedras','asdads@sm.com','1223134123','34','c'),(106,'Albert','Camus','deotrolado@gmail.com','4564564564','sisi nono','c'),(119,'Franz','Kafka','qween@bic.ho','1234565123','1122 north','c'),(122,'Edgar Allan','Poe','cuervos@gmail.com','1187224653','castillo','c'),(123,'Aldoux','Huxley','somaforever@gmail.com','1234567893','brave new world','c'),(124,'Gabriel','Elizondo','gabrielelizondo@gmail.com','3644572525','Superiora Palmira N643','e'),(125,'Daniella','Kolarik','danikol@gmail.com','3644123456','Calle 4 e/5 y 7, centro','e'),(126,'Tech Toys',NULL,'techtoys@games.com','0800123546','Capital','p');
/*!40000 ALTER TABLE `personas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `piezas_reparacion`
--

DROP TABLE IF EXISTS `piezas_reparacion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `piezas_reparacion` (
  `ID_reparacion` int DEFAULT NULL,
  `ID_productos` int DEFAULT NULL,
  `cantidad_usada` int NOT NULL,
  KEY `ID_productos_idx` (`ID_productos`),
  KEY `ID_reparacion_idx` (`ID_reparacion`),
  CONSTRAINT `IDproductos` FOREIGN KEY (`ID_productos`) REFERENCES `productos` (`ID_productos`) ON DELETE SET NULL,
  CONSTRAINT `IDreparacion` FOREIGN KEY (`ID_reparacion`) REFERENCES `reparaciones` (`ID_reparaciones`) ON DELETE SET NULL
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
  `nombre_producto` int DEFAULT NULL,
  `precio_costo` decimal(10,2) NOT NULL,
  `precio_venta` decimal(12,2) NOT NULL,
  `stock` int NOT NULL,
  `proveedor` int DEFAULT NULL,
  `marca` int DEFAULT NULL,
  `modelo` int DEFAULT NULL,
  PRIMARY KEY (`ID_productos`),
  KEY `proveedor_pieza_idx` (`proveedor`),
  KEY `ID_marca_idx` (`marca`),
  KEY `nombre_producto_idx` (`nombre_producto`),
  KEY `model_idx` (`modelo`),
  CONSTRAINT `ID_marca` FOREIGN KEY (`marca`) REFERENCES `marcas` (`ID_marcas`) ON DELETE SET NULL,
  CONSTRAINT `model` FOREIGN KEY (`modelo`) REFERENCES `modelos` (`ID_modelos`) ON DELETE SET NULL,
  CONSTRAINT `nombre_producto` FOREIGN KEY (`nombre_producto`) REFERENCES `productos_genericos` (`ID_nombre_productos`) ON DELETE SET NULL,
  CONSTRAINT `proveedor_pieza` FOREIGN KEY (`proveedor`) REFERENCES `proveedores` (`ID_proveedores`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productos`
--

LOCK TABLES `productos` WRITE;
/*!40000 ALTER TABLE `productos` DISABLE KEYS */;
INSERT INTO `productos` VALUES (1,3,150.00,5000.00,123,1,8,2),(2,3,120000.00,300000.00,2,1,2,2),(16,7,1000.00,2500.00,50,1,2,4),(25,8,2000.00,3500.00,20,1,2,5),(26,9,1400.00,2600.00,14,10,5,4),(27,10,2230.00,4000.00,11,8,5,3),(28,11,5600.00,10000.00,5,9,3,4),(29,2,2000.00,5000.00,11,10,4,6),(30,5,3000.00,6000.00,9,1,1,1);
/*!40000 ALTER TABLE `productos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `productos_genericos`
--

DROP TABLE IF EXISTS `productos_genericos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `productos_genericos` (
  `ID_nombre_productos` int NOT NULL AUTO_INCREMENT,
  `nombre_generico` varchar(45) NOT NULL,
  `tipo_producto` int DEFAULT NULL,
  PRIMARY KEY (`ID_nombre_productos`),
  KEY `id_tipo_idx` (`tipo_producto`),
  CONSTRAINT `id_tipo` FOREIGN KEY (`tipo_producto`) REFERENCES `tipos_productos` (`ID_tipos_productos`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productos_genericos`
--

LOCK TABLES `productos_genericos` WRITE;
/*!40000 ALTER TABLE `productos_genericos` DISABLE KEYS */;
INSERT INTO `productos_genericos` VALUES (1,'pin de carga',1),(2,'funda transparente',2),(3,'celular',3),(4,'tablet',3),(5,'adaptador USB C a USB',2),(6,'modulo',1),(7,'cambio de bateria',4),(8,'cambio pin de carga',4),(9,'cambio de modulo',4),(10,'cambio de flex',4),(11,'cambio de chip',4);
/*!40000 ALTER TABLE `productos_genericos` ENABLE KEYS */;
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
  `ID_persona` int DEFAULT NULL,
  `cuit` varchar(11) DEFAULT NULL,
  PRIMARY KEY (`ID_proveedores`),
  KEY `ID_persona_idx` (`ID_persona`),
  CONSTRAINT `IDpersonas` FOREIGN KEY (`ID_persona`) REFERENCES `personas` (`ID_persona`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `proveedores`
--

LOCK TABLES `proveedores` WRITE;
/*!40000 ALTER TABLE `proveedores` DISABLE KEYS */;
INSERT INTO `proveedores` VALUES (1,'provetech.com.ar',20,NULL),(8,'cellworld.com.ar',34,NULL),(9,'repuestotech.com.ar',33,NULL),(10,'gadgetparts.com.ar',32,NULL),(11,'techtoys.com',126,NULL);
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
  `cliente` int DEFAULT NULL,
  `tecnico_cargo` int DEFAULT NULL,
  `dispotivo` int DEFAULT NULL,
  `estado` int DEFAULT NULL,
  `servicio` int DEFAULT NULL,
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
  CONSTRAINT `cliente` FOREIGN KEY (`cliente`) REFERENCES `clientes` (`ID_clientes`) ON DELETE SET NULL,
  CONSTRAINT `dispositivo` FOREIGN KEY (`dispotivo`) REFERENCES `productos` (`ID_productos`) ON DELETE SET NULL,
  CONSTRAINT `estado` FOREIGN KEY (`estado`) REFERENCES `estados` (`ID_estados`) ON DELETE SET NULL,
  CONSTRAINT `servicio` FOREIGN KEY (`servicio`) REFERENCES `servicios` (`ID_servicios`) ON DELETE SET NULL,
  CONSTRAINT `tecnico_cargo` FOREIGN KEY (`tecnico_cargo`) REFERENCES `empleados` (`ID_empleados`) ON DELETE SET NULL
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
-- Table structure for table `servicio_reparacion`
--

DROP TABLE IF EXISTS `servicio_reparacion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `servicio_reparacion` (
  `idservicio_reparacion` int NOT NULL AUTO_INCREMENT,
  `ingreso` int DEFAULT NULL,
  `precio_pieza` int DEFAULT NULL,
  `cantidad` int NOT NULL,
  `estado` int DEFAULT NULL,
  PRIMARY KEY (`idservicio_reparacion`),
  KEY `Ingreso_idx` (`ingreso`),
  KEY `pieza_idx` (`precio_pieza`),
  KEY `status_idx` (`estado`),
  CONSTRAINT `Ingreso` FOREIGN KEY (`ingreso`) REFERENCES `ingreso_reparacion` (`idingreso_reparacion`) ON DELETE SET NULL,
  CONSTRAINT `pieza` FOREIGN KEY (`precio_pieza`) REFERENCES `productos` (`ID_productos`) ON DELETE SET NULL,
  CONSTRAINT `status` FOREIGN KEY (`estado`) REFERENCES `estados` (`ID_estados`) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `servicio_reparacion`
--

LOCK TABLES `servicio_reparacion` WRITE;
/*!40000 ALTER TABLE `servicio_reparacion` DISABLE KEYS */;
/*!40000 ALTER TABLE `servicio_reparacion` ENABLE KEYS */;
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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `servicios`
--

LOCK TABLES `servicios` WRITE;
/*!40000 ALTER TABLE `servicios` DISABLE KEYS */;
INSERT INTO `servicios` VALUES (1,'limpieza de pin de carga',10000.00),(2,'reinicio de fabrica',15000.00);
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
  `nombre_tipo_producto` varchar(15) NOT NULL,
  PRIMARY KEY (`ID_tipos_productos`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tipos_productos`
--

LOCK TABLES `tipos_productos` WRITE;
/*!40000 ALTER TABLE `tipos_productos` DISABLE KEYS */;
INSERT INTO `tipos_productos` VALUES (1,'repuesto'),(2,'accesorio'),(3,'dispositivo'),(4,'servicio');
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
  `ID_cliente` int DEFAULT NULL,
  `ID_empleado` int DEFAULT NULL,
  `ID_caja` int DEFAULT NULL,
  `tipo_pago` int DEFAULT NULL,
  PRIMARY KEY (`ID_ventas`),
  KEY `ID_cliente_idx` (`ID_cliente`),
  KEY `ID_empleado_idx` (`ID_empleado`),
  KEY `ID_caja_idx` (`ID_caja`),
  KEY `tipo_pago_idx` (`tipo_pago`),
  CONSTRAINT `ID_caja` FOREIGN KEY (`ID_caja`) REFERENCES `caja` (`ID_caja`) ON DELETE SET NULL,
  CONSTRAINT `ID_cliente` FOREIGN KEY (`ID_cliente`) REFERENCES `clientes` (`ID_clientes`) ON DELETE SET NULL,
  CONSTRAINT `ID_empleado` FOREIGN KEY (`ID_empleado`) REFERENCES `empleados` (`ID_empleados`) ON DELETE SET NULL,
  CONSTRAINT `tipo_pago` FOREIGN KEY (`tipo_pago`) REFERENCES `pagos` (`ID_pagos`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ventas`
--

LOCK TABLES `ventas` WRITE;
/*!40000 ALTER TABLE `ventas` DISABLE KEYS */;
INSERT INTO `ventas` VALUES (1,'2025-11-03',500000.00,500001.00,126,1,1,1),(2,'2025-11-01',4000.00,4000.00,109,1,1,1),(3,'2025-11-02',300000.00,300002.00,138,1,1,1),(4,'2025-11-05',500000.00,500000.00,143,1,1,1),(5,'2025-11-04',500000.00,500000.00,144,1,1,1),(6,'2025-11-03',2500.00,2500.00,141,1,1,1),(7,'2025-11-02',6000.00,6000.00,144,1,1,1),(8,'2025-11-01',2500.00,2500.00,124,1,1,1);
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

-- Dump completed on 2025-11-13  1:45:20
