using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest Data 0", menuName = "Data Level")]
public class QuestData : ScriptableObject
{
    public int id;
    public GameMode mode;
    [Header("======= MODE-TAXI =======")]
    public string customerName;
    public Sprite avatar;
    public string description;
    public string customerShortText;
    public float timer;
    // public GameObject sPos;
    // public string customerPosition;
    // public GameObject dPos;
    // public string destinationPosition;

    // void OnValidate()
    // {
    //     if (sPos) customerPosition = sPos.name;
    //     if (dPos) destinationPosition = dPos.name;
    // }
}
