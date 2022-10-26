using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;



/*
 *	LB2 Photos
 *
 *	Write a GUI application for a photography business:
 *
 *	●	The cost of all 8"  by 10" photos is $3.99
 *
 *	●	The cost of all 10" by 12" photos is $5.99
 *
 *	●	The cost of any other photo size  is $9.99
 *
 *	●	Customers can optionally add a Matte or a Frame.
 *
 *	●	Matted photos are an additional $10
 *
 *	●	Framed photos are an additional $25
 *
 *	●	You are to override the ToString() methods on
 *		both the Photo, MattedPhoto, and FramedPhoto 
 *		classes so that they display all of the 
 *		information in the fields.	
 */

namespace LB2_Photos
{
    public partial class frmPhotos : Form
    {
        /*
         *  I am assuming here that the biggest photo
         *  possible will be 6 feet by 6 feet, or in
         *  this program, 72 inches by 72 inches.
         */
        //  Global constants
        const int MINWIDTH  = 8;
        const int MINHEIGHT = 10;
        const int MAXWIDTH  = 72;
        const int MAXHEIGHT = 72;

        const decimal EIGHTXTEN     =  3.99M;
        const decimal TENXTWELVE    =  5.99M;
        const decimal OTHERSIZES    =  9.99M;
        const decimal UNFRAMED      =  0.00M;
        const decimal MATTED        = 10.00M;
        const decimal FRAMED        = 25.00M;

        //  Global variables
        decimal width       = 0.00M;
        decimal height      = 0.00M;
        decimal totalCost   = 0.00M;

        RadioButton radioButtonFrame;
        RadioButton radioButtonColor;
        RadioButton radioButtonMaterial;
        RadioButton radioButtonStyle;
        string frameStr;
        string colorStr;
        string materialStr;
        string styleStr;

        public frmPhotos()
        {
            InitializeComponent();

            // Set up framed groupBox radioButtons
            radUnframed.CheckedChanged  += new EventHandler(framed_Check_Changed);
            radMatted.CheckedChanged    += new EventHandler(framed_Check_Changed);
            radFramed.CheckedChanged    += new EventHandler(framed_Check_Changed);

            // Set up color groupBox radioButtons
            radBlack.CheckedChanged     += new EventHandler(color_Check_Changed);
            radRed.CheckedChanged       += new EventHandler(color_Check_Changed);
            radGreen.CheckedChanged     += new EventHandler(color_Check_Changed);
            radBlue.CheckedChanged      += new EventHandler(color_Check_Changed);
            radWhite.CheckedChanged     += new EventHandler(color_Check_Changed);

            // Set up material groupBox radioButtons
            radPine.CheckedChanged      += new EventHandler(material_Check_Changed);
            radOak.CheckedChanged       += new EventHandler(material_Check_Changed);
            radSteel.CheckedChanged     += new EventHandler(material_Check_Changed);
            radSilver.CheckedChanged    += new EventHandler(material_Check_Changed);
            radGold.CheckedChanged      += new EventHandler(material_Check_Changed);

            // Set up style groupBox radioButtons
            radSimple.CheckedChanged    += new EventHandler(style_Check_Changed);
            radModern.CheckedChanged    += new EventHandler(style_Check_Changed);
            radAntique.CheckedChanged   += new EventHandler(style_Check_Changed);
            radVintage.CheckedChanged   += new EventHandler(style_Check_Changed);
            radEclectic.CheckedChanged  += new EventHandler(style_Check_Changed);

        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            //  Use validator.cs class to validate width and height
            bool keepGoing = IsValidData();

            if (keepGoing)
            {
                width  = Convert.ToDecimal(txtWidth.Text);
                height = Convert.ToDecimal(txtHeight.Text);

                //  Valid values for width and height
                CalculateCost();
            }
        }

        private bool IsValidData()
        {
            bool success  = true;
            string errMsg = "";

            //	width and height Presence Validation
            errMsg += Validator.IsPresent(txtWidth.Text, txtWidth.Tag.ToString());
            errMsg += Validator.IsPresent(txtHeight.Text, txtHeight.Tag.ToString());

            //	width and height Numeric Validation
            errMsg += Validator.IsInt32(txtWidth.Text, txtWidth.Tag.ToString());
            errMsg += Validator.IsInt32(txtHeight.Text, txtHeight.Tag.ToString());

            //	width and height Range Validation
            errMsg += Validator.IsWithinRange(txtWidth.Text, txtWidth.Tag.ToString(), MINWIDTH, MAXWIDTH);
            errMsg += Validator.IsWithinRange(txtHeight.Text, txtHeight.Tag.ToString(), MINHEIGHT, MAXHEIGHT);

            if (errMsg != "")
            {
                success = false;
                MessageBox.Show(errMsg, "YOU HAVE ERRORS!");
            }

            return success;
        }

