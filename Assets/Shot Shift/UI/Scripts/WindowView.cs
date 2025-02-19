using UnityEngine;
using Zenject;

namespace Shot_Shift.UI.Scripts.StartScene
{
    public class WindowView : MonoBehaviour
    {
        public void ShowWindow()
        {
            gameObject.SetActive(true);
        }

        public void HideWindow()
        {
            gameObject.SetActive(false);
        }
    }
}
