using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TextMeshProUGUI orbsText;
    int numOrbs;

    public int AddOrb()
    {
        numOrbs++;
        orbsText.text = "Orbs: " + numOrbs;
        return numOrbs;
    }
}
