using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class TutorialManager : MonoBehaviour
{
    // This function must be public so the button can see it
    public void ReturnToMainMenu()
    {
        // Replace "MainMenu" with the exact name of your main menu scene
        // as it appears in your Build Settings.
        SceneManager.LoadScene("Main Menu");
    }
}
