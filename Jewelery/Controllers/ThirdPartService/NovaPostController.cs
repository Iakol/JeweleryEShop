using Jewelery.Servise.NovaPostServise;
using Jewelery.ViewModels.DTO.NovaPost;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jewelery.Controllers.ThirdPartService
{
    public class NovaPostController : Controller
    {
        private readonly INovaPostServise _novaPostServise;

        public NovaPostController(INovaPostServise novaPostServise)
        {
            _novaPostServise = novaPostServise;
        }

        public async Task<JsonResult> GetCityList(String city)
        {
            NovaPostCityResponce response = await _novaPostServise.GetCityListByName(city);

            List<AdressCityDTO> list = response.data[0].Addresses;

            return Json(list);
        }

        public async Task<JsonResult> GetPostList(String cityRef)
        {
            NovaPostPostResponce response = await _novaPostServise.GetPostListByCityRef(cityRef);

            List<PostAdressDTO> list = response.data;

            return Json(list);
        }


    }

}