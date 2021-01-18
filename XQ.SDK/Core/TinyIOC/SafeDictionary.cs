using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace XQ.SDK.Core
{
    public class SafeDictionary<TKey, TValue> : IDisposable
    {
        private readonly Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();
        private readonly ReaderWriterLockSlim _padlock = new ReaderWriterLockSlim();

        public TValue this[TKey key]
        {
            set
            {
                _padlock.EnterWriteLock();

                try
                {
                    if (_dictionary.TryGetValue(key, out var current))
                        if (current is IDisposable disposable)
                            disposable.Dispose();

                    _dictionary[key] = value;
                }
                finally
                {
                    _padlock.ExitWriteLock();
                }
            }
        }

        public void Dispose()
        {
            _padlock.EnterWriteLock();

            try
            {
                var disposableItems = from item in _dictionary.Values
                                      where item is IDisposable
                                      select item as IDisposable;

                foreach (var item in disposableItems) item.Dispose();
            }
            finally
            {
                _padlock.ExitWriteLock();
            }

            GC.SuppressFinalize(this);
        }


        public bool TryGetValue(TKey key, out TValue value)
        {
            _padlock.EnterReadLock();
            try
            {
                return _dictionary.TryGetValue(key, out value);
            }
            finally
            {
                _padlock.ExitReadLock();
            }
        }
    }
}