namespace Cyper.Models.Solves
{
    public class GetSolveDetails
    {
        public int ProblemId { get; set; }
        public string UserName { get; set; }
        public string CollageName { get; set; }
        public string Description_Solve { get; set; }
        public string Description_Problem { get; set; }
    }
}
