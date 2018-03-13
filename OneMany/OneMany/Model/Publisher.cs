using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace OneMany.Model
{
    public class Publisher : ObservableObject
    {

        // BACKING FIELDS
        private int _publisherId;
        private string _name;
        private bool _imprint;

        public int PublisherId
        {
            get => _publisherId;
            set => Set(ref _publisherId, value);
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public bool Imprint
        {
            get => _imprint;
            set => Set(ref _imprint, value);
        }

        // FOREIGN KEY AREA
        public virtual ICollection<ComicInfo> Comics { get; set; }

        public virtual ICollection<Writer> Writers { get; set; }
    }
}