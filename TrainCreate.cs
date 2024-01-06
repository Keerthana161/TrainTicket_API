using System.ComponentModel.DataAnnotations;

namespace Assignment.Model
{
    public class TrainCreate
    {
        [Required]
        public string from {  get; set; }
        [Required]
        public string to { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string sectionIn { get; set; }

        [Required]
        public float price {  get; set; }

    }

    public class TrainModify
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        public string email { get; set; }

        [Required]
        public string? sectionIn { get; set; }
    }

    public class TrainRemove
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }

        [Required]
        public string? sectionIn { get; set; }
    }

    public class TrainShow
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
    }


    public enum section
    {
        sectionA,
        sectionB
    }
}
