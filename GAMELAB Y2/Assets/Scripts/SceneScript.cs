using Mirror;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace QuickStart
{
    public class SceneScript : NetworkBehaviour
    {
        public TMP_Text canvasStatusText;
        public PlayerScript playerScript;

        public SceneReference sceneReference;

        [SyncVar(hook = nameof(OnStatusTextChanged))]
        public string statusText;

        public TMP_Text canvasAmmoText;

        public void UIAmmo(int _value)
        {
            canvasAmmoText.text = "Ammo: " + _value;
        }

        void OnStatusTextChanged(string _Old, string _New)
        {
            //called from sync var hook, to update info on screen for all players
            canvasStatusText.text = statusText;
        }

        public void ButtonSendMessage()
        {
            if (playerScript != null)
                playerScript.CmdSendPlayerMessage();
        }

        public void ButtonChangeScene()
        {
            if (isServer)
            {
                Scene scene = SceneManager.GetActiveScene();
                if (scene.name == "TestScene")
                    NetworkManager.singleton.ServerChangeScene("OtherTestScene");
                else
                    NetworkManager.singleton.ServerChangeScene("TestScene");
            }
            else
                Debug.Log("You are not Host.");
        }
    }
}