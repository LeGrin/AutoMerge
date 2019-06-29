using System;
using AutoMerge.IO;
using AutoMerge.Merger;
using NUnit.Framework;

namespace AutoMerge.Test
{
    [TestFixture]
    public class AutoMergeTests
    {

        public AutoMergeTests()
        {
        }

        [Test]
        public void ThreeWayMerge_NoChange_ResultEqualsToSource()
        {
            var source = FileService.ReadFile("TestFiles/NoChanges/source.txt");
            var changeA = FileService.ReadFile("TestFiles/NoChanges/changeA.txt");
            var changeB = FileService.ReadFile("TestFiles/NoChanges/changeB.txt");

            var result = MergeService.ThreeWayMerge(source, changeA, changeB);
            Assert.AreEqual(source, result);
        }
        
        [Test]
        public void ThreeWayMerge_MergeConflict_ResultHaveConflictNotice()
        {
            var source = FileService.ReadFile("TestFiles/MergeConflict/source.txt");
            var changeA = FileService.ReadFile("TestFiles/MergeConflict/changeA.txt");
            var changeB = FileService.ReadFile("TestFiles/MergeConflict/changeB.txt");
            var expectedResult = FileService.ReadFile("TestFiles/MergeConflict/result.txt");

            var result = MergeService.ThreeWayMerge(source, changeA, changeB);
            Assert.AreEqual(expectedResult, result);
        }
        
        [Test]
        public void ThreeWayMerge_EditInBoth_ResultHaveBothChanges()
        {
            var source = FileService.ReadFile("TestFiles/Edit/source.txt");
            var changeA = FileService.ReadFile("TestFiles/Edit/changeA.txt");
            var changeB = FileService.ReadFile("TestFiles/Edit/changeB.txt");
            var expectedResult = FileService.ReadFile("TestFiles/Edit/result.txt");

            var result = MergeService.ThreeWayMerge(source, changeA, changeB);
            Assert.AreEqual(expectedResult, result);
        }
        
        [Test]
        public void ThreeWayMerge_Delete_ResultHaveBothChanges()
        {
            var source = FileService.ReadFile("TestFiles/Delete/source.txt");
            var changeA = FileService.ReadFile("TestFiles/Delete/changeA.txt");
            var changeB = FileService.ReadFile("TestFiles/Delete/changeB.txt");
            var expectedResult = FileService.ReadFile("TestFiles/Delete/result.txt");

            var result = MergeService.ThreeWayMerge(source, changeA, changeB);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ThreeWayMerge_AddedInBoth_ResultHaveBothChanges()
        {
            var source = FileService.ReadFile("TestFiles/Added/source.txt");
            var changeA = FileService.ReadFile("TestFiles/Added/changeA.txt");
            var changeB = FileService.ReadFile("TestFiles/Added/changeB.txt");
            var expectedResult = FileService.ReadFile("TestFiles/Added/result.txt");

            var result = MergeService.ThreeWayMerge(source, changeA, changeB);
            Assert.AreEqual(expectedResult, result);
        }
        
        [Test]
        public void ThreeWayMerge_MultipleChanges_ResultAllChanges()
        {
            var source = FileService.ReadFile("TestFiles/Multiple/source.txt");
            var changeA = FileService.ReadFile("TestFiles/Multiple/changeA.txt");
            var changeB = FileService.ReadFile("TestFiles/Multiple/changeB.txt");
            var expectedResult = FileService.ReadFile("TestFiles/Multiple/result.txt");

            var result = MergeService.ThreeWayMerge(source, changeA, changeB);
            Assert.AreEqual(expectedResult, result);
        }
    }
}