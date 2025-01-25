using Jewelery.ViewModels.DTO.DetermineTheSizeEditor;

namespace Jewelery.Servise.DetermineTheSizeService
{
    public interface IDetermineTheSizeService
    {
        public void SetUpDetermineTheSize(DetermineTheSizePageObjectFormDTO obj);

        public List<DetermineTheSizePageDTO> GetDetermineTheSizePageList();

        public DetermineTheSizePageDTO GetDetermineTheSizePage(int id);

        public List<DetermineSizeSelectList> GetDetermineSizeSelectList();

        public void DeleteDetermineTheSizePage(int id);
        public IQueryable<DetermineTheSizePageObjectDTO> GetPageObjectListByPageId(int id);



    }
}
