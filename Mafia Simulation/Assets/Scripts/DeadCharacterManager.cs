using System.Collections.Generic;
using UnityEngine;

public class DeadCharacterManager : MonoBehaviour
{
    // Singleton instance
    public static DeadCharacterManager Instance { get; private set; }

    // List to store dead characters
    private List<Character> deadCharacters = new List<Character>();
    private List<Character> criminalCharacters = new List<Character>();

    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    // Add a dead character to the list
    public void AddDeadCharacter(Character character)
    {
        deadCharacters.Add(character);
    }

    public void AddCriminalCharacter(Character character)
    {

        criminalCharacters.Add(character);
    }

    // Remove a criminal character from the list
    public void RemoveCriminalCharacter(Character character)
    {
        criminalCharacters.Remove(character);
    }

    // Remove a dead character from the list
    public void RemoveDeadCharacter(Character character)
    {
        deadCharacters.Remove(character);
    }

    // Get the list of dead characters
    public List<Character> GetDeadCharacters()
    {
        return deadCharacters;
    }

    public List<Character> GetCriminalCharacters()
    {
        return criminalCharacters;
    }

    public bool IsCharacterDead(Character character)
    {
        return deadCharacters.Contains(character);
    }

    public bool IsCriminal(Character character)
    {
        return criminalCharacters.Contains(character);
    }
}
