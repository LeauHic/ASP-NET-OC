using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChoixResto.Models
{
    [Table ("Utilisateurs")]
    public class Utilisateur
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Prenom { get; set; }
        
        [Required]
        public string motDePasse { get; set; }
    }
}