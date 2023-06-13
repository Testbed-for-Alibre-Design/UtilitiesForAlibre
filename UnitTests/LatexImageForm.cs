﻿using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using Bolsover.Gear.Images;

namespace UnitTests
{
    public class LatexImageForm : Form
    {
        private IContainer components = null;
        private FieldInfo[] props;
        private int i = 0;
        GearInternalExternalLatexFormulae instance = new GearInternalExternalLatexFormulae();

        public LatexImageForm()
        {
            InitializeComponent();
            props = typeof(GearInternalExternalLatexFormulae).GetFields();
            latexLabel.Text = "Click next to check Latex formulae";
        }


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.latexLabel = new System.Windows.Forms.Label();
            this.nextbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // latexLabel
            // 
            this.latexLabel.Location = new System.Drawing.Point(86, 80);
            this.latexLabel.Name = "latexLabel";
            this.latexLabel.Size = new System.Drawing.Size(268, 138);
            this.latexLabel.TabIndex = 0;
            // 
            // nextbutton
            // 
            this.nextbutton.Location = new System.Drawing.Point(124, 259);
            this.nextbutton.Name = "nextbutton";
            this.nextbutton.Size = new System.Drawing.Size(75, 23);
            this.nextbutton.TabIndex = 1;
            this.nextbutton.Text = "Next";
            this.nextbutton.UseVisualStyleBackColor = true;
            this.nextbutton.Click += new System.EventHandler(this.nextbutton_Click);
            // 
            // LatexImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.nextbutton);
            this.Controls.Add(this.latexLabel);
            this.Name = "LatexImageForm";
            this.Text = "GearsForm";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button nextbutton;


        public System.Windows.Forms.Label latexLabel;

        private void nextbutton_Click(object sender, EventArgs e)
        {
            if (i < props.Length)
            {
                FieldInfo info = props[i++];
                var value = info.Name;
                var property = instance.GetType().GetField(value).GetValue(instance);

                if (property != null)
                {
                    latexLabel.Text = value;
                    latexLabel.Image = GearInternalExternalLatexFormulae.CreateImageFromLatex(property.ToString());
                }
            }
            else
            {
                latexLabel.Text = "DONE!!";
                latexLabel.Image = GearInternalExternalLatexFormulae.CreateImageFromLatex("All Done!!");
            }
        }
    }
}