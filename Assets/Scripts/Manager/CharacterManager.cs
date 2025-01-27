using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterManager
{
    private static Dictionary<string, CharacterData> _characterDict = new Dictionary<string, CharacterData>();
    static CharacterManager()
    {

    }

    public static void RegisterCharacter(GameObject characterObject, ICharacter character)
    {
        ICharacter characterComponent = characterObject.GetComponent<ICharacter>();
        if (character == null || character != characterComponent) return;


        CharacterData characterDB = new CharacterData(character, characterObject);
        _characterDict[character.Name] = characterDB;
    }

    public static void DeregisterCharacter(GameObject characterObject, ICharacter character)
    {
        ICharacter characterComponent = characterObject.GetComponent<ICharacter>();
        if (character == null || character != characterComponent) return;

        if (_characterDict.ContainsKey(character.Name))
        {
            _characterDict.Remove(character.Name);
        }
    }

    public static CharacterData GetCharacter(string name)
    {
        if(_characterDict.TryGetValue(name, out CharacterData character))
        {
            return character;
        }
        return null;
    }
}

