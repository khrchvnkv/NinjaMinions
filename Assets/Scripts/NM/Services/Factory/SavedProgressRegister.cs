using System.Collections.Generic;
using NM.Services.PersistentProgress;
using UnityEngine;

namespace NM.Services.Factory
{
    public class SavedProgressRegister
    {
        public readonly List<ISavedProgressReader> ProgressReaders = new List<ISavedProgressReader>();
        public readonly List<ISavedProgressWriter> ProgressWriters = new List<ISavedProgressWriter>();
        private readonly List<IClearable> _clearables = new List<IClearable>();

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
            foreach (var clearable in _clearables)
            {
                clearable?.Clear();
            }
            _clearables.Clear();
        }
        public void RegisterProgressListener(GameObject gameObject)
        {
            RegisterClearable();
            RegisterProgressReaders();
            RegisterProgressWriters();

            void RegisterClearable()
            {
                if (gameObject.TryGetComponent(out IClearable clearable))
                {
                    _clearables.Add(clearable);
                }
            }
            void RegisterProgressReaders()
            {
                var readers = gameObject.GetComponentsInChildren<ISavedProgressReader>();
                ProgressReaders.AddRange(readers);
            }
            void RegisterProgressWriters()
            {
                var writers = gameObject.GetComponentsInChildren<ISavedProgressReaderWriter>();
                ProgressWriters.AddRange(writers);
            }
        }
    }
}