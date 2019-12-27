namespace Bindings
{
    public enum TestType
    {
        written = 0,
        verbal = 1
    }

    public class Student
    {
        public ulong ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ulong DiscordID { get; set; }
    }

    public class Subject
    {
        public ulong ID { get; set; }
        public string Name { get; set; }
    }

    public class Test
    {
        public ulong ID { get; set; }
        public ulong timeStamp { get; set; }
        public ulong subjectID { get; set; }
        public TestType type { get; set; }
        public string comment { get; set; }
    }
}