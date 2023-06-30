using Features.AbilitySystem;
using Tool;
using UnityEngine;

namespace Game.Car
{
    internal class CarController : BaseController, IAbilityActivator
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Game/Car");
        private readonly CarView _view;
        private readonly CarModel _model;

        public GameObject ViewGameObject => _view.gameObject;
        public float JumpHeight => _model.JumpHeight;

        public CarController(CarModel carModel)
        {
            _view = LoadView();
            _model = carModel;
        }

        private CarView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<CarView>();
        }
    }
}
