using System.Collections.ObjectModel;

namespace UI.ViewModel
{
    public class DoneRatioList : ObservableCollection<int>
    {
        public DoneRatioList()
        {
            this.Add(0);
            this.Add(10);
            this.Add(20);
            this.Add(30);
            this.Add(40);
            this.Add(50);
            this.Add(60);
            this.Add(70);
            this.Add(80);
            this.Add(90);
            this.Add(100);
        }
    }
}