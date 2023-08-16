using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance; // Singleton instance

    private List<Character> characters = new List<Character>();

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterCharacter(Character character)
    {
        characters.Add(character);
    }

    public void UnregisterCharacter(Character character)
    {
        characters.Remove(character);
    }

}
