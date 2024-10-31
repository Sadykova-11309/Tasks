namespace Task4
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        private static Queue<string> orderQueue = new Queue<string>();
        private static readonly object queueLock = new object();
        private static bool isProducing = true;

        static void Main(string[] args)
        {
            Task producerTask = Task.Run(() => Producer());
            Task consumerTask = Task.Run(() => Consumer());

            Task.WaitAll(producerTask, consumerTask);
        }
        
        private static void Producer()
        {
            string[] orders = { "Салат", "Суп", "Паста", "Десерт" };

            foreach (var order in orders)
            {
                Console.WriteLine($"Официант: Принял заказ: {order}");
                lock (queueLock)
                {
                    orderQueue.Enqueue(order);
                    Monitor.Pulse(queueLock);
                }
                Thread.Sleep(1000);
            }

            lock (queueLock)
            {
                isProducing = false;
                Monitor.Pulse(queueLock);
            }
        }

        private static void Consumer()
        {
            while (true)
            {
                string order;

                lock (queueLock)
                {
                    while (orderQueue.Count == 0 && isProducing)
                    {
                        Monitor.Wait(queueLock);
                    }

                    if (orderQueue.Count == 0 && !isProducing)
                    {
                        break;
                    }

                    order = orderQueue.Dequeue();
                }

                Console.WriteLine($"Кухня: Готовлю {order}");
                Thread.Sleep(2000);
                Console.WriteLine($"Кухня: {order} готово!");
            }

            Console.WriteLine("Кухня: Все заказы обработаны.");
        }
    }

}
