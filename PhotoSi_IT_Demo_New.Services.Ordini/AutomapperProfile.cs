using AutoMapper;
using PhotoSi_IT_Demo_New.Common.Ordini;
using PhotoSi_IT_Demo_New.Services.Ordini.Data.Entities;

namespace PhotoSi_IT_Demo_New.Services.Ordini
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<OrdineModel, OrdineEntity>()
                .ReverseMap();         
        }       
    }
}
