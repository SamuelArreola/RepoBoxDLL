using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System;
namespace RepoBox.Donat11
{
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/donat")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.sat.gob.mx/donat", IsNullable = false)]
    public partial class Donatarias
    {
        [XmlNamespaceDeclarationsAttribute]
        public XmlSerializerNamespaces myNamespaces = new XmlSerializerNamespaces();
        private string versionField;

        private string noAutorizacionField;

        private System.DateTime fechaAutorizacionField;

        private string leyendaField;

        public Donatarias()
        {
            this.versionField = "1.1";
            this.myNamespaces.Add("donat", "http://www.sat.gob.mx/donat");
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string noAutorizacion
        {
            get
            {
                return this.noAutorizacionField;
            }
            set
            {
                this.noAutorizacionField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime fechaAutorizacion
        {
            get
            {
                return this.fechaAutorizacionField;
            }
            set
            {
                this.fechaAutorizacionField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string leyenda
        {
            get
            {
                return this.leyendaField;
            }
            set
            {
                this.leyendaField = value;
            }
        }
        public string GenerarCFD()
        {
            try
            {
                //string validacion = this.Validar();
                //if (validacion != "OK")
                //    return validacion;
                XmlDocument document = new XmlDocument();
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(this.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    x.Serialize(stream, this);
                    //electronicDocument.SaveToStream(stream);
                    stream.Position = 0L;
                    StreamReader reader = new StreamReader(stream);
                    string xml = reader.ReadToEnd();
                    reader.Close();
                    document.LoadXml(xml);
                }
                return document.InnerXml;
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("RepoBox"))
                    throw new Exception(ex.Message);
                else
                    throw new Exception("RepoBox: " + ex.Message);
            }
        }
    }
}