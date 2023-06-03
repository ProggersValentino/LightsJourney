using UnityEngine.SceneManagement;
using UnityEngine;
 
public class SceneManage : MonoBehaviour
{

        

        private void Start()
        {
        SceneChecker();
        }
        void OnTriggerEnter(Collider other)
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1; // calculation for next scene
            SceneManager.LoadScene(sceneIndex);
            // SceneManager.LoadScene("Level1");
        }


        public void playWithDark()
        {
            SceneManager.LoadScene("Level1");
        }

        public void rageQuit()
        {
            Application.Quit();
        }

        public void ChangeScene(string sceneToLoad)
        {
            SceneManager.LoadScene(sceneToLoad);

        }

    public void SceneChecker()
    {


        if (SceneManager.GetActiveScene().buildIndex == 6 || SceneManager.GetActiveScene().buildIndex == 7)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

}