
/****** Object:  StoredProcedure [dbo].[Custom_Exportacion]    Script Date: 18/4/2019 15:14:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[Custom_Exportacion]
as

select codigoproveedor, CodigoInterno, foto from Producto
where foto is not null and len(convert(varchar(max),foto,2)) >1 


