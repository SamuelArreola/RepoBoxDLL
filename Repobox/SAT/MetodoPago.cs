using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace RepoBox33.SATCatalogos
{
    [Serializable]
    public class MetodoPago
    {
        public string ID { get; set; }
        public string Descripcion { get; set; }
        public static List<MetodoPago> LoadMetodosPago()
        {
            List<MetodoPago> list = new List<MetodoPago>();
            list.Add(new MetodoPago() { ID = "PUE", Descripcion = "PAGO EN UNA SOLA EXHIBICION" });
            list.Add(new MetodoPago() { ID = "PPD", Descripcion = "PAGO EN PARCIALIDADES O DIFERIDO" });
            return list;
        }
    }
}
