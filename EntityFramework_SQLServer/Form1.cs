using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFramework_SQLServer
{
    public partial class Form1 : Form
    {
        int? selected_id = null;

        public Form1()
        {
            InitializeComponent();
            SetupGrd();
            SetupCbGenero();
            GrdGetPersonas();
            logicaFormulario("limpiar");
        }

        //Inicializar el formulario
        private void SetupGrd()
        {
            grd.EditMode = DataGridViewEditMode.EditProgrammatically;

            grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                HeaderText = "id",
                Visible = false
            });
            grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                HeaderText = "nombre"
            });
            grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                HeaderText = "apellido"
            });
            grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                HeaderText = "edad"
            });
            grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                HeaderText = "genero"
            });
        }
        private void SetupCbGenero()
        {
            CbGenero.DisplayMember = "nombre";
            CbGenero.ValueMember = "id";

            using (PruebaEntities db = new PruebaEntities())
            {
                var a = db.Persona_Genero.ToList();
                CbGenero.DataSource = a;
            }
        }

        //Metodos
        private void GrdGetPersonas()
        {
            grd.Rows.Clear();

            using (PruebaEntities db = new PruebaEntities())
            {
                foreach (var item in db.Persona)
                    grd.Rows.Add(item.id, item.nombre, item.apellido, item.edad, item.Persona_Genero.nombre);
            }
        }
        private void logicaFormulario(string accion)
        {
            switch (accion)
            {
                case "editar":
                    BtnCrear.Enabled = false;
                    BtnModificar.Enabled = true;
                    BtnEliminar.Enabled = true;
                    BtnLimpiar.Enabled = true;
                    break;

                case "limpiar":
                    selected_id = null;

                    TxtNombre.Text = "";
                    TxtApellido.Text = "";
                    TxtEdad.Text = "";
                    CbGenero.SelectedIndex = 0;

                    BtnCrear.Enabled = true;
                    BtnModificar.Enabled = false;
                    BtnEliminar.Enabled = false;
                    BtnLimpiar.Enabled = false;
                    break;
            }
        }

        //Eventos
        private void grd_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            using(PruebaEntities db = new PruebaEntities())
            {
                Persona o = db.Persona.Find(grd.Rows[e.RowIndex].Cells[0].Value);

                selected_id = o.id;
                TxtNombre.Text = o.nombre;
                TxtApellido.Text = o.apellido;
                TxtEdad.Text = o.edad.ToString();
                CbGenero.SelectedValue = o.Persona_Genero.id;

                logicaFormulario("editar");
            }
        }
        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            logicaFormulario("limpiar");
        }
        private void BtnCrear_Click(object sender, EventArgs e)
        {
            using (PruebaEntities db = new PruebaEntities())
            {
                Persona o = new Persona();
                o.nombre = TxtNombre.Text;
                o.apellido = TxtApellido.Text;
                o.edad = int.Parse(TxtEdad.Text);
                o.Persona_Genero = db.Persona_Genero.Find(((Persona_Genero)CbGenero.SelectedItem).id);

                db.Persona.Add(o);
                db.SaveChanges();

                GrdGetPersonas();
                logicaFormulario("limpiar");
            }
        }
        private void BtnModificar_Click(object sender, EventArgs e)
        {
            using (PruebaEntities db = new PruebaEntities())
            {
                Persona o = db.Persona.Find(selected_id);
                o.nombre = TxtNombre.Text;
                o.apellido = TxtApellido.Text;
                o.edad = int.Parse(TxtEdad.Text);
                o.Persona_Genero = db.Persona_Genero.Find(((Persona_Genero)CbGenero.SelectedItem).id);

                db.Entry(o).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                GrdGetPersonas();
                logicaFormulario("limpiar");
            }
        }
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            using (PruebaEntities db = new PruebaEntities())
            {
                Persona o = db.Persona.Find(selected_id);

                db.Persona.Remove(o);
                db.SaveChanges();

                GrdGetPersonas();
                logicaFormulario("limpiar");
            }
        }
    }
}
