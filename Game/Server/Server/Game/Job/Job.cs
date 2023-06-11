﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Game {
    public interface IJob {
        void Execute(); // 실행 함수
    }

    public class Job : IJob {
        Action _action; // 함수를 델리게이트 방식으로 저장

        public Job(Action action) { // 생성자
            _action = action;
        }

        public void Execute() { // 실행
            _action.Invoke();
        }
    }

    public class Job<T1> : IJob { // 제네릭으로 
        Action<T1> _action;
        T1 _t1;

        public Job(Action<T1> action, T1 t1) {
            _action = action;
            _t1 = t1;
        }

        public void Execute() {
            _action.Invoke(_t1);
        }
    }

    public class Job<T1, T2> : IJob { // 위와 동일
        Action<T1, T2> _action;
        T1 _t1;
        T2 _t2;

        public Job(Action<T1, T2> action, T1 t1, T2 t2) {
            _action = action;
            _t1 = t1;
            _t2 = t2;
        }

        public void Execute() {
            _action.Invoke(_t1, _t2);
        }
    }

    public class Job<T1, T2, T3> : IJob { // 위와 동일
        Action<T1, T2, T3> _action;
        T1 _t1;
        T2 _t2;
        T3 _t3;

        public Job(Action<T1, T2, T3> action, T1 t1, T2 t2, T3 t3) {
            _action = action;
            _t1 = t1;
            _t2 = t2;
            _t3 = t3;
        }

        public void Execute() {
            _action.Invoke(_t1, _t2, _t3);
        }
    }
}
