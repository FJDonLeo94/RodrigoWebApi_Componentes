namespace WebApi_Componentes.Models;

public class Componente
{
    public int Id { get; set; }

    public string NumeroDeSerie { get; set; }

    public string Descripcion { get; set; }

    public int Calor { get; set; }

    public long Megas { get; set; }

    public int Cores { get; set; }

    public int Coste { get; set; }

    public TipoComponente Tipo { get; set; }

    public int? OrdenadorId { get; set; } // Optional foreign key property

    public Ordenador? Ordenador { get; set; } // Optional reference navigation to principal
}

public enum TipoComponente
{
    Procesador, RAM, DiscoDuro
}
