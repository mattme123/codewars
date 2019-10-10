using System;
using System.ComponentModel.DataAnnotations;

namespace codeWarsAPI.Models
{
    public class Kata
    {
        [Key]
        public int KataId { get; set; }
        public string KataName { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
