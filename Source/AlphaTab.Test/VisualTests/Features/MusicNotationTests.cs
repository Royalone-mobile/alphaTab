﻿
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlphaTab.Test.VisualTests.Features
{
    [TestClass]
    public class MusicNotationTests : VisualTestBase
    {
        [TestMethod, AsyncTestMethod]
        public void Clefs()
        {
            var settings = Settings.Defaults;
            settings.Staves.Id = "score";
            settings.Layout.Mode = "page";
            settings.Layout.AdditionalSettings["hideInfo"] = true;
            var tex = @"\title ""Clefs"" . \clef G2 | \clef F4 | \clef C3 | \clef C4";
            RunVisualTestTex(settings, new[] { 0 }, tex, "TestFiles/VisualTests/Features/MusicNotation/Clefs.png");
        }

        [TestMethod, AsyncTestMethod]
        public void KeySignatures()
        {
            var settings = Settings.Defaults;
            settings.Staves.Id = "score";
            RunVisualTest(settings, new[] { 0 }, "TestFiles/Docs/features/KeySignatures.gp5", "TestFiles/VisualTests/Features/MusicNotation/KeySignatures.png");
        }


        [TestMethod, AsyncTestMethod]
        public void TimeSignatures()
        {
            var settings = Settings.Defaults;
            settings.Staves.Id = "score";
            RunVisualTest(settings, new[] { 0 }, "TestFiles/Docs/features/TimeSignatures.gp5", "TestFiles/VisualTests/Features/MusicNotation/TimeSignatures.png");
        }

        [TestMethod, AsyncTestMethod]
        public void NotesRestsBeams()
        {
            var settings = Settings.Defaults;
            settings.Staves.Id = "score";
            RunVisualTest(settings, new[] { 0 }, "TestFiles/Docs/features/Beams.gp5", "TestFiles/VisualTests/Features/MusicNotation/NotesRestsBeams.png");
        }

        [TestMethod, AsyncTestMethod]
        public void Accidentals()
        {
            var settings = Settings.Defaults;
            settings.Staves.Id = "score";
            RunVisualTest(settings, new[] { 0 }, "TestFiles/Docs/features/Accidentals.gp5", "TestFiles/VisualTests/Features/MusicNotation/Accidentals.png");
        }


    }
}
