namespace API.Entities
{
    public class Сomplaints
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool Processed {get; set;}
        public string UserName {get;set;}
    }
}