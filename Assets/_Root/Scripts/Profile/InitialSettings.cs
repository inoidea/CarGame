using Profile;
using Services.Ads.UnityAds;
using Services.Analytics;
using Services.IAP;
using UnityEngine;

namespace Profile
{
    [CreateAssetMenu(fileName = nameof(InitialSettings), menuName = "Settings/" + nameof(InitialSettings))]
    internal class InitialSettings : ScriptableObject
    {
        [field: SerializeField] public GameState State { get; private set; }
        [field: SerializeField] public float CarSpeed { get; private set; }
        [field: SerializeField] public float CarJumpHeight { get; private set; }
    }
}