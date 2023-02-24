namespace NM.Services
{
    public class AllServices
    {
        private static AllServices _intstance;
        public static AllServices Container => _intstance ??= new AllServices();

        public void RegisterSingle<TService>(TService implementation) where TService : IService => 
            Implementation<TService>.ServiceInstance = implementation;
        public TService Single<TService>() where TService : IService => 
            Implementation<TService>.ServiceInstance;
    }
    public static class Implementation<TService> where TService : IService
    {
        public static TService ServiceInstance;
    }
}