using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace RepoBox33.SATCatalogos
{
    [Serializable]
    public class TasaOCuota
    {
        /*TasaOCuota:
                            0.000000 	IVA	    Tasa
                            0.160000 	IVA	    Tasa
                            0.160000 	IVA	    Tasa
                            0.265000 	IEPS	Tasa
                            0.300000 	IEPS	Tasa
                            0.530000 	IEPS	Tasa
                            0.500000 	IEPS	Tasa
                            1.600000 	IEPS	Tasa
                            0.304000 	IEPS	Tasa
                            0.250000 	IEPS	Tasa
                            0.090000 	IEPS	Tasa
                            0.080000 	IEPS	Tasa
                            0.070000 	IEPS	Tasa
                            0.060000 	IEPS	Tasa
                            0.030000 	IEPS	Tasa
                            0.000000 	IEPS	Tasa
                            43.770000 	IEPS	Cuota
                            0.350000 	ISR	    Tasa
                         */
        public decimal TasaCuota { get; set; }
        public string Impuesto { get; set; }
        public string Factor { get; set; }
        public bool EsTraslado { get; set; }

        public static List<TasaOCuota> LoadTasaOCuota()
        {
            List<TasaOCuota> list = new List<TasaOCuota>();
            list.Add(new TasaOCuota() { TasaCuota = (decimal)0, Impuesto = "IVA", Factor = "Tasa", EsTraslado = true });
            list.Add(new TasaOCuota() { TasaCuota = (decimal)0.16, Impuesto = "IVA", Factor = "Tasa", EsTraslado = true });
            list.Add(new TasaOCuota() { TasaCuota = (decimal)0.16, Impuesto = "IVA", Factor = "Tasa", EsTraslado = false });
            list.Add(new TasaOCuota() { TasaCuota = (decimal)0.265, Impuesto = "IEPS", Factor = "Tasa", EsTraslado = true });
            list.Add(new TasaOCuota() { TasaCuota = (decimal)0.3, Impuesto = "IEPS", Factor = "Tasa", EsTraslado = true });
            list.Add(new TasaOCuota() { TasaCuota = (decimal)0.53, Impuesto = "IEPS", Factor = "Tasa", EsTraslado = true });
            list.Add(new TasaOCuota() { TasaCuota = (decimal)0.53, Impuesto = "IEPS", Factor = "Tasa", EsTraslado = true });
            list.Add(new TasaOCuota() { TasaCuota = (decimal)1.6, Impuesto = "IEPS", Factor = "Tasa", EsTraslado = true });
            list.Add(new TasaOCuota() { TasaCuota = (decimal)0.304, Impuesto = "IEPS", Factor = "Tasa", EsTraslado = true });
            list.Add(new TasaOCuota() { TasaCuota = (decimal)0.25, Impuesto = "IEPS", Factor = "Tasa", EsTraslado = true });
            list.Add(new TasaOCuota() { TasaCuota = (decimal)0.09, Impuesto = "IEPS", Factor = "Tasa", EsTraslado = true });
            list.Add(new TasaOCuota() { TasaCuota = (decimal)0.08, Impuesto = "IEPS", Factor = "Tasa", EsTraslado = true });
            list.Add(new TasaOCuota() { TasaCuota = (decimal)0.07, Impuesto = "IEPS", Factor = "Tasa", EsTraslado = true });
            list.Add(new TasaOCuota() { TasaCuota = (decimal)0.06, Impuesto = "IEPS", Factor = "Tasa", EsTraslado = true });
            list.Add(new TasaOCuota() { TasaCuota = (decimal)0.03, Impuesto = "IEPS", Factor = "Tasa", EsTraslado = true });
            list.Add(new TasaOCuota() { TasaCuota = (decimal)0, Impuesto = "IEPS", Factor = "Tasa", EsTraslado = true });
            list.Add(new TasaOCuota() { TasaCuota = (decimal)43.77, Impuesto = "IEPS", Factor = "Cuota", EsTraslado = true });
            list.Add(new TasaOCuota() { TasaCuota = (decimal)0.35, Impuesto = "ISR", Factor = "Tasa", EsTraslado = false });
            return list;
        }
    }
}
