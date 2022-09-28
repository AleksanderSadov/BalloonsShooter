using UnityEngine;

namespace BalloonsShooter.Gameplay
{
    public static class GameConstants
    {
        public static readonly Vector3 PLANE_DEFAULT_SIZE = new(10f, 0f, 10f);
        public static readonly string TAGS_DEATH_ZONE = "DeathZone";

        public static readonly string GAME_SCENE_NAME = "GameScene";
        public static readonly string GAME_OVER_TEST_SCENE_NAME = "GameOverTestScene";
        public static readonly string GAME_OVER_ADDITIVE_SCENE_NAME = "GameOverAdditiveScene";
    }
}

