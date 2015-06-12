using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace threading
{
	class Program
	{
		public static Synch synch = new Synch();

		static void Main(string[] args)
		{
			int n = -1;
				while (n != 0)
			{
				Console.Clear();
				Console.Write("1 Manual Thread\n2 Thread Wrapper\n3 Thread Pool\n>");
				try
				{
					n = int.Parse(Console.ReadLine());
				}
				catch
				{
					n = 0;
				}
				Console.Clear();
				switch (n)
				{
					case 1:
						ManualThread mt = new ManualThread();
						mt.StartWorkers();
						break;
					case 2:
						ThreadWrapper tw = new ThreadWrapper();
						tw.StartWorkers();
						break;
					case 3:
						PoolThread pt = new PoolThread();
						pt.StartWorkers();
						break;
				}
				Console.SetCursorPosition(0, Console.WindowHeight - 1);
				Console.Write("Press ENTER to continue.");
				Console.ReadLine();
			}
		}
	}
}