namespace ContestSystem.Models
{
    public class ContestParticipation
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ContestId { get; set; }

        public int Score { get; set; }

        public bool IsSubmitted { get; set; }

        public User User { get; set; }

        public Contest Contest { get; set; }
    }
}
