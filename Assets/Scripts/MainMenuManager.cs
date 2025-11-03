using UnityEngine;
using UnityEngine.SceneManagement; // You MUST add this line to use the SceneManager

public class MainMenuManager : MonoBehaviour
{
    // This function must be 'public' so the button can see it
    public void StartGame()
    {
        // Replace "MainGame" with the exact name of your game scene
        // as it appears in your Build Settings.
        SceneManager.LoadScene("MainGame");
    }

    // --- NEW FUNCTION ---
    // This function will be called by your new Tutorial Button
    public void StartTutorial()
    {
        // Replace "TutorialScene" with the exact name of your tutorial scene
        // as it appears in your Build Settings.
        SceneManager.LoadScene("Tutorial");
    }
    // --- END OF NEW FUNCTION ---

    // (Optional) You can add a function for a quit button
    public void QuitGame()
    {
        Debug.Log("QUIT GAME!"); // This message helps you test in the editor
        Application.Quit();
    }
}
