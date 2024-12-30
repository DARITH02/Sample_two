using System.ComponentModel.DataAnnotations;

namespace Sample_two.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Cate_name { get; set; }
    }
}
