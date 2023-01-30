namespace PolygonEditor
{
    public partial class EdgeLength : Form
    {
        public string _edgeLength = "";
        public string outcome = "";

        public EdgeLength()
        {
            InitializeComponent();
        }
        private void EdgeLength_Load(object sender, EventArgs e)
        {
            edgeLengthTextBox.Text = _edgeLength;
        }
        private void OKbutton_Click(object sender, EventArgs e)
        {
            outcome = edgeLengthTextBox.Text;
            this.Close();
        }
    }
}
