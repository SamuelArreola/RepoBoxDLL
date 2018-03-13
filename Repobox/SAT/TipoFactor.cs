using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace RepoBox33.SATCatalogos
{
    [Serializable]
    public class TipoFactor
    {
        public string Tipo { get; set; }
        public static List<TipoFactor> LoadTipoFactor()
        {   /*TipoFactor: Tasa,Cuota,Exento*/
            List<TipoFactor> list = new List<TipoFactor>();
            list.Add(new TipoFactor() { Tipo = "Tasa" });
            list.Add(new TipoFactor() { Tipo = "Cuota" });
            list.Add(new TipoFactor() { Tipo = "Exento" });
            return list;
        }
    }
}
