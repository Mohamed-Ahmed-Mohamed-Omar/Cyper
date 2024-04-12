using System.ComponentModel.DataAnnotations.Schema;

namespace Cyper.Data.Entities
{
    public class Solve
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public int ProblemId { get; set; }

        [ForeignKey(nameof(ProblemId))]
        public Problem problem { get; set; }
    }
}
