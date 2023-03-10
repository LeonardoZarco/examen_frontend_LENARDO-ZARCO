USE [examen_backend_Leonardo_Zarco]
GO
/****** Object:  StoredProcedure [dbo].[AgregarUsuario_y_datos]    Script Date: 19-Dec-22 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AgregarUsuario_y_datos]   

	@UserName VARCHAR (50),
    @Email VARCHAR (50),
    @Password  VARCHAR (50),
    @Nombre VARCHAR (50),
    @ApellidoPaterno VARCHAR (50),
    @ApellidoMaterno VARCHAR (50),
    @Direccion VARCHAR (50),
    @Telefono VARCHAR (15),
    @FechaNacimiento VARCHAR (50)
AS
BEGIN
    INSERT INTO Usuario (UserName, Email, Password)
    VALUES (@UserName, @Email, @Password)




    INSERT INTO DatosUsuario (ID, Nombre, ApellidoPaterno, ApellidoMaterno, Direccion, Telefono, FechaNacimiento)
    VALUES (@@IDENTITY, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @Direccion, @Telefono, CONVERT(DATE,@FechaNacimiento))

END
GO
/****** Object:  StoredProcedure [dbo].[EliminarUsuario]    Script Date: 19-Dec-22 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[EliminarUsuario] 
	    @ID INT
AS
BEGIN
    DELETE FROM DatosUsuario WHERE ID = @ID
    DELETE FROM Usuario WHERE ID = @ID
END

GO
/****** Object:  StoredProcedure [dbo].[GetByUsuarioDatos]    Script Date: 19-Dec-22 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetByUsuarioDatos] 
@ID INT
AS
SELECT 
		DatosUsuario.ID_Datos
		,Nombre
		,ApellidoPaterno 
		,ApellidoMaterno 
		,Direccion
		,Telefono
		,FechaNacimiento 
		,Usuario.ID
		,Usuario.UserName
		,Usuario.Email
		,Usuario.Password


  FROM [Usuario]
  INNER JOIN DatosUsuario ON Usuario.ID = DatosUsuario.ID

  WHERE Usuario.ID = @ID

GO
/****** Object:  StoredProcedure [dbo].[GetUsuarioDatos]    Script Date: 19-Dec-22 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUsuarioDatos] 
AS
	BEGIN
		SELECT 
		DatosUsuario.ID_Datos
		,Nombre
		,ApellidoPaterno 
		,ApellidoMaterno 
		,Direccion
		,Telefono
		,FechaNacimiento 
		,Usuario.ID
		,Usuario.UserName
		,Usuario.Email
		,Usuario.Password

		FROM [Usuario]

		INNER JOIN DatosUsuario ON Usuario.ID = DatosUsuario.ID
	
	END 

GO
/****** Object:  StoredProcedure [dbo].[ModificarUsuarioDatos]    Script Date: 19-Dec-22 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModificarUsuarioDatos]
	@ID INT,
	@Nombre VARCHAR (50),
	@ApellidoPaterno VARCHAR (50),
	@ApellidoMaterno VARCHAR (50),
	@Direccion VARCHAR (50),
	@Telefono VARCHAR (15),
	@FechaNacimiento VARCHAR (20),
    @UserName VARCHAR (50),
    @Email VARCHAR (50),
	@Password  VARCHAR (50)
		
AS
	UPDATE [Usuario]
   SET [UserName] = @UserName,
      [Email] = @Email, 
      [Password] = @Password
	  					
	 WHERE ID = @ID

 		UPDATE  DatosUsuario
		set
					Nombre=@Nombre, 
					ApellidoPaterno=@ApellidoPaterno,
					ApellidoMaterno=@ApellidoMaterno,
					Direccion=@Direccion,
					Telefono=@Telefono,
					FechaNacimiento=CONVERT(DATE,@FechaNacimiento,105)

					 WHERE ID = @ID
GO
/****** Object:  UserDefinedFunction [dbo].[CalcularEdadUsuario]    Script Date: 19-Dec-22 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[CalcularEdadUsuario] (@FechaNacimiento DATE)
RETURNS INT
AS
BEGIN
DECLARE @Edad INT
SET @Edad = DATEDIFF(YEAR, @FechaNacimiento, GETDATE())
RETURN @Edad
END
GO
/****** Object:  Table [dbo].[DatosUsuario]    Script Date: 19-Dec-22 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DatosUsuario](
	[ID_Datos] [int] IDENTITY(1,1) NOT NULL,
	[ID] [int] NULL,
	[Nombre] [varchar](50) NULL,
	[ApellidoPaterno] [varchar](50) NULL,
	[ApellidoMaterno] [varchar](50) NULL,
	[Direccion] [varchar](50) NULL,
	[Telefono] [varchar](15) NULL,
	[FechaNacimiento] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Datos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 19-Dec-22 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[DatosUsuario]  WITH CHECK ADD FOREIGN KEY([ID])
REFERENCES [dbo].[Usuario] ([ID])
GO
