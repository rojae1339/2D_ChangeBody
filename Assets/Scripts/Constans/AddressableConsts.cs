public static class AddressableLabelConstants
{
    public const string Weapon = "Weapon";
    public const string Leg = "Leg";
    public const string Body = "Body";
    public const string Particle = "Particle";
    public const string Enemy = "Enemy";
    public const string Boss = "Boss";
}

public static class AddressableLabelGroup
{
    public static readonly string[] PlayerGroup = new[] { "Weapon", "Body"  };
    public static readonly string[] StartGroup = new[] { "Weapon", "Body", "Particle", "Enemy", "Boss", "UI" };
}
