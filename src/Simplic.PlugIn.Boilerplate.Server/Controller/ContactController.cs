using System;
using Simplic.Boilerplate.Shared;
using Simplic.Framework.WebAPI.Core;
using Simplic.Boilerplate;
using System.Threading.Tasks;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;

namespace Simplic.PlugIn.Boilerplate.Server
{
    [Framework.WebAPI.JWTAuthorize]
    public class ContactController : ExtendedApiController
    {
        private readonly IContactService contactService;

        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        [HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "Returns a contact instance by id", typeof(ContactModel))]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "Create a new contact instance and returns the created contact", typeof(ContactModel))]
        public async Task<IHttpActionResult> Create(CreateContactRequest model)
        {
            return Ok();
        }

        [HttpPut]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "Updates an exusting contact instance and returns the created contact", typeof(ContactModel))]
        public async Task<IHttpActionResult> Update(UpdateContactRequest model)
        {
            return Ok();
        }

        [HttpDelete]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "Deletes an existing contact", typeof(DeleteContactResponse))]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            return Ok();
        }
    }
}
