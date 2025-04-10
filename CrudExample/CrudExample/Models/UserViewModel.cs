using System.ComponentModel.DataAnnotations;

namespace CrudExample.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required]
        public string name { get; set; }
        
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string status { get; set; }
        public string dept_id {  get; set; }

    }
    public class UserDetailsViewModel
    {
        [Required]
        public string Id { get; set; }

    }
}
