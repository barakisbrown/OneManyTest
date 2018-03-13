using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace OneMany.Model
{
    public class Writer : ObservableObject
    {
        private int _writerId;
        private string _name;

        public int WriterId
        {
            get => _writerId;
            set => Set(ref _writerId, value);
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public int PublisherId { get; set; }

        public virtual ICollection<ComicInfo> Comics { get; set; }

        public virtual Publisher Publisher { get; set; }
    }
}