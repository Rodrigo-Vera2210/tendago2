
/****** Object:  StoredProcedure [dbo].[Usuario_LoadByPK]    Script Date: 13/4/2019 0:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* ---------------------------------------------------------------------------------------

 * ---------------------------------------------------------------------------------------
 * Created By:   Edison Romero 
 * Date Created: 18/FEB/2019 
  --------------------------------------------------------------------------------------- */
              
              
ALTER PROCEDURE [dbo].[Usuario_LoadByPK]
(
  	@InicioSesion varchar(50)
)
AS

 SELECT 
		[dbo].[Usuario].InicioSesion,
		[dbo].[Usuario].IdEmpresa,
		[dbo].[Usuario].IdPerifl,
		[dbo].[Usuario].Nombres,
		[dbo].[Usuario].Identificacion,
		[dbo].[Usuario].Sexo,
		[dbo].[Usuario].Direccion,
		[dbo].[Usuario].Correo,
		[dbo].[Usuario].Contraseña,
		[dbo].[Usuario].Telefono,
		[dbo].[Usuario].Foto,
		[dbo].[Usuario].IpIngreso,
		[dbo].[Usuario].UsuarioIngreso,
		[dbo].[Usuario].FechaIngreso,
		[dbo].[Usuario].IpModificacion,
		[dbo].[Usuario].UsuarioModificacion,
		[dbo].[Usuario].FechaModificacion,
		[dbo].[Usuario].IdEstado,


		EmpresaFromIdEmpresa.Id AS Id_EmpresaFromIdEmpresa,
		EmpresaFromIdEmpresa.NombreEmpresa AS NombreEmpresa_EmpresaFromIdEmpresa,
		EmpresaFromIdEmpresa.Direccion AS Direccion_EmpresaFromIdEmpresa,
		EmpresaFromIdEmpresa.Telefono AS Telefono_EmpresaFromIdEmpresa,
		EmpresaFromIdEmpresa.IpIngreso AS IpIngreso_EmpresaFromIdEmpresa,
		EmpresaFromIdEmpresa.UsuarioIngreso AS UsuarioIngreso_EmpresaFromIdEmpresa,
		EmpresaFromIdEmpresa.FechaIngreso AS FechaIngreso_EmpresaFromIdEmpresa,
		EmpresaFromIdEmpresa.IpModificacion AS IpModificacion_EmpresaFromIdEmpresa,
		EmpresaFromIdEmpresa.UsuarioModificacion AS UsuarioModificacion_EmpresaFromIdEmpresa,
		EmpresaFromIdEmpresa.FechaModificacion AS FechaModificacion_EmpresaFromIdEmpresa,
		EmpresaFromIdEmpresa.IdEstado AS IdEstado_EmpresaFromIdEmpresa,
		EmpresaFromIdEmpresa.RaizArchivo AS RaizArchivo_EmpresaFromIdEmpresa,

		PerfilFromIdPerifl.Id AS Id_PerfilFromIdPerifl,
		PerfilFromIdPerifl.Perfil AS Perfil_PerfilFromIdPerifl,
		PerfilFromIdPerifl.IpIngreso AS IpIngreso_PerfilFromIdPerifl,
		PerfilFromIdPerifl.UsuarioIngreso AS UsuarioIngreso_PerfilFromIdPerifl,
		PerfilFromIdPerifl.FechaIngreso AS FechaIngreso_PerfilFromIdPerifl,
		PerfilFromIdPerifl.IpModificacion AS IpModificacion_PerfilFromIdPerifl,
		PerfilFromIdPerifl.UsuarioModificacion AS UsuarioModificacion_PerfilFromIdPerifl,
		PerfilFromIdPerifl.FechaModificacion AS FechaModificacion_PerfilFromIdPerifl,
		PerfilFromIdPerifl.IdEstado AS IdEstado_PerfilFromIdPerifl

 FROM [dbo].[Usuario]
 INNER JOIN [dbo].[Empresa] as EmpresaFromIdEmpresa
 ON
 (
	[dbo].[Usuario].IdEmpresa = EmpresaFromIdEmpresa.Id
 )
 INNER JOIN [dbo].[Perfil] as PerfilFromIdPerifl
 ON
 (
	[dbo].[Usuario].IdPerifl = PerfilFromIdPerifl.Id
 )

 WHERE
 	[dbo].[Usuario].InicioSesion = @InicioSesion 
 

