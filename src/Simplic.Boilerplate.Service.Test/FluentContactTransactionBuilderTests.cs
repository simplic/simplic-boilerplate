using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Simplic.Boilerplate.Service.Test
{
    public class FluentContactTransactionBuilderTests
    {
        private readonly Mock<IContactRepository> contactRepositoryMock;
        private readonly FluentContactTransactionBuilder sut;

        public FluentContactTransactionBuilderTests()
        {
            contactRepositoryMock = new Mock<IContactRepository>();
            sut = new FluentContactTransactionBuilder(contactRepositoryMock.Object);
        }

        [Fact]
        public async Task ShouldCallCreateAndCommit()
        {
            var contact = new Contact();

            var result = await sut.AddCreate(contact)
                .CommitAsync();

            result.Should().Be(0);
            contactRepositoryMock.Verify(m => m.CreateAsync(contact), Times.Once);
            contactRepositoryMock.Verify(m => m.UpdateAsync(contact), Times.Never);
            contactRepositoryMock.Verify(m => m.DeleteAsync(contact.Id), Times.Never);
            contactRepositoryMock.Verify(m => m.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task ShouldCallMultipleCreateAndCommit()
        {
            var contact = new Contact();

            var result = await sut.AddCreate(contact)
                .AddCreate(contact)
                .AddCreate(contact)
                .CommitAsync();

            result.Should().Be(0);
            contactRepositoryMock.Verify(m => m.CreateAsync(contact), Times.Exactly(3));
            contactRepositoryMock.Verify(m => m.UpdateAsync(contact), Times.Never);
            contactRepositoryMock.Verify(m => m.DeleteAsync(contact.Id), Times.Never);
            contactRepositoryMock.Verify(m => m.CommitAsync(), Times.Once);
        }


        [Fact]
        public async Task ShouldCallUpdateAndCommit()
        {
            var contact = new Contact();

            var result = await sut.AddUpdate(contact)
                .CommitAsync();

            result.Should().Be(0);
            contactRepositoryMock.Verify(m => m.UpdateAsync(contact), Times.Once);
            contactRepositoryMock.Verify(m => m.CreateAsync(contact), Times.Never);
            contactRepositoryMock.Verify(m => m.DeleteAsync(contact.Id), Times.Never);
            contactRepositoryMock.Verify(m => m.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task ShouldCallMultipleUpdateAndCommit()
        {
            var contact = new Contact();

            var result = await sut.AddUpdate(contact)
                .AddUpdate(contact)
                .AddUpdate(contact)
                .CommitAsync();

            result.Should().Be(0);
            contactRepositoryMock.Verify(m => m.UpdateAsync(contact), Times.Exactly(3));
            contactRepositoryMock.Verify(m => m.CreateAsync(contact), Times.Never);
            contactRepositoryMock.Verify(m => m.DeleteAsync(contact.Id), Times.Never);
            contactRepositoryMock.Verify(m => m.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task ShouldCallDeleteAndCommit()
        {
            var contact = new Contact();

            var result = await sut.AddDelete(contact)
                .CommitAsync();

            result.Should().Be(0);
            contactRepositoryMock.Verify(m => m.DeleteAsync(contact.Id), Times.Once);
            contactRepositoryMock.Verify(m => m.CreateAsync(contact), Times.Never);
            contactRepositoryMock.Verify(m => m.UpdateAsync(contact), Times.Never);
            contactRepositoryMock.Verify(m => m.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task ShouldCallMultipleDeleteAndCommit()
        {
            var contact = new Contact();

            var result = await sut.AddDelete(contact)
                .AddDelete(contact)
                .AddDelete(contact)
                .CommitAsync();

            result.Should().Be(0);
            contactRepositoryMock.Verify(m => m.DeleteAsync(contact.Id), Times.Exactly(3));
            contactRepositoryMock.Verify(m => m.CreateAsync(contact), Times.Never);
            contactRepositoryMock.Verify(m => m.UpdateAsync(contact), Times.Never);
            contactRepositoryMock.Verify(m => m.CommitAsync(), Times.Once);
        }
    }
}
