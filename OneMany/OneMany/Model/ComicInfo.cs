using GalaSoft.MvvmLight;

namespace OneMany.Model
{
    public class ComicInfo : ObservableObject
    {
        // BACKING FIELD
        private int _comicInfoId;
        private string _name;
        private int _issueNumber;
        private double _coverPrice;

        // PROPERTIES
        public int ComicInfoId
        {
            get => _comicInfoId;
            set => Set(ref _comicInfoId, value);
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public int IssueNumber
        {
            get => _issueNumber;
            set => Set(ref _issueNumber, value);
        }

        public double CoverPrice
        {
            get => _coverPrice;
            set => Set(ref _coverPrice, value);
        }

        public int PublisherId { get; set; }

        public int WriterId { get; set; }

        // NAVIGATION PROPERTY
        public virtual Publisher Publisher { get; set; }

        public virtual Writer Writer { get; set; }
    }
}