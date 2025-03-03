namespace TestApi.Models
{
    public class Cours
    {
        public int Id { get; set; }
        public string CourName { get; set; } = string.Empty;
        public string CourCode { get; set; } = string.Empty;
        public string CourDescription { get; set; } = string.Empty;
        public string CoursTeacher { get; set; } = string.Empty;
    }
}
