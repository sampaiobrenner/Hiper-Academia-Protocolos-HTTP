using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProtocolosHttp.Controllers
{
    [Route("usuario")]
    [ApiController]
    public class UsuarioController : BaseController<UsuarioModel>
    {
        private static readonly List<UsuarioModel> ListaDeUsuarios = new List<UsuarioModel>();

        [HttpPost]
        public override ActionResult Adicionar(UsuarioModel usuario)
        {
            if (ListaDeUsuarios.Any(x => x.Codigo == usuario.Codigo))
                return BadRequest("Já existe um usuário com o código informado");

            ListaDeUsuarios.Add(usuario);

            return Ok($"Usuário {usuario.Nome} cadastrado com sucesso!");
        }

        [HttpPut]
        public override ActionResult Alterar(UsuarioModel usuario)
        {
            if (ListaDeUsuarios.All(x => x.Codigo != usuario.Codigo))
                return BadRequest("Não existe nenhum usuário com o código informado");

            ListaDeUsuarios.Where(x => x.Codigo == usuario.Codigo).ToList()
                .ForEach(x =>
                {
                    x.Nome = usuario.Nome;
                    x.Login = usuario.Login;
                });

            return Ok($"Usuário {usuario.Nome} alterado com sucesso!");
        }

        [HttpGet]
        [Route("busca-codigo/{codigo}")]
        public UsuarioModel ConsultarUsuarioPorCodigo(int codigo) =>
            ListaDeUsuarios.FirstOrDefault(n => n.Codigo == codigo);

        [HttpGet]
        [Route("busca-nome/{nome}")]
        public List<UsuarioModel> ConsultarUsuarioPorNome(string nome) =>
            ListaDeUsuarios.Where(n => n.Nome.Contains(nome, StringComparison.CurrentCultureIgnoreCase)).ToList();

        [HttpDelete]
        [Route("{codigo}")]
        public override ActionResult Deletar(int codigo)
        {
            var usuario = ListaDeUsuarios.Where(n => n.Codigo == codigo)
                .Select(n => n)
                .First();

            if (usuario is null)
                return BadRequest("Usuário não encontrado");

            ListaDeUsuarios.Remove(usuario);

            return Ok("Registro excluido com sucesso!");
        }

        [HttpGet]
        public override List<UsuarioModel> GetAll() => ListaDeUsuarios;
    }
}