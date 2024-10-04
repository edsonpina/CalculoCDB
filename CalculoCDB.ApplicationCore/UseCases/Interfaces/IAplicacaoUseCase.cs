using CalculoCDB.ApplicationCore.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculoCDB.ApplicationCore.UseCases.Interfaces
{
    public interface IAplicacaoUseCase
    {
        public Resgate Calcular(Aplicacao aplicacao);
    }
}
