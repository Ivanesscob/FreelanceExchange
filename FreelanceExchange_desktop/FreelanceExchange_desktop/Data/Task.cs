using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Media.Imaging;

namespace FreelanceExchange_desktop.Data
{
    public class Task : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Budget { get; set; }
        public int StatusId { get; set; }
        public List<Response> Responses { get; set; } = new List<Response>();
        public string Image { get; set; }

        //private byte[] imageData;
        //public byte[] ImageData
        //{
        //    get => imageData;
        //    set
        //    {
        //        imageData = value;
        //        OnPropertyChanged(nameof(ImageData));
        //        OnPropertyChanged(nameof(Image));
        //    }
        //}




        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
