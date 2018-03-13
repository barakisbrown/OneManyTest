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
                InsertComics(conn);
                // QueryPublishers(conn);

            }
        }

        private static void InsertComics(MySqlConnection conn)
        {
            using (var db = new MyDbContext(conn, false))
            {

                var C1 = new ComicInfo { Name = "Eternal Warrior", IssueNumber = 2, CoverPrice = 3.99 };
                var C2 = new ComicInfo { Name = "Eternal Warrior", IssueNumber = 1, CoverPrice = 3.99 };

                var W = new Writer() { Name = "Greg Pak" };


                var P = db.Publisher.FirstOrDefault(p => p.Name == "Valiant Entertainment");
                P.Comics = new List<ComicInfo>();
                P.Writers = new List<Writer>();
                P.Comics.Add(C1);
                P.Comics.Add(C2);
                P.Writers.Add(W);
                W.Comics = new List<ComicInfo>();
                W.Comics.Add(C1);
                W.Comics.Add(C2);

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
                    select new { t1.Name, t1.IssueNumber, t1.CoverPrice };

                Console.WriteLine("Number of records returned = {0}", query.Count());
                foreach (var Q in query.ToList())
                {
                    Console.WriteLine("Name => {0}/tIssueNumber => {1}/tCoverPrice => {2}", Q.Name, Q.IssueNumber, Q.CoverPrice);
                }

;
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
