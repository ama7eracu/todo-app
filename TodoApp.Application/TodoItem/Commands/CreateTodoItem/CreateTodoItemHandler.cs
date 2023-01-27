using MediatR;
using TodoApp.Application.Interfaces;

namespace TodoApp.Application.TodoItem.Commands.CreateTodoItem;

public class CreateTodoItemHandler:IRequestHandler<CreateTodoItemCommand,Guid>
{
    private readonly ITodoDbContext _dbContext;

    public CreateTodoItemHandler(ITodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var item = new Todo.Domain.TodoItem
        {
            Description = request.Description,
            Done = false,
            Id = Guid.NewGuid(),
            ListID = request.ListId,
            Title = request.Title
        };

        _dbContext.Items.AddAsync(item, cancellationToken);
        
        return item.Id;
    }
}