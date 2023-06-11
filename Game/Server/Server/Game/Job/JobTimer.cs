using System;
using System.Collections.Generic;
using System.Text;
using ServerCore;

namespace Server.Game {
    struct JobTimerElem : IComparable<JobTimerElem> {
        public int execTick; // 실행 시간
        public IJob job; // Action -> IJob으로 수정하며 코드 수정

        public int CompareTo(JobTimerElem other) {
            return other.execTick - execTick;
        }
    }

    public class JobTimer {
        PriorityQueue<JobTimerElem> _pq = new PriorityQueue<JobTimerElem>();
        object _lock = new object();

        // 전역으로 사용하던 Instance를 삭제
        // 관리를 JobSerializer에서 작업

        public void Push(IJob job, int tickAfter = 0) {
            JobTimerElem jobElement;
            jobElement.execTick = System.Environment.TickCount + tickAfter;
            jobElement.job = job;

            lock (_lock) {
                _pq.Push(jobElement);
            }
        }

        public void Flush() { // 무한 루프를 돌며 틱 확인하여 하는것도 낫베드
            while (true) {
                int now = System.Environment.TickCount;

                JobTimerElem jobElement;

                lock (_lock) {
                    if (_pq.Count == 0)
                        break;

                    jobElement = _pq.Peek();
                    if (jobElement.execTick > now)
                        break;

                    _pq.Pop();
                }

                jobElement.job.Execute(); // 실행
            }
        }
    }
}
