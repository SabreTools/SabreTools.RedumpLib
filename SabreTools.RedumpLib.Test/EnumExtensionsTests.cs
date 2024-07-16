﻿using System;
using System.Collections.Generic;
using System.Linq;
using SabreTools.RedumpLib.Data;
using Xunit;

namespace SabreTools.RedumpLib.Test
{
    public class EnumExtensionsTests
    {
        /// <summary>
        /// MediaType values that support drive speeds
        /// </summary>
        private static readonly MediaType?[] _supportDriveSpeeds =
        [
            MediaType.CDROM,
            MediaType.DVD,
            MediaType.GDROM,
            MediaType.HDDVD,
            MediaType.BluRay,
            MediaType.NintendoGameCubeGameDisc,
            MediaType.NintendoWiiOpticalDisc,
        ];

        /// <summary>
        /// RedumpSystem values that are considered Audio
        /// </summary>
        private static readonly RedumpSystem?[] _audioSystems =
        [
            RedumpSystem.AtariJaguarCDInteractiveMultimediaSystem,
            RedumpSystem.AudioCD,
            RedumpSystem.DVDAudio,
            RedumpSystem.HasbroiONEducationalGamingSystem,
            RedumpSystem.HasbroVideoNow,
            RedumpSystem.HasbroVideoNowColor,
            RedumpSystem.HasbroVideoNowJr,
            RedumpSystem.HasbroVideoNowXP,
            RedumpSystem.PlayStationGameSharkUpdates,
            RedumpSystem.PhilipsCDi,
            RedumpSystem.SuperAudioCD,
        ];

        /// <summary>
        /// RedumpSystem values that are considered markers
        /// </summary>
        private static readonly RedumpSystem?[] _markerSystems =
        [
            RedumpSystem.MarkerArcadeEnd,
            RedumpSystem.MarkerComputerEnd,
            RedumpSystem.MarkerDiscBasedConsoleEnd,
            RedumpSystem.MarkerOtherEnd,
        ];

        /// <summary>
        /// RedumpSystem values that are have reversed ringcodes
        /// </summary>
        private static readonly RedumpSystem?[] _reverseRingcodeSystems =
        [
            RedumpSystem.SonyPlayStation2,
            RedumpSystem.SonyPlayStation3,
            RedumpSystem.SonyPlayStation4,
            RedumpSystem.SonyPlayStation5,
            RedumpSystem.SonyPlayStationPortable,
        ];

        /// <summary>
        /// RedumpSystem values that are considered XGD
        /// </summary>
        private static readonly RedumpSystem?[] _xgdSystems =
        [
            RedumpSystem.MicrosoftXbox,
            RedumpSystem.MicrosoftXbox360,
            RedumpSystem.MicrosoftXboxOne,
            RedumpSystem.MicrosoftXboxSeriesXS,
        ];

        /// <summary>
        /// Check that all systems with reversed ringcodes are marked properly
        /// </summary>
        /// <param name="redumpSystem">RedumpSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateReversedRingcodeSystemsTestData))]
        public void HasReversedRingcodesTest(RedumpSystem? redumpSystem, bool expected)
        {
            bool actual = redumpSystem.HasReversedRingcodes();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all audio systems are marked properly
        /// </summary>
        /// <param name="redumpSystem">RedumpSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateAudioSystemsTestData))]
        public void IsAudioTest(RedumpSystem? redumpSystem, bool expected)
        {
            bool actual = redumpSystem.IsAudio();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all marker systems are marked properly
        /// </summary>
        /// <param name="redumpSystem">RedumpSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateMarkerSystemsTestData))]
        public void IsMarkerTest(RedumpSystem? redumpSystem, bool expected)
        {
            bool actual = redumpSystem.IsMarker();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Check that all XGD systems are marked properly
        /// </summary>
        /// <param name="redumpSystem">RedumpSystem value to check</param>
        /// <param name="expected">The expected value to come from the check</param>
        [Theory]
        [MemberData(nameof(GenerateXGDSystemsTestData))]
        public void IsXGDTest(RedumpSystem? redumpSystem, bool expected)
        {
            bool actual = redumpSystem.IsXGD();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Generate a test set of RedumpSystem values that are considered Audio
        /// </summary>
        /// <returns>MemberData-compatible list of RedumpSystem values</returns>
        public static List<object?[]> GenerateAudioSystemsTestData()
        {
            var testData = new List<object?[]>() { new object?[] { null, false } };
            foreach (RedumpSystem redumpSystem in Enum.GetValues(typeof(RedumpSystem)))
            {
                if (_audioSystems.Contains(redumpSystem))
                    testData.Add([redumpSystem, true]);
                else
                    testData.Add([redumpSystem, false]);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of RedumpSystem values that are considered markers
        /// </summary>
        /// <returns>MemberData-compatible list of RedumpSystem values</returns>
        public static List<object?[]> GenerateMarkerSystemsTestData()
        {
            var testData = new List<object?[]>() { new object?[] { null, false } };
            foreach (RedumpSystem redumpSystem in Enum.GetValues(typeof(RedumpSystem)))
            {
                if (_markerSystems.Contains(redumpSystem))
                    testData.Add([redumpSystem, true]);
                else
                    testData.Add([redumpSystem, false]);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of RedumpSystem values that are considered markers
        /// </summary>
        /// <returns>MemberData-compatible list of RedumpSystem values</returns>
        public static List<object?[]> GenerateReversedRingcodeSystemsTestData()
        {
            var testData = new List<object?[]>() { new object?[] { null, false } };
            foreach (RedumpSystem redumpSystem in Enum.GetValues(typeof(RedumpSystem)))
            {
                if (_reverseRingcodeSystems.Contains(redumpSystem))
                    testData.Add([redumpSystem, true]);
                else
                    testData.Add([redumpSystem, false]);
            }

            return testData;
        }

        /// <summary>
        /// Generate a test set of RedumpSystem values that are considered XGD
        /// </summary>
        /// <returns>MemberData-compatible list of RedumpSystem values</returns>
        public static List<object?[]> GenerateXGDSystemsTestData()
        {
            var testData = new List<object?[]>() { new object?[] { null, false } };
            foreach (RedumpSystem redumpSystem in Enum.GetValues(typeof(RedumpSystem)))
            {
                if (_xgdSystems.Contains(redumpSystem))
                    testData.Add([redumpSystem, true]);
                else
                    testData.Add([redumpSystem, false]);
            }

            return testData;
        }
    }
}
