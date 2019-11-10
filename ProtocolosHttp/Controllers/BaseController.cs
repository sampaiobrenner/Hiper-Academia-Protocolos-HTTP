using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ProtocolosHttp.Controllers
{
    public abstract class BaseController<T> : ControllerBase
    {
        public abstract ActionResult Adicionar(T model);

        public abstract ActionResult Alterar(T model);

        public abstract ActionResult Deletar(int codigo);

        public abstract List<T> GetAll();
    }
}