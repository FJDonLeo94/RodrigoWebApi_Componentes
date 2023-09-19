namespace WebApi_Componentes.Models;

public class Pedido
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public DateTime Fecha { get; set; }

    public ICollection<Ordenador> Ordenadores { get; set; } = new List<Ordenador>();

    public int Coste { get; set; }
}
