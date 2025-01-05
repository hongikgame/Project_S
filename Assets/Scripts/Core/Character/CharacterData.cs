using UnityEngine;
public class CharacterData
{
    public ICharacter Character { get; }
    public GameObject GameObject { get; }

    public CharacterData(ICharacter character, GameObject gameObject)
    {
        Character = character;
        GameObject = gameObject;
    }
}