﻿using System.Threading.Tasks;
using System;

namespace Simplic.Boilerplate
{
    /// <summary>
    /// Service for managing contact.
    /// </summary>
    public interface IContactBaseService
    {
        /// <summary>
        /// Asynchronously deletes a contact object and sends a deleted event.
        /// </summary>
        /// <param name="contact">Contact to delete.</param>
        /// <returns>Task.</returns>
        Task DeleteAsync(Contact contact);

        /// <summary>
        /// Asynchronously creates a new contact object and sends a created event.
        /// </summary>
        /// <param name="contact">Contact to create.</param>
        /// <returns>Task.</returns>
        Task<Contact> GetAsync(Guid id);

        /// <summary>
        /// Asynchronously deletes a contact object and sends a deleted event.
        /// </summary>
        /// <param name="contact">Contact to delete.</param>
        /// <returns>Task.</returns>
        Task CreateAsync(Contact contact);

        /// <summary>
        /// Asynchronously deletes a contact object and sends a deleted event.
        /// </summary>
        /// <param name="id">Identifier of the contact to delete.</param>
        /// <returns>Task.</returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Asynchronously updates a contact object and sends a updated event.
        /// </summary>
        /// <param name="contact">Contact to update.</param>
        /// <returns>Task.</returns>
        Task UpdateAsync(Contact contact);
    }
}
