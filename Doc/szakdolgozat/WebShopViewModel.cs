public class WebShopsViewModel : ViewModelBase
{
    private ObservableCollection<WebShop> webShops;

    public ObservableCollection<WebShop> WebShops
    {
        get { return webShops; }
        set
        {
            if (webShops != value)
            {
                webShops = value;
                OnPropertyChanged();
            }
        }
    }

    //...
}


