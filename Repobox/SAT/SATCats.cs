using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;

namespace RepoBox33.SATCatalogos
{
    public class SATCats
    {
        public List<FormaPago> FormasDePago { get; set; }
        public List<TipoFactor> TiposFactor { get; set; }
        public List<Impuesto> ImpuestosList { get; set; }
        public List<TasaOCuota> TasasOCuota { get; set; }
        public List<TipoComprobante> TiposComprobante { get; set; }
        public List<Moneda> Monedas { get; set; }
        public List<MetodoPago> MetodosPago { get; set; }
        public List<RegimenFiscal> RegimenesFiscales { get; set; }
        public List<UsoCFDI> UsosCFDI { get; set; }
        public List<UnidadAduana> UnidadesAduana { get; set; }
        public void CargarCatalogos()
        {
            // Formas de Pago.
            if (FormasDePago == null || FormasDePago.Count > 0)
                FormasDePago = new List<FormaPago>();
            FormasDePago = FormaPago.GetFormasDePagoList();
            // TiposFactor
            if (TiposFactor == null || TiposFactor.Count > 0)
                TiposFactor = new List<TipoFactor>();
            TiposFactor = TipoFactor.LoadTipoFactor();
            // Impuesto
            if (ImpuestosList == null || ImpuestosList.Count > 0)
                ImpuestosList = new List<Impuesto>();
            ImpuestosList = Impuesto.LoadImpuestos();
            // Tasas o Cuotas
            if (TasasOCuota == null || TasasOCuota.Count > 0)
                TasasOCuota = new List<TasaOCuota>();
            TasasOCuota = TasaOCuota.LoadTasaOCuota();
            // Tipos Comprobante
            if (TiposComprobante == null || TiposComprobante.Count > 0)
                TiposComprobante = new List<TipoComprobante>();
            TiposComprobante = TipoComprobante.LoadTiposComprobante();
            // Monedas
            if (Monedas == null || Monedas.Count > 0)
                Monedas = new List<Moneda>();
            Monedas = Moneda.GetMonedasList();
            // MetodosPago
            if (MetodosPago == null || MetodosPago.Count > 0)
                MetodosPago = new List<MetodoPago>();
            MetodosPago = MetodoPago.LoadMetodosPago();
            // RegimenFiscal
            if (RegimenesFiscales == null || RegimenesFiscales.Count > 0)
                RegimenesFiscales = new List<RegimenFiscal>();
            RegimenesFiscales = RegimenFiscal.GetRegimenesFiscales();
            // UsosCFDI
            if (UsosCFDI == null || UsosCFDI.Count > 0)
                UsosCFDI = new List<UsoCFDI>();
            UsosCFDI = UsoCFDI.GetUsosCFDI();
            if (UnidadesAduana == null || UnidadesAduana.Count > 0)
                UnidadesAduana = new List<UnidadAduana>();
            UnidadesAduana = UnidadAduana.GetUnidadAduanaList();
        }
        public ClavePyS GetClavePS(string clave)
        {
            try
            {
                RepoBox.SAT.Conexion sql = new RepoBox.SAT.Conexion();
                string respuesta = sql.ExecuteToXML("GetDescripcion_SAT_PyS @Clave = '" + clave + "'");
                if (respuesta == "")
                    return new ClavePyS() { Clave = clave };
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(respuesta);
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                    return new ClavePyS() { Clave = node.Attributes["Clave"].Value, Descripcion = node.Attributes["Descripcion"].Value };
                return new ClavePyS() { Clave = clave };
            }
            catch (Exception)
            {
                return new ClavePyS() { Clave = clave };
            }
        }
        public ClaveUnidad GetClaveUnidad(string clave)
        {
            try
            {
                RepoBox.SAT.Conexion sql = new RepoBox.SAT.Conexion();
                string respuesta = sql.ExecuteToXML("GetDescripcion_SAT_Unidad @Clave = '" + clave + "'");
                if (respuesta == "")
                    return new ClaveUnidad() { Clave = clave };
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(respuesta);
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                    return new ClaveUnidad() { Clave = node.Attributes["Clave"].Value, Descripcion = node.Attributes["Descripcion"].Value };
                return new ClaveUnidad() { Clave = clave };
            }
            catch (Exception)
            {
                return new ClaveUnidad() { Clave = clave };
            }
        }
    }
}
