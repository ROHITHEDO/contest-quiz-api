namespace ContestSystem.DTOs
{
    public class AnswerDto
    {
        public int QuestionId { get; set; }

        public List<int> SelectedOptionIds { get; set; }
    }
}
