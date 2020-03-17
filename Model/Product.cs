using System.ComponentModel.DataAnnotations;

namespace EjemploPracticaNetCore.Model
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
