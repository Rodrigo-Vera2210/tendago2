
/****** Object:  StoredProcedure [dbo].[Custom_Pantalla_ObtenerPantallasPerfil]    Script Date: 13/6/2019 0:43:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[Custom_Pantalla_ObtenerPantallasPerfil]
(
	@usuario VARCHAR(50)
)
AS
BEGIN
	--DECLARE @usuario VARCHAR(50)='dev'

	SELECT *
	FROM Pantalla Where Id In (
						SELECT IdPantalla
						FROM PantallaXPerfil
						where IdPerfil = (
								SELECT IdPerifl 
								FROM Usuario 
								where InicioSesion=@usuario
							) AND IdEstado = 1
						)

END

