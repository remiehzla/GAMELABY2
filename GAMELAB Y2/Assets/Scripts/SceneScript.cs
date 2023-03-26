using Mirror;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace QuickStart
{
    public class SceneScript : NetworkBehaviour
    {
        public TMP_Text canvasStatusText;
        public PlayerScript playerScript;

        [SyncVar(hook = nameof(OnStatusTextChanged))]
        public string statusText;

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
    }
}