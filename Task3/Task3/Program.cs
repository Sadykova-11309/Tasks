namespace Task3
{
    internal class Program
    {
        public class A
        {
            public int CostomerCode { get; set; }
            public string Street { get; set; }
            public string BirthYear { get; set; }
        }

        public class B
        {
            public string Category { get; set; }
            public string Contry { get; set; }
            public string Item { get; set; }

        }
        public class C
        {
            public string StoreName { get; set; }

            public int CostomerCode { get; set; }

            public int Discount { get; set; }
        }

        public class E
        {
            public string StoreName { get; set; }

            public int CostomerCode { get; set; }

            public string Item { get; set; }
        }

        static void Main(string[] args)
        {
            A[] A =
            {
                new A{CostomerCode = 001, Street = "Grand Boulevard", BirthYear = "1989"},
                new A{CostomerCode = 002, Street = "Grand Boulevard", BirthYear = "1945"},
                new A{CostomerCode = 003, Street = "Prime Street", BirthYear = "1990"},
                new A{CostomerCode = 004, Street = "Prime Street", BirthYear = "2009"},
                new A{CostomerCode = 005, Street = "Victory Drive", BirthYear = "1987"}
            };

            B[] B =
            {
                new B{Item = "ER123-1234", Contry = "USA", Category = "clothing"},
                new B{Item = "PO153-1834", Contry = "USA", Category = "clothing"},
                new B{Item = "TG123-7234", Contry = "USA", Category = "clothing"},
                new B{Item = "JH123-1254", Contry = "UK", Category = "clothing"},
                new B{Item = "CF323-1234", Contry = "UK", Category = "clothing"}
            };

            C[] C =
            {
                new C{CostomerCode = 001, StoreName = "Super Store", Discount = 10},
                new C{CostomerCode = 002, StoreName = "Best Store", Discount = 20},
                new C{CostomerCode = 003, StoreName = "Super Store", Discount = 30},
                new C{CostomerCode = 004, StoreName = "Best Store", Discount = 40},
                new C{CostomerCode = 005, StoreName = "Super Store", Discount = 50}
            };

            E[] E =
            {
                new E{StoreName = "Super Store", CostomerCode= 001, Item= "CU163-7234"},
                new E{StoreName = "Best Store", CostomerCode= 002, Item= "CF323-1234"},
                new E{StoreName = "Super Store", CostomerCode= 003, Item= "JH123-1254"},
                new E{StoreName = "Best Store", CostomerCode= 004, Item= "TG123-7234"},
                new E{StoreName = "Best Store", CostomerCode= 005, Item= "PO153-1834"}
            };

            var result = from e in E
                         join a in A on e.CostomerCode equals a.CostomerCode
                         join b in B on e.Item equals b.Item
                         join c in C on new { e.CostomerCode, e.StoreName } equals new { c.CostomerCode, c.StoreName } into discountGroup
                         from c in discountGroup.DefaultIfEmpty()
                         group c by new { b.Contry, a.Street } into grouped
                         select new
                         {
                             Country = grouped.Key.Contry,
                             Street = grouped.Key.Street,
                             MaxDiscount = grouped.Max(d => d?.Discount ?? 0)
                         };
            foreach (var item in result.OrderBy(r => r.Country).ThenBy(r => r.Street))
            {
                if (item.MaxDiscount > 0)
                {
                    Console.WriteLine($"{item.Country} {item.Street} {item.MaxDiscount}");
                }
            }
        }
    }
}