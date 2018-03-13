using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;

namespace RepoBox33.SATCatalogos
{
    [Serializable]
    public class Moneda
    {
        public string ID { get; set; }
        public string Descripcion { get; set; }
        public int Decimales { get; set; }
        public static List<Moneda> GetMonedasList()
        {
            List<Moneda> list = new List<Moneda>();
            try
            {
                RepoBox.SAT.Conexion sql = new RepoBox.SAT.Conexion();
                string respuesta = sql.ExecuteToXML("GetMonedasList");
                if (respuesta == "")
                    return list;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(respuesta);
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                    list.Add(new Moneda() { ID = node.Attributes["ID"].Value, Descripcion = node.Attributes["Descripcion"].Value, Decimales = Convert.ToInt32(node.Attributes["Decimales"].Value) });
                return list;
            }
            catch (Exception)
            {
                return new List<Moneda>();
            }
        }
    }
}
