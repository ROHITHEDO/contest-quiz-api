using ContestSystem.Enums;

namespace ContestSystem.Models
{
    public class Contest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public ContestType ContestType { get; set; }

        public string Prize { get; set; }

        public ICollection<Question>? Questions { get; set; }
    }
}
