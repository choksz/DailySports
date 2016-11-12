namespace DailySports.ServiceLayer.Dtos
{
    public class CommentsDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public int NewsId { get; set; }
    }
}
