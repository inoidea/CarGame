using Features.Inventory;
using Features.Shed.Upgrade;
using Profile;
using System;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.Shed
{
    internal class ShedContext : BaseContext
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Shed/ShedView");
        private readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/Shed/UpgradeItemConfigDataSource");

        public ShedContext(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            if (placeForUi == null) throw new ArgumentNullException(nameof(placeForUi));
            if (profilePlayer == null) throw new ArgumentNullException(nameof(profilePlayer));

            CreateController(placeForUi, profilePlayer);
        }

        private ShedController CreateController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            InventoryContext inventoryContext = CreateInventoryContext(placeForUi, profilePlayer.Inventory);
            ShedView shedView = LoadView(placeForUi);
            UpgradeHandlersRepository shedRepository = CreateRepository();

            return new ShedController(shedView, profilePlayer, shedRepository);
        }

        private InventoryContext CreateInventoryContext(Transform placeForUi, IInventoryModel model)
        {
            var context = new InventoryContext(placeForUi, model);
            AddContext(context);

            return context;
        }

        private UpgradeHandlersRepository CreateRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);
            var repository = new UpgradeHandlersRepository(upgradeConfigs);
            AddRepository(repository);

            return repository;
        }

        private UpgradeItemConfig[] LoadConfigs() => ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);

        private ShedView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<ShedView>();
        }
    }
}
