using System;
using Bolsover.Utils;
using NUnit.Framework;

namespace UnitTests.Utils
{
    [TestFixture]
    public class ConversionUtilsTests
    {
        [Test]
        public void RadiansTest()
        {
            double degrees = 90.0;
            double expected = Math.PI / 2;
            double actual = ConversionUtils.Radians(degrees);
            Assert.AreEqual(expected, actual, 1e-6);
        }

        [Test]
        public void DegreesTest()
        {
            double radians = Math.PI / 2;
            double expected = 90.0;
            double actual = ConversionUtils.Degrees(radians);
            Assert.AreEqual(expected, actual, 1e-6);
        }

        [Test]
        public void MillimetersToInchesTest()
        {
            double mm = 25.4;
            double expected = 1;
            double actual = ConversionUtils.MillimetersToInches(mm);
            Assert.AreEqual(expected, actual, 1e-6);
        }

        [Test]
        public void InchesToMillimetersTest()
        {
            double inches = 1;
            double expected = 25.4;
            double actual = ConversionUtils.InchesToMillimeters(inches);
            Assert.AreEqual(expected, actual, 1e-6);
        }

        [Test]
        public void ModuleToDiametralPitchTest()
        {
            double module = 1;
            double expected = 25.4;
            double actual = ConversionUtils.ModuleToDiametralPitch(module);
            Assert.AreEqual(expected, actual, 1e-6);
        }

        [Test]
        public void DiametralPitchToModuleTest()
        {
            double diametralPitch = 25.4;
            double expected = 1;
            double actual = ConversionUtils.DiametralPitchToModule(diametralPitch);
            Assert.AreEqual(expected, actual, 1e-6);
        }

        [Test]
        public void PitchMillimetersToModuleTest()
        {
            double pitchMillimeters = Math.PI;
            double expected = 1;
            double actual = ConversionUtils.PitchMillimetersToModule(pitchMillimeters);
            Assert.AreEqual(expected, actual, 1e-6);
        }

        [Test]
        public void ModuleToPitchInchesTest()
        {
            double module = 1;
            double expected = Math.PI / 25.4;
            double actual = ConversionUtils.ModuleToPitchInches(module);
            Assert.AreEqual(expected, actual, 1e-6);
        }

        [Test]
        public void PitchInchesToModuleTest()
        {
            double pitchInches = Math.PI / 25.4;
            double expected = 1;
            double actual = ConversionUtils.PitchInchesToModule(pitchInches);
            Assert.AreEqual(expected, actual, 1e-6);
        }

        [Test]
        public void ModuleToCircularPitchTest()
        {
            double module = 1;
            double expected = Math.PI;
            double actual = ConversionUtils.ModuleToCircularPitch(module);
            Assert.AreEqual(expected, actual, 1e-6);
        }

        [Test] public void NormalModuleToRadialModuleTest()
        {
            double axialModule = 1;
            double helixAngle = ConversionUtils.Radians(20);
            double expected = axialModule / Math.Sin(helixAngle);
            double actual = ConversionUtils. WormNormalModuleToRadialModule(axialModule, helixAngle);
            Assert.AreEqual(expected, actual, 1e-6);
        }
        
        [Test] public void RadialModuleToNormalModuleTest()
        {
            double axialModule = 1;
            double helixAngle = ConversionUtils.Radians(20);
            double expected = axialModule * Math.Sin(helixAngle);
            double actual = ConversionUtils. WormRadialModuleToNormalModule(axialModule, helixAngle);
            Assert.AreEqual(expected, actual, 1e-6);
        }
        
        [Test] public void NormalModuleToAxialModuleTest()
        {
            double axialModule = 1;
            double helixAngle = ConversionUtils.Radians(20);
            double expected = axialModule / Math.Cos(helixAngle);
            //double expected = 1;
            double actual = ConversionUtils. WormNormalModuleToAxialModule(axialModule, helixAngle);
            Assert.AreEqual(expected, actual, 1e-6);
        }
        
        [Test] public void AxialModuleToNormalModuleTest()
        {
            double axialModule = 1;
            double helixAngle = ConversionUtils.Radians(20);
          double expected = axialModule * Math.Cos(helixAngle);
            //double expected = 1;
            double actual = ConversionUtils. WormAxialModuleToNormalModule(axialModule, helixAngle);
            Assert.AreEqual(expected, actual, 1e-6);
        }

        [Test]
        public void RadialModuleToAxialModuleTest()
        {
            double axialModule = 1;
            double helixAngle = ConversionUtils.Radians(20);
            double expected = 0.36397023;
            double actual = ConversionUtils. WormRadialModuleToAxialModule(axialModule, helixAngle);
            Assert.AreEqual(expected, actual, 1e-6);
        }
        
        [Test]
        public void AxialModuleToRadialModuleTest()
        {
            double axialModule = 1;
            double helixAngle = ConversionUtils.Radians(20);
            double expected = 2.747477 ;
            double actual = ConversionUtils.WormAxialModuleToRadialModule(axialModule, helixAngle);
            Assert.AreEqual(expected, actual, 1e-6);
        }
    }
}