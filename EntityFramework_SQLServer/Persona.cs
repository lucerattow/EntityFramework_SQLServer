//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityFramework_SQLServer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Persona
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public Nullable<int> edad { get; set; }
        public Nullable<int> genero_id { get; set; }
    
        public virtual Persona_Genero Persona_Genero { get; set; }
    }
}
