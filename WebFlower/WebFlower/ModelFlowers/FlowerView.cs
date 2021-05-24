using System;
using System.ComponentModel.DataAnnotations;


namespace WebFlower.ModelFlowers
{
    public class FlowerView
    {

        [Required(ErrorMessage = "{0} не може бути порожнім"), StringLength(255, ErrorMessage = "{0} не може бути коротше {2} і довше {1} символов.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} не може бути порожнім"), StringLength(255, ErrorMessage = "{0} не може бути коротше {2} і довше {1} символов.", MinimumLength = 2)]
        public string Family { get; set; }

        [Required, Range(1, 300, ErrorMessage = "неправильні межі дозволених значень")]
        public int Weight { get; set; }

        [Required(ErrorMessage = "{0} не може бути порожнім"), StringLength(255, ErrorMessage = "Перевищено ліміт введених символів!")]
        public string Image { get; set; }

    }
}
