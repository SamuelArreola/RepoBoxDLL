using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;

namespace RepoBox33.SATCatalogos
{
    [Serializable]
    public class UsoCFDI
    {
        public string ID { get; set; }
        public string Descripcion { get; set; }
        public int TipoContribuyentePermitido { get; set; }
        public static List<UsoCFDI> GetUsosCFDI()
        {
            List<UsoCFDI> list = new List<UsoCFDI>();
            try
            {
                RepoBox.SAT.Conexion sql = new RepoBox.SAT.Conexion();
                string respuesta = sql.ExecuteToXML("GetUsosCFDI");
                if (respuesta == "")
                    return list;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(respuesta);
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                    list.Add(new UsoCFDI() { ID = node.Attributes["ID"].Value, Descripcion = node.Attributes["Descripcion"].Value, TipoContribuyentePermitido = int.Parse(node.Attributes["TipoContribuyentePermitido"].Value) });
                return list;
            }
            catch (Exception)
            {
                return new List<UsoCFDI>();
            }
        }
    }
}
