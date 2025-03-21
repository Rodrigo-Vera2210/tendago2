USE [DB_A13395_TendaGo]
GO
/****** Object:  StoredProcedure [dbo].[PagoCredito_Insert]    Script Date: 19/4/2019 17:10:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* ---------------------------------------------------------------------------------------

 * ---------------------------------------------------------------------------------------
 * Created By:   Edison Romero 
 * Date Created: 18/FEB/2019
 * Updated By: Christian Morante
 * Date Updated: 19/APR/2019 
  --------------------------------------------------------------------------------------- */
              
              

ALTER PROCEDURE [dbo].[PagoCredito_Insert]
(
	@Id int OUTPUT,
	@IdEmpresa int,
	@IdProveedor int,
	@Fecha datetime ,
	@Detalle varchar(50) ,
	@IpIngreso varchar(15) ,
	@UsuarioIngreso varchar(15) ,
	@FechaIngreso datetime ,
	@IpModificacion varchar(15) ,
	@UsuarioModificacion varchar(15) ,
	@FechaModificacion datetime ,
	@IdEstado smallint  

)
AS

SELECT @Id = MAX(Id)+1 FROM [dbo].[PagoCredito]
SELECT @Id = ISNULL(@Id,1)  
 
 INSERT
 INTO  [dbo].[PagoCredito]
       ( 
		Id,
		IdEmpresa,
		IdProveedor,
		Fecha,
		Detalle,
		IpIngreso,
		UsuarioIngreso,
		FechaIngreso,
		IpModificacion,
		UsuarioModificacion,
		FechaModificacion,
		IdEstado
	    ) 
 VALUES 
       ( 
		@Id,
		@IdEmpresa,
		@IdProveedor,
		@Fecha,
		@Detalle,
		@IpIngreso,
		@UsuarioIngreso,
		@FechaIngreso,
		@IpModificacion,
		@UsuarioModificacion,
		@FechaModificacion,
		@IdEstado
       )      
 
  

