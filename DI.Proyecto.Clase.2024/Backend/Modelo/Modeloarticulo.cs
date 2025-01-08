using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using di.proyecto.clase._2024.MVVM.Base;
using Microsoft.EntityFrameworkCore;

namespace DI.Proyecto.Clase._2024.Backend.Modelo;

[Table("modeloarticulo")]
[Index("Tipo", Name = "fk_tipoarticulos_modeloarticulo_idx")]
public partial class Modeloarticulo : PropertyChangedDataError
{
    /// <summary>
    /// Es un catalogo de articulos existentes. De cada modelo puede haber varias unidades con distintos numeros de serie, etc
    /// </summary>
    [Key]
    [Column("idmodeloarticulo")]
    public int Idmodeloarticulo { get; set; }

    [Column("nombre")]
    [StringLength(45, ErrorMessage = "El campo nombre no puede superar los 45 caracteres")]
    [Required(ErrorMessage ="El nombre del modelo no puede estar vacío")]
    public string? Nombre { get; set; }

    [Column("descripcion", TypeName = "mediumtext")]
    public string? Descripcion { get; set; }

    [Column("marca")]
    [StringLength(255,ErrorMessage = "El campo marca no puede superar los 255 caracteres")]
    public string? Marca { get; set; }

    [Column("modelo")]
    [StringLength(255, ErrorMessage = "El campo modelo no puede superar los 255 caracteres")]
    public string? Modelo { get; set; }

    [Column("tipo")]
    public int? Tipo { get; set; }

    [InverseProperty("ModeloNavigation")]
    public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();

    [InverseProperty("ModeloNavigation")]
    public virtual ICollection<Ficheromodelo> Ficheromodelos { get; set; } = new List<Ficheromodelo>();

    [ForeignKey("Tipo")]
    [InverseProperty("Modeloarticulos")]
    [Required (ErrorMessage = " Debe de seleccionar un tipo de artículo")]
    public virtual Tipoarticulo? TipoNavigation { get; set; }
}
