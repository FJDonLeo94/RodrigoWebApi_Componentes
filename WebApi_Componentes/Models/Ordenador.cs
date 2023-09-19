namespace WebApi_Componentes.Models;

public class Ordenador
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public ICollection<Componente> Componentes { get; set; } = new List<Componente>();

    // Foreign Keys
    public int? PedidoId { get; set; } // Optional foreign key property

    public Pedido? Pedido { get; set; } // Optional reference navigation to principal

    public int Coste { get; set; }
}
