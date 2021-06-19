USE [Quejas&Reclamaciones]
GO
/****** Object:  Table [dbo].[PRODUCTO]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCTO](
	[ID_PRODUCTO] [int] IDENTITY(1,1) NOT NULL,
	[NOMBRE_PRODUCTO] [varchar](50) NOT NULL,
	[PRECIO_PRODUCTO] [decimal](8, 2) NOT NULL,
	[ID_ESTADO] [int] NULL,
	[ID_TIPO_PRODUCTO] [int] NULL,
 CONSTRAINT [PK_PRODUCTO] PRIMARY KEY CLUSTERED 
(
	[ID_PRODUCTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PERSONA]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PERSONA](
	[ID_PERSONA] [int] IDENTITY(1,1) NOT NULL,
	[NOMBRE_PERSONA] [varchar](50) NOT NULL,
	[FECHA_NAC_PERSONA] [date] NOT NULL,
	[CEDULA_PERSONA] [char](11) NOT NULL,
	[CORREO_PERSONA] [varchar](70) NOT NULL,
	[TELEFONO_PERSONA] [char](10) NOT NULL,
	[GENERO_PERSONA] [char](1) NOT NULL,
 CONSTRAINT [PK_PERSONA_1] PRIMARY KEY CLUSTERED 
(
	[ID_PERSONA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ENVIO]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ENVIO](
	[ID_ENVIO] [int] IDENTITY(1,1) NOT NULL,
	[ID_PERSONA] [int] NOT NULL,
	[ID_PRODUCTO] [int] NOT NULL,
	[FECHA_ENVIO] [date] NOT NULL,
	[DIRECCION] [varchar](200) NOT NULL,
	[ID_ESTADO] [int] NULL,
 CONSTRAINT [PK_ENVIO] PRIMARY KEY CLUSTERED 
(
	[ID_ENVIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VISTA_ENVIO]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VISTA_ENVIO]
AS
(
	SELECT 
		ID_ENVIO,
		FECHA_ENVIO,
		DIRECCION,
		E.ID_ESTADO,
		P.ID_PERSONA,
		P.NOMBRE_PERSONA,
		PD.NOMBRE_PRODUCTO,
		PD.PRECIO_PRODUCTO 
	FROM ENVIO E
	INNER JOIN PERSONA P ON P.ID_PERSONA=E.ID_PERSONA
	INNER JOIN PRODUCTO PD ON PD.ID_PRODUCTO=E.ID_PRODUCTO
)
GO
/****** Object:  Table [dbo].[EMPLEADO]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EMPLEADO](
	[ID_EMPLEADO] [int] IDENTITY(1,1) NOT NULL,
	[ID_PERSONA] [int] NULL,
	[ID_DEPARTAMENTO] [int] NULL,
	[ID_ESTADO] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_EMPLEADO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DEPARTAMENTO]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DEPARTAMENTO](
	[ID_DEPARTAMENTO] [int] IDENTITY(1,1) NOT NULL,
	[NOMBRE_DEPARTAMENTO] [varchar](50) NOT NULL,
	[ID_ENCARGADO] [int] NOT NULL,
 CONSTRAINT [PK_DEPARTAMENTO] PRIMARY KEY CLUSTERED 
(
	[ID_DEPARTAMENTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USUARIO]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIO](
	[ID_USUARIO] [int] IDENTITY(1,1) NOT NULL,
	[ID_PERSONA] [int] NOT NULL,
	[NOMBRE_USUARIO] [varchar](50) NOT NULL,
	[CLAVE_USUARIO] [varchar](50) NOT NULL,
	[ID_TIPO_USUARIO] [int] NULL,
	[ID_ESTADO] [int] NULL,
 CONSTRAINT [PK_USUARIO_1] PRIMARY KEY CLUSTERED 
(
	[ID_USUARIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VISTA_EMPLEADO]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VISTA_EMPLEADO]
AS
(
	SELECT 
		P.ID_PERSONA,
		P.NOMBRE_PERSONA,
		P.CEDULA_PERSONA,
		P.CORREO_PERSONA,
		P.FECHA_NAC_PERSONA,
		P.GENERO_PERSONA,
		P.TELEFONO_PERSONA,
		U.ID_USUARIO,
		U.NOMBRE_USUARIO,
		U.CLAVE_USUARIO,
		U.ID_TIPO_USUARIO,
		D.ID_DEPARTAMENTO,
		D.NOMBRE_DEPARTAMENTO,
		E.ID_ESTADO
	FROM PERSONA P 
	INNER JOIN EMPLEADO E ON E.ID_PERSONA=P.ID_PERSONA 
	INNER JOIN DEPARTAMENTO D ON D.ID_DEPARTAMENTO=E.ID_DEPARTAMENTO 
	INNER JOIN USUARIO U ON U.ID_PERSONA=P.ID_PERSONA
)
GO
/****** Object:  Table [dbo].[TIPO_QUEJA]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TIPO_QUEJA](
	[ID_TIPO_QUEJA] [int] IDENTITY(1,1) NOT NULL,
	[DESCRIPCION_QUEJA] [varchar](200) NOT NULL,
	[TITULO_QUEJA] [varchar](200) NOT NULL,
	[ID_ESTADO] [int] NULL,
 CONSTRAINT [PK_TIPO_QUEJA] PRIMARY KEY CLUSTERED 
(
	[ID_TIPO_QUEJA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QUEJA]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QUEJA](
	[ID_QUEJA] [int] IDENTITY(1,1) NOT NULL,
	[ID_DEPARTAMENTO] [int] NOT NULL,
	[FECHA_QUEJA] [datetime] NOT NULL,
	[DESCRIPCION_QUEJA] [varchar](200) NOT NULL,
	[ID_TIPO_QUEJA] [int] NOT NULL,
	[ID_ESTADO] [int] NULL,
	[ID_PERSONA] [int] NULL,
 CONSTRAINT [PK_QUEJA] PRIMARY KEY CLUSTERED 
(
	[ID_QUEJA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ESTADO]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ESTADO](
	[ID_ESTADO] [int] IDENTITY(1,1) NOT NULL,
	[TITULO_ESTADO] [varchar](50) NOT NULL,
	[DESCRIPCION_ESTADO] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_ESTADO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VISTA_QUEJA]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[VISTA_QUEJA]
AS
(
	SELECT 
		Q.ID_QUEJA,
		Q.ID_PERSONA,
		P.NOMBRE_PERSONA,
		Q.DESCRIPCION_QUEJA,
		Q.ID_DEPARTAMENTO,
		D.NOMBRE_DEPARTAMENTO,
		Q.FECHA_QUEJA,
		TQ.ID_TIPO_QUEJA,
		TQ.TITULO_QUEJA,
		Q.ID_ESTADO,
		E.TITULO_ESTADO
	FROM QUEJA Q 
	INNER JOIN TIPO_QUEJA TQ ON TQ.ID_TIPO_QUEJA=Q.ID_TIPO_QUEJA
	INNER JOIN ESTADO E ON E.ID_ESTADO=Q.ID_ESTADO
	INNER JOIN DEPARTAMENTO D ON D.ID_DEPARTAMENTO=Q.ID_DEPARTAMENTO
	INNER JOIN PERSONA P ON P.ID_PERSONA = Q.ID_PERSONA
)
GO
/****** Object:  Table [dbo].[RECLAMACION]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RECLAMACION](
	[ID_RECLAMACION] [int] IDENTITY(1,1) NOT NULL,
	[ID_PERSONA] [int] NULL,
	[ID_DEPARTAMENTO] [int] NULL,
	[FECHA_RECLAMACION] [date] NOT NULL,
	[DESCRIPCION_RECLAMACION] [varchar](200) NULL,
	[ID_TIPO_RECLAMACION] [int] NULL,
	[ID_ESTADO] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_RECLAMACION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TIPO_RECLAMACION]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TIPO_RECLAMACION](
	[ID_TIPO_RECLAMACION] [int] IDENTITY(1,1) NOT NULL,
	[DESCRIPCION_RECLAMACION] [varchar](200) NOT NULL,
	[TITULO_RECLAMACION] [varchar](50) NOT NULL,
	[ID_ESTADO] [int] NULL,
 CONSTRAINT [PK_TIPO_RECLAMACION] PRIMARY KEY CLUSTERED 
(
	[ID_TIPO_RECLAMACION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VISTA_RECLAMACION]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[VISTA_RECLAMACION]
AS
(
	SELECT 
		R.ID_RECLAMACION,
		R.FECHA_RECLAMACION,
		R.DESCRIPCION_RECLAMACION,
		P.ID_PERSONA,
		P.NOMBRE_PERSONA,
		D.ID_DEPARTAMENTO,D.NOMBRE_DEPARTAMENTO,
		TR.ID_TIPO_RECLAMACION,
		TR.TITULO_RECLAMACION,
		R.ID_ESTADO,
		E.TITULO_ESTADO
	FROM RECLAMACION R
	INNER JOIN TIPO_RECLAMACION TR ON TR.ID_TIPO_RECLAMACION=R.ID_TIPO_RECLAMACION
	INNER JOIN DEPARTAMENTO D ON D.ID_DEPARTAMENTO=R.ID_DEPARTAMENTO
	INNER JOIN PERSONA P ON P.ID_PERSONA=R.ID_PERSONA
	INNER JOIN ESTADO E ON E.ID_ESTADO = R.ID_ESTADO
)
GO
/****** Object:  Table [dbo].[RESPUESTA]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RESPUESTA](
	[ID_RESPUESTA] [int] IDENTITY(1,1) NOT NULL,
	[ID_EMPLEADO] [int] NOT NULL,
	[ID_QUEJA] [int] NULL,
	[ID_RECLAMACION] [int] NULL,
	[MENSAJE_RESPUESTA] [varchar](200) NOT NULL,
	[FECHA_RESPUESTA] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_RESPUESTA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VISTA_RESPUESTA]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   VIEW [dbo].[VISTA_RESPUESTA] 
AS
(

	SELECT 
		R.ID_RESPUESTA,
		E.ID_EMPLEADO,
		R.FECHA_RESPUESTA,
		PE.NOMBRE_PERSONA AS NOMBRE_EMPLEADO,
		E.ID_DEPARTAMENTO,
		D.NOMBRE_DEPARTAMENTO,
		R.MENSAJE_RESPUESTA,
		Q.ID_QUEJA,
		PQ.ID_PERSONA AS ID_PERSONA_QUEJA,
		PQ.NOMBRE_PERSONA AS NOMBRE_PERSONA_QUEJA,
		RE.ID_RECLAMACION,
		PR.ID_PERSONA AS ID_PERSONA_RECLAMACION,
		PR.NOMBRE_PERSONA AS NOMBRE_PERSONA_RECLAMACION
	FROM RESPUESTA R 
	FULL OUTER JOIN QUEJA Q ON Q.ID_QUEJA=R.ID_QUEJA
	FULL OUTER JOIN PERSONA PQ ON PQ.ID_PERSONA=Q.ID_PERSONA
	FULL OUTER JOIN RECLAMACION RE ON RE.ID_RECLAMACION =R.ID_RECLAMACION
	FULL OUTER JOIN PERSONA PR ON PR.ID_PERSONA = RE.ID_PERSONA
	INNER JOIN EMPLEADO E ON E.ID_EMPLEADO = R.ID_EMPLEADO
	INNER JOIN PERSONA PE ON PE.ID_PERSONA=E.ID_PERSONA
	INNER JOIN DEPARTAMENTO D ON D.ID_DEPARTAMENTO=E.ID_DEPARTAMENTO

		
)

GO
/****** Object:  View [dbo].[VISTA_RESPUESTA_QUEJA]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE   VIEW [dbo].[VISTA_RESPUESTA_QUEJA] 
AS
(

	SELECT 
		R.ID_RESPUESTA,
		E.ID_EMPLEADO,
		PE.NOMBRE_PERSONA AS NOMBRE_EMPLEADO,
		E.ID_DEPARTAMENTO AS ID_DEPARTAMENTO_EMPLEADO,
		D.NOMBRE_DEPARTAMENTO AS NOMBRE_DEPARTAMENTO_EMPLEADO,
		R.MENSAJE_RESPUESTA,
		R.FECHA_RESPUESTA,
		Q.ID_QUEJA,
		PQ.ID_PERSONA AS ID_PERSONA_QUEJA,
		PQ.NOMBRE_PERSONA AS NOMBRE_PERSONA_QUEJA,
		DQ.ID_DEPARTAMENTO AS ID_DEPARTAMENTO_QUEJA,
		DQ.NOMBRE_DEPARTAMENTO AS NOMBRE_DEPARTAMENTO_QUEJA,
		Q.FECHA_QUEJA
	FROM RESPUESTA R 
	INNER JOIN QUEJA Q ON Q.ID_QUEJA=R.ID_QUEJA
	INNER JOIN PERSONA PQ ON PQ.ID_PERSONA=Q.ID_PERSONA
	INNER JOIN DEPARTAMENTO DQ ON DQ.ID_DEPARTAMENTO=Q.ID_DEPARTAMENTO
	INNER JOIN EMPLEADO E ON E.ID_EMPLEADO = R.ID_EMPLEADO
	INNER JOIN PERSONA PE ON PE.ID_PERSONA=E.ID_PERSONA
	INNER JOIN DEPARTAMENTO D ON D.ID_DEPARTAMENTO=E.ID_DEPARTAMENTO

		
)

GO
/****** Object:  View [dbo].[VISTA_RESPUESTA_RECLAMACION]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE   VIEW [dbo].[VISTA_RESPUESTA_RECLAMACION] 
AS
(

	SELECT 
		R.ID_RESPUESTA,
		E.ID_EMPLEADO,
		PE.NOMBRE_PERSONA AS NOMBRE_EMPLEADO,
		E.ID_DEPARTAMENTO AS ID_DEPARTAMENTO_EMPLEADO,
		D.NOMBRE_DEPARTAMENTO AS NOMBRE_DEPARTAMENTO_EMPLEADO,
		R.MENSAJE_RESPUESTA,
		R.FECHA_RESPUESTA,
		RE.ID_RECLAMACION,
		PR.ID_PERSONA AS ID_PERSONA_QUEJA,
		PR.NOMBRE_PERSONA AS NOMBRE_PERSONA_QUEJA,
		DR.ID_DEPARTAMENTO AS ID_DEPARTAMENTO_QUEJA,
		DR.NOMBRE_DEPARTAMENTO AS NOMBRE_DEPARTAMENTO_QUEJA,
		RE.FECHA_RECLAMACION
	FROM RESPUESTA R 
	INNER JOIN RECLAMACION RE ON RE.ID_RECLAMACION=R.ID_RECLAMACION
	INNER JOIN PERSONA PR ON PR.ID_PERSONA=RE.ID_PERSONA
	INNER JOIN DEPARTAMENTO DR ON DR.ID_DEPARTAMENTO=RE.ID_DEPARTAMENTO
	INNER JOIN EMPLEADO E ON E.ID_EMPLEADO = R.ID_EMPLEADO
	INNER JOIN PERSONA PE ON PE.ID_PERSONA=E.ID_PERSONA
	INNER JOIN DEPARTAMENTO D ON D.ID_DEPARTAMENTO=E.ID_DEPARTAMENTO

		
)

GO
/****** Object:  Table [dbo].[TIPO_PRODUCTO]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TIPO_PRODUCTO](
	[ID_TIPO_PRODUCTO] [int] IDENTITY(1,1) NOT NULL,
	[TITULO_TIPO_PRODUCTO] [varchar](50) NOT NULL,
	[DESCRIPCION_TIPO_PRODUCTO] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_TIPO_PRODUCTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TIPO_USUARIO]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TIPO_USUARIO](
	[ID_TIPO_USUARIO] [int] IDENTITY(1,1) NOT NULL,
	[TITULO_TIPO_USUARIO] [varchar](50) NOT NULL,
	[DESCRIPCION_TIPO_USUARIO] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_TIPO_USUARIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DEPARTAMENTO] ON 

INSERT [dbo].[DEPARTAMENTO] ([ID_DEPARTAMENTO], [NOMBRE_DEPARTAMENTO], [ID_ENCARGADO]) VALUES (4, N'INFORMATICA', 2)
SET IDENTITY_INSERT [dbo].[DEPARTAMENTO] OFF
GO
SET IDENTITY_INSERT [dbo].[EMPLEADO] ON 

INSERT [dbo].[EMPLEADO] ([ID_EMPLEADO], [ID_PERSONA], [ID_DEPARTAMENTO], [ID_ESTADO]) VALUES (10, 20, 4, 3)
SET IDENTITY_INSERT [dbo].[EMPLEADO] OFF
GO
SET IDENTITY_INSERT [dbo].[ESTADO] ON 

INSERT [dbo].[ESTADO] ([ID_ESTADO], [TITULO_ESTADO], [DESCRIPCION_ESTADO]) VALUES (1, N'PRUEBA', N'PRUEBA')
INSERT [dbo].[ESTADO] ([ID_ESTADO], [TITULO_ESTADO], [DESCRIPCION_ESTADO]) VALUES (2, N'ACTIVO', N'EL RECURSO ESTA DISPONIBLE')
INSERT [dbo].[ESTADO] ([ID_ESTADO], [TITULO_ESTADO], [DESCRIPCION_ESTADO]) VALUES (3, N'INACTIVO', N'EL RECURSO NO ESTA DISPONIBLE')
SET IDENTITY_INSERT [dbo].[ESTADO] OFF
GO
SET IDENTITY_INSERT [dbo].[PERSONA] ON 

INSERT [dbo].[PERSONA] ([ID_PERSONA], [NOMBRE_PERSONA], [FECHA_NAC_PERSONA], [CEDULA_PERSONA], [CORREO_PERSONA], [TELEFONO_PERSONA], [GENERO_PERSONA]) VALUES (2, N'Guillermo', CAST(N'2001-01-01' AS Date), N'12345678901', N'Lala', N'1234567890', N'M')
INSERT [dbo].[PERSONA] ([ID_PERSONA], [NOMBRE_PERSONA], [FECHA_NAC_PERSONA], [CEDULA_PERSONA], [CORREO_PERSONA], [TELEFONO_PERSONA], [GENERO_PERSONA]) VALUES (3, N'Juan', CAST(N'2001-01-01' AS Date), N'12345678901', N'lala', N'1234567890', N'm')
INSERT [dbo].[PERSONA] ([ID_PERSONA], [NOMBRE_PERSONA], [FECHA_NAC_PERSONA], [CEDULA_PERSONA], [CORREO_PERSONA], [TELEFONO_PERSONA], [GENERO_PERSONA]) VALUES (4, N'Juan', CAST(N'2001-01-01' AS Date), N'12345678901', N'lala', N'1234567890', N'm')
INSERT [dbo].[PERSONA] ([ID_PERSONA], [NOMBRE_PERSONA], [FECHA_NAC_PERSONA], [CEDULA_PERSONA], [CORREO_PERSONA], [TELEFONO_PERSONA], [GENERO_PERSONA]) VALUES (5, N'Juan', CAST(N'2001-01-01' AS Date), N'12345678901', N'lala', N'1234567890', N'm')
INSERT [dbo].[PERSONA] ([ID_PERSONA], [NOMBRE_PERSONA], [FECHA_NAC_PERSONA], [CEDULA_PERSONA], [CORREO_PERSONA], [TELEFONO_PERSONA], [GENERO_PERSONA]) VALUES (6, N'Juan', CAST(N'2001-01-01' AS Date), N'12345678901', N'lala', N'1234567890', N'm')
INSERT [dbo].[PERSONA] ([ID_PERSONA], [NOMBRE_PERSONA], [FECHA_NAC_PERSONA], [CEDULA_PERSONA], [CORREO_PERSONA], [TELEFONO_PERSONA], [GENERO_PERSONA]) VALUES (7, N'Marcos', CAST(N'2001-01-01' AS Date), N'12345678901', N'lala', N'1234567890', N'm')
INSERT [dbo].[PERSONA] ([ID_PERSONA], [NOMBRE_PERSONA], [FECHA_NAC_PERSONA], [CEDULA_PERSONA], [CORREO_PERSONA], [TELEFONO_PERSONA], [GENERO_PERSONA]) VALUES (20, N'Guillermo Emmanuel Matos', CAST(N'2001-08-30' AS Date), N'12345678901', N'emmanuelmtr1234@gmail.com', N'8099696755', N'M')
SET IDENTITY_INSERT [dbo].[PERSONA] OFF
GO
SET IDENTITY_INSERT [dbo].[PRODUCTO] ON 

INSERT [dbo].[PRODUCTO] ([ID_PRODUCTO], [NOMBRE_PRODUCTO], [PRECIO_PRODUCTO], [ID_ESTADO], [ID_TIPO_PRODUCTO]) VALUES (6, N'juan', CAST(1.00 AS Decimal(8, 2)), 1, 1)
INSERT [dbo].[PRODUCTO] ([ID_PRODUCTO], [NOMBRE_PRODUCTO], [PRECIO_PRODUCTO], [ID_ESTADO], [ID_TIPO_PRODUCTO]) VALUES (8, N'PRODUCTO1', CAST(8999.99 AS Decimal(8, 2)), 1, 1)
INSERT [dbo].[PRODUCTO] ([ID_PRODUCTO], [NOMBRE_PRODUCTO], [PRECIO_PRODUCTO], [ID_ESTADO], [ID_TIPO_PRODUCTO]) VALUES (9, N'PRODUCTO1', CAST(8999.99 AS Decimal(8, 2)), 1, 1)
INSERT [dbo].[PRODUCTO] ([ID_PRODUCTO], [NOMBRE_PRODUCTO], [PRECIO_PRODUCTO], [ID_ESTADO], [ID_TIPO_PRODUCTO]) VALUES (14, N'juan', CAST(1.00 AS Decimal(8, 2)), 1, 1)
INSERT [dbo].[PRODUCTO] ([ID_PRODUCTO], [NOMBRE_PRODUCTO], [PRECIO_PRODUCTO], [ID_ESTADO], [ID_TIPO_PRODUCTO]) VALUES (15, N'juan', CAST(1.00 AS Decimal(8, 2)), 1, 1)
INSERT [dbo].[PRODUCTO] ([ID_PRODUCTO], [NOMBRE_PRODUCTO], [PRECIO_PRODUCTO], [ID_ESTADO], [ID_TIPO_PRODUCTO]) VALUES (16, N'juan', CAST(1.00 AS Decimal(8, 2)), 1, 1)
INSERT [dbo].[PRODUCTO] ([ID_PRODUCTO], [NOMBRE_PRODUCTO], [PRECIO_PRODUCTO], [ID_ESTADO], [ID_TIPO_PRODUCTO]) VALUES (17, N'XD', CAST(1.00 AS Decimal(8, 2)), 1, 1)
SET IDENTITY_INSERT [dbo].[PRODUCTO] OFF
GO
SET IDENTITY_INSERT [dbo].[QUEJA] ON 

INSERT [dbo].[QUEJA] ([ID_QUEJA], [ID_DEPARTAMENTO], [FECHA_QUEJA], [DESCRIPCION_QUEJA], [ID_TIPO_QUEJA], [ID_ESTADO], [ID_PERSONA]) VALUES (3, 4, CAST(N'2020-11-11T00:00:00.000' AS DateTime), N'LALALA', 1, 1, 2)
INSERT [dbo].[QUEJA] ([ID_QUEJA], [ID_DEPARTAMENTO], [FECHA_QUEJA], [DESCRIPCION_QUEJA], [ID_TIPO_QUEJA], [ID_ESTADO], [ID_PERSONA]) VALUES (4, 4, CAST(N'2021-03-03T00:00:00.000' AS DateTime), N'Xdddddddd', 1, 2, 7)
INSERT [dbo].[QUEJA] ([ID_QUEJA], [ID_DEPARTAMENTO], [FECHA_QUEJA], [DESCRIPCION_QUEJA], [ID_TIPO_QUEJA], [ID_ESTADO], [ID_PERSONA]) VALUES (5, 4, CAST(N'2021-03-03T00:00:00.000' AS DateTime), N'Xdddddddd12324', 1, 2, 7)
INSERT [dbo].[QUEJA] ([ID_QUEJA], [ID_DEPARTAMENTO], [FECHA_QUEJA], [DESCRIPCION_QUEJA], [ID_TIPO_QUEJA], [ID_ESTADO], [ID_PERSONA]) VALUES (6, 4, CAST(N'2021-03-03T00:00:00.000' AS DateTime), N'Xdddddddd12324', 1, 2, 7)
INSERT [dbo].[QUEJA] ([ID_QUEJA], [ID_DEPARTAMENTO], [FECHA_QUEJA], [DESCRIPCION_QUEJA], [ID_TIPO_QUEJA], [ID_ESTADO], [ID_PERSONA]) VALUES (7, 4, CAST(N'2021-03-03T00:00:00.000' AS DateTime), N'Xdddddddd12324', 1, 2, 7)
INSERT [dbo].[QUEJA] ([ID_QUEJA], [ID_DEPARTAMENTO], [FECHA_QUEJA], [DESCRIPCION_QUEJA], [ID_TIPO_QUEJA], [ID_ESTADO], [ID_PERSONA]) VALUES (8, 4, CAST(N'2021-03-03T00:00:00.000' AS DateTime), N'Xdddddddd12324', 1, 3, 7)
INSERT [dbo].[QUEJA] ([ID_QUEJA], [ID_DEPARTAMENTO], [FECHA_QUEJA], [DESCRIPCION_QUEJA], [ID_TIPO_QUEJA], [ID_ESTADO], [ID_PERSONA]) VALUES (9, 4, CAST(N'2020-01-01T00:00:00.000' AS DateTime), N'Xddddd', 2, 2, 4)
INSERT [dbo].[QUEJA] ([ID_QUEJA], [ID_DEPARTAMENTO], [FECHA_QUEJA], [DESCRIPCION_QUEJA], [ID_TIPO_QUEJA], [ID_ESTADO], [ID_PERSONA]) VALUES (10, 4, CAST(N'2020-01-01T00:00:00.000' AS DateTime), N'Xddddd', 2, 2, 4)
INSERT [dbo].[QUEJA] ([ID_QUEJA], [ID_DEPARTAMENTO], [FECHA_QUEJA], [DESCRIPCION_QUEJA], [ID_TIPO_QUEJA], [ID_ESTADO], [ID_PERSONA]) VALUES (1009, 4, CAST(N'2020-01-01T00:00:00.000' AS DateTime), N'Xddddd', 2, 2, 4)
SET IDENTITY_INSERT [dbo].[QUEJA] OFF
GO
SET IDENTITY_INSERT [dbo].[RECLAMACION] ON 

INSERT [dbo].[RECLAMACION] ([ID_RECLAMACION], [ID_PERSONA], [ID_DEPARTAMENTO], [FECHA_RECLAMACION], [DESCRIPCION_RECLAMACION], [ID_TIPO_RECLAMACION], [ID_ESTADO]) VALUES (1, 3, 4, CAST(N'2021-01-01' AS Date), N'1234', 1, 2)
INSERT [dbo].[RECLAMACION] ([ID_RECLAMACION], [ID_PERSONA], [ID_DEPARTAMENTO], [FECHA_RECLAMACION], [DESCRIPCION_RECLAMACION], [ID_TIPO_RECLAMACION], [ID_ESTADO]) VALUES (2, 2, 4, CAST(N'2021-03-03' AS Date), N'1234', 1, 2)
INSERT [dbo].[RECLAMACION] ([ID_RECLAMACION], [ID_PERSONA], [ID_DEPARTAMENTO], [FECHA_RECLAMACION], [DESCRIPCION_RECLAMACION], [ID_TIPO_RECLAMACION], [ID_ESTADO]) VALUES (7, 7, 4, CAST(N'2021-03-03' AS Date), N'XDDDDDDDD12324', 1, 2)
INSERT [dbo].[RECLAMACION] ([ID_RECLAMACION], [ID_PERSONA], [ID_DEPARTAMENTO], [FECHA_RECLAMACION], [DESCRIPCION_RECLAMACION], [ID_TIPO_RECLAMACION], [ID_ESTADO]) VALUES (8, 7, 4, CAST(N'2021-03-03' AS Date), N'1234', 1, 3)
INSERT [dbo].[RECLAMACION] ([ID_RECLAMACION], [ID_PERSONA], [ID_DEPARTAMENTO], [FECHA_RECLAMACION], [DESCRIPCION_RECLAMACION], [ID_TIPO_RECLAMACION], [ID_ESTADO]) VALUES (9, 7, 4, CAST(N'2021-03-03' AS Date), N'1234', 1, 3)
INSERT [dbo].[RECLAMACION] ([ID_RECLAMACION], [ID_PERSONA], [ID_DEPARTAMENTO], [FECHA_RECLAMACION], [DESCRIPCION_RECLAMACION], [ID_TIPO_RECLAMACION], [ID_ESTADO]) VALUES (10, 7, 4, CAST(N'2021-03-03' AS Date), N'1234', 1, 3)
SET IDENTITY_INSERT [dbo].[RECLAMACION] OFF
GO
SET IDENTITY_INSERT [dbo].[RESPUESTA] ON 

INSERT [dbo].[RESPUESTA] ([ID_RESPUESTA], [ID_EMPLEADO], [ID_QUEJA], [ID_RECLAMACION], [MENSAJE_RESPUESTA], [FECHA_RESPUESTA]) VALUES (1, 10, 3, NULL, N'LALALA', CAST(N'2021-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[RESPUESTA] ([ID_RESPUESTA], [ID_EMPLEADO], [ID_QUEJA], [ID_RECLAMACION], [MENSAJE_RESPUESTA], [FECHA_RESPUESTA]) VALUES (2, 10, NULL, 1, N'LALALA', CAST(N'2021-01-01T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[RESPUESTA] OFF
GO
SET IDENTITY_INSERT [dbo].[TIPO_PRODUCTO] ON 

INSERT [dbo].[TIPO_PRODUCTO] ([ID_TIPO_PRODUCTO], [TITULO_TIPO_PRODUCTO], [DESCRIPCION_TIPO_PRODUCTO]) VALUES (1, N'PRUEBA', N'PRUEBA')
SET IDENTITY_INSERT [dbo].[TIPO_PRODUCTO] OFF
GO
SET IDENTITY_INSERT [dbo].[TIPO_QUEJA] ON 

INSERT [dbo].[TIPO_QUEJA] ([ID_TIPO_QUEJA], [DESCRIPCION_QUEJA], [TITULO_QUEJA], [ID_ESTADO]) VALUES (1, N'QUEJA TIPO 1', N'QUEJA TIPO 1', 1)
INSERT [dbo].[TIPO_QUEJA] ([ID_TIPO_QUEJA], [DESCRIPCION_QUEJA], [TITULO_QUEJA], [ID_ESTADO]) VALUES (2, N'PRUEBA 1', N'PRUEBA', 1)
INSERT [dbo].[TIPO_QUEJA] ([ID_TIPO_QUEJA], [DESCRIPCION_QUEJA], [TITULO_QUEJA], [ID_ESTADO]) VALUES (3, N'PRUEBA 1', N'PRUEBA', 1)
SET IDENTITY_INSERT [dbo].[TIPO_QUEJA] OFF
GO
SET IDENTITY_INSERT [dbo].[TIPO_RECLAMACION] ON 

INSERT [dbo].[TIPO_RECLAMACION] ([ID_TIPO_RECLAMACION], [DESCRIPCION_RECLAMACION], [TITULO_RECLAMACION], [ID_ESTADO]) VALUES (1, N'PRUEBA 1', N'PRUEBA', 2)
SET IDENTITY_INSERT [dbo].[TIPO_RECLAMACION] OFF
GO
SET IDENTITY_INSERT [dbo].[TIPO_USUARIO] ON 

INSERT [dbo].[TIPO_USUARIO] ([ID_TIPO_USUARIO], [TITULO_TIPO_USUARIO], [DESCRIPCION_TIPO_USUARIO]) VALUES (1, N'ADMINISTRADOR', N'ADMINISTRADOR')
INSERT [dbo].[TIPO_USUARIO] ([ID_TIPO_USUARIO], [TITULO_TIPO_USUARIO], [DESCRIPCION_TIPO_USUARIO]) VALUES (2, N'CLIENTE', N'CLIENTE')
INSERT [dbo].[TIPO_USUARIO] ([ID_TIPO_USUARIO], [TITULO_TIPO_USUARIO], [DESCRIPCION_TIPO_USUARIO]) VALUES (3, N'EMPLEADO', N'EMPLEADO')
SET IDENTITY_INSERT [dbo].[TIPO_USUARIO] OFF
GO
SET IDENTITY_INSERT [dbo].[USUARIO] ON 

INSERT [dbo].[USUARIO] ([ID_USUARIO], [ID_PERSONA], [NOMBRE_USUARIO], [CLAVE_USUARIO], [ID_TIPO_USUARIO], [ID_ESTADO]) VALUES (2, 2, N'Xddd', N'1234', 1, 3)
INSERT [dbo].[USUARIO] ([ID_USUARIO], [ID_PERSONA], [NOMBRE_USUARIO], [CLAVE_USUARIO], [ID_TIPO_USUARIO], [ID_ESTADO]) VALUES (3, 3, N'XDDD', N'1234', 3, 2)
INSERT [dbo].[USUARIO] ([ID_USUARIO], [ID_PERSONA], [NOMBRE_USUARIO], [CLAVE_USUARIO], [ID_TIPO_USUARIO], [ID_ESTADO]) VALUES (4, 4, N'XDDD', N'1234', 3, 2)
INSERT [dbo].[USUARIO] ([ID_USUARIO], [ID_PERSONA], [NOMBRE_USUARIO], [CLAVE_USUARIO], [ID_TIPO_USUARIO], [ID_ESTADO]) VALUES (5, 5, N'XDDD', N'1234', 3, 2)
INSERT [dbo].[USUARIO] ([ID_USUARIO], [ID_PERSONA], [NOMBRE_USUARIO], [CLAVE_USUARIO], [ID_TIPO_USUARIO], [ID_ESTADO]) VALUES (6, 6, N'XDDD', N'1234', 3, 2)
INSERT [dbo].[USUARIO] ([ID_USUARIO], [ID_PERSONA], [NOMBRE_USUARIO], [CLAVE_USUARIO], [ID_TIPO_USUARIO], [ID_ESTADO]) VALUES (7, 7, N'XDDD', N'1234', 3, 3)
INSERT [dbo].[USUARIO] ([ID_USUARIO], [ID_PERSONA], [NOMBRE_USUARIO], [CLAVE_USUARIO], [ID_TIPO_USUARIO], [ID_ESTADO]) VALUES (20, 20, N'KillerMaker721', N'1234', 3, 3)
SET IDENTITY_INSERT [dbo].[USUARIO] OFF
GO
ALTER TABLE [dbo].[DEPARTAMENTO]  WITH CHECK ADD  CONSTRAINT [FK_DEPARTAMENTO_PERSONA1] FOREIGN KEY([ID_ENCARGADO])
REFERENCES [dbo].[PERSONA] ([ID_PERSONA])
GO
ALTER TABLE [dbo].[DEPARTAMENTO] CHECK CONSTRAINT [FK_DEPARTAMENTO_PERSONA1]
GO
ALTER TABLE [dbo].[EMPLEADO]  WITH CHECK ADD FOREIGN KEY([ID_DEPARTAMENTO])
REFERENCES [dbo].[DEPARTAMENTO] ([ID_DEPARTAMENTO])
GO
ALTER TABLE [dbo].[EMPLEADO]  WITH CHECK ADD FOREIGN KEY([ID_ESTADO])
REFERENCES [dbo].[ESTADO] ([ID_ESTADO])
GO
ALTER TABLE [dbo].[EMPLEADO]  WITH CHECK ADD FOREIGN KEY([ID_PERSONA])
REFERENCES [dbo].[PERSONA] ([ID_PERSONA])
GO
ALTER TABLE [dbo].[ENVIO]  WITH CHECK ADD FOREIGN KEY([ID_ESTADO])
REFERENCES [dbo].[ESTADO] ([ID_ESTADO])
GO
ALTER TABLE [dbo].[ENVIO]  WITH CHECK ADD  CONSTRAINT [FK_ENVIO_PERSONA1] FOREIGN KEY([ID_PERSONA])
REFERENCES [dbo].[PERSONA] ([ID_PERSONA])
GO
ALTER TABLE [dbo].[ENVIO] CHECK CONSTRAINT [FK_ENVIO_PERSONA1]
GO
ALTER TABLE [dbo].[ENVIO]  WITH CHECK ADD  CONSTRAINT [FK_ENVIO_PRODUCTO] FOREIGN KEY([ID_PRODUCTO])
REFERENCES [dbo].[PRODUCTO] ([ID_PRODUCTO])
GO
ALTER TABLE [dbo].[ENVIO] CHECK CONSTRAINT [FK_ENVIO_PRODUCTO]
GO
ALTER TABLE [dbo].[PRODUCTO]  WITH CHECK ADD FOREIGN KEY([ID_ESTADO])
REFERENCES [dbo].[ESTADO] ([ID_ESTADO])
GO
ALTER TABLE [dbo].[PRODUCTO]  WITH CHECK ADD FOREIGN KEY([ID_TIPO_PRODUCTO])
REFERENCES [dbo].[TIPO_PRODUCTO] ([ID_TIPO_PRODUCTO])
GO
ALTER TABLE [dbo].[QUEJA]  WITH CHECK ADD FOREIGN KEY([ID_ESTADO])
REFERENCES [dbo].[ESTADO] ([ID_ESTADO])
GO
ALTER TABLE [dbo].[QUEJA]  WITH CHECK ADD FOREIGN KEY([ID_PERSONA])
REFERENCES [dbo].[PERSONA] ([ID_PERSONA])
GO
ALTER TABLE [dbo].[QUEJA]  WITH CHECK ADD  CONSTRAINT [FK_QUEJA_DEPARTAMENTO] FOREIGN KEY([ID_DEPARTAMENTO])
REFERENCES [dbo].[DEPARTAMENTO] ([ID_DEPARTAMENTO])
GO
ALTER TABLE [dbo].[QUEJA] CHECK CONSTRAINT [FK_QUEJA_DEPARTAMENTO]
GO
ALTER TABLE [dbo].[QUEJA]  WITH CHECK ADD  CONSTRAINT [FK_QUEJA_TIPO_QUEJA] FOREIGN KEY([ID_TIPO_QUEJA])
REFERENCES [dbo].[TIPO_QUEJA] ([ID_TIPO_QUEJA])
GO
ALTER TABLE [dbo].[QUEJA] CHECK CONSTRAINT [FK_QUEJA_TIPO_QUEJA]
GO
ALTER TABLE [dbo].[RECLAMACION]  WITH CHECK ADD FOREIGN KEY([ID_DEPARTAMENTO])
REFERENCES [dbo].[DEPARTAMENTO] ([ID_DEPARTAMENTO])
GO
ALTER TABLE [dbo].[RECLAMACION]  WITH CHECK ADD FOREIGN KEY([ID_ESTADO])
REFERENCES [dbo].[ESTADO] ([ID_ESTADO])
GO
ALTER TABLE [dbo].[RECLAMACION]  WITH CHECK ADD FOREIGN KEY([ID_PERSONA])
REFERENCES [dbo].[PERSONA] ([ID_PERSONA])
GO
ALTER TABLE [dbo].[RECLAMACION]  WITH CHECK ADD FOREIGN KEY([ID_TIPO_RECLAMACION])
REFERENCES [dbo].[TIPO_RECLAMACION] ([ID_TIPO_RECLAMACION])
GO
ALTER TABLE [dbo].[RESPUESTA]  WITH CHECK ADD FOREIGN KEY([ID_EMPLEADO])
REFERENCES [dbo].[EMPLEADO] ([ID_EMPLEADO])
GO
ALTER TABLE [dbo].[RESPUESTA]  WITH CHECK ADD FOREIGN KEY([ID_QUEJA])
REFERENCES [dbo].[QUEJA] ([ID_QUEJA])
GO
ALTER TABLE [dbo].[RESPUESTA]  WITH CHECK ADD FOREIGN KEY([ID_RECLAMACION])
REFERENCES [dbo].[RECLAMACION] ([ID_RECLAMACION])
GO
ALTER TABLE [dbo].[TIPO_QUEJA]  WITH CHECK ADD FOREIGN KEY([ID_ESTADO])
REFERENCES [dbo].[ESTADO] ([ID_ESTADO])
GO
ALTER TABLE [dbo].[TIPO_RECLAMACION]  WITH CHECK ADD FOREIGN KEY([ID_ESTADO])
REFERENCES [dbo].[ESTADO] ([ID_ESTADO])
GO
ALTER TABLE [dbo].[USUARIO]  WITH CHECK ADD FOREIGN KEY([ID_ESTADO])
REFERENCES [dbo].[ESTADO] ([ID_ESTADO])
GO
ALTER TABLE [dbo].[USUARIO]  WITH CHECK ADD FOREIGN KEY([ID_TIPO_USUARIO])
REFERENCES [dbo].[TIPO_USUARIO] ([ID_TIPO_USUARIO])
GO
ALTER TABLE [dbo].[USUARIO]  WITH CHECK ADD  CONSTRAINT [FK_USUARIO_PERSONA] FOREIGN KEY([ID_PERSONA])
REFERENCES [dbo].[PERSONA] ([ID_PERSONA])
GO
ALTER TABLE [dbo].[USUARIO] CHECK CONSTRAINT [FK_USUARIO_PERSONA]
GO
/****** Object:  StoredProcedure [dbo].[ELIMINA_EMPLEADO]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ELIMINA_EMPLEADO]
(
	@ID_PERSONA INT
)
AS
BEGIN
	UPDATE EMPLEADO
	SET ID_ESTADO=3
	WHERE ID_PERSONA=@ID_PERSONA
END


GO
/****** Object:  StoredProcedure [dbo].[ELIMINA_PERSONA]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ELIMINA_PERSONA]
(
	@ID_PERSONA INT
)
AS
BEGIN
	UPDATE EMPLEADO
	SET ID_ESTADO=3
	WHERE ID_PERSONA=@ID_PERSONA
END


SELECT * FROM ESTADO
GO
/****** Object:  StoredProcedure [dbo].[ELIMINA_PERSONA_USUARIO]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ELIMINA_PERSONA_USUARIO]
(
	@ID_PERSONA INT
)
AS
	
BEGIN
	UPDATE USUARIO SET  ID_ESTADO =3 WHERE ID_PERSONA=@ID_PERSONA
END
GO
/****** Object:  StoredProcedure [dbo].[ELIMINA_QUEJA]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ELIMINA_QUEJA](@ID_QUEJA INT)
AS
BEGIN
	UPDATE QUEJA SET ID_ESTADO=3 WHERE ID_QUEJA=@ID_QUEJA
END
GO
/****** Object:  StoredProcedure [dbo].[ELIMINA_RECLAMACION]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ELIMINA_RECLAMACION](@ID_RECLAMACION INT)
AS
BEGIN
	UPDATE RECLAMACION SET ID_ESTADO=3 WHERE ID_RECLAMACION=@ID_RECLAMACION
END
GO
/****** Object:  StoredProcedure [dbo].[ERROR_MESSAGES]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[ERROR_MESSAGES]
AS
BEGIN 
	DECLARE @ERROR INT=@@ERROR

	IF @ERROR=0
		SELECT 'Solicitud satisfactoria' AS 'Text'
	
	ELSE IF @@ROWCOUNT=0
		SELECT 'Ningun Registro afectado' AS 'Text'
	
	ELSE
		(SELECT sys.messages.text FROM sys.messages WHERE sys.messages.message_id= @@ERROR AND SYS.messages.language_id=3082)
END
GO
/****** Object:  StoredProcedure [dbo].[INSERTA_DEPARTAMENTO]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[INSERTA_DEPARTAMENTO]
(
	@NOMBRE_DEPARTAMENTO VARCHAR(50),
	@ID_ENCARGADO INT

)
AS
BEGIN
	INSERT INTO DEPARTAMENTO VALUES 
	(
		@NOMBRE_DEPARTAMENTO,
		@ID_ENCARGADO
	)
END
GO
/****** Object:  StoredProcedure [dbo].[INSERTA_EMPLEADO]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[INSERTA_EMPLEADO]
(
	@NOMBRE VARCHAR(50),
	@FECHA_NACIMIENTO_EMPLEADO DATE,
	@CEDULA_EMPLEADO CHAR(11),
	@CORREO_EMPLEADO VARCHAR(70),
	@TELEFONO_EMPLADO CHAR(10),
	@GENERO_EMPLEADO CHAR(1),
	@NOMBRE_USUARIO VARCHAR(50),
	@CLAVE_USUARIO VARCHAR(50),
	@ID_DEPARTAMENTO INT

)
AS
BEGIN
	BEGIN TRAN T1
		BEGIN TRY
		EXEC INSERTA_PERSONA
			@NOMBRE,
			@FECHA_NACIMIENTO_EMPLEADO,
			@CEDULA_EMPLEADO,
			@CORREO_EMPLEADO,
			@TELEFONO_EMPLADO,
			@GENERO_EMPLEADO,
			@NOMBRE_USUARIO,
			@CLAVE_USUARIO,
			3

		INSERT INTO EMPLEADO VALUES
		(
			(SELECT MAX(ID_PERSONA)FROM PERSONA),
			@ID_DEPARTAMENTO,
			2
		)
		COMMIT TRAN T1
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN T1
	END CATCH
END


GO
/****** Object:  StoredProcedure [dbo].[INSERTA_ENVIO]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[INSERTA_ENVIO]
(
	@ID_PERSONA INT,
	@ID_PRODUCTO INT,
	@FECHA_ENVIO DATETIME,
	@DIRECCION_ENVIO VARCHAR(200),
	@ID_ESTADO INT
)
AS
BEGIN
	INSERT INTO ENVIO VALUES 
	(
		@ID_PERSONA,
		@ID_PRODUCTO,
		@FECHA_ENVIO,
		@DIRECCION_ENVIO,
		@ID_ESTADO
	)
END
GO
/****** Object:  StoredProcedure [dbo].[INSERTA_ESTADO]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[INSERTA_ESTADO]
(
	@TITULO_ESTADO VARCHAR(50),
	@DESCRIPCION_ESTADO VARCHAR(200)

)
AS
BEGIN
	INSERT INTO ESTADO VALUES 
	(
		@TITULO_ESTADO,
		@DESCRIPCION_ESTADO
	)
END
GO
/****** Object:  StoredProcedure [dbo].[INSERTA_PERSONA]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[INSERTA_PERSONA]
(
	@NOMBRE VARCHAR(50),
	@FECHA_NACIMIENTO_PERSONA DATE,
	@CEDULA_PERSONA CHAR(11),
	@CORREO_PERSONA VARCHAR(70),
	@TELEFONO_PERSONA CHAR(10),
	@GENERO_PERSONA CHAR(1),
	@NOMBRE_USUARIO VARCHAR(50),
	@CLAVE_USUARIO VARCHAR(50),
	@TIPO_USUARIO INT


)
AS
BEGIN
	--BEGIN TRAN T1
	--BEGIN
		INSERT INTO PERSONA VALUES 
		(
			@NOMBRE,
			@FECHA_NACIMIENTO_PERSONA,
			@CEDULA_PERSONA,
			@CORREO_PERSONA,
			@TELEFONO_PERSONA,
			@GENERO_PERSONA
		)

		INSERT INTO USUARIO VALUES
		(
			(SELECT MAX(ID_PERSONA) FROM PERSONA),
			@NOMBRE_USUARIO,
			@CLAVE_USUARIO,
			@TIPO_USUARIO,
			2
		)
	--END
	--IF @@ERROR!=0
--		ROLLBACK TRAN T1
	END
GO
/****** Object:  StoredProcedure [dbo].[INSERTA_PRODUCTO]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[INSERTA_PRODUCTO]
(
	@NOMBRE_PRODUCTO VARCHAR(50),
	@PRECIO_PRODUCTO DECIMAL(8,2),
	@ID_ESTADO INT,
	@ID_TIPO_PRODUCTO INT
)
AS
BEGIN
	INSERT INTO PRODUCTO VALUES
	(
		@NOMBRE_PRODUCTO,
		@PRECIO_PRODUCTO,
		@ID_ESTADO,
		@ID_TIPO_PRODUCTO
	)
END
GO
/****** Object:  StoredProcedure [dbo].[INSERTA_QUEJA]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[INSERTA_QUEJA]
(
	@ID_PERSONA INT,
	@ID_DEPARTAMENTO INT,
	@FECHA_QUEJA DATETIME,
	@DESCRIPCION_QUEJA VARCHAR(50),
	@ID_TIPO_QUEJA INT,
	@ID_ESTADO INT
)
AS
BEGIN
	INSERT INTO QUEJA(ID_PERSONA,ID_DEPARTAMENTO,FECHA_QUEJA,DESCRIPCION_QUEJA,ID_TIPO_QUEJA,ID_ESTADO) VALUES 
	(
		@ID_PERSONA,
		@ID_DEPARTAMENTO,
		@FECHA_QUEJA,
		@DESCRIPCION_QUEJA,
		@ID_TIPO_QUEJA,
		@ID_ESTADO

	)
END
GO
/****** Object:  StoredProcedure [dbo].[INSERTA_RECLAMACION]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[INSERTA_RECLAMACION]
(
	@ID_PERSONA INT,
	@ID_DEPARTAMENTO INT,
	@FECHA_RECLAMACION DATETIME,
	@DESCRIPCION_RECLAMACION VARCHAR(200),
	@ID_TIPO_RECLAMACION INT,
	@ID_ESTADO_RECLAMACION INT
)
AS
BEGIN
	INSERT INTO RECLAMACION VALUES
	(
		@ID_PERSONA,
		@ID_DEPARTAMENTO,
		@FECHA_RECLAMACION,
		@DESCRIPCION_RECLAMACION,
		@ID_TIPO_RECLAMACION,
		@ID_ESTADO_RECLAMACION
	)
END
GO
/****** Object:  StoredProcedure [dbo].[INSERTA_RESPUESTA]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[INSERTA_RESPUESTA]
(
	@ID_EMPLEADO INT,
	@ID_QUEJA INT,
	@ID_RECLAMACION INT,
	@MENSAJE_RESPUESTA VARCHAR(200),
	@FECHA_RESPUESTA DATETIME
)
AS
BEGIN
	INSERT INTO RESPUESTA VALUES
	(
		@ID_EMPLEADO,
		@ID_QUEJA,
		@ID_RECLAMACION,
		@MENSAJE_RESPUESTA,
		@FECHA_RESPUESTA
	)
END
GO
/****** Object:  StoredProcedure [dbo].[INSERTA_TIPO_PRODUCTO]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[INSERTA_TIPO_PRODUCTO]
(
	@TITULO_TIPO_PRODUCTO VARCHAR(50),
	@DESCRIPCION_TIPO_PRODUCTO VARCHAR(200)
)
AS
BEGIN
	INSERT INTO TIPO_PRODUCTO VALUES
	(
		@DESCRIPCION_TIPO_PRODUCTO,
		@TITULO_TIPO_PRODUCTO
	)
END
GO
/****** Object:  StoredProcedure [dbo].[INSERTA_TIPO_QUEJA]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[INSERTA_TIPO_QUEJA]
(
	@DESCRIPCION_TIPO_QUEJA VARCHAR(200),
	@TITULO_TIPO_QUEJA VARCHAR(50),
	@ID_ESTADO INT
)
AS
BEGIN
	INSERT INTO TIPO_QUEJA VALUES
	(
		@DESCRIPCION_TIPO_QUEJA,
		@TITULO_TIPO_QUEJA,
		@ID_ESTADO
	)
END
GO
/****** Object:  StoredProcedure [dbo].[INSERTA_TIPO_RECLAMACION]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[INSERTA_TIPO_RECLAMACION]
(
	@DESCRIPCION_TIPO_RECLAMACION VARCHAR(200),
	@TITULO_TIPO_RECLAMACION VARCHAR(50),
	@ID_ESTADO INT
)
AS
BEGIN
	INSERT INTO TIPO_RECLAMACION VALUES
	(
		@DESCRIPCION_TIPO_RECLAMACION,
		@TITULO_TIPO_RECLAMACION,
		@ID_ESTADO
	)
END
GO
/****** Object:  StoredProcedure [dbo].[INSERTA_TIPO_USUARIO]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[INSERTA_TIPO_USUARIO]
(
	@TITULO_TIPO_USUARIO VARCHAR(50),
	@DESCRIPCION_TIPO_USUARIO VARCHAR(200)
)
AS
BEGIN
	INSERT INTO TIPO_USUARIO VALUES
	(
		@DESCRIPCION_TIPO_USUARIO,
		@TITULO_TIPO_USUARIO
	)
END
GO
/****** Object:  StoredProcedure [dbo].[INSERTA_USUARIO]    Script Date: 6/19/2021 8:24:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[INSERTA_USUARIO]
(
	@ID_PERSONA INT,
	@NOMBRE_USUARIO VARCHAR(50),
	@CLAVE_USUARIO VARCHAR(50),
	@TIPO_USUARIO INT
)
AS
BEGIN 
	INSERT INTO USUARIO VALUES
	(
		@ID_PERSONA,
		@NOMBRE_USUARIO,
		@CLAVE_USUARIO,
		@TIPO_USUARIO
	)
END
GO
