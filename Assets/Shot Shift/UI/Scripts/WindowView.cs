using UnityEngine;

namespace Shot_Shift.UI.Scripts
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
