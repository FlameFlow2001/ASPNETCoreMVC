using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNETCoreMVC.Domain
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int _id { get; set; }

        [Required(ErrorMessage = "Заполните название")]
        [Display(Name = "Название задачи")]
        [StringLength(150)]
        public string _title { get; set; }
        [Display(Name = "Описание")]
        public string? _content { get; set; } = null;

        [Display(Name = "Время создания")]
        public DateTime _created_At { get; set; } = DateTime.Now;
    }
}
