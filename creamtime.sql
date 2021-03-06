USE [master]
GO
/****** Object:  Database [creamtime]    Script Date: 29/10/2016 00:43:29 ******/
CREATE DATABASE [creamtime]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'creamtime', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\creamtime.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'creamtime_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\creamtime_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [creamtime] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [creamtime].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [creamtime] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [creamtime] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [creamtime] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [creamtime] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [creamtime] SET ARITHABORT OFF 
GO
ALTER DATABASE [creamtime] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [creamtime] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [creamtime] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [creamtime] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [creamtime] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [creamtime] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [creamtime] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [creamtime] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [creamtime] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [creamtime] SET  DISABLE_BROKER 
GO
ALTER DATABASE [creamtime] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [creamtime] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [creamtime] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [creamtime] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [creamtime] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [creamtime] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [creamtime] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [creamtime] SET RECOVERY FULL 
GO
ALTER DATABASE [creamtime] SET  MULTI_USER 
GO
ALTER DATABASE [creamtime] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [creamtime] SET DB_CHAINING OFF 
GO
ALTER DATABASE [creamtime] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [creamtime] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [creamtime] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'creamtime', N'ON'
GO
USE [creamtime]
GO
/****** Object:  Table [dbo].[ambitos]    Script Date: 29/10/2016 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ambitos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ambitos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[barrios]    Script Date: 29/10/2016 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[barrios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[id_localidad] [int] NOT NULL,
 CONSTRAINT [PK_barrios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[compras]    Script Date: 29/10/2016 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[compras](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fecha_compra] [datetime] NOT NULL,
	[id_proveedor] [int] NOT NULL,
	[monto] [float] NOT NULL,
	[nro_compra] [bigint] NOT NULL,
 CONSTRAINT [PK_compras] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[detalle_compra]    Script Date: 29/10/2016 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalle_compra](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_compra] [int] NOT NULL,
	[id_producto] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio] [float] NOT NULL,
 CONSTRAINT [PK_detalle_compra] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[detalle_pedido]    Script Date: 29/10/2016 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalle_pedido](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_pedido] [int] NOT NULL,
	[id_producto] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio] [float] NOT NULL,
 CONSTRAINT [PK_detalle_venta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[domicilios]    Script Date: 29/10/2016 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[domicilios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[calle] [varchar](50) NOT NULL,
	[numero] [varchar](50) NOT NULL,
	[id_barrio] [int] NOT NULL,
 CONSTRAINT [PK_domicilios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[envios]    Script Date: 29/10/2016 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[envios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_repartidor] [int] NOT NULL,
	[fecha_hora_partida] [datetime] NOT NULL,
	[fecha_hora_llegada] [datetime] NOT NULL,
	[id_pedido] [int] NOT NULL,
	[id_estado] [int] NOT NULL,
	[nro_envio] [bigint] NOT NULL,
 CONSTRAINT [PK_envios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[estados]    Script Date: 29/10/2016 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[estados](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](50) NULL,
	[id_ambito] [int] NOT NULL,
 CONSTRAINT [PK_estados] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[localidades]    Script Date: 29/10/2016 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[localidades](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_localidades] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pedido]    Script Date: 29/10/2016 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pedido](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fecha_pedido] [datetime] NOT NULL,
	[id_cliente] [int] NOT NULL,
	[monto] [float] NOT NULL,
	[id_vendedor] [int] NOT NULL,
	[id_estado] [int] NOT NULL,
	[fecha_pago] [datetime] NOT NULL,
	[nro_pedido] [bigint] NOT NULL,
 CONSTRAINT [PK_venta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[personas]    Script Date: 29/10/2016 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[personas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[apellido] [varchar](50) NOT NULL,
	[dni] [bigint] NOT NULL,
	[id_rol] [int] NOT NULL,
	[fecha_nacimiento] [datetime] NOT NULL,
	[vigente] [tinyint] NOT NULL,
	[id_sexo] [int] NOT NULL,
	[telefono] [varchar](50) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[id_domicilio] [int] NOT NULL,
 CONSTRAINT [PK_empleados] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[productos]    Script Date: 29/10/2016 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[productos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[id_tipo] [int] NOT NULL,
	[codigo_producto] [int] NOT NULL,
	[precio] [float] NOT NULL,
	[fecha_alta] [datetime] NOT NULL,
	[vigente] [tinyint] NOT NULL,
 CONSTRAINT [PK_productos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[proveedores]    Script Date: 29/10/2016 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[proveedores](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[razon_social] [varchar](50) NOT NULL,
	[cuit] [bigint] NOT NULL,
	[vigente] [tinyint] NOT NULL,
	[fecha_alta] [nchar](10) NOT NULL,
	[id_domicilio] [int] NOT NULL,
	[email] [varchar](50) NOT NULL,
	[telefono] [varchar](50) NULL,
 CONSTRAINT [PK_proveedores] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rol]    Script Date: 29/10/2016 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rol](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_cargos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sexo]    Script Date: 29/10/2016 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sexo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_sexo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[subdetalle_pedido]    Script Date: 29/10/2016 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subdetalle_pedido](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_detalle_pedido] [int] NOT NULL,
	[id_producto] [int] NOT NULL,
 CONSTRAINT [PK_subdetalle_venta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tipo_producto]    Script Date: 29/10/2016 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipo_producto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_tipo_producto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 29/10/2016 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[usuarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[id_persona] [int] NOT NULL,
 CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ambitos]    Script Date: 29/10/2016 00:43:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ambitos] ON [dbo].[ambitos]
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_barrios]    Script Date: 29/10/2016 00:43:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_barrios] ON [dbo].[barrios]
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_compras]    Script Date: 29/10/2016 00:43:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_compras] ON [dbo].[compras]
(
	[nro_compra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_envios]    Script Date: 29/10/2016 00:43:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_envios] ON [dbo].[envios]
(
	[nro_envio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_estados]    Script Date: 29/10/2016 00:43:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_estados] ON [dbo].[estados]
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_localidades]    Script Date: 29/10/2016 00:43:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_localidades] ON [dbo].[localidades]
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_pedido]    Script Date: 29/10/2016 00:43:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_pedido] ON [dbo].[pedido]
(
	[nro_pedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_personas]    Script Date: 29/10/2016 00:43:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_personas] ON [dbo].[personas]
(
	[dni] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_productos]    Script Date: 29/10/2016 00:43:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_productos] ON [dbo].[productos]
(
	[codigo_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_productos_1]    Script Date: 29/10/2016 00:43:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_productos_1] ON [dbo].[productos]
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_proveedores]    Script Date: 29/10/2016 00:43:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_proveedores] ON [dbo].[proveedores]
(
	[cuit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_rol]    Script Date: 29/10/2016 00:43:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_rol] ON [dbo].[rol]
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_sexo]    Script Date: 29/10/2016 00:43:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_sexo] ON [dbo].[sexo]
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_tipo_producto]    Script Date: 29/10/2016 00:43:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_tipo_producto] ON [dbo].[tipo_producto]
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_usuarios]    Script Date: 29/10/2016 00:43:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_usuarios] ON [dbo].[usuarios]
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[barrios]  WITH CHECK ADD  CONSTRAINT [FK_barrios_localidades1] FOREIGN KEY([id_localidad])
REFERENCES [dbo].[localidades] ([id])
GO
ALTER TABLE [dbo].[barrios] CHECK CONSTRAINT [FK_barrios_localidades1]
GO
ALTER TABLE [dbo].[compras]  WITH CHECK ADD  CONSTRAINT [FK_compras_proveedores] FOREIGN KEY([id_proveedor])
REFERENCES [dbo].[proveedores] ([id])
GO
ALTER TABLE [dbo].[compras] CHECK CONSTRAINT [FK_compras_proveedores]
GO
ALTER TABLE [dbo].[detalle_compra]  WITH CHECK ADD  CONSTRAINT [FK_detalle_compra_compras] FOREIGN KEY([id_compra])
REFERENCES [dbo].[compras] ([id])
GO
ALTER TABLE [dbo].[detalle_compra] CHECK CONSTRAINT [FK_detalle_compra_compras]
GO
ALTER TABLE [dbo].[detalle_pedido]  WITH CHECK ADD  CONSTRAINT [FK_detalle_venta_productos] FOREIGN KEY([id_producto])
REFERENCES [dbo].[productos] ([id])
GO
ALTER TABLE [dbo].[detalle_pedido] CHECK CONSTRAINT [FK_detalle_venta_productos]
GO
ALTER TABLE [dbo].[detalle_pedido]  WITH CHECK ADD  CONSTRAINT [FK_detalle_venta_venta] FOREIGN KEY([id_pedido])
REFERENCES [dbo].[pedido] ([id])
GO
ALTER TABLE [dbo].[detalle_pedido] CHECK CONSTRAINT [FK_detalle_venta_venta]
GO
ALTER TABLE [dbo].[domicilios]  WITH CHECK ADD  CONSTRAINT [FK_domicilios_barrios] FOREIGN KEY([id_barrio])
REFERENCES [dbo].[barrios] ([id])
GO
ALTER TABLE [dbo].[domicilios] CHECK CONSTRAINT [FK_domicilios_barrios]
GO
ALTER TABLE [dbo].[envios]  WITH CHECK ADD  CONSTRAINT [FK_envios_empleados] FOREIGN KEY([id_repartidor])
REFERENCES [dbo].[personas] ([id])
GO
ALTER TABLE [dbo].[envios] CHECK CONSTRAINT [FK_envios_empleados]
GO
ALTER TABLE [dbo].[envios]  WITH CHECK ADD  CONSTRAINT [FK_envios_estados] FOREIGN KEY([id_estado])
REFERENCES [dbo].[estados] ([id])
GO
ALTER TABLE [dbo].[envios] CHECK CONSTRAINT [FK_envios_estados]
GO
ALTER TABLE [dbo].[envios]  WITH CHECK ADD  CONSTRAINT [FK_envios_venta] FOREIGN KEY([id_pedido])
REFERENCES [dbo].[pedido] ([id])
GO
ALTER TABLE [dbo].[envios] CHECK CONSTRAINT [FK_envios_venta]
GO
ALTER TABLE [dbo].[estados]  WITH CHECK ADD  CONSTRAINT [FK_estados_ambitos] FOREIGN KEY([id_ambito])
REFERENCES [dbo].[ambitos] ([id])
GO
ALTER TABLE [dbo].[estados] CHECK CONSTRAINT [FK_estados_ambitos]
GO
ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_venta_clientes] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[personas] ([id])
GO
ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_venta_clientes]
GO
ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_venta_empleados] FOREIGN KEY([id_vendedor])
REFERENCES [dbo].[personas] ([id])
GO
ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_venta_empleados]
GO
ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_venta_estados] FOREIGN KEY([id_estado])
REFERENCES [dbo].[estados] ([id])
GO
ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_venta_estados]
GO
ALTER TABLE [dbo].[personas]  WITH CHECK ADD  CONSTRAINT [FK_empleados_cargos] FOREIGN KEY([id_rol])
REFERENCES [dbo].[rol] ([id])
GO
ALTER TABLE [dbo].[personas] CHECK CONSTRAINT [FK_empleados_cargos]
GO
ALTER TABLE [dbo].[personas]  WITH CHECK ADD  CONSTRAINT [FK_personas_domicilios] FOREIGN KEY([id_domicilio])
REFERENCES [dbo].[domicilios] ([id])
GO
ALTER TABLE [dbo].[personas] CHECK CONSTRAINT [FK_personas_domicilios]
GO
ALTER TABLE [dbo].[personas]  WITH CHECK ADD  CONSTRAINT [FK_personas_sexo] FOREIGN KEY([id_sexo])
REFERENCES [dbo].[sexo] ([id])
GO
ALTER TABLE [dbo].[personas] CHECK CONSTRAINT [FK_personas_sexo]
GO
ALTER TABLE [dbo].[productos]  WITH CHECK ADD  CONSTRAINT [FK_productos_tipo_producto] FOREIGN KEY([id_tipo])
REFERENCES [dbo].[tipo_producto] ([id])
GO
ALTER TABLE [dbo].[productos] CHECK CONSTRAINT [FK_productos_tipo_producto]
GO
ALTER TABLE [dbo].[proveedores]  WITH CHECK ADD  CONSTRAINT [FK_proveedores_domicilios] FOREIGN KEY([id_domicilio])
REFERENCES [dbo].[domicilios] ([id])
GO
ALTER TABLE [dbo].[proveedores] CHECK CONSTRAINT [FK_proveedores_domicilios]
GO
ALTER TABLE [dbo].[subdetalle_pedido]  WITH CHECK ADD  CONSTRAINT [FK_subdetalle_venta_detalle_venta] FOREIGN KEY([id_detalle_pedido])
REFERENCES [dbo].[detalle_pedido] ([id])
GO
ALTER TABLE [dbo].[subdetalle_pedido] CHECK CONSTRAINT [FK_subdetalle_venta_detalle_venta]
GO
ALTER TABLE [dbo].[subdetalle_pedido]  WITH CHECK ADD  CONSTRAINT [FK_subdetalle_venta_productos] FOREIGN KEY([id_producto])
REFERENCES [dbo].[productos] ([id])
GO
ALTER TABLE [dbo].[subdetalle_pedido] CHECK CONSTRAINT [FK_subdetalle_venta_productos]
GO
ALTER TABLE [dbo].[usuarios]  WITH CHECK ADD  CONSTRAINT [FK_usuarios_personas] FOREIGN KEY([id_persona])
REFERENCES [dbo].[personas] ([id])
GO
ALTER TABLE [dbo].[usuarios] CHECK CONSTRAINT [FK_usuarios_personas]
GO
USE [master]
GO
ALTER DATABASE [creamtime] SET  READ_WRITE 
GO
