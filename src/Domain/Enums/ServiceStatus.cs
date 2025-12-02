using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum ServiceStatus 
    {
        Creado = 'C',           
        Acordado = 'A',         
        PagoPendiente = 'P',    
        Programado = 'R',       
        EnCurso = 'E',          
        Completado = 'F',       
        Cancelado = 'X'         
    }
}
