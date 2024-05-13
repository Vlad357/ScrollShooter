using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScrollShooter.Supports
{
    public class ButtonFunctions : MonoBehaviour
    {
        public void Pause()
        {
            Time.timeScale = 0;
        }

        public void Unpause()
        {
            Time.timeScale = 1;
        }

        public void Repeat()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoadScene(int id)
        {
            SceneManager.LoadScene(id);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}