USE [DB_A13395_TendaGo]
GO
/****** Object:  StoredProcedure [dbo].[Custom_Entrada_ValidarCompra]    Script Date: 12/4/2019 15:16:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Custom_Entrada_ValidarCompra]
(
	@CodigoProducto VARCHAR(50),
	@CodigoUnidad varchar(50),
	@IdLocal int,
	@IdEmpresa int
)
AS
BEGIN 
--DECLARE @CodigoProducto VARCHAR(50)='006', @CodigoUnidad varchar(50)='UNIDAD', @IdLocal int=1, @IdEmpresa int=1

DECLARE @IDPRODUCTO INT, @UNIDAD varchar(50), @LOCAL Varchar(50), @PRODUCTO VARCHAR(50)

SELECT @IDPRODUCTO=ID,@PRODUCTO=Producto  FROM Producto WHERE CodigoProveedor=@CodigoProducto and IdEmpresa=@IdEmpresa

SELECT @UNIDAD=TipoUnidad FROM TipoUnidad WHERE IdProducto=@IDPRODUCTO and IdEmpresa=@IdEmpresa and TipoUnidad=@CodigoUnidad

SELECT @Local=Local FROM LocalBodega WHERE Id=@IdLocal and IdEmpresa=@IdEmpresa

SELECT ISNULL(@PRODUCTO,'Producto no existe') Producto, Isnull(@UNIDAD, 'Unidad no existe') Unidad, ISNULL(@LOCAL,'Local no existe') LocalBodega

END 