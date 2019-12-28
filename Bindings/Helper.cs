namespace Bindings
{
    public static class Helper
    {
        public static string TestTypeToString(this TestType type)
        {
            switch (type)
            {
                case TestType.verbal:
                    return "Usmeno";
                case TestType.written:
                    return "Pismeni";
                default:
                    return "UNKNOWN";
            }
        }
    }
}
