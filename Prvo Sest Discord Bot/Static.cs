using Discord;

public static class Static
{
    public static readonly Color Color = new Color(255, 0, 0);
#if DEBUG
    public static readonly string Prefix = "-";
#else
    public static readonly string Prefix = "!";
#endif
}