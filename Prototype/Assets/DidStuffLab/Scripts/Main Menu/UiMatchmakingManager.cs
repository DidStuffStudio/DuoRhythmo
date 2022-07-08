using UnityEngine;

namespace DidStuffLab.Scripts.Main_Menu
{
    public class UiMatchmakingManager : MonoBehaviour {
        private static UiMatchmakingManager _instance;
        public static UiMatchmakingManager Instance => _instance;

        private void Start() {
        
            if (_instance == null) _instance = this;
        }
    }
}
