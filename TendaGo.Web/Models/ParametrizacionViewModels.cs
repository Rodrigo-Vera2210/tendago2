using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TendaGo.Web.Models
{
    public class UnidadMedidaViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
    }

    public class ClienteViewModel : EntidadViewModel { }

    public class ProveedorViewModel : EntidadViewModel { }

    public class EntidadViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Empresa")]
        public int IdEmpresa { get; set; }

        [Display(Name = "Razón Social")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string RazonSocial { get; set; }

        [Display(Name = "Tipo Identificación")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string TipoIdentificacion { get; set; }
        
        [Display(Name = "Identificación")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Identificacion { get; set; }

        [Display(Name = "País")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public short IdPais { get; set; }

        public PaisViewModel Pais { get; set; }

        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdProvincia { get; set; }

        public ProvinciaViewModel Provincia { get; set; }

        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdCiudad { get; set; }

        public CiudadViewModel Ciudad { get; set; }

        [Display(Name = "Direccion")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Direccion { get; set; }

        [Display(Name = "Sector")]
        [Required(ErrorMessage = "El campo {0} es requerido")] 
        public int IdSector { get; set; }

        [Display(Name = "Telefono")]
        [Phone(ErrorMessage = "Debe ingresar un # {0} valido")]
        public string Telefono { get; set; }

        [Display(Name = "Celular")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Phone(ErrorMessage = "Debe ingresar un # {0} valido")]
        public string Celular { get; set; }

        [Display(Name = "Correo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "Debe ingresar un {0} válido")]
        public string Correo { get; set; }

        [Display(Name = "Foto")]
        public byte[] Foto { get; set; }

        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        [Display(Name = "Estado")]
        public short IdEstado { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime? FechaNacimiento { get; set; }
        
        [Display(Name = "Estado Civil")]
        public string EstadoCivil { get; set; }

        [Display(Name = "Nivel de Estudio")]
        public string NivelEstudio { get; set; }

        [Display(Name = "Profesion")]
        public string Profesion { get; set; }

        [Display(Name = "Traer todas las facturas")]
        public bool Full { get; set; }
    }

















    public class SectorViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public short IdEstado { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }

    public class PaisViewModel
    {
        public short Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Nacionalidad { get; set; }
        public string CodigoNacionalidad { get; set; }
        public short IdEstado { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }

    public class ProvinciaViewModel
    {
        public int Id { get; set; }
        public short IdPais { get; set; }
        public PaisViewModel Pais { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public short IdEstado { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }

    public class CiudadViewModel
    {
        public int Id { get; set; }
        public int IdProvincia { get; set; }
        public ProvinciaViewModel Provincia { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public short IdEstado { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }

    //public class TipoUnidadViewModel
    //{
    //    public int Id { get; set; }
    //    public short IdEmpresa { get; set; }
    //    public int IdProducto { get; set; }
    //    public string Nombre { get; set; }
    //    public decimal Cantidad { get; set; }
    //    public int IdUnidadMedidad { get; set; }
    //    //public UnidadMedidaViewModel UnidadMedida { get; set; }
    //    public short IdEstado { get; set; }
    //}

    //public class PrecioViewModel
    //{
    //    public int Id { get; set; }
    //    [Required(ErrorMessage = "Dato requerido")]
    //    public int IdProducto { get; set; }
    //    [Required(ErrorMessage = "Dato requerido")]
    //    public int IdTipoUnidad { get; set; }
    //    public TipoUnidadViewModel TipoUnidad { get; set; }
    //    [Required(ErrorMessage = "Dato requerido")]
    //    public decimal ValorPrecio { get; set; }
    //    [Required(ErrorMessage = "Dato requerido")]
    //    public int Moneda { get; set; }
    //    [Required(ErrorMessage = "Dato requerido")]
    //    public bool IncluyeIva { get; set; }
    //    public short IdEstado { get; set; }
    //}

    

    public enum Estado
    {
        INACTIVO = 0,
        ACTIVO = 1
    }

    public class DivisionModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "División")]
        public string Name { get; set; }

        [Display(Name = "Estado")]
        public bool IdEstado { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

    public class LineModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "División")]
        public int IdDivision { get; set; }

        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Línea")]
        public string Name { get; set; }

        [Display(Name = "Estado")]
        public bool IdEstado { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class CategoriaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "División")]
        public int IdDivision{ get; set; }

        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Línea")]
        public int IdLinea { get; set; }

        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Categoría")]
        public string Categoria { get; set; }

        [Display(Name = "Estado")]
        public bool IdEstado { get; set; }
        public override string ToString()
        {
            return Categoria;
        }
    }

    public class MarcaModel
    {
        public int Id { get; set; }

        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Display(Name = "Estado")]
        public bool IdEstado { get; set; }

        public override string ToString()
        {
            return Marca;
        }
    }

    public class UnidadMedidaModel
    {
        public int Id { get; set; }

        [Display(Name = "Unidad")]
        public string UnidadMedida { get; set; }

        [Display(Name = "Estado")]
        public bool IdEstado { get; set; }
    }

    public class PrecioProductoViewModel
    {
        public int IdProducto { get; set; }
        public string FotoDataUrl { get; set; }

    }

    public class TipoUnidadModel
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }

        [Display(Name = "Tipo de Unidad")]
        public string TipoUnidad { get; set; }
        public decimal Cantidad { get; set; }
        [Display(Name = "Cantidad Mínima")]
        public decimal CantidadMinima { get; set; }

        [Display(Name = "Unidad de Medida")]
        public int UnidadMedidad { get; set; }
        public decimal Precio { get; set; }

        [Display(Name = "% Margen")]
        public decimal Margen { get; set; }

        [Display(Name = "IVA")]
        public bool IncluyeIva { get; set; }

        [Display(Name = "Estado")]
        public bool IdEstado { get; set; }

        public string Producto { get; set; }
        [Display(Name = "Unidad de Medida")]
        public string UnidadMedida { get; set; }

        public decimal Costo { get; set; }

    }

}