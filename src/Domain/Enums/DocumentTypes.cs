using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    /// <summary>
    /// Tipos de documento de identidad válidos en Colombia:
    /// CC = Cédula de Ciudadanía,
    /// TI = Tarjeta de Identidad,
    /// CE = Cédula de Extranjería,
    /// PA = Pasaporte,
    /// RC = Registro Civil,
    /// NIT = Número de Identificación Tributaria,
    /// PEP = Permiso Especial de Permanencia,
    /// SC = Salvoconducto de Permanencia,
    /// DIM = Documento de Identificación de Menor no nacional,
    /// TEMP = Documento Temporal por trámite.
    /// </summary>
    public enum DocumentType
    {
        CC = '0',
        TI = '1',
        CE = '2',
        PA = '3',
        RC = '4',
        NIT = '5',
        PEP = '6',
        SC = '7',
        DIM = '8',
        TEMP = '9'
    }
}
