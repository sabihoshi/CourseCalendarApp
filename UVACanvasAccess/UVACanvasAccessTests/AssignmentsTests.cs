using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Structures.Assignments;
using UVACanvasAccess.Util;
using Xunit;
using Xunit.Abstractions;

namespace UVACanvasAccessTests
{
    public class AssignmentsTests : IClassFixture<ApiFixture>
    {
        private const ulong TestCourse = 1028;
        private readonly Api _api;
        private readonly ITestOutputHelper _testOutputHelper;

        public AssignmentsTests(ApiFixture fixture, ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _api              = fixture.Api;
        }

        /// <summary>
        ///     Tests ListCourseAssignments.
        /// </summary>
        [Fact]
        public async Task Test1()
        {
            IEnumerable<Assignment> assignments = await _api.StreamCourseAssignments(TestCourse).ToListAsync();
            Assert.NotEmpty(assignments);
        }

        /// <summary>
        ///     Tests GetAssignment.
        /// </summary>
        [Theory]
        [InlineData(9844)]
        [InlineData(10486)]
        public async Task Test2(ulong assignmentId)
        {
            var assignment = await _api.GetAssignment(TestCourse, assignmentId);

            Assert.Equal(assignmentId, assignment.Id);
            Assert.Equal(TestCourse, assignment.CourseId);
        }

        /// <summary>
        ///     Test ListAssignmentOverrides.
        /// </summary>
        [Theory]
        [InlineData(9844)]
        [InlineData(10486)]
        public async Task Test3(ulong assignmentId)
        {
            var assignment = await _api.GetAssignment(TestCourse, assignmentId);
            Assert.True(assignment.HasOverrides);

            var overrides = await _api.ListAssignmentOverrides(TestCourse, assignmentId)
                .ThenApply(ie => ie.ToList());
            Assert.NotEmpty(overrides);

            foreach (var @override in overrides)
            {
                Assert.Equal(assignmentId, @override.AssignmentId);
                await _api.GetAssignmentOverride(TestCourse, assignmentId, @override.Id);
            }
        }

        /// <summary>
        ///     Tests BatchGetAssignmentOverrides.
        /// </summary>
        [Fact]
        public async Task Test4()
        {
            var overrides = await _api.BatchGetAssignmentOverrides(TestCourse, new[]
            {
                new KeyValuePair<ulong, ulong>(10486, 70),
                new KeyValuePair<ulong, ulong>(10486, 71),
                new KeyValuePair<ulong, ulong>(9844, 72)
            }.Lookup());
            Assert.Equal(3, overrides.Count());
        }
    }
}