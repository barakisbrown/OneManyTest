using System.Collections.Generic;
using System.Data.Entity;

namespace OneMany.Model
{
    public class ComicDBInitializer : DropCreateDatabaseAlways<MyDbContext>
    {
        protected override void Seed(MyDbContext context)
        {
            var P = new Publisher
            {
                Name = "Valiant Entertainment",
                Imprint = false
            };

            var C = new ComicInfo
            {
                Name = "Eternal Warrior",
                IssueNumber = 1,
                CoverPrice = 3.99
            };

            var W = new Writer
            {
                Name = "Greg Pak"
            };

            // PUBLISHER TABLE
            P.Comics = new List<ComicInfo>();
            P.Writers = new List<Writer>();
            // WRITER TABLE
            W.Comics = new List<ComicInfo>();
            W.Comics.Add(C);
            // ADD TO THE DATABASE
            P.Comics.Add(C);
            P.Writers.Add(W);

            context.Publisher.Add(P);

            base.Seed(context);
        }
    }
}