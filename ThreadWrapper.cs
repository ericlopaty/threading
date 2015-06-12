using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace threading
{
	// wrapper class encapsulates the logic to start the thread with parameters passed into its constructor.
	class ThreadWrapper
	{
		public void StartWorkers()
		{
			string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			Thread[] t = new Thread[20];
			t[0] = new Wrapper(letters, 0).Start();
			t[1] = new Wrapper(letters, 1).Start();
			t[2] = new Wrapper(letters, 2).Start();
			t[3] = new Wrapper(letters, 3).Start();
			t[4] = new Wrapper(letters, 4).Start();
			t[5] = new Wrapper(letters, 5).Start();
			t[6] = new Wrapper(letters, 6).Start();
			t[7] = new Wrapper(letters, 7).Start();
			t[8] = new Wrapper(letters, 8).Start();
			t[9] = new Wrapper(letters, 9).Start();
			t[10] = new Wrapper(letters, 10).Start();
			t[11] = new Wrapper(letters, 11).Start();
			t[12] = new Wrapper(letters, 12).Start();
			t[13] = new Wrapper(letters, 13).Start();
			t[14] = new Wrapper(letters, 14).Start();
			t[15] = new Wrapper(letters, 15).Start();
			t[16] = new Wrapper(letters, 16).Start();
			t[17] = new Wrapper(letters, 17).Start();
			t[18] = new Wrapper(letters, 18).Start();
			t[19] = new Wrapper(letters, 19).Start();
			for (int i = 0; i < t.Length; i++)
				t[i].Join();
		}
	}

	class Wrapper
	{
		static Random rand = new Random(DateTime.Now.Millisecond);
		string letters;
		int top;
		Thread thread;

		public Wrapper(string letters, int top)
		{
			this.letters = letters;
			this.top = top;
		}

		void ThreadProc()
		{
			try
			{
				DumpLetters(letters, top);
			}
			catch (System.Threading.ThreadInterruptedException)
			{
			}
		}

		public Thread Start()
		{
			thread = new Thread(new ThreadStart(ThreadProc));
			thread.Start();
			return thread;
		}

		public static void DumpLetters(string letters, int top)
		{
			int left = 0;
			for (int i = 0; i < letters.Length; i++)
			{
				try
				{
					Monitor.Enter(Program.synch);
					Console.SetCursorPosition(left++, top);
					Console.Write(letters[i]);
				}
				finally
				{
					Monitor.Exit(Program.synch);
				}
				Thread.Sleep(rand.Next(500));
			}
		}
	}
}
