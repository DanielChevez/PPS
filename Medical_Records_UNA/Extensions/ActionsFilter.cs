using Medical_Records_Data;
using Medical_Record_Models;


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

using Microsoft.AspNetCore.Identity;
using Medical_Record_Data;

namespace Medical_Records_UNA.Extensions
{
    public class ActionsFilterAttribute : TypeFilterAttribute
    {
        public int NumeroDeAccion { get; set; } // Propiedad para almacenar el número de acción


        public ActionsFilterAttribute(int numeroDeAccion) : base(typeof(NumberActionsFilterImpl))
        {
            Arguments = new object[] { numeroDeAccion }; // Pasar el número de acción como argumento al constructor interno
        }

        private class NumberActionsFilterImpl : IAuthorizationFilter
        {
            private readonly int _numeroDeAccion;
            private static Dictionary<int, string> listaAcciones = new Dictionary<int, string>() { };
            public NumberActionsFilterImpl(int numeroDeAccion)
            {
                _numeroDeAccion = numeroDeAccion;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {

                try
                {

                    // Aquí puedes obtener el usuario logueado (ajusta esta parte según cómo implementaste la autenticación)
                    LoginCredentials us = context.HttpContext.Session.GetObject<LoginCredentials>("User");
                    //UsuarioLogueado usuarioLogueado = ObtenerUsuarioLogueado();
                    var claimUser = context.HttpContext.User;
                    if (claimUser.Identity.IsAuthenticated)
                    {
                        var prueba = false;
                        List<int> actionsAllow = context.HttpContext.Session.GetObject<List<int>>("ActionsOfUser");
                        if (!(actionsAllow.Contains(_numeroDeAccion)))
                        {
                            // Verificar si el número de acción solicitado está en la lista de números de acción permitidos para el usuario
                            if (actionsAllow.Contains(_numeroDeAccion))
                            {
                                string url = listaAcciones[_numeroDeAccion];
                                context.Result = new ForbidResult();
                            }
                            else
                            {
                                context.Result = new RedirectResult("/Home/Index");
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    context.Result = new RedirectResult("/Home/Index");
                }
            }

            // Propiedad para obtener el número de acción permitido
            public int NumeroDeAccion => _numeroDeAccion;
        }
    }
}
