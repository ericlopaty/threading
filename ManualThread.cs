using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace threading
{
	// create a few threads manually. main is blocked until both threads complete.
	class ManualThread
	{
		static Random rand = new Random(DateTime.Now.Millisecond);

		public void StartWorkers()
		{
			Thread t1 = new Thread(new ThreadStart(MyThread1));
			t1.Priority = ThreadPriority.Lowest;
			t1.Start();

			Thread t2 = new Thread(new ThreadStart(MyThread2));
			t2.Priority = ThreadPriority.Lowest;
			t2.Start();
			
			t1.Join();
			t2.Join();
		}

		static void ThreadWorker()
		{
			try
			{
				DumpLetters("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 0);
			}
			catch (System.Threading.ThreadInterruptedException)
			{
			}
			finally
			{
			}
		}

		static void MyThread1()
		{
			try
			{
				DumpLetters("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 0);
			}
			catch (System.Threading.ThreadInterruptedException)
			{
			}
			finally
			{
			}
		}

		static void MyThread2()
		{
			try
			{
				DumpLetters("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 1);
			}
			catch (System.Threading.ThreadInterruptedException)
			{
			}
			finally
			{
			}
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
