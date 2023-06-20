using System;
using JetBrains.Annotations;
using Features.AbilitySystem.Abilities;
using System.Collections.Generic;

namespace Features.AbilitySystem
{
    internal interface IAbilitiesController
    { }

    internal class AbilitiesController : BaseController, IAbilitiesController
    {
        private readonly IAbilitiesView _view;
        private readonly IAbilitiesRepository _repository;
        private readonly IAbilityActivator _abilityActivator;

        public AbilitiesController(
            [NotNull] IAbilitiesView view,
            [NotNull] IAbilitiesRepository repository,
            [NotNull] IEnumerable<IAbilityItem> abilityItemConfigs,
            [NotNull] IAbilityActivator abilityActivator)
        {
            _abilityActivator = abilityActivator ?? throw new ArgumentNullException(nameof(abilityActivator));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _view.Display(abilityItemConfigs, OnAbilityViewClicked);
        }

        protected override void OnDispose() =>
            _view.Clear();

        private void OnAbilityViewClicked(string abilityId)
        {
            if (_repository.Items.TryGetValue(abilityId, out IAbility ability))
                ability.Apply(_abilityActivator);
        }
    }
}
