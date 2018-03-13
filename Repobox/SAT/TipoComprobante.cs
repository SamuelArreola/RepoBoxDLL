using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace RepoBox33.SATCatalogos
{
    [Serializable]
    public class TipoComprobante
    {
        public string ID { get; set; }
        public string Descripcion { get; set; }
        public static List<TipoComprobante> LoadTiposComprobante()
        {
            List<TipoComprobante> list = new List<TipoComprobante>();
            list.Add(new TipoComprobante() { ID = "I", Descripcion = "Ingreso" });
            list.Add(new TipoComprobante() { ID = "E", Descripcion = "Egreso" });
            list.Add(new TipoComprobante() { ID = "T", Descripcion = "Traslado" });
            list.Add(new TipoComprobante() { ID = "N", Descripcion = "Nómina" });
            list.Add(new TipoComprobante() { ID = "P", Descripcion = "Pago" });
            return list;
        }
        /*  I	Ingreso
            E	Egreso
            T	Traslado
            N	Nómina	
            P	Pago    */
    }
}
