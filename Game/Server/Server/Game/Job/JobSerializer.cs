using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Game {
    public class JobSerializer {
        JobTimer _timer = new JobTimer();
        Queue<IJob> _jobQueue = new Queue<IJob>();
        object _lock = new object();
        bool _flush = false;

        public void PushAfter(int tickAfter, Action action) { PushAfter(tickAfter, new Job(action)); }
        public void PushAfter<T1>(int tickAfter, Action<T1> action, T1 t1) { PushAfter(tickAfter, new Job<T1>(action, t1)); }
        public void PushAfter<T1, T2>(int tickAfter, Action<T1, T2> action, T1 t1, T2 t2) { PushAfter(tickAfter, new Job<T1, T2>(action, t1, t2)); }
        public void PushAfter<T1, T2, T3>(int tickAfter, Action<T1, T2, T3> action, T1 t1, T2 t2, T3 t3) { PushAfter(tickAfter, new Job<T1, T2, T3>(action, t1, t2, t3)); }

        public void PushAfter(int tickAfter, IJob job) { 
            _timer.Push(job, tickAfter);
        }

        public void Push(Action action) { Push(new Job(action)); }
        public void Push<T1>(Action<T1> action, T1 t1) { Push(new Job<T1>(action, t1)); } // 편의성 증가
        public void Push<T1, T2>(Action<T1, T2> action, T1 t1, T2 t2) { Push(new Job<T1, T2>(action, t1, t2)); } // 위와 동일
        public void Push<T1, T2, T3>(Action<T1, T2, T3> action, T1 t1, T2 t2, T3 t3) { Push(new Job<T1, T2, T3>(action, t1, t2, t3)); } // 위와 동일

        public void Push(IJob job) { // 추가
            lock (_lock) {
                _jobQueue.Enqueue(job); // queue에 넣어주기
            }
        }

        public void Flush() { // 처리 
            _timer.Flush();

            while (true) {
                IJob job = Pop();
                if (job == null)
                    return;

                job.Execute();
            }
        }

        IJob Pop() { // 반환
            lock (_lock) {
                if (_jobQueue.Count == 0) { // queue에 남은 작업이 없으면 
                    _flush = false;
                    return null;
                }
                return _jobQueue.Dequeue(); // queue에서 빼주기
            }
        }
    }
}
