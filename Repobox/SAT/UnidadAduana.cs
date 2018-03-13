using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoBox33.SATCatalogos
{
    [Serializable]
    public class UnidadAduana
    {
        public string ID { get; set; }
        public string Descripcion { get; set; }
        // Pendiente propiedades para validación de complemento de pagos.

        public static List<UnidadAduana> GetUnidadAduanaList()
        {
            List<UnidadAduana> list = new List<UnidadAduana>();
            list.Add(new UnidadAduana() { ID = "01", Descripcion = "KILO" });
            list.Add(new UnidadAduana() { ID = "02", Descripcion = "GRAMO" });
            list.Add(new UnidadAduana() { ID = "03", Descripcion = "METRO LINEAL" });
            list.Add(new UnidadAduana() { ID = "04", Descripcion = "METRO CUADRADO" });
            list.Add(new UnidadAduana() { ID = "05", Descripcion = "METRO CUBICO" });

            list.Add(new UnidadAduana() { ID = "06", Descripcion = "PIEZA" });
            list.Add(new UnidadAduana() { ID = "07", Descripcion = "CABEZA" });
            list.Add(new UnidadAduana() { ID = "08", Descripcion = "LITRO" });
            list.Add(new UnidadAduana() { ID = "09", Descripcion = "PAR" });
            list.Add(new UnidadAduana() { ID = "10", Descripcion = "KILOWATT" });

            list.Add(new UnidadAduana() { ID = "11", Descripcion = "MILLAR" });
            list.Add(new UnidadAduana() { ID = "12", Descripcion = "JUEGO" });
            list.Add(new UnidadAduana() { ID = "13", Descripcion = "KILOWATT/HORA" });
            list.Add(new UnidadAduana() { ID = "14", Descripcion = "TONELADA" });
            list.Add(new UnidadAduana() { ID = "15", Descripcion = "BARRIL" });

            list.Add(new UnidadAduana() { ID = "16", Descripcion = "GRAMO NETO" });
            list.Add(new UnidadAduana() { ID = "17", Descripcion = "DECENAS" });
            list.Add(new UnidadAduana() { ID = "18", Descripcion = "CIENTOS" });
            list.Add(new UnidadAduana() { ID = "19", Descripcion = "DOCENAS" });
            list.Add(new UnidadAduana() { ID = "20", Descripcion = "CAJA" });

            list.Add(new UnidadAduana() { ID = "21", Descripcion = "BOTELLA" });
            list.Add(new UnidadAduana() { ID = "99", Descripcion = "SERVICIO" });
            return list;
        }
    }
}
