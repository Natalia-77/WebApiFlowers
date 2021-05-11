using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebFlower.Entities.Domain
{
    [Table("tblFlowers")]
    public class Flower
    {
        [Key]
        public long Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [Required, StringLength(255)]
        public string Family { get; set; }

        [Required]
        public int Weight { get; set; }

        [ StringLength(255)]
        public string Image { get; set; }
    }
}
