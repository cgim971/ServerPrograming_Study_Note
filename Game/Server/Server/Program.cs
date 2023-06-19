using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using Google.Protobuf.WellKnownTypes;
using Server.Data;
using Server.Game;
using ServerCore;

namespace Server
{
	class Program
	{
		static Listener _listener = new Listener();
		static List<System.Timers.Timer> _timers = new List<System.Timers.Timer>(); // 주기적으로 도는 모든 타이머 리스트

		static void TickRoom(GameRoom room, int tick = 100)
		{
			var timer = new System.Timers.Timer(); // 타이머 설정 
			timer.Interval = tick; // 몇 tick 마다 실행할지
			timer.Elapsed += ((s, e) => { room.Update(); }); // 무슨 이벤트를 실행할지
			timer.AutoReset = true; // 매번 다시 리셋
			timer.Enabled = true; // 실행

			_timers.Add(timer);
		}

		static void Main(string[] args)
		{
			ConfigManager.LoadConfig();
			DataManager.LoadData();

			GameRoom room = RoomManager.Instance.Add(1);
			TickRoom(room, 50); // 룸 생성하면 50ms 마다 실행

			// DNS (Domain Name System)
			string host = Dns.GetHostName();
			IPHostEntry ipHost = Dns.GetHostEntry(host);
			IPAddress ipAddr = ipHost.AddressList[0];
			IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

			_listener.Init(endPoint, () => { return SessionManager.Instance.Generate(); });
			Console.WriteLine("Listening...");

			//FlushRoom();
			//JobTimer.Instance.Push(FlushRoom);

			// TODO
			while (true)
			{
				//JobTimer.Instance.Flush();
				Thread.Sleep(100); // 꺼지지 않게 유지만
			}
		}
	}
}
