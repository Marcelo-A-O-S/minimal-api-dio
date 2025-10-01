namespace Test.Requests.Interfaces
{
    public interface IRequestTest
    {
        //GET
        Task BuscarResgistros();
        Task BuscarRegistroPorId();
        Task BuscarRegistroInexistente();
        //PUT
        Task AtualizarRegistro();
        Task AtualizarRegistroInexistente();
        Task AtualizarRegistroInvalido();
        //Create
        Task CriarRegistro();
        Task CriarRegistroInvalido();
        //Delete
        Task DeletarRegistro();
        Task DeletarRegistroInexistente();
    }
}