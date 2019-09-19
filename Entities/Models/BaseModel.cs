using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public DateTime createdAt { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? updatedAt { get; set; }
    }
}