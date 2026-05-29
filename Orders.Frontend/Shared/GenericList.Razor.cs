using Microsoft.AspNetCore.Components;

namespace Orders.Frontend.Shared
{
    public partial class GenericList<TItem>
    {
        [Parameter] public RenderFragment? Loading { get; set; } //Cuando este cargando
        [Parameter] public RenderFragment? NoRecords { get; set; } // Cuando no haya nada
        [EditorRequired, Parameter] public RenderFragment? Body { get; set; } = null!; // Cuando haya datos
        [EditorRequired, Parameter] public List<TItem>? MyList { get; set; } = null!; // Cuando haya datos



    }
}
