using AutoMapper;

namespace Morpheus.Test.Commons
{
    public class MapperFixture<TProfile> where TProfile : Profile, new()
    {
        public MapperFixture()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<TProfile>());

            Mapper = configuration.CreateMapper();
        }

        /// <summary>
        /// Instance to map objects.
        /// </summary>
        public IMapper Mapper { get; }
    }
}
