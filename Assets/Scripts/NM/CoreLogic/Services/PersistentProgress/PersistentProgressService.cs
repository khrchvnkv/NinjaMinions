using NM.CoreLogic.Data;

namespace NM.CoreLogic.Services.PersistentProgress
{
    public class PersistentProgressService : IService
    {
        public ProgressData Progress { get; set; }
    }
}