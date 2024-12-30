using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Sample_two.Models
{
    public class Items
    {
        [Key]
        public int Id { get; set; }
        public string Pro_name { get; set; }
        public decimal Price{ get; set; }
        public bool Status{ get; set; }
        public string Cate_name { get; set; }
        public string Condition{ get; set; }
    }
}
