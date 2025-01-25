namespace Jewelery.ViewModels.DTO.FillterSession
{
    public class UserFillterDTO
    {

        public SearchUSerFillterDTO SearchFilter { get; set; }
        
        public bool? WithOrder {  get; set; }
        public bool? WithUnCloseOrder { get; set; }
        public bool? WithItemInCart { get; set; }
    }

    public class SearchUSerFillterDTO 
    {
        public string SearchValue { get; set; } = "";
        public bool UserName { get; set; } = true;
        public bool UserEmail { get; set; } = true;
        public bool UserTelephone { get; set; } = true;
    }
}
