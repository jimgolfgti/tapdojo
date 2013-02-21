using System;
using System.Threading;

namespace tapdojo.tests.APM
{
    public static class Addition
    {
        public static IAsyncResult BeginAdd(int augend, int addend, AsyncCallback callback, object state)
        {
            var result = new AsyncResult<int>(callback, state);

            ThreadPool.QueueUserWorkItem(_ =>
                {
                    Thread.Sleep(500);
                    result.Complete(3, false);
                });

            return result;
        }

        public static int EndAdd(IAsyncResult asyncResult)
        {
            var result = asyncResult as AsyncResult<int>;
            if (result == null)
                throw new InvalidOperationException();

            return result.Result;
        }

        private class AsyncResult<T> : IAsyncResult, IDisposable
        {
            private readonly AsyncCallback _callback;
            private bool _completed;
            private bool _completedSynchronously;
            private readonly object _asyncState;
            private readonly ManualResetEvent _waitHandle;
            private T _result;
            private Exception _e;
            private readonly object _syncRoot;

            internal AsyncResult(AsyncCallback cb, object state)
            {
                _callback = cb;
                _asyncState = state;
                _completed = false;
                _completedSynchronously = false;

                _waitHandle = new ManualResetEvent(false);
                _syncRoot = new object();
            }

            public object AsyncState
            {
                get { return _asyncState; }
            }

            public WaitHandle AsyncWaitHandle
            {
                get { return _waitHandle; }
            }

            public bool CompletedSynchronously
            {
                get
                {
                    lock (_syncRoot)
                    {
                        return _completedSynchronously;
                    }
                }
            }

            public bool IsCompleted
            {
                get
                {
                    lock (_syncRoot)
                    {
                        return _completed;
                    }
                }
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!disposing) return;
                lock (_syncRoot)
                {
                    if (_waitHandle != null)
                    {
                        ((IDisposable) _waitHandle).Dispose();
                    }
                }
            }

            internal Exception Exception
            {
                get
                {
                    lock (_syncRoot)
                    {
                        return _e;
                    }
                }
            }

            internal T Result
            {
                get
                {
                    lock (_syncRoot)
                    {
                        return _result;
                    }
                }
            }

            internal void Complete(T result, bool completedSynchronously)
            {
                lock (_syncRoot)
                {
                    _completed = true;
                    _completedSynchronously = completedSynchronously;
                    _result = result;
                }

                SignalCompletion();
            }

            internal void HandleException(Exception e, bool completedSynchronously)
            {
                lock (_syncRoot)
                {
                    _completed = true;
                    _completedSynchronously = completedSynchronously;
                    _e = e;
                }

                SignalCompletion();
            }

            private void SignalCompletion()
            {
                _waitHandle.Set();

                ThreadPool.QueueUserWorkItem(InvokeCallback);
            }

            private void InvokeCallback(object state)
            {
                if (_callback != null)
                {
                    _callback(this);
                }
            }
        }
    }
}