using System.ComponentModel.DataAnnotations;

namespace Orders.Shared.Entities
{
    public class Country
    {
        public int Id { get; set; }

        [Display(Name = "País")] //DataAnnotations para indicar el nombre que se mostrará en la interfaz de usuario. En este caso, se mostrará "País" en lugar de "Name".
        [MaxLength(100, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")] // DataAnnotations para indicar la longitud máxima del campo. En este caso, el nombre del país no puede exceder los 100 caracteres.
        [Required(ErrorMessage = "El campo {0} es obligatorio.")] // DataAnnotations para indicar que el campo es obligatorio.
        public string Name { get; set; } = null!; //Signo de ? indica que el valor puede ser nulo

        //Y null! indica que el valor no puede ser nulo, pero se inicializará en otro lugar (por ejemplo, en el constructor o mediante la asignación directa)
    }
}