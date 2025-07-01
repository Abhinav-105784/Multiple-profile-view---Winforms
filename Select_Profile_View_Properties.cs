using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Aec.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using ObjectId = Autodesk.AutoCAD.DatabaseServices.ObjectId;

namespace multiple_Profile_views
{
    public partial class Select_Profile_View_Properties : Form
    {
        public Select_Profile_View_Properties()
        {
            InitializeComponent();
            PVName.Text = "Profile_View";
            P_P_L_S.Text = "None";
            P_P_S.Text = "None";
        }
        public ObjectId[] SelectedSurfaces { get; set; }
        public SelectionSet selectedAlignments { get; set; }
        public ComboBox E_P_L_S_ComboBox => E_P_L_S;
        public ComboBox E_P_S_ComboBox => E_P_S;
        public ComboBox P_P_L_S_ComboBox => P_P_L_S;
        public ComboBox P_P_S_ComboBox => P_P_S;
        public ComboBox PV_Style_ComboBox => PV_Style;
        public ComboBox PV_Style1_Combobox => ProfileViewStyle1;

        string pvStyle1 = "";
        string existingProfileStyle = "";
        string proposedProfileStyle = "";
        string existingProfileLabelStyle = "";
        string proposedProfileLabelStyle = "";
        string pvStyle = "";
        string pvName = "";

        private void E_P_L_S_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void E_P_S_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void P_P_L_S_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void P_P_S_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PV_Style_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PVName_TextChanged(object sender, EventArgs e)
        {

        }

        private void Open_Click(object sender, EventArgs e)
        {
            pvStyle1 = ProfileViewStyle1.Text;
            existingProfileStyle= E_P_S.Text;
            existingProfileLabelStyle = E_P_L_S.Text;
            proposedProfileStyle = P_P_S.Text;
            proposedProfileLabelStyle = P_P_L_S.Text;
            pvStyle = PV_Style.Text;
            pvName = PVName.Text;
            ObjectId ppls = ObjectId.Null, pps = ObjectId.Null;
            // ObjectId layerId = ReturnNames.LayerList2(layer);
            ObjectId profileViewStyle1 = ReturnNames.PrfoileViewStyleIdName1(pvStyle1);
            ObjectId eps = ReturnNames.PrfoileStyleIdName(existingProfileStyle);
            if (proposedProfileStyle != "None")
            {
                pps = ReturnNames.PrfoileStyleIdName(proposedProfileStyle);
            }
            ObjectId epls = ReturnNames.LabelSetIdName(existingProfileLabelStyle);
            if (proposedProfileLabelStyle != "None")
            {
                ppls = ReturnNames.LabelSetIdName(proposedProfileLabelStyle);
            }
            ObjectId pvBs = ReturnNames.PrfoileViewStyleIdName(pvStyle);
            MainClass.Create(SelectedSurfaces, selectedAlignments, pvBs, profileViewStyle1, ppls, epls, eps, pps, pvName);
            Close();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ProfileViewLayer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
