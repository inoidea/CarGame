using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Advertisements;

namespace Services.Ads.UnityAds
{
    internal sealed class RewardedPlayer : UnityAdsPlayer
    {
        public RewardedPlayer(string Id) : base(Id) { }

        protected override void OnPlaying() => Advertisement.Show(Id);
        protected override void Load() => Advertisement.Load(Id);
    }
}
