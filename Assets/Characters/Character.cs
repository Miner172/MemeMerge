using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Characters", order = 51)]
public class Character : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private int _level;
    [SerializeField] private AudioClip _clip;

    public Sprite Icon => _icon;
    public string Name => _name;
    public int Price => _price;
    public int Level => _level;
    public AudioClip Clip => _clip;
}
