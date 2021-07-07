using Biblioteca.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Biblioteca.WPF.API.Client
{
    public static class Extension
    {
        public static void Validate(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var message = response.Content.ReadAsStringAsync().Result;
                throw new BibliotecaApplicationException(message);
            }
        }
    }
}
