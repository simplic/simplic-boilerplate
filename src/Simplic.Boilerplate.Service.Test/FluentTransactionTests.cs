using Moq;
using Simplic.Data;
using System.Threading.Tasks;
using Xunit;

namespace Simplic.Boilerplate.Service.Test
{
    public class FluentTransactionTests
    {
        private readonly Mock<IContactRepository> contactRepositoryMock = new();
        private readonly Mock<IContactEventService> contactEventServiceMock = new();
        private readonly Mock<ITransactionService> transactionServiceMock = new();
        private readonly IContactService contactService;

        public FluentTransactionTests()
        {
            transactionServiceMock.Setup(t => t.CreateAsync()).Returns(new Task<ITransaction>(() => new Mock<ITransaction>().Object));
            contactService = new ContactService(contactEventServiceMock.Object, contactRepositoryMock.Object, transactionServiceMock.Object);
        }

        [Fact]
        public void ShouldCallCreateTransaction()
        {
            var contact = new Contact();

            contactService.CreateFluentTransaction();

            transactionServiceMock.Verify(m => m.CreateAsync(), Times.Once);
            transactionServiceMock.Verify(m => m.CommitAsync(It.IsAny<ITransaction>()), Times.Never);
            transactionServiceMock.Verify(m => m.AbortAsync(It.IsAny<ITransaction>()), Times.Never);
        }

        [Fact]
        public async Task ShouldCallCommit()
        {
            var contact = new Contact();

            await contactService.CreateFluentTransaction().CommitAsync();

            transactionServiceMock.Verify(m => m.CommitAsync(It.IsAny<ITransaction>()), Times.Once);
            transactionServiceMock.Verify(m => m.AbortAsync(It.IsAny<ITransaction>()), Times.Never);
        }

        [Fact]
        public async Task ShouldCallAbort()
        {
            var contact = new Contact();

            await contactService.CreateFluentTransaction().AbortAsync();

            transactionServiceMock.Verify(m => m.AbortAsync(It.IsAny<ITransaction>()), Times.Once);
            transactionServiceMock.Verify(m => m.CommitAsync(It.IsAny<ITransaction>()), Times.Never);
        }

        [Fact]
        public void ShouldCallCreate()
        {
            var contact = new Contact();

            contactService.CreateFluentTransaction().AddCreate(contact);

            contactRepositoryMock.Verify(m => m.CreateAsync(contact, It.IsAny<ITransaction>()), Times.Once);
            contactRepositoryMock.Verify(m => m.UpdateAsync(contact, It.IsAny<ITransaction>()), Times.Never);
            contactRepositoryMock.Verify(m => m.DeleteAsync(contact.Id, It.IsAny<ITransaction>()), Times.Never);
        }

        [Fact]
        public async Task ShouldSendEventOnCreateCommit()
        {
            var contact = new Contact();

            await contactService.CreateFluentTransaction().AddCreate(contact).CommitAsync();

            contactEventServiceMock.Verify(m => m.SendCreatedEventAsync(contact), Times.Once);
            contactEventServiceMock.Verify(m => m.SendUpdatedEventAsync(contact), Times.Never);
            contactEventServiceMock.Verify(m => m.SendDeletedEventAsync(contact.Id), Times.Never);
        }

        [Fact]
        public void ShouldCallUpdate()
        {
            var contact = new Contact();

            contactService.CreateFluentTransaction().AddUpdate(contact);

            contactRepositoryMock.Verify(m => m.UpdateAsync(contact, It.IsAny<ITransaction>()), Times.Once);
            contactRepositoryMock.Verify(m => m.CreateAsync(contact, It.IsAny<ITransaction>()), Times.Never);
            contactRepositoryMock.Verify(m => m.DeleteAsync(contact.Id, It.IsAny<ITransaction>()), Times.Never);
        }

        [Fact]
        public async Task ShouldSendEventOnUpdateCommit()
        {
            var contact = new Contact();

            await contactService.CreateFluentTransaction().AddUpdate(contact).CommitAsync();

            contactEventServiceMock.Verify(m => m.SendUpdatedEventAsync(contact), Times.Once);
            contactEventServiceMock.Verify(m => m.SendCreatedEventAsync(contact), Times.Never);
            contactEventServiceMock.Verify(m => m.SendDeletedEventAsync(contact.Id), Times.Never);
        }

        [Fact]
        public void ShouldCallDelete()
        {
            var contact = new Contact();

            contactService.CreateFluentTransaction().AddDelete(contact);

            contactRepositoryMock.Verify(m => m.DeleteAsync(contact.Id, It.IsAny<ITransaction>()), Times.Once);
            contactRepositoryMock.Verify(m => m.UpdateAsync(contact, It.IsAny<ITransaction>()), Times.Never);
            contactRepositoryMock.Verify(m => m.CreateAsync(contact, It.IsAny<ITransaction>()), Times.Never);
        }

        [Fact]
        public async Task ShouldSendEventOnDeleteCommit()
        {
            var contact = new Contact();

            await contactService.CreateFluentTransaction().AddDelete(contact).CommitAsync();

            contactEventServiceMock.Verify(m => m.SendDeletedEventAsync(contact.Id), Times.Once);
            contactEventServiceMock.Verify(m => m.SendUpdatedEventAsync(contact), Times.Never);
            contactEventServiceMock.Verify(m => m.SendCreatedEventAsync(contact), Times.Never);
        }

        [Fact]
        public async Task ShouldChainCommands()
        {
            var contact1 = new Contact();
            var contact2 = new Contact();
            var contact3 = new Contact();

            await contactService
                .CreateFluentTransaction()
                .AddCreate(contact1)
                .AddUpdate(contact2)
                .AddDelete(contact3)
                .CommitAsync();

            contactEventServiceMock.Verify(m => m.SendCreatedEventAsync(contact1), Times.Once);
            contactEventServiceMock.Verify(m => m.SendUpdatedEventAsync(contact2), Times.Once);
            contactEventServiceMock.Verify(m => m.SendDeletedEventAsync(contact3.Id), Times.Once);
            contactEventServiceMock.Verify(m => m.SendCreatedEventAsync(contact1), Times.Once);
            contactEventServiceMock.Verify(m => m.SendUpdatedEventAsync(contact2), Times.Once);
            contactEventServiceMock.Verify(m => m.SendDeletedEventAsync(contact3.Id), Times.Once);
            transactionServiceMock.Verify(m => m.CommitAsync(It.IsAny<ITransaction>()), Times.Once);
        }
    }
}
