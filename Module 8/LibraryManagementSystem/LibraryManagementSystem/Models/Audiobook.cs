namespace LibraryManagementSystem.Models
{
    public class Audiobook : LibraryItem
    {
        private int _durationMinutes;

        public string NarratorName { get; set; }

        public int DurationMinutes
        {
            get => _durationMinutes;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Duration must be positive.");

                _durationMinutes = value;
            }
        }

        public override int MaxBorrowDays => 21;

        public Audiobook(int id, string title, int publicationYear,
            string narratorName, int durationMinutes)
            : base(id, title, publicationYear)
        {
            NarratorName = narratorName;
            DurationMinutes = durationMinutes;
        }

        public override string ToString()
        {
            return $"[Audiobook] {base.ToString()} | Narrator: {NarratorName} | Duration: {DurationMinutes} mins";
        }
    }
}