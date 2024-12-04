using Abstract;

namespace WPFApp.Model
{
    public class Human : IRowData
    {
        public Human() { }
        public Human(int number, string lName, string fName)
        {
            Number = number;
            LastName = lName;
            FirstName = fName;
        }
        public int Number { get; set; }

        public string LastName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
    }
}
