using UnityEngine;
using UnityEngine.UI; // Required for the Image component
using System.Collections.Generic;

public class SpriteScoreDisplay : MonoBehaviour
{
    [Header("Setup")]
    public Sprite[] numberSprites; // Drag your 0-9 sprites here in order
    public GameObject digitPrefab; // The prefab with an Image component
    public Transform scoreContainer; // The Panel with the Horizontal Layout Group

    // A list to keep track of the images we spawn so we can delete them when the score changes
    private List<GameObject> activeDigits = new List<GameObject>();

    public void UpdateScore(int currentScore)
    {
        // 1. Destroy the old digits from the previous score
        foreach (GameObject digit in activeDigits)
        {
            Destroy(digit);
        }
        activeDigits.Clear();

        // 2. Convert the integer to a string to easily loop through each number
        string scoreString = currentScore.ToString();

        // 3. Loop through each character, convert it back to a number, and spawn the sprite
        foreach (char c in scoreString)
        {
            // Parse the character back into an integer (e.g., '5' becomes 5)
            int digitValue = int.Parse(c.ToString());

            // Instantiate a new UI image inside the Layout Group panel
            GameObject newDigit = Instantiate(digitPrefab, scoreContainer);

            // Apply the correct sprite from the array
            newDigit.GetComponent<Image>().sprite = numberSprites[digitValue];

            // Add it to our list so we can clean it up next time the score updates
            activeDigits.Add(newDigit);
        }
    }
}