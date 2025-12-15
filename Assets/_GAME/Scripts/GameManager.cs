using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TextMeshProUGUI orbsText;
    int numOrbs;
    public int NumOrbs => numOrbs; // Read only

    public int AddOrb()
    {
        numOrbs++;
        orbsText.text = "Orbs: " + numOrbs;
        return numOrbs;
    }

    public int RemoveOrb()
    {
        numOrbs--;
        orbsText.text = "Orbs: " + numOrbs;
        return numOrbs;
    }
}
