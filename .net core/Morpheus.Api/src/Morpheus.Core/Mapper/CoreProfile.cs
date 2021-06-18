using AutoMapper;
using Morpheus.Core.Filters;
using Morpheus.Core.Models;
using Morpheus.DataContracts.Person;

namespace Morpheus.Core.Mapper
{
    public class CoreProfile : Profile
    {
        public CoreProfile()
        {
            Person();
        }

        private void Person()
        {
            CreateMap<PersonRequest, PersonModel>();
            CreateMap<PersonFilterRequest, PersonFilter>();
            CreateMap<PersonModel, PersonResponse>();
        }
    }
}
