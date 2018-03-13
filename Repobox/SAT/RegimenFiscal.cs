using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;

namespace RepoBox33.SATCatalogos
{
    [Serializable]
    public class RegimenFiscal
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public bool PersonaFisical { get; set; }
        public static List<RegimenFiscal> GetRegimenesFiscales()
        {
            List<RegimenFiscal> list = new List<RegimenFiscal>();
            try
            {
                RepoBox.SAT.Conexion sql = new RepoBox.SAT.Conexion();
                string respuesta = sql.ExecuteToXML("Get_RegimenesFiscales");
                if (respuesta == "")
                    return list;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(respuesta);
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                    list.Add(new RegimenFiscal() { ID = int.Parse(node.Attributes["ID"].Value), Descripcion = node.Attributes["Descripcion"].Value, PersonaFisical = (node.Attributes["PersonaFisica"].Value == "1") });
                return list;
            }
            catch (Exception)
            {
                return new List<RegimenFiscal>();
            }
        }
    }
}
