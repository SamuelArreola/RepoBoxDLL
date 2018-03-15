using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoBox.SAT
{
    public class Funciones
    {
        public decimal Redondeo(decimal x, bool Min)
        {
            try
            {
                int decimales = (x.ToString().Split('.').Length > 1 ? x.ToString().Split('.')[1].Length : 0);
                if (decimales <= 2)
                    decimales = 2;
                if(Min)
                    return (x - (decimal)Math.Pow(10, -(decimales)) / (decimal)2);
                else
                    return (x + (decimal)Math.Pow(10, -(decimales)) / (decimal)2 - (decimal)Math.Pow(10, -12));
                //decimal valorfinal = (min + max) / 2;
                //decimal final = Convert.ToDecimal(valorfinal.ToString().Split('.')[0] + "." +
                //    (valorfinal.ToString().Split('.')[1].Trim().Length > 2 ? valorfinal.ToString().Split('.')[1].Trim().Substring(0, 2) :
                //    valorfinal.ToString().Split('.')[1].Trim().PadRight(2, '0')));
                //return final;
            }
            catch (Exception)
            { return x; }
        }

        public bool Importes(decimal cantidad, decimal valorUnitario, decimal importe)
        {
            try
            {
                decimal min = Redondeo(cantidad, true) * Redondeo(valorUnitario, true);
                decimal max = Redondeo(cantidad, false) * Redondeo(valorUnitario, false);
                return (min <= importe && importe <= max); 
            }
            catch (Exception)
            { return false; }
        }
        public bool Impuestos(decimal importeBase, decimal tasaoCuota, decimal importe)
        {
            try
            {
                int decimales = (importeBase.ToString().Split('.').Length > 1 ? importeBase.ToString().Split('.')[1].Length : 0);
                if (decimales <= 2)
                    decimales = 2;
                decimal min = (importeBase - (decimal)Math.Pow(10, -(decimales)) / (decimal)2) * (tasaoCuota);
                //min = Convert.ToDecimal(min.ToString().Split('.')[0] + "." +
                //    (min.ToString().Split('.')[1].Trim().Length > decimales ? min.ToString().Split('.')[1].Trim().Substring(0, decimales) :
                //    min.ToString().Split('.')[1].Trim().PadRight(decimales, '0')));
                decimal max = (importeBase + (decimal)Math.Pow(10, -(decimales)) / (decimal)2 - (decimal)Math.Pow(10, -12)) * (tasaoCuota);
                //max = Convert.ToDecimal(max.ToString().Split('.')[0] + "." +
                //    (max.ToString().Split('.')[1].Trim().Length > decimales ? max.ToString().Split('.')[1].Trim().Substring(0, decimales) :
                //    max.ToString().Split('.')[1].Trim().PadRight(decimales, '0'))) + (Convert.ToDecimal("0.".PadRight(decimales)+;
                if (decimales < 2)
                    decimales = 2;
                string ent = min.ToString().Split('.')[0];
                string residuo = (min.ToString().Split('.').Length > 1 ? min.ToString().Split('.')[1] : "");
                residuo = residuo.PadRight(decimales, '0');
                min = Convert.ToDecimal(ent + "." + residuo.Substring(0, decimales));
                // Max
                ent = max.ToString().Split('.')[0];
                residuo = (max.ToString().Split('.').Length > 1 ? max.ToString().Split('.')[1] : "");
                residuo = residuo.PadRight(decimales, '0');
                residuo = residuo.Substring(0, decimales);
                if (int.Parse(residuo) + 1 > 9)
                    residuo = (int.Parse(residuo) + 1).ToString().PadRight(decimales, '0');
                else
                    residuo = (int.Parse(residuo) + 1).ToString().PadLeft(decimales, '0');
                if (Convert.ToInt32(residuo) >= 100)
                {
                    ent = (Convert.ToInt32(ent) + 1).ToString();
                    residuo = residuo.Substring(1, 2);
                }
                max = Convert.ToDecimal(ent + "." + residuo.Substring(0, decimales));
                return (min <= importe && importe <= max);
            }
            catch (Exception)
            { return false; }
        }
    }
}
