using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using AlibreX;
using com.alibre.topology.server;

namespace Bolsover.FaceArea
{
    public partial class FaceArea : UserControl
    {
        
        private IADSession _session;
        private IADFace _face;
        public FaceArea(IADSession session)
        {
            _session = session;
            InitializeComponent();
        }
        
        private void GetAreaOfFace(IADFace face)
        {
            try
            {
                var targetProxy = face;
                areaTextBox.Text = GetArea(face).ToString("0.0000");
            }
            catch (Exception ex)
            {
                areaTextBox.Text = "Error: no face detected";
                Debug.WriteLine(ex.ToString());
            }
        }
        
        public double GetArea(IADFace face)
        {
            List<Vector3D> vector3DList = new List<Vector3D>();
            for (int i = 0; i < face.Vertices.Count; i++)
            {
                IADVertex vertex = face.Vertices.Item(i);
                vector3DList.Add(new Vector3D(vertex.Point.X, vertex.Point.Y, vertex.Point.Z));
            }
            
            // foreach (Vertex vertex in face.Vertices)
            //     vector3DList.Add(new Vector3D(vertex.getPoint().x, vertex.getPoint().y, vertex.getPoint().z));
            int count = vector3DList.Count;
            Vector3D vector3D = Vector3D.CrossProduct(vector3DList[count - 1], vector3DList[0]);
            for (int index = 1; index < count; ++index)
                vector3D += Vector3D.CrossProduct(vector3DList[index - 1], vector3DList[index]);
            return vector3D.Length / 2.0;
        }
        
        public IADFace Face
        {
            get => _face;
            set
            {
                _face = value;
                faceTextBox.Text = _face.AppearanceID;
                GetAreaOfFace(_face);
            }
        }
    }
}