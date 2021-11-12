using Simplic.Boilerplate.Shared;
using Simplic.Boilerplate;

namespace Simplic.PlugIn.Boilerplate.Server
{
    internal class ContactMapperProfile : AutoMapper.Profile
    {
        public ContactMapperProfile()
        {
            CreateMap<CreateContactRequest, Contact>();
            CreateMap<UpdateContactRequest, Contact>();

            CreateMap<Contact, ContactModel>();
        }
    }
}
