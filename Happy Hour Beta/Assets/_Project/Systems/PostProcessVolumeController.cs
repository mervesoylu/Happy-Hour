using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Project
{
    public class PostProcessVolumeController : MonoBehaviour
    {
        #region ------------------------------dependencies
        ChromaticAberration _chromaticAberrationEffect;
        #endregion

        #region ------------------------------interface
        public void OnRoundBegan()
        {
            _chromaticAberrationEffect.intensity.value = 0;
        }

        public void OnHappyHourRan()
        {
            DOTween.To(() => _chromaticAberrationEffect.intensity.value, x => _chromaticAberrationEffect.intensity.value = x, _happyHourIntensity, _transitionDuration);
        }
        [SerializeField][Range(0.1f, 1.0f)] float _happyHourIntensity;

        public void OnHappyHourStopped()
        {
            DOTween.To(() => _chromaticAberrationEffect.intensity.value, x => _chromaticAberrationEffect.intensity.value = x, _defaultIntensity, _transitionDuration);
        }
        [SerializeField] [Range(0.0f, 0.9f)] float _defaultIntensity;
        [SerializeField] float _transitionDuration;
        #endregion

        #region ------------------------------Unity messages
        void Start()
        {
            _chromaticAberrationEffect = GetComponent<PostProcessVolume>().sharedProfile.GetSetting<ChromaticAberration>();
        }
        #endregion
    }
}
