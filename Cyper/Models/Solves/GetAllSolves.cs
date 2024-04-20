namespace Cyper.Models.Solves
{
    public class GetAllSolves
    {
        public int Id { get; set; }
        public int ProblemId { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
    }
}
