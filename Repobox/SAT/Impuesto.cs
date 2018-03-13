using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace RepoBox33.SATCatalogos
{
    [Serializable]
    public class Impuesto
    {
        public string ID { get; set; }
        public string Descripcion { get; set; }
        public static List<Impuesto> LoadImpuestos()
        {   /*Impuesto: 001=ISR, 002=IVA,003=IEPS*/
            List<Impuesto> list = new List<Impuesto>();
            list.Add(new Impuesto() { ID = "001", Descripcion = "ISR" });
            list.Add(new Impuesto() { ID = "002", Descripcion = "IVA" });
            list.Add(new Impuesto() { ID = "003", Descripcion = "IEPS" });
            return list;
        }
    }
}
