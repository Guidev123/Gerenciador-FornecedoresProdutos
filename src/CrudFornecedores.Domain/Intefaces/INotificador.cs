using CrudFornecedores.Domain.Notificacoes;
using System.Collections.Generic;

namespace CrudFornecedores.Domain.Intefaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}