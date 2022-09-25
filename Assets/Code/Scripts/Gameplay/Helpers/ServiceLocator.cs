namespace BalloonsShooter.Gameplay.Helpers
{
    public static class ServiceLocator<T>
    {
        private static T service;

        public static void ProvideService(T providedService)
        {
            service = providedService;
        }

        public static T GetService()
        {
            return service;
        }
    }
}

