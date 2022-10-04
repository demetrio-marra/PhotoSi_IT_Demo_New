using AutoMapper;
using PhotoSi_IT_Demo_New.Common.Recapiti;
using PhotoSi_IT_Demo_New.Services.Recapiti.Data.Entities;

namespace PhotoSi_IT_Demo_New.Services.Recapiti
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<RecapitoModel, RecapitoEntity>()
                .ReverseMap();
        }       
    }
}
