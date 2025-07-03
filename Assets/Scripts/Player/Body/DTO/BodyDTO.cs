public class BodyDTO
{
    public string BodyName { get; set; }      
    public TierType Tier { get; set; }
    public int Hp { get; set; }
    public int Shield { get; set; }
    public bool IsDead { get; set; }
    public bool IsDmgHalf { get; set; }
    public int UpgradeFleshCount { get; set; }
    public float PartDropProbability { get; set; }
}