using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.EF
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [Required(ErrorMessage = "Not Allow to be null")]
        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Not Allow to be null")]
        public bool Status { get; set; }
    }
}
