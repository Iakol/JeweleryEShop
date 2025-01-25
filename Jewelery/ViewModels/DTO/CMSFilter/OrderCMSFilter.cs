namespace Jewelery.ViewModels.DTO.CMSFilter
{
    public class OrderCMSFilter
    {
        public int OrderSelect { get; set; } = 0; // means by the newest (1 - max Total prise 2 - min Total Price)

        public List<int> SelectedStatus { get; set; } = new List<int>(); // empty means all
        public string SearchValue { get; set; } = string.Empty;

        public List<int> SearchType { get; set; } = new List<int>(); //empty means All ( 0 - by user name , 1 by delivery name , 2 by product name)
    }

    public class OrderSearchValueDTO
    {
        public string SearchValue { get; set; }

        public List<int> SearchType { get; set; }
    }

    public class SetOrderFilterType
    {
        public string partOfFilter { get; set; }
        public int typeOfFiltering { get; set; }
    }

    
}
