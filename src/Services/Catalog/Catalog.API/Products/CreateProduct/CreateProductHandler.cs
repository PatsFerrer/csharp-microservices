namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid id); // representa o obj de retorno ^ aqui de cima, nesse caso retorna o ID
    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // criar uma entidade de produto a partir do obj de command
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            // salvar no banco de dados
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            // retorna o resultado de criação de produto
            return new CreateProductResult(product.Id);
        }
    }
}
