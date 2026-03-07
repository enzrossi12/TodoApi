using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Services;
using TaskStatus = TodoApi.Models.TaskStatus;

namespace TodoApi.Tests.Services;

public class EfTaskServiceTests
{
    private static AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateAsync_ShouldPersistTask()
    {
        await using var context = CreateDbContext();
        var service = new EfTaskService(context);

        var created = await service.CreateAsync("Estudar", "Revisar EF Core");

        Assert.True(created.Id > 0);
        Assert.Equal("Estudar", created.Title);
        Assert.Equal(1, await context.Tasks.CountAsync());
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTasks()
    {
        await using var context = CreateDbContext();
        var service = new EfTaskService(context);
        await service.CreateAsync("T1", "D1");
        await service.CreateAsync("T2", "D2");

        var all = await service.GetAllAsync();

        Assert.Equal(2, all.Count);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenTaskDoesNotExist()
    {
        await using var context = CreateDbContext();
        var service = new EfTaskService(context);

        var result = await service.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenTaskDoesNotExist()
    {
        await using var context = CreateDbContext();
        var service = new EfTaskService(context);

        var deleted = await service.DeleteAsync(123);

        Assert.False(deleted);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveTask_WhenTaskExists()
    {
        await using var context = CreateDbContext();
        var service = new EfTaskService(context);
        var created = await service.CreateAsync("Excluir", "Item");

        var deleted = await service.DeleteAsync(created.Id);

        Assert.True(deleted);
        Assert.Equal(0, await context.Tasks.CountAsync());
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateOnlyProvidedFields()
    {
        await using var context = CreateDbContext();
        var service = new EfTaskService(context);
        var created = await service.CreateAsync("Original", "Descricao original");

        var updated = await service.UpdateAsync(
            created.Id,
            "Novo titulo",
            null,
            TaskStatus.Concluido);

        Assert.NotNull(updated);
        Assert.Equal("Novo titulo", updated!.Title);
        Assert.Equal("Descricao original", updated.Description);
        Assert.Equal(TaskStatus.Concluido, updated.Status);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNull_WhenTaskDoesNotExist()
    {
        await using var context = CreateDbContext();
        var service = new EfTaskService(context);

        var updated = await service.UpdateAsync(404, "Titulo", "Descricao", TaskStatus.EmAndamento);

        Assert.Null(updated);
    }
}
