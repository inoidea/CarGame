using System;
using UnityEngine;
using JetBrains.Annotations;
using Object = UnityEngine.Object;

namespace Features.AbilitySystem.Abilities
{
    internal class JumpAbility : IAbility
    {
        private readonly AbilityItemConfig _config;

        public JumpAbility([NotNull] AbilityItemConfig config) => _config = config ?? throw new ArgumentNullException(nameof(config));

        public void Apply(IAbilityActivator activator)
        {
            //var car = activator.ViewGameObject.AddComponent<Rigidbody2D>();
            //Vector3 force = activator.ViewGameObject.transform.right * _config.Value;
            //car.AddForce(force, ForceMode2D.Force);

            Debug.Log("прыг");
        }
    }
}
