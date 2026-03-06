using ContestSystem.Enums;

namespace ContestSystem.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public QuestionType Type { get; set; }

        public int ContestId { get; set; }

        public Contest? Contest { get; set; }

        public ICollection<Option>? Options { get; set; }
    }
}
