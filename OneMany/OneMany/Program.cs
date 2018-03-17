using MySql.Data.MySqlClient;
using OneMany.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OneMany
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connString = "server=lokislayer.com;database=MANYTEST;uid=BARAKIS;password=BarakisMJB48;persistsecurityinfo=True;";
            using (var conn = new MySqlConnection(connString))
            {
                conn.Open();
                // InsertComics(conn);
                // InsertComicsPart2(conn);
                // InsertComicsPart3(conn);  // <= FIXED I THINK
                InsertComicsPart4(conn);
                // QueryPublishers(conn);
            }
        }

        private static void InsertComics(MySqlConnection conn)
        {
            using (var db = new MyDbContext(conn, false))
            {
                var C1 = new ComicInfo { Name = "Eternal Warrior", IssueNumber = 2, CoverPrice = 3.99 };
                var C2 = new ComicInfo { Name = "Eternal Warrior", IssueNumber = 3, CoverPrice = 3.99 };
                                
                var W = db.Writer.FirstOrDefault(w => w.Name == "Greg Pak");


                var P = db.Publisher.FirstOrDefault(p => p.Name == "Valiant Entertainment");

                // COMIC 1
                C1.Publisher = P;
                C1.Writer = W;
                // COMIC 2
                C2.Publisher = P;
                C2.Writer = W;
                // ADD BOTH COMICS TO THE DATABASE
                db.Comics.Add(C1);
                db.Comics.Add(C2);
                // SAVE THE COMICS TO THE DATABASE
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
                var C = new ComicInfo {Name = "Quantum and Woody", IssueNumber = 8, CoverPrice = 3.99};
                var C1 = new ComicInfo { Name = "Quantum and Woody", IssueNumber = 9, CoverPrice = 3.99 };
                var C2 = new ComicInfo { Name = "Quantum and Woody", IssueNumber = 10, CoverPrice = 3.99 };

                /*
                    var W = new Writer {Name = "James Asmus"};
                    // LET SEE IF I CAN ADD JUST A NEW WRITER AND LINK IT TO ID#1[Valiant Entertainment]
                    W.Publisher = P;
                    db.Writer.Add(W);
                */
                // ^THE ABOVE WORKED^
                var W = db.Writer.FirstOrDefault(w => w.Name == "James Asmus");
                // COMIC 1
                C.Publisher = P;
                C.Writer = W;
                // COMIC 2
                C1.Publisher = P;
                C1.Writer = W;
                // COMIC 3
                C2.Publisher = P;
                C2.Writer = W;
                // ADD COMICS TO THE TABLES
                db.Comics.Add(C);
                db.Comics.Add(C1);
                db.Comics.Add(C2);
                db.SaveChanges();
            }
        }

        public static void InsertComicsPart4(MySqlConnection connection)
        {
            using (var db = new MyDbContext(connection,false))
            {
                // LETS LINK THE NEW WRITER TO THE PUBLISHER [Paul Cornel to Marvel]
                var w = new Writer() {Name = "Paul Cornell"};
                var P = db.Publisher.FirstOrDefault(p => p.Name == "Marvel");
                w.Publisher = P;
                db.Writer.Add(w);
                db.SaveChanges();
                // AT THIS POINT THE WRITER SHOULD BE SAVED IN THE TABLE AND LINKED TO PUBLISHER
                var W = db.Writer.FirstOrDefault(w1 => w1.Name == "Paul Cornell");
                var C = new ComicInfo() {Name = "Wolverine", IssueNumber = 1, CoverPrice = 3.99};
                C.Writer = W;
                C.Publisher = P;
                // ADD COMIC TO THE TABLE AND THEN SUBMIT TO DATABASE
                db.Comics.Add(C);
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
    }
}
