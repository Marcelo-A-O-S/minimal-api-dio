namespace Test.Dominio.Services.Interfaces
{
    public interface ITestServices
    {
        void Salvar();
        void SalvarObjetoInvalido();
        void SalvarObjetoVazio();
        void BuscarPorId();
        void BuscarPorIdInexistente();
        void BuscarTodos();
        void Atualizar();
        void AtualizarObjetoInvalido();
        void AtualizarObjetoVazio();
        void Deletar();
        void DeletarObjetoInvalido();
        void DeletarObjetoVazio();
    }
}