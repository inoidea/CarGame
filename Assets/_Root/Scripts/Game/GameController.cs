using Features.AbilitySystem;
using Game.Car;
using Game.InputLogic;
using Game.TapeBackground;
using Profile;
using Services;
using Tool;
using UnityEngine;

namespace Game
{
    internal class GameController : BaseController
    {
        private readonly SubscriptionProperty<float> _leftMoveDiff;
        private readonly SubscriptionProperty<float> _rightMoveDiff;

        private readonly CarController _carController;
        private readonly InputGameController _inputGameController;
        private readonly AbilitiesContext _abilitiesContext;
        private readonly TapeBackgroundController _tapeBackgroundController;

        public GameController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _leftMoveDiff = new SubscriptionProperty<float>();
            _rightMoveDiff = new SubscriptionProperty<float>();

            _carController = CreateCarController(profilePlayer.CurrentCar);
            _inputGameController = CreateInputGameController(profilePlayer, _leftMoveDiff, _rightMoveDiff);
            _abilitiesContext = CreateAbilitiesContext(placeForUi, _carController);
            _tapeBackgroundController = CreateTapeBackground(_leftMoveDiff, _rightMoveDiff);

            ServiceRoster.Analytics.SendLevelStartEvent();
        }

        private TapeBackgroundController CreateTapeBackground(SubscriptionProperty<float> leftMoveDiff, SubscriptionProperty<float> rightMoveDiff)
        {
            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            return tapeBackgroundController;
        }

        private InputGameController CreateInputGameController(ProfilePlayer profilePlayer,
            SubscriptionProperty<float> leftMoveDiff, SubscriptionProperty<float> rightMoveDiff)
        {
            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);

            return inputGameController;
        }

        private CarController CreateCarController(CarModel carModel)
        {
            var carController = new CarController(carModel);
            AddController(carController);

            return carController;
        }

        private AbilitiesContext CreateAbilitiesContext(Transform placeForUi, IAbilityActivator abilityActivator)
        {
            var context = new AbilitiesContext(placeForUi, abilityActivator);
            AddContext(context);

            return context;
        }
    }
}
