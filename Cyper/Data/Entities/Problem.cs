using System.ComponentModel.DataAnnotations.Schema;

namespace Cyper.Data.Entities
{
    public class Problem
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
    }
}
