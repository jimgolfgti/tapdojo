using System;
using System.Threading;

namespace tapdojo.tests.EAP
{
    public class Addition
    {
        public event AdditionCompletedEventHandler AdditionCompleted;

        public delegate void AdditionCompletedEventHandler(object sender, AdditionCompletedEventArgs e);

        public class AdditionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
        {
            internal AdditionCompletedEventArgs(int result, Exception error, bool cancelled, object userState)
                : base(error, cancelled, userState)
            {
                Result = result;
            }

            public int Result { get; private set; }
        }

        public void AddAsync(int augend, int addend)
        {
            ThreadPool.QueueUserWorkItem(_ =>
                {
                    Thread.Sleep(500);
                    var handler = AdditionCompleted;
                    if (handler != null)
                        handler(this, new AdditionCompletedEventArgs(3, null, false, null));
                });
        }
    }
}