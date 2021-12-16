namespace ReasignAddressesCAP32
{
    public class PrimeSignalInfo
    {
        public const int TotalSignals = 41;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Units { get; set; }
        public string DataType { get; set; }

        public PrimeSignalInfo()
        {
        }

        public static PrimeSignalInfo GetInfoById(int id)
        {
            PrimeSignalInfo result = new PrimeSignalInfo { Id = id };
            switch (id)
            {
                case 1:
                    result.Name = "AI.TensionFase1";
                    result.Description = "AI.Tension Fase 1";
                    result.Units = "V";
                    result.DataType = "Float32";
                    break;
                case 2:
                    result.Name = "AI.CorrienteFase1";
                    result.Description = "AI.Corriente Fase 1";
                    result.Units = "A";
                    result.DataType = "Float32";
                    break;
                case 3:
                    result.Name = "AI.PotenciaActivaImportada";
                    result.Description = "AI.Potencia Activa Importada";
                    result.Units = "W";
                    result.DataType = "Float32";
                    break;
                case 4:
                    result.Name = "AI.PotenciaActivaExportada";
                    result.Description = "AI.Potencia Activa Exportada";
                    result.Units = "W";
                    result.DataType = "Float32";
                    break;
                case 5:
                    result.Name = "AI.PotenciaReactivaImportada";
                    result.Description = "AI.Potencia Reactiva Importada";
                    result.Units = "VAR";
                    result.DataType = "Float32";
                    break;
                case 6:
                    result.Name = "AI.PotenciaReactivaExportada";
                    result.Description = "AI.Potencia Reactiva Exportada";
                    result.Units = "VAR";
                    result.DataType = "Float32";
                    break;
                case 7:
                    result.Name = "AI.FactorPotencia";
                    result.Description = "AI.Factor Potencia";
                    result.Units = null;
                    result.DataType = "Float32";
                    break;
                case 8:
                    result.Name = "AI.Cuadrante";
                    result.Description = "AI.Cuadrante";
                    result.Units = null;
                    result.DataType = "Int32";
                    break;
                case 9:
                    result.Name = "AI.FaseContador";
                    result.Description = "AI.Fase Contador";
                    result.Units = null;
                    result.DataType = "Int32";
                    break;
                case 10:
                    result.Name = "AI.EstadoActualSwitch";
                    result.Description = "AI.Estado Actual Switch";
                    result.Units = null;
                    result.DataType = "Int32";
                    break;
                case 11:
                    result.Name = "AI.EstadoAnteriorSwitch";
                    result.Description = "AI.Estado Anterior Switch";
                    result.Units = null;
                    result.DataType = "Int32";
                    break;
                case 12:
                    result.Name = "AI.ActivaImportada";
                    result.Description = "AI.Activa Importada";
                    result.Units = "kWh";
                    result.DataType = "Float32";
                    break;
                case 13:
                    result.Name = "AI.ActivaExportada";
                    result.Description = "AI.Activa Exportada";
                    result.Units = "kWh";
                    result.DataType = "Float32";
                    break;
                case 14:
                    result.Name = "AI.ReactivaQ1";
                    result.Description = "AI.Reactiva Q1";
                    result.Units = "kVARh";
                    result.DataType = "Float32";
                    break;
                case 15:
                    result.Name = "AI.ReactivaQ2";
                    result.Description = "AI.Reactiva Q2";
                    result.Units = "kVARh";
                    result.DataType = "Float32";
                    break;
                case 16:
                    result.Name = "AI.ReactivaQ3";
                    result.Description = "AI.Reactiva Q3";
                    result.Units = "kVARh";
                    result.DataType = "Float32";
                    break;
                case 17:
                    result.Name = "AI.ReactivaQ4";
                    result.Description = "AI.Reactiva Q4";
                    result.Units = "kVARh";
                    result.DataType = "Float32";
                    break;
                case 18:
                    result.Name = "AI.FasePresente";
                    result.Description = "AI.Fase Presente";
                    result.Units = null;
                    result.DataType = "String";
                    break;
                case 19:
                    result.Name = "AI.CorrienteSuma3Fases";
                    result.Description = "AI.Corriente suma 3 Fases";
                    result.Units = "A";
                    result.DataType = "Float32";
                    break;
                case 20:
                    result.Name = "AI.PotenciaActivaImportadaFase1";
                    result.Description = "AI.Potencia Activa Importada Fase 1";
                    result.Units = "W";
                    result.DataType = "Float32";
                    break;
                case 21:
                    result.Name = "AI.PotenciaActiveExportedFase1";
                    result.Description = "AI.Potencia Activa Exportada Fase 1";
                    result.Units = "W";
                    result.DataType = "Float32";
                    break;
                case 22:
                    result.Name = "AI.PotenciaReactivaImportadaFase1";
                    result.Description = "AI.Potencia Reactiva Importada Fase 1";
                    result.Units = "VAR";
                    result.DataType = "Float32";
                    break;
                case 23:
                    result.Name = "AI.PotenciaReactivaExportadaFase1";
                    result.Description = "I.Potencia Reactiva Exportada Fase 1";
                    result.Units = "VAR";
                    result.DataType = "Float32";
                    break;
                case 24:
                    result.Name = "AI.FactorPotenciaFase1";
                    result.Description = "AI.Factor Potencia Fase 1";
                    result.Units = null;
                    result.DataType = "Float32";
                    break;
                case 25:
                    result.Name = "AI.CuadranteFase1";
                    result.Description = "AI.Cuadrante Fase 1";
                    result.Units = null;
                    result.DataType = "Int32";
                    break;
                case 26:
                    result.Name = "AI.TensionFase2";
                    result.Description = "AI.Tension Fase 2";
                    result.Units = "V";
                    result.DataType = "Float32";
                    break;
                case 27:
                    result.Name = "AI.CorrienteFase2";
                    result.Description = "AI.Corriente Fase 2";
                    result.Units = "A";
                    result.DataType = "Float32";
                    break;
                case 28:
                    result.Name = "AI.PotenciaActivaImportadaFase2";
                    result.Description = "AI.Potencia Activa Importada Fase 2";
                    result.Units = "W";
                    result.DataType = "Float32";
                    break;
                case 29:
                    result.Name = "AI.PotenciaActiveExportedFase2";
                    result.Description = "AI.Potencia Active Exported Fase 2";
                    result.Units = "W";
                    result.DataType = "Float32";
                    break;
                case 30:
                    result.Name = "AI.PotenciaReactivaImportadaFase2";
                    result.Description = "AI.Potencia Reactiva Importada Fase 2";
                    result.Units = "VAR";
                    result.DataType = "Float32";
                    break;
                case 31:
                    result.Name = "AI.PotenciaReactivaExportadaFase2";
                    result.Description = "AI.Potencia Reactiva Exportada Fase 2";
                    result.Units = "VAR";
                    result.DataType = "Float32";
                    break;
                case 32:
                    result.Name = "AI.FactorPotenciaFase2";
                    result.Description = "AI.Factor Potencia Fase 2";
                    result.Units = null;
                    result.DataType = "Float32";
                    break;
                case 33:
                    result.Name = "AI.CuadranteFase2";
                    result.Description = "AI.Cuadrante Fase 2";
                    result.Units = null;
                    result.DataType = "Int32";
                    break;
                case 34:
                    result.Name = "AI.TensionFase3";
                    result.Description = "AI.Tension Fase 3";
                    result.Units = null;
                    result.DataType = "Float32";
                    break;
                case 35:
                    result.Name = "AI.CorrienteFase3";
                    result.Description = "AI.Corriente Fase 3";
                    result.Units = null;
                    result.DataType = "Float32";
                    break;
                case 36:
                    result.Name = "AI.PotenciaActivaImportadaFase3";
                    result.Description = "AI.Potencia Activa Importada Fase 3";
                    result.Units = "W";
                    result.DataType = "Float32";
                    break;
                case 37:
                    result.Name = "AI.PotenciaActiveExportedFase3";
                    result.Description = "AI.Potencia Active Exported Fase 3";
                    result.Units = "W";
                    result.DataType = "Float32";
                    break;
                case 38:
                    result.Name = "AI.PotenciaReactivaImportadaFase3";
                    result.Description = "AI.Potencia Reactiva Importada Fase 3";
                    result.Units = "VAR";
                    result.DataType = "Float32";
                    break;
                case 39:
                    result.Name = "AI.PotenciaReactivaExportadaFase3";
                    result.Description = "AI.Potencia Reactiva Exportada Fase 3";
                    result.Units = "VAR";
                    result.DataType = "Float32";
                    break;
                case 40:
                    result.Name = "AI.FactorPotenciaFase3";
                    result.Description = "AI.Factor Potencia Fase 3";
                    result.Units = null;
                    result.DataType = "Float32";
                    break;
                case 41:
                    result.Name = "AI.CuadranteFase3";
                    result.Description = "AI.Cuadrante Fase 3";
                    result.Units = null;
                    result.DataType = "Int32";
                    break;
                default:
                    // Unknown type with this id
                    result = null;
                    break;
            }
            return result;
        }
    }
}
