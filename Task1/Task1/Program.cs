namespace Task1
{
    class Program
    {
        static void Main()
        {
            var numbers = new List<int> { 39, 20, 96, 70, 13, 80};

            var result = numbers.InsertAfter(
                n => n % 10 == 0, 
                n => new List<int> { n / 10 }
            );

            foreach (var number in result)
            {
                Console.WriteLine(number);
            }
        }
    }

    public static class MethodExtension
    {
        public static IEnumerable<TItem> InsertAfter<TItem>(
            this IEnumerable<TItem> source,
            Func<TItem, bool> predicate,
            Func<TItem, IEnumerable<TItem>> inserter)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (inserter == null) throw new ArgumentNullException(nameof(inserter));

            foreach (var item in source)
            {
                yield return item;

                if (predicate(item))
                {
                    foreach (var insertItem in inserter(item))
                    {
                        yield return insertItem;
                    }
                }
            }
        }
    }
}
