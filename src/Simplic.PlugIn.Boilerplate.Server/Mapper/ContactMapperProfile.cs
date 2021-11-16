using Simplic.Boilerplate.Shared;
using Simplic.Boilerplate;

namespace Simplic.PlugIn.Boilerplate.Server
{
    /// <summary>
    /// Profile for the contact mapper.
    /// </summary>
    internal class ContactMapperProfile : AutoMapper.Profile
    {
        /// <summary>
        /// INitializes a new instance of the mapper profile.
        /// </summary>
        public ContactMapperProfile()
        {
            CreateMap<CreateContactRequest, Contact>();
            CreateMap<UpdateContactRequest, Contact>();

            CreateMap<Contact, ContactModel>();
        }
    }
}
