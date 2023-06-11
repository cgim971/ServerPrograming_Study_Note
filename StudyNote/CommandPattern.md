직원이 주방에 가 요리까지 하고 고객의 요청 모두 처리하는게 원래 우리 방식
-> 네트워크 메세지가 오면 게임룸으로 가져가 락을 걸고 작업 진행

주방에서 요리할 수 있는 최대치가 있으면 밀리기 시작 

올바른 방법은 
주방 담당 셰프 존재 -> 주문을 받으면 주문서라는 개념으로 요청사항을 주방에 전달하고 일을 계속함

요청 자체를 캡슐화 = 커맨드패턴

람다 캡처를 이용할 시 나중에 작업을 해도 미리 정의하기 때문에 널로 변했을 때 문제가 발생할 수 있다. 

```cs
using System;
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
```