        //  Executes each time frame groupbox radio button changes
        private void framed_Check_Changed(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                radioButtonFrame = (RadioButton)sender;
                frameStr = radioButtonFrame.Name.Substring(3);

                switch (frameStr)
                {
                    case "Unframed":
                        gbColor.Enabled = false;
                        gbMaterial.Enabled = false;
                        gbStyle.Enabled = false;
                        break;

                    case "Matted":
                        gbColor.Enabled = true;
                        radBlack.Checked = true;
                        gbMaterial.Enabled = false;
                        gbStyle.Enabled = false;
                        break;

                    case "Framed":
                        gbColor.Enabled = false;
                        gbMaterial.Enabled = true;
                        radPine.Enabled = true;
                        gbStyle.Enabled = true;
                        radSimple.Enabled = true;
                        break;

                    default:
                        gbColor.Enabled = false;
                        gbMaterial.Enabled = false;
                        gbStyle.Enabled = false;
                        break;
                }
            }
        }

        //  Executes each time color groupbox radio button changes
        private void color_Check_Changed(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                radioButtonColor = (RadioButton)sender;
                colorStr = radioButtonColor.Name.Substring(3);
            }
        }

        //  Executes each time material groupbox radio button changes
        private void material_Check_Changed(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                radioButtonMaterial = (RadioButton)sender;
                materialStr = radioButtonMaterial.Name.Substring(3);
            }
        }

        //  Executes each time style groupbox radio button changes
        private void style_Check_Changed(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                radioButtonStyle = (RadioButton)sender;
                styleStr = radioButtonStyle.Name.Substring(3);
            }
        }

        private void CalculateCost()
        {
            decimal areaCost  = CalculateAreaCost();
            decimal frameCost = CalculateFrameCost();
            totalCost = areaCost + frameCost;

            lblTotalCost.Text = "Total Cost: " + totalCost.ToString("c");
        }

        private decimal CalculateAreaCost()
        {
            decimal area = width * height;
            decimal ac = 0.00M;

            switch (area)
            {
                case 80.0M:
                    ac = EIGHTXTEN;
                    break;

                case 120.0M:
                    ac = TENXTWELVE;
                    break;

                default:
                    ac = OTHERSIZES;
                    break;
            }

            return ac;
        }

        private decimal CalculateFrameCost()
        {
            decimal fc = 0.0M;

            switch (radioButtonFrame.Name.Substring(3))
            {
                case "Unframed":
                    fc = UNFRAMED;
                    Photo p = new Photo(width, height);
                    lblPictureStats.Text = p.ToString();
                    break;

                case "Matted":
                    fc = MATTED;
                    MattedPhoto.Color colorEnum = (MattedPhoto.Color)Enum.Parse(
                                                typeof(MattedPhoto.Color), radioButtonColor.Text.ToUpper());
                    MattedPhoto mp = new MattedPhoto(width, height, colorEnum);
                    lblPictureStats.Text = mp.ToString();
                    break;

                case "Framed":
                    fc = FRAMED;
                    FramedPhoto.Material materialEnum = (FramedPhoto.Material)Enum.Parse(typeof(FramedPhoto.Material), radioButtonMaterial.Text.ToUpper());
                    FramedPhoto.Style styleEnum = (FramedPhoto.Style)Enum.Parse(typeof(FramedPhoto.Style), radioButtonStyle.Text.ToUpper());
                    FramedPhoto fp = new FramedPhoto(width, height, materialEnum, styleEnum);
                    lblPictureStats.Text = fp.ToString();
                    break;

                default:
                    break;
            }

            return fc;
        }



        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAndSetFocus();
        }

        private void clearAndSetFocus()
        {
            clearFrame();
            clearColor();
            clearMaterial();
            clearStyle();
            txtWidth.Text = "";
            txtHeight.Text = "";
            txtWidth.Focus();
        }

        private void clearFrame()
        {
            radUnframed.Checked = false;
            radMatted.Checked   = false;
            radFramed.Checked   = false;
        }

        private void clearColor()
        {
            radBlack.Checked    = false;
            radRed.Checked      = false;
            radGreen.Checked    = false;
            radBlue.Checked     = false;
            radWhite.Checked    = false;
        }

        private void clearMaterial()
        {
            radPine.Checked     = false;
            radOak.Checked      = false;
            radSteel.Checked    = false;
            radSilver.Checked   = false;
            radGold.Checked     = false;
        }

        private void clearStyle()
        {
            radSimple.Checked   = false;
            radModern.Checked   = false;
            radAntique.Checked  = false;
            radVintage.Checked  = false;
            radEclectic.Checked = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            exitProgramOrNot();
        }

        private void exitProgramOrNot()
        {
            DialogResult dialog = MessageBox.Show(
                            "Do You Really Want To Exit?",
                            "EXIT NOW?",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
