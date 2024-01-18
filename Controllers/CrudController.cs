using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using FireSharp;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;

using BaseDatos2crudFirebase.Models;
using Microsoft.VisualBasic;



namespace BaseDatos2crudFirebase.Controllers
{
    public class CrudController : Controller
    {
        // GET: Crud
   
            //Get crud

            IFirebaseClient cliente;

            public CrudController()
            {
                //configuracion para conectar
                IFirebaseConfig config = new FirebaseConfig()
                {
                    AuthSecret = "nOISbXuESt1BdfZltDV3fDCVfzu2ipIYa58vW0OR",
                    BasePath = "https://finalbdii2023-default-rtdb.firebaseio.com/"
                };

                cliente = new FirebaseClient(config);
            }


            public ActionResult Inicio()
            {
                // KEY y muestra los datos de cliente
                Dictionary<String, ClienteFB> lista = new Dictionary<String, ClienteFB>();

                FirebaseResponse response = cliente.Get("Cliente");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //desarializar porque esta en en notacion Json

                    lista = JsonConvert.DeserializeObject<Dictionary<string, ClienteFB>>(response.Body);


                }
                // validar si no trae datos
                if (lista != null && lista.Count > 0)
                {
                    List<ClienteFB> listadocliente = new List<ClienteFB>();
                    foreach (KeyValuePair<String, ClienteFB> item in lista)
                    {
                        listadocliente.Add(new ClienteFB()
                        {
                            IdClienteFB = item.Key,
                            Nombre = item.Value.Nombre,
                            TipoCliente = item.Value.TipoCliente,
                            EstadoCivil = item.Value.EstadoCivil,
                            Genero = item.Value.Genero,
                            Direcion = item.Value.Direcion



                        });


                    }
                    return View(listadocliente);


                }
                return View();
            }
        
        public ActionResult Crear()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Crear(ClienteFB OBcliente)
        {
            string idgenerate = Guid.NewGuid().ToString("N");
            SetResponse response = cliente.Set("Cliente/" + idgenerate, OBcliente);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("Inicio", "Crud");
            }
            return View();
        }

        public ActionResult Eliminar(string idcliente)
        {
            FirebaseResponse response = cliente.Delete("Cliente/" + idcliente);

            return RedirectToAction("Inicio", "Crud");

        }

        public ActionResult Editar(string idcliente)
        {
            FirebaseResponse response = cliente.Get("Cliente/" + idcliente);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ClienteFB cliente = response.ResultAs<ClienteFB>();
                return View(cliente);
            }

            return RedirectToAction("Inicio", "Crud");
        }

        [HttpPost]
         public ActionResult Editar(ClienteFB clienteModel)
         {


           /* FirebaseResponse response = cliente.Update("Cliente/" + clienteModel.Id, 
                  JsonConvert.SerializeObject(clienteModel));*/
           
             FirebaseResponse response = cliente.Update("Cliente/" + clienteModel.IdClienteFB, clienteModel);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
             {
                 return RedirectToAction("Inicio", "Crud");
             }

            return View(clienteModel);
            




        }












    }
}