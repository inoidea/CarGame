using UnityEngine;

namespace Features.Fight
{
    internal interface IEnemy
    {
        void Update(PlayerData playerData);
    }

    internal class Enemy : IEnemy
    {
        private const float KMoney = 5f;
        private const float KMinPower = 1f;
        private const float KMaxPower = 2.5f;
        private const float MaxHealthPlayer = 20;
        private const float KCrime = 10f;

        private readonly string _name;

        private int _moneyPlayer;
        private int _healthPlayer;
        private int _powerPlayer;
        private int _crimePlayer;

        public Enemy(string name) => _name = name;


        public void Update(PlayerData playerData)
        {
            switch (playerData.DataType)
            {
                case DataType.Money:
                    _moneyPlayer = playerData.Value;
                    break;

                case DataType.Health:
                    _healthPlayer = playerData.Value;
                    break;

                case DataType.Power:
                    _powerPlayer = playerData.Value;
                    break;

                case DataType.Crime:
                    _crimePlayer = playerData.Value;
                    break;
            }

            Debug.Log($"Notified {_name} change to {playerData.DataType:F}");
        }

        public int CalcPower()
        {
            int kHealth = CalcKHealth();
            float moneyRatio = _moneyPlayer / KMoney;
            var power = Random.Range(KMinPower, KMaxPower);
            float powerRatio = _powerPlayer / power;
            float crimtRatio = _crimePlayer / KCrime;

            return (int)(moneyRatio + kHealth + powerRatio + crimtRatio);
        }

        private int CalcKHealth() => _healthPlayer > MaxHealthPlayer ? 100 : 5;
    }
}

