using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;

namespace RepoBox33.SATCatalogos
{
    [Serializable]
    public class FormaPago
    {
        public string ID { get; set; }
        public string Descripcion { get; set; }
        // Pendiente propiedades para validación de complemento de pagos.

        public static List<FormaPago> GetFormasDePagoList()
        {
            List<FormaPago> list = new List<FormaPago>();
            try
            {
                RepoBox.SAT.Conexion sql = new RepoBox.SAT.Conexion();
                string respuesta = sql.ExecuteToXML("GetFormasDePagoList");
                if (respuesta == "")
                    return list;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(respuesta);
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                    list.Add(new FormaPago() { ID = node.Attributes["ID"].Value, Descripcion = node.Attributes["Descripcion"].Value });
                return list;
            }
            catch (Exception)
            {
                return new List<FormaPago>();
            }
        }
    }
}
