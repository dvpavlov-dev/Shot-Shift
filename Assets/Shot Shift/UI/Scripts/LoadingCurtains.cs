using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shot_Shift.UI.Scripts
{
    public class LoadingCurtains : MonoBehaviour, ILoadingCurtains
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _loadingImage;
        [SerializeField] private TMP_Text _descriptionText;

        public void ShowLoadingCurtains(string description = "Loading...")
        {
            _descriptionText.text = description;
            _canvasGroup.DOFade(1, 1f);
            
            _loadingImage.DOLocalRotate(new Vector3(0, 0, -360), 3, RotateMode.FastBeyond360)
                .SetLoops(-1)
                .SetEase(Ease.OutBounce);
            
            EventSystem.current.enabled = false;
        }

        public void HideLoadingCurtains()
        {
            _canvasGroup.DOFade(0, 1f).OnComplete(() => EventSystem.current.enabled = true);
            
            _loadingImage.DOKill();
        }

        public void UpdateDescriptionText(string description)
        {
            _descriptionText.text = description;
        }
    }

    public interface ILoadingCurtains
    {
        void ShowLoadingCurtains(string description = "Loading...");
        void HideLoadingCurtains();
        void UpdateDescriptionText(string description);
    }
}
