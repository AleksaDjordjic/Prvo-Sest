namespace Bindings
{
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
}