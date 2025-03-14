USE [DB_A412E7_TendaGo]
GO
/****** Object:  StoredProcedure [dbo].[Custom_Stock_SaldoInventario]    Script Date: 1/5/2019 15:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* ---------------------------------------------------------------------------------------

 * ---------------------------------------------------------------------------------------
 * Created By:   Edison Romero 
 * Date Created: 03/NOV/2018 
 * Actualizado el 01/MAY/2019 - cmodev
  --------------------------------------------------------------------------------------- */
              
              
ALTER PROCEDURE [dbo].[Custom_Stock_SaldoInventario]
(
	@Id varchar(50)=NULL,
	@IdEmpresa int,
	@IdProducto int,
	@IdLocal int = NULL
)
AS
BEGIN 

	DECLARE @FECHAULTIMATRASACCION DATETIME

	SELECT @FECHAULTIMATRASACCION=MAX(FECHA) FROM [dbo].[Stock] S 
	WHERE S.IdEmpresa=@IdEmpresa and s.idProducto=@IdProducto and s.idlocal=@IdLocal

	 SELECT 
			P.Id,
			S.IdEmpresa,
			S.IdProducto,
			P.Producto,
			P.Descripcion,
			P.PathArchivo,
			P.CobraIva,
			S.IdLocal,
			L.[Local],
			L.Direccion,
			S.Fecha,
			S.StockInventario Stock,
			S.CostoPromedioPonderado Costo,
			S.SaldoInventario
	 FROM [dbo].[Stock] S
	 INNER JOIN Producto P ON S.IdProducto=P.Id
	 INNER JOIN LocalBodega L ON S.IdLocal=L.Id
	 WHERE 	S.Id LIKE ISNULL('%' + @Id + '%',S.Id) AND
		S.IdEmpresa = @IdEmpresa AND
		S.IdProducto=@IdProducto AND
		S.IdLocal = ISNULL(@IdLocal,S.IdLocal) AND
		S.Fecha = ISNULL(@FECHAULTIMATRASACCION,S.Fecha)
	
END

