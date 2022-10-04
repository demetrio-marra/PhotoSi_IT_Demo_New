using AutoMapper;
using PhotoSi_IT_Demo_New.Common.Prodotti;
using PhotoSi_IT_Demo_New.Services.Prodotti.Data.Entities;

namespace PhotoSi_IT_Demo_New.Services.Prodotti
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<ProdottoModel, ProdottoEntity>()
                .ReverseMap();
            CreateMap<ProdottoOrdinatoModel, ProdottoOrdinatoEntity>()
                .ReverseMap();
        }       
    }
}
