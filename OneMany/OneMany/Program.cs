using MySql.Data.MySqlClient;
using OneMany.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OneMany
{
    class Program
    {
        static void Main(string[] args)
        {
            string connString = "server=lokislayer.com;database=MANYTEST;uid=BARAKIS;password=BarakisMJB48;persistsecurityinfo=True;";
            using (var conn = new MySqlConnection(connString))
            {
                conn.Open();
                // InitializeDB(conn);
                // InsertComics(conn);
                // InsertComicsPart2(conn);
                InsertComicsPart3(conn);
                // QueryPublishers(conn);
            }
        }

        private static void InsertComics(MySqlConnection conn)
        {
            using (var db = new MyDbContext(conn, false))
            {
                var C1 = new ComicInfo { Name = "Eternal Warrior", IssueNumber = 2, CoverPrice = 3.99 };
                var C2 = new ComicInfo { Name = "Eternal Warrior", IssueNumber = 1, CoverPrice = 3.99 };
                                
                var W = db.Writer.FirstOrDefault(w => w.Name == "Greg Pak");


                var P = db.Publisher.FirstOrDefault(p => p.Name == "Valiant Entertainment");
                C1.Publisher = P;
                C2.Publisher = P;
                C1.Writer = W;
                C1.Publisher = P;
                C1.Writer = W;
                C2.Publisher = P;
                C2.Writer = W;

                db.Comics.Add(C1);
                db.Comics.Add(C2);
                db.SaveChanges();


            }
        }

        public static void InsertComicsPart2(MySqlConnection connection)
        {
            using (var db = new MyDbContext(connection, false))
            {
                var P = new Publisher {Name = "Marvel", Imprint = false};
                var W = new Writer {Name = "Brian Bendis"};
                var C = new ComicInfo {Name = "All-New X-men", IssueNumber = 22, CoverPrice = 4.25};
                var C1 = new ComicInfo { Name = "All-New X-men", IssueNumber = 23, CoverPrice = 3.99 };
                var C2 = new ComicInfo { Name = "All-New X-men", IssueNumber = 24, CoverPrice = 3.99 };

                P.Comics = new List<ComicInfo>();
                P.Comics.Add(C);
                P.Comics.Add(C1);
                P.Comics.Add(C2);
                P.Writers = new List<Writer>();
                P.Writers.Add(W);
                db.Publisher.Add(P);
                db.SaveChanges();
            }
        }

        public static void InsertComicsPart3(MySqlConnection connection)
        {
            using (var db = new MyDbContext(connection, false))
            {
                var P = db.Publisher.FirstOrDefault(p => p.Name == "Valiant Entertainment");
                var W = new Writer {Name = "James Asmus"};
                var C = new ComicInfo {Name = "Quantum and Woody", IssueNumber = 8, CoverPrice = 3.99};
                var C1 = new ComicInfo { Name = "Quantum and Woody", IssueNumber = 9, CoverPrice = 3.99 };
                var C2 = new ComicInfo { Name = "Quantum and Woody", IssueNumber = 10, CoverPrice = 3.99 };

                P.Writers.Add(W);
                // P.Comics.Add(C);
                // P.Comics.Add(C1);
                // P.Comics.Add(C2);

                db.Publisher.Add(P);
                db.SaveChanges();
            }
        }

        public static void QueryPublishers(MySqlConnection con)
        {
            using (var db = new MyDbContext(con, false))
            {
                var query =
                    from t1 in db.Comics
                    join t2 in db.Publisher on t1.PublisherId equals t2.PublisherId
                    join t3 in db.Writer on t1.WriterId equals t3.WriterId
                    select new { t1.Name, t1.WriterId, t1.IssueNumber, t1.CoverPrice };

                Console.WriteLine("Number of records returned = {0}", query.Count());
                foreach (var Q in query.ToList())
                {
                    Console.WriteLine("Name => {0}   Writer = {3}  IssueNumber => {1}     CoverPrice => {2}", Q.Name, Q.IssueNumber, Q.CoverPrice, Q.WriterId);
                }

                Console.ReadKey();
            }
        }

        public static void InitializeDB(MySqlConnection conn)
        {
            using (var db = new MyDbContext(conn, false))
            {
                var P = new Publisher
                {
                    Name = "Valiant Entertainment",
                    Imprint = false
                };

                var C = new ComicInfo
                {
                    Name = "Eternal Warrior",
                    IssueNumber = 5,
                    CoverPrice = 3.99
                };

                var w = new Writer
                {
                    Name = "Greg Pak"
                };

                // PUBLISHERS
                P.Comics = new Collection<ComicInfo>();
                P.Writers = new Collection<Writer>();
                // WRITERS
                w.Comics = new Collection<ComicInfo>();
                w.Comics.Add(C);
                // Add to the database
                P.Comics.Add(C);
                P.Writers.Add(w);

                db.Publisher.Add(P);
                db.SaveChanges();
            }
        }
    }
}
