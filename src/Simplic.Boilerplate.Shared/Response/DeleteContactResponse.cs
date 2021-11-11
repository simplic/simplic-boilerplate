using System;

namespace Simplic.Boilerplate.Shared
{
    public class DeleteContactResponse
    {
        public Guid Guid { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
