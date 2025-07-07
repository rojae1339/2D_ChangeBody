using UnityEngine;
using Random = System.Random;

public class P_UIModel : MonoBehaviour
{
    private string[] _randStrings = new[]
        { "James", "Jae", "Aurora", "UWU", "OWO", "IWI", "TT TT", "MERONG", "JODER", "NoTengoPutaNidea" };
    Random random = new Random();
    
    public string GenerateRandomString()
    {
        
        string newText = _randStrings[random.Next(0, _randStrings.Length)];
        return newText;
    }
}
