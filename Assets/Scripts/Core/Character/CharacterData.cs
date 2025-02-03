using UnityEngine;
public class CharacterData
{
    public CharacterBase Character { get; }
    public GameObject GameObject { get; }

    public CharacterData(CharacterBase character, GameObject gameObject)
    {
        Character = character;
        GameObject = gameObject;
    }
}