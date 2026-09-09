using UnityEngine;

namespace DeadZone.Core.Managers
{
    /// <summary>
    /// Manages graphics quality settings for mobile optimization.
    /// </summary>
    public class QualityManager : MonoBehaviour
    {
        public static QualityManager Instance { get; private set; }

        [SerializeField] private QualityLevel autoDetectedQuality = QualityLevel.Medium;
        [SerializeField] private QualityLevel currentQuality = QualityLevel.Medium;

        public QualityLevel CurrentQuality => currentQuality;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            AutoDetectQuality();
        }

        public void AutoDetectQuality()
        {
            int cpuCores = SystemInfo.processorCount;
            int ramMB = SystemInfo.systemMemorySize;

            if (cpuCores <= 4 && ramMB <= 2048)
            {
                autoDetectedQuality = QualityLevel.Low;
            }
            else if (cpuCores <= 6 && ramMB <= 4096)
            {
                autoDetectedQuality = QualityLevel.Medium;
            }
            else
            {
                autoDetectedQuality = QualityLevel.High;
            }

            SetQuality(autoDetectedQuality);
            Debug.Log($"Auto-detected quality: {autoDetectedQuality} (Cores: {cpuCores}, RAM: {ramMB}MB)");
        }

        public void SetQuality(QualityLevel quality)
        {
            currentQuality = quality;

            switch (quality)
            {
                case QualityLevel.Low:
                    ApplyLowQuality();
                    break;
                case QualityLevel.Medium:
                    ApplyMediumQuality();
                    break;
                case QualityLevel.High:
                    ApplyHighQuality();
                    break;
                case QualityLevel.Ultra:
                    ApplyUltraQuality();
                    break;
            }
        }

        private void ApplyLowQuality()
        {
            QualitySettings.SetQualityLevel(0, true);
            QualitySettings.masterTextureLimit = 2;
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
            QualitySettings.antiAliasing = 0;
            QualitySettings.shadowDistance = 10f;
        }

        private void ApplyMediumQuality()
        {
            QualitySettings.SetQualityLevel(2, true);
            QualitySettings.masterTextureLimit = 1;
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
            QualitySettings.antiAliasing = 2;
            QualitySettings.shadowDistance = 50f;
        }

        private void ApplyHighQuality()
        {
            QualitySettings.SetQualityLevel(4, true);
            QualitySettings.masterTextureLimit = 0;
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
            QualitySettings.antiAliasing = 4;
            QualitySettings.shadowDistance = 100f;
        }

        private void ApplyUltraQuality()
        {
            QualitySettings.SetQualityLevel(5, true);
            QualitySettings.masterTextureLimit = 0;
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
            QualitySettings.antiAliasing = 8;
            QualitySettings.shadowDistance = 150f;
        }
    }

    public enum QualityLevel
    {
        Low,
        Medium,
        High,
        Ultra
    }
}
