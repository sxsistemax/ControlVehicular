USE [ControlVehicular]
GO
/****** Object:  User [NT AUTHORITY\IUSR]    Script Date: 09/03/2013 10:28:10 ******/
CREATE USER [NT AUTHORITY\IUSR] FOR LOGIN [NT AUTHORITY\IUSR]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 09/03/2013 10:28:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Usuarios](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
	[NombreUsuario] [varchar](50) NOT NULL,
	[Password] [varchar](32) NOT NULL,
	[Correo] [varchar](100) NULL,
	[Activo] [bit] NOT NULL,
	[IdUsuarioNivel] [int] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserLevels]    Script Date: 09/03/2013 10:28:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLevels](
	[UserLevelID] [int] NOT NULL,
	[UserLevelName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_UserLevels] PRIMARY KEY CLUSTERED 
(
	[UserLevelID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cargos]    Script Date: 09/03/2013 10:28:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cargos](
	[IdCargo] [int] IDENTITY(1,1) NOT NULL,
	[Cargo] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Cargos] PRIMARY KEY CLUSTERED 
(
	[IdCargo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Areas]    Script Date: 09/03/2013 10:28:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Areas](
	[IdArea] [int] IDENTITY(1,1) NOT NULL,
	[Area] [varchar](50) NOT NULL,
	[Codigo] [varchar](10) NULL,
 CONSTRAINT [PK_Areas] PRIMARY KEY CLUSTERED 
(
	[IdArea] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TiposVehiculos]    Script Date: 09/03/2013 10:28:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TiposVehiculos](
	[IdTipoVehiculo] [int] IDENTITY(1,1) NOT NULL,
	[TipoVehiculo] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TiposVehiculos] PRIMARY KEY CLUSTERED 
(
	[IdTipoVehiculo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TiposDocumentos]    Script Date: 09/03/2013 10:28:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TiposDocumentos](
	[IdTipoDocumento] [int] IDENTITY(1,1) NOT NULL,
	[TipoDocumento] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TiposDocumentos] PRIMARY KEY CLUSTERED 
(
	[IdTipoDocumento] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RegistrosVehiculos]    Script Date: 09/03/2013 10:28:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RegistrosVehiculos](
	[IdRegistroVehiculo] [bigint] IDENTITY(1,1) NOT NULL,
	[IdTipoVehiculo] [int] NOT NULL,
	[Placa] [varchar](10) NOT NULL,
	[FechaIngreso] [datetime] NOT NULL,
	[FechaSalida] [datetime] NULL,
	[Observaciones] [varchar](max) NULL,
 CONSTRAINT [PK_RegistrosVehiculos] PRIMARY KEY CLUSTERED 
(
	[IdRegistroVehiculo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserLevelPermissions]    Script Date: 09/03/2013 10:28:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLevelPermissions](
	[UserLevelID] [int] NOT NULL,
	[UserLevelTableName] [nvarchar](50) NOT NULL,
	[UserLevelPermission] [int] NOT NULL,
	[IdAplicacion] [int] NULL,
 CONSTRAINT [PK_UserLevelPermissions] PRIMARY KEY CLUSTERED 
(
	[UserLevelID] ASC,
	[UserLevelTableName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personas]    Script Date: 09/03/2013 10:28:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Personas](
	[IdPersona] [int] IDENTITY(1,1) NOT NULL,
	[IdArea] [int] NOT NULL,
	[IdCargo] [int] NOT NULL,
	[Persona] [varchar](50) NOT NULL,
	[Documento] [varchar](50) NOT NULL,
	[Activa] [bit] NOT NULL,
 CONSTRAINT [PK_Personas] PRIMARY KEY CLUSTERED 
(
	[IdPersona] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VehiculosAutorizados]    Script Date: 09/03/2013 10:28:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VehiculosAutorizados](
	[IdVehiculoAutorizado] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoVehiculo] [int] NOT NULL,
	[Marca] [varchar](50) NOT NULL,
	[Placa] [varchar](10) NOT NULL,
	[Autorizado] [bit] NOT NULL,
	[IdPersona] [int] NOT NULL,
	[PicoyPlaca] [bit] NOT NULL,
	[Lunes] [bit] NOT NULL,
	[Martes] [bit] NOT NULL,
	[Miercoles] [bit] NOT NULL,
	[Jueves] [bit] NOT NULL,
	[Viernes] [bit] NOT NULL,
	[Sabado] [bit] NOT NULL,
	[Domingo] [bit] NOT NULL,
 CONSTRAINT [PK_VehiculosAtuorizados] PRIMARY KEY CLUSTERED 
(
	[IdVehiculoAutorizado] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RegistrosPeatones]    Script Date: 09/03/2013 10:28:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RegistrosPeatones](
	[IdRegistroPeaton] [bigint] IDENTITY(1,1) NOT NULL,
	[IdTipoDocumento] [int] NOT NULL,
	[Documento] [varchar](15) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellidos] [varchar](50) NOT NULL,
	[IdArea] [int] NOT NULL,
	[IdPersona] [int] NOT NULL,
	[FechaIngreso] [datetime] NOT NULL,
	[FechaSalida] [datetime] NULL,
	[Observacion] [varchar](max) NULL,
 CONSTRAINT [PK_RegistrosPeatones] PRIMARY KEY CLUSTERED 
(
	[IdRegistroPeaton] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VehiculosPicoYPlacaHoras]    Script Date: 09/03/2013 10:28:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VehiculosPicoYPlacaHoras](
	[IdVehiculoAutorizado] [int] NOT NULL,
	[HoraInicial] [time](7) NOT NULL,
	[HoraFinal] [time](7) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Default [DF_Usuarios_Activo]    Script Date: 09/03/2013 10:28:12 ******/
ALTER TABLE [dbo].[Usuarios] ADD  CONSTRAINT [DF_Usuarios_Activo]  DEFAULT ((0)) FOR [Activo]
GO
/****** Object:  Default [DF_RegistrosVehiculos_FechaIngreso]    Script Date: 09/03/2013 10:28:12 ******/
ALTER TABLE [dbo].[RegistrosVehiculos] ADD  CONSTRAINT [DF_RegistrosVehiculos_FechaIngreso]  DEFAULT (getdate()) FOR [FechaIngreso]
GO
/****** Object:  Default [DF_Personas_Activa]    Script Date: 09/03/2013 10:28:13 ******/
ALTER TABLE [dbo].[Personas] ADD  CONSTRAINT [DF_Personas_Activa]  DEFAULT ((1)) FOR [Activa]
GO
/****** Object:  Default [DF_VehiculosAtuorizados_Activo]    Script Date: 09/03/2013 10:28:13 ******/
ALTER TABLE [dbo].[VehiculosAutorizados] ADD  CONSTRAINT [DF_VehiculosAtuorizados_Activo]  DEFAULT ((1)) FOR [Autorizado]
GO
/****** Object:  Default [DF_VehiculosAtuorizados_PicoyPlaca]    Script Date: 09/03/2013 10:28:13 ******/
ALTER TABLE [dbo].[VehiculosAutorizados] ADD  CONSTRAINT [DF_VehiculosAtuorizados_PicoyPlaca]  DEFAULT ((1)) FOR [PicoyPlaca]
GO
/****** Object:  Default [DF_VehiculosAtuorizados_Lunes]    Script Date: 09/03/2013 10:28:13 ******/
ALTER TABLE [dbo].[VehiculosAutorizados] ADD  CONSTRAINT [DF_VehiculosAtuorizados_Lunes]  DEFAULT ((0)) FOR [Lunes]
GO
/****** Object:  Default [DF_VehiculosAtuorizados_Martes]    Script Date: 09/03/2013 10:28:13 ******/
ALTER TABLE [dbo].[VehiculosAutorizados] ADD  CONSTRAINT [DF_VehiculosAtuorizados_Martes]  DEFAULT ((0)) FOR [Martes]
GO
/****** Object:  Default [DF_VehiculosAtuorizados_Miercoles]    Script Date: 09/03/2013 10:28:13 ******/
ALTER TABLE [dbo].[VehiculosAutorizados] ADD  CONSTRAINT [DF_VehiculosAtuorizados_Miercoles]  DEFAULT ((0)) FOR [Miercoles]
GO
/****** Object:  Default [DF_VehiculosAtuorizados_Jueves]    Script Date: 09/03/2013 10:28:13 ******/
ALTER TABLE [dbo].[VehiculosAutorizados] ADD  CONSTRAINT [DF_VehiculosAtuorizados_Jueves]  DEFAULT ((0)) FOR [Jueves]
GO
/****** Object:  Default [DF_VehiculosAtuorizados_Viernes]    Script Date: 09/03/2013 10:28:13 ******/
ALTER TABLE [dbo].[VehiculosAutorizados] ADD  CONSTRAINT [DF_VehiculosAtuorizados_Viernes]  DEFAULT ((0)) FOR [Viernes]
GO
/****** Object:  Default [DF_VehiculosAtuorizados_Sabado]    Script Date: 09/03/2013 10:28:13 ******/
ALTER TABLE [dbo].[VehiculosAutorizados] ADD  CONSTRAINT [DF_VehiculosAtuorizados_Sabado]  DEFAULT ((0)) FOR [Sabado]
GO
/****** Object:  Default [DF_VehiculosAtuorizados_Domingo]    Script Date: 09/03/2013 10:28:13 ******/
ALTER TABLE [dbo].[VehiculosAutorizados] ADD  CONSTRAINT [DF_VehiculosAtuorizados_Domingo]  DEFAULT ((0)) FOR [Domingo]
GO
/****** Object:  Default [DF_RegistrosPeatones_FechaIngreso]    Script Date: 09/03/2013 10:28:13 ******/
ALTER TABLE [dbo].[RegistrosPeatones] ADD  CONSTRAINT [DF_RegistrosPeatones_FechaIngreso]  DEFAULT (getdate()) FOR [FechaIngreso]
GO
/****** Object:  ForeignKey [FK_RegistrosVehiculos_TiposVehiculos]    Script Date: 09/03/2013 10:28:12 ******/
ALTER TABLE [dbo].[RegistrosVehiculos]  WITH CHECK ADD  CONSTRAINT [FK_RegistrosVehiculos_TiposVehiculos] FOREIGN KEY([IdTipoVehiculo])
REFERENCES [dbo].[TiposVehiculos] ([IdTipoVehiculo])
GO
ALTER TABLE [dbo].[RegistrosVehiculos] CHECK CONSTRAINT [FK_RegistrosVehiculos_TiposVehiculos]
GO
/****** Object:  ForeignKey [FK_UserLevelPermissions_UserLevels]    Script Date: 09/03/2013 10:28:12 ******/
ALTER TABLE [dbo].[UserLevelPermissions]  WITH CHECK ADD  CONSTRAINT [FK_UserLevelPermissions_UserLevels] FOREIGN KEY([UserLevelID])
REFERENCES [dbo].[UserLevels] ([UserLevelID])
GO
ALTER TABLE [dbo].[UserLevelPermissions] CHECK CONSTRAINT [FK_UserLevelPermissions_UserLevels]
GO
/****** Object:  ForeignKey [FK_Personas_Areas]    Script Date: 09/03/2013 10:28:13 ******/
ALTER TABLE [dbo].[Personas]  WITH CHECK ADD  CONSTRAINT [FK_Personas_Areas] FOREIGN KEY([IdArea])
REFERENCES [dbo].[Areas] ([IdArea])
GO
ALTER TABLE [dbo].[Personas] CHECK CONSTRAINT [FK_Personas_Areas]
GO
/****** Object:  ForeignKey [FK_VehiculosAutorizados_Personas]    Script Date: 09/03/2013 10:28:13 ******/
ALTER TABLE [dbo].[VehiculosAutorizados]  WITH CHECK ADD  CONSTRAINT [FK_VehiculosAutorizados_Personas] FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Personas] ([IdPersona])
GO
ALTER TABLE [dbo].[VehiculosAutorizados] CHECK CONSTRAINT [FK_VehiculosAutorizados_Personas]
GO
/****** Object:  ForeignKey [FK_VehiculosAutorizados_TiposVehiculos]    Script Date: 09/03/2013 10:28:13 ******/
ALTER TABLE [dbo].[VehiculosAutorizados]  WITH CHECK ADD  CONSTRAINT [FK_VehiculosAutorizados_TiposVehiculos] FOREIGN KEY([IdTipoVehiculo])
REFERENCES [dbo].[TiposVehiculos] ([IdTipoVehiculo])
GO
ALTER TABLE [dbo].[VehiculosAutorizados] CHECK CONSTRAINT [FK_VehiculosAutorizados_TiposVehiculos]
GO
/****** Object:  ForeignKey [FK_RegistrosPeatones_Areas]    Script Date: 09/03/2013 10:28:13 ******/
ALTER TABLE [dbo].[RegistrosPeatones]  WITH CHECK ADD  CONSTRAINT [FK_RegistrosPeatones_Areas] FOREIGN KEY([IdArea])
REFERENCES [dbo].[Areas] ([IdArea])
GO
ALTER TABLE [dbo].[RegistrosPeatones] CHECK CONSTRAINT [FK_RegistrosPeatones_Areas]
GO
/****** Object:  ForeignKey [FK_RegistrosPeatones_Personas]    Script Date: 09/03/2013 10:28:13 ******/
ALTER TABLE [dbo].[RegistrosPeatones]  WITH CHECK ADD  CONSTRAINT [FK_RegistrosPeatones_Personas] FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Personas] ([IdPersona])
GO
ALTER TABLE [dbo].[RegistrosPeatones] CHECK CONSTRAINT [FK_RegistrosPeatones_Personas]
GO
/****** Object:  ForeignKey [FK_RegistrosPeatones_TiposDocumentos]    Script Date: 09/03/2013 10:28:13 ******/
ALTER TABLE [dbo].[RegistrosPeatones]  WITH CHECK ADD  CONSTRAINT [FK_RegistrosPeatones_TiposDocumentos] FOREIGN KEY([IdTipoDocumento])
REFERENCES [dbo].[TiposDocumentos] ([IdTipoDocumento])
GO
ALTER TABLE [dbo].[RegistrosPeatones] CHECK CONSTRAINT [FK_RegistrosPeatones_TiposDocumentos]
GO
/****** Object:  ForeignKey [FK_VehiculosPicoYPlacaHoras_VehiculosAtuorizados]    Script Date: 09/03/2013 10:28:13 ******/
ALTER TABLE [dbo].[VehiculosPicoYPlacaHoras]  WITH CHECK ADD  CONSTRAINT [FK_VehiculosPicoYPlacaHoras_VehiculosAtuorizados] FOREIGN KEY([IdVehiculoAutorizado])
REFERENCES [dbo].[VehiculosAutorizados] ([IdVehiculoAutorizado])
GO
ALTER TABLE [dbo].[VehiculosPicoYPlacaHoras] CHECK CONSTRAINT [FK_VehiculosPicoYPlacaHoras_VehiculosAtuorizados]
GO
